using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Use_JSON : MonoBehaviour
{ 
    public furnitureMaterialContral FMControl;
    public AddToScene addScene;
    public Web web; 
    string LoadData;public GameObject[] wallTemp ;
    string LoadWData;

   // Data MyData;
    UserData MyData;
    UserData MyWData;
    public bool IsProduceTxt = false;
    [System.Serializable]
    public class Data
    {
        public string name;
        public Vector3 location;
        public Quaternion rotation;
        public int Rend; 
    }

    [System.Serializable]
    public class UserData{
        public List<Data> ItemList;
    }
    Renderer rend;
    public void SaveInfo()
    {
        FMControl = FindObjectOfType<furnitureMaterialContral>();
        List<Data> userdata = new List<Data>();
        List<Data> walldata = new List<Data>();
        foreach (GameObject f in FMControl.furnitures)
        {
            Data Item = new Data();
            Item.name = f.name;
            Item.location = f.transform.position;
            Item.rotation = f.transform.rotation;

            
            int childCount = f.transform.childCount;
            if(childCount == 0)
            {//沒有子物件
                for(int k=0;k<FMControl.material.Length;k++)
                {
                    rend = f.GetComponent<Renderer>();
                    if(rend.sharedMaterial == FMControl.material[k])
                    {
                        Item.Rend = k;
                    }
                }
            }
            else
            {
                foreach (Transform child in f.transform)
                {// 搜尋所有子物件
                    if (child.tag == "same") continue;
                    rend = child.GetComponent<Renderer>();
                    for(int k=0;k<FMControl.material.Length;k++)
                    {
                        if(rend.sharedMaterial == FMControl.material[k])
                        {
                            Item.Rend = k;
                            break;
                        }
                            
                        
                    }
                }
            }

            userdata.Add(Item);
        }
        foreach (GameObject f in FMControl.Walls)
        {
            Data Item = new Data();
            Item.name = f.name;
            Item.location = f.transform.position;
            Item.rotation = f.transform.rotation;
            walldata.Add(Item);
        }
        UserData newItemList = new UserData{
            ItemList = userdata
        };
        UserData newItemList2 = new UserData{
            ItemList = walldata
        };
        string jsonInfo = JsonUtility.ToJson(newItemList,true);
        File.WriteAllText("C:\\Users\\HUANG HUNG CHIN\\UnityPrject\\3d_Object\\Assets\\json\\UserContent.json", jsonInfo);
        jsonInfo = JsonUtility.ToJson(newItemList2,true);
        File.WriteAllText("C:\\Users\\HUANG HUNG CHIN\\UnityPrject\\3d_Object\\Assets\\json\\UserContentWall.json", jsonInfo);
        FMControl.Message.text = "Save successful !";
        web = FindObjectOfType<Web>();
        Debug.Log(" user  " + Web.USER_NAME);
        StartCoroutine(Main.Instance.Web.JsonToSQL(Web.USER_NAME));
        FMControl.Message.text = "Save to SQL successful !";
    }

    public void LoadInfo()//讀檔
    {
        FMControl = FindObjectOfType<furnitureMaterialContral>();
        //讀取指定路徑的Json檔案並轉成字串(路徑同)
        LoadData = File.ReadAllText("C:\\Users\\HUANG HUNG CHIN\\UnityPrject\\3d_Object\\Assets\\json\\UserContent.json");
        LoadWData = File.ReadAllText("C:\\Users\\HUANG HUNG CHIN\\UnityPrject\\3d_Object\\Assets\\json\\UserContentWall.json");

        //把字串轉換成Data物件
        MyData = JsonUtility.FromJson<UserData>(LoadData);
        MyWData = JsonUtility.FromJson<UserData>(LoadWData);
        //用json紀錄的資料，建造gameobject(furniture)
        for(int i = 0; i < MyData.ItemList.Count; i++){
            int index=0;//家具在furnitureSample的index 
            int InFurnitureSample = 0;
            string[]temp;
            temp = MyData.ItemList[i].name.Split('-');
            for(int j = 0; j < FMControl.furnitureSample.Count ; j++)
            {
                if(FMControl.furnitureSample[j].name == temp[0])
                {
                    index = j;
                    InFurnitureSample = 1;
                    break;
                }
            }
            //建物件 
            if(InFurnitureSample == 0)//該物件為匯入家具
            {
                addScene = FindObjectOfType<AddToScene>();
                addScene.AddFurnitureToScene(temp[0],"Other");//將該家具匯入Scene
                index = FMControl.furnitureSample.Count - 1;

            }
           GameObject furnitureCopy = Instantiate( FMControl.furnitureSample[index], MyData.ItemList[i].location, MyData.ItemList[i].rotation );
           //還原material
           int childCount = furnitureCopy.transform.childCount;
           if(childCount == 0)
           {
                rend = furnitureCopy.GetComponent<Renderer>();
                rend.sharedMaterial = FMControl.material[MyData.ItemList[i].Rend];
           }
           else
           {
                foreach (Transform child in furnitureCopy.transform)
                {// 搜尋所有子物件
                    if (child.tag == "same") continue;
                    rend = child.GetComponent<Renderer>();
                    rend.sharedMaterial = FMControl.material[MyData.ItemList[i].Rend];
                }
                
           }
           



           //重新命名
           furnitureCopy.name = MyData.ItemList[i].name;
           //給tag
           furnitureCopy.tag = "furniture";
           furnitureCopy.AddComponent(typeof(Rigidbody));
        }
        FMControl.furnitures=GameObject.FindGameObjectsWithTag("furniture");
        Debug.Log("家具讀檔成功");

        //用json紀錄的資料，建造gameobject(Wall)
        for(int i = 0; i < MyWData.ItemList.Count; i++){
            int index=0;
            string[]temp;
            temp = MyWData.ItemList[i].name.Split('-');
            for(int j = 0; j < FMControl.WallSet.Length ; j++)
            {
                if(FMControl.WallSet[j].name == temp[0])
                {
                    index = j;
                    break;
                }
            }
           GameObject WallCopy = Instantiate( FMControl.WallSet[index], MyWData.ItemList[i].location, MyWData.ItemList[i].rotation );  //用json紀錄的資料，建造gameobject
           WallCopy.name =  MyWData.ItemList[i].name;
           WallCopy.tag = "room";
        }
        FMControl.Walls = GameObject.FindGameObjectsWithTag("room");
        Debug.Log("牆壁讀檔成功");    

    }
}
