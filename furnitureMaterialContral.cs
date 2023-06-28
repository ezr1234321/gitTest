using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using TMPro;

public class furnitureMaterialContral : MonoBehaviour
{
    public int x;//材質 material[] 的 index
    int select = 0;
    
    public Material[] material;
    public GameObject[] furnitures;//場景上家具物件
    public List<GameObject> furnitureSample = new List<GameObject>();
    public GameObject[] WallSet;
    public GameObject[] Walls;
   

    public UI_Follow_Mouse funcUI;
    public Web web;
    public Use_JSON json;
    public AddToScene ads;
    public AddObject ao;

    Renderer rend;
    private float mZ;
    private int childCount = 0;
    Vector3 mouseP;
    public int originalSampleCount =0; //紀錄初始F_Sample數目
    string ResourcePath = @"C:\Users\smile\3d_Object\3d_Object\Assets\Resources\"; //file path
    public TMP_Text Message;
    //MyList
    public GameObject myList;
    public TMP_Text ctrlBtn;
    bool myListOpen = false;
    public TMP_Text[] MyListBtn;
    int ArrayLength=0,totalPage=0,remainItem,nowPage=0;
    
    void Start()
    {
        //把所有有"furniture"tag的物件放入 furnitures[]
        x=0;
        web = FindObjectOfType<Web>();
        furnitures=GameObject.FindGameObjectsWithTag("furniture");
        

        GameObject[] SampleSet = GameObject.FindGameObjectsWithTag("Sample");
        foreach(GameObject f in SampleSet){
            DragObject drag = f.AddComponent(typeof(DragObject)) as DragObject; 
            furnitureSample.Add(f);
        }
       
        originalSampleCount = furnitureSample.Count;
        
        Walls = GameObject.FindGameObjectsWithTag("room");
        /**/
        GetResourceFiles("Sofa");
        GetResourceFiles("Table");
        GetResourceFiles("Chair");
        GetResourceFiles("Desk");
        GetResourceFiles("Cabinet");
        GetResourceFiles("Bed");
        GetResourceFiles("Other");
  
        if (Web.IsLogin == 1) // 是從Lojin 進入
        {
            //從json讀資訊並建立場景
            json = FindObjectOfType<Use_JSON>();
            json.LoadInfo();
        }
         ao = FindObjectOfType<AddObject>();
    }

    void Update()
    {
        if (Input.GetKeyDown("escape")) funcUI.OpenFunction(4);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        funcUI = FindObjectOfType<UI_Follow_Mouse>(); //funcUI = script <UI_Follow_Mouse> ，可調用函式，參數
        //判定點擊的GameObject.name    
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform.gameObject.tag =="furniture")
                {
                    //從furnitures[]中找到該物件的index
                    for (var i = 0; i<furnitures.Length; i++)
                    {
                        if (furnitures[i].name == hit.transform.gameObject.name)
                        {
                            select=i;
                            break;
                        }
                    }
                    if (funcUI.state == 1)
                    {// In delete mode
                        DeletObject();
                    }
                }
            }

        }
    }
    // ================== 更換家具材質 ========================================
    public void chooseMaterial(int i)
    {
        if (furnitures.Length==0) return;
        childCount = furnitures[select].transform.childCount;
        Debug.Log("小孩個數 "+childCount);
        x=i;
        //        Debug.Log(select);
        if (childCount == 0)
        {
            rend = furnitures[select].GetComponent<Renderer>();
            rend.sharedMaterial = material[x];
        }
        else
        {
            foreach (Transform child in furnitures[select].transform)
            {// 搜尋所有子物件
                if (child.transform.childCount != 0)
                {
                    foreach (Transform grandchild in child.transform)
                    {// 處理子物件的子物件
                        if (grandchild.tag == "same") continue;// 不想跟換材質的部分
                        rend = grandchild.GetComponent<Renderer>();
                        rend.sharedMaterial = material[x];
                    }
                }
                if (child.tag == "same") continue;
                rend = child.GetComponent<Renderer>();
                rend.sharedMaterial = material[x];
            }
        }

    }
    // ================= 新增家具物件 ========================================
    int toNonRepeat = 0;
    public void AddFurniture(int i)
    {
        // 新增家具物件，類別為非複合型
        mouseP = GetMouseWorldPos();
        Vector3 s = furnitureSample[i].gameObject.transform.localPosition;
        Vector3 w = Camera.main.transform.position + transform.forward*10;
        GameObject furnitureClone = Instantiate(furnitureSample[i], new Vector3(w.x, w.y+2.3f, w.z), furnitureSample[i].transform.rotation);
        furnitureClone.tag = "furniture";
        furnitureClone.name = furnitureSample[i].name+"-"+toNonRepeat.ToString();//讓物件名稱不重複，避免刪除出問題
        toNonRepeat++;
        furnitures = GameObject.FindGameObjectsWithTag("furniture");
        DragObject drag = furnitureClone.AddComponent(typeof(DragObject)) as DragObject; 
        furnitureClone.AddComponent(typeof(Rigidbody));
        closeList();

    }
    private Vector3 GetMouseWorldPos()
    {
        //pixel coordinates (X,Y)
        Vector3 mousePosition = Input.mousePosition;
        mousePosition += new Vector3(100.0f, -100.0f, 10.0f);
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
    public void DeletObject()
    {
        closeList();
        Destroy(furnitures[select]);
        furnitures = GameObject.FindGameObjectsWithTag("furniture");
     //   furnitures[select].SetActive(false);
    }
    public void EXIT()
    {
        Message.text = "GoodBye !";
        Application.Quit();
    }
    public void GetResourceFiles(string folder) //Add furniture from Resources/FF (where manager import)
    {
      List<string> RNameList= new List<string>();
      ads = FindObjectOfType<AddToScene>();
      int metaC=0;

    // Get all filename in Resources/folder 
      string [] files = Directory.GetFiles (ResourcePath+folder, "*.*");
    // folder is empty => break

      if (files.Length == 0) {
        ads.AddtoClassNumList(0);
        return;
      }
      foreach (string sourceFile in files)
      {     
           if(metaC == 0)//區分meta file
           {
                metaC++;
                string fileName = Path.GetFileName (sourceFile);
                string[]temp;
                temp = fileName.Split('.');
                RNameList.Add(temp[0]);
                metaC = 1;
            }
            else
            {
                metaC=0;
            }    

      }ads.AddtoClassNumList(RNameList.Count);
      for(int i=0;i<RNameList.Count;i++)
      {
         
        ads.AddFurnitureToScene(RNameList[i],folder);
      }
    }
    public void UpdateAddMenuBtn(int folder){
        GetResourceFiles("Sofa");
        GetResourceFiles("Table");
        GetResourceFiles("Chair");
        GetResourceFiles("Desk");
        GetResourceFiles("Cabinet");
        GetResourceFiles("Bed");
        GetResourceFiles("Other");
        funcUI.funcMenuIsTrue = true;
        funcUI.state = 0;
        funcUI.CloseFuncChild();
        funcUI.UIMove();
    }
    void UpdateMyList(){
        furnitures = GameObject.FindGameObjectsWithTag("furniture");
        ArrayLength = furnitures.Length;
        if(ArrayLength ==0 )return;
        totalPage  = ArrayLength / 5;//總頁數
        remainItem = ArrayLength % 5;
        for(int i = 0; i < 5; i++){
        //    if(nowPage >totalPage) break;//頁數超過
            if(nowPage == totalPage ){//到最後一頁
                if(remainItem!=0){//
                    if(i+1>remainItem) {
                        MyListBtn[i].text = "NONE";
                        continue;
                    }  
                }
            }
            MyListBtn[i].text = furnitures[i+nowPage*5].name; 
        }
    }
    public void nextList(){
        Debug.Log(ArrayLength+" AAAAAAAAAA");
        Debug.Log(nowPage+" BBBBBBBB");
        Debug.Log(totalPage+" CCCCCCCCC");
        if(nowPage == totalPage) return;
        if(remainItem == 0) return;
        nowPage++;
        UpdateMyList();
    }
    public void returnList(){
        if(nowPage == 0) return;
        nowPage--;
        UpdateMyList();
    }
    public void  GoToFurnitureLocation(int i){
        i-=1;
        if(MyListBtn[i].text == "NONE") return;
        Vector3  camPos = Camera.main.transform.position ;
        Vector3 fPos = furnitures[i+ nowPage*5].transform.position;
        Camera.main.transform.position =new Vector3(fPos.x, camPos.y,fPos.z-30.0f);
    }
    public void openMyList(){
        if(myListOpen){
            myList.SetActive(false);
            ctrlBtn.text = "OpenMyList";
            myListOpen = false;
        }
        else {
            myList.SetActive(true);
            UpdateMyList();
            ctrlBtn.text = "CloseMyList";
            myListOpen = true;
        }
    }
    void closeList(){
        nowPage=0;
        if(myListOpen){
            myList.SetActive(false);
            ctrlBtn.text = "OpenMyList";
            myListOpen = false;
        }
    }
}
