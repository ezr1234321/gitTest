using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
public class GetUserName : MonoBehaviour
{
    
    public TMP_Text[]  UserName;

    public Web Web;
    int startIndex = 1, next = 5,ArrayLength=0,iName=0,page=0,firstLoad=1;
    string[] temp;
    List <string> NameList;
    bool inFirstPage = true,inLastPage = false;
    void Update(){
        if(firstLoad == 1){
            Web = GetComponent<Web>();
            if(Web.IsManager){
                firstLoad = 0;
                Invoke("reset",1);
            }
        }
       
    }
    public void NextPage()
    {   //換下頁
        
        if(inLastPage) return;
        iName=0;
        page++;
        startIndex = startIndex + next;
        if(startIndex == ArrayLength-next) //Is Final Page
        {
            inLastPage = true;
        }
        for(int i = startIndex; i < startIndex + next; i++)
        { 
            inFirstPage = false;
            UserName[iName].text = NameList[i];
            iName++;
        }
    }
    public void ReturnPage(){//回上頁
        Debug.Log("RETURN");
        if(inFirstPage) return;
        Debug.Log("RETURN2");
        iName=0;
        page -- ;
        startIndex = startIndex - next;
        if(startIndex == 0) inFirstPage = true;
        for(int i = startIndex; i < startIndex + next; i++){ 
            inLastPage = false;
            UserName[iName].text = NameList[i];
            iName++;
        }
    }
    public void DeleteUser(int i)
    {
        
        if(NameList[i+page] == "No Date.") return;
        // for(int j = 0;j<NameList.Count;j++){
        //     Debug.Log("j= "+j+" NameList " + NameList[j]);
        // }
        // Debug.Log("i= "+i+" page = "+page+" NameList " + NameList[i +page*5]);
        UserName[i].text = "No Date.";
        
        StartCoroutine(Main.Instance.Web.DeleteUser(NameList[i+page*5]));

        NameList[i+page*5] = "No Date.";
        Debug.Log("Delete ok");
        /*StartCoroutine(Main.Instance.Web.UpdataInfor());
        
        NameList.Clear();
        Debug.Log("Update");
        Invoke("reset",1);*/
    }
 
    void reset()
    {
        string content = File.ReadAllText("C:\\Users\\HUANG HUNG CHIN\\UnityPrject\\3d_Object\\Assets\\json\\TotalName.txt");
        temp = content.Split('-');// 前 後 會多出 " "
        NameList = new List<string>();
        startIndex = 0;   
        iName=0;
        inFirstPage = true;
        inLastPage = false;
  
    //  修正為5的倍數，不足的補上No Date.
        for(int i = 1;i<temp.Length -1;i++){
            NameList.Add(temp[i]);
        }
        int disp = NameList.Count % 5;
        if(disp!=0)
        {
            for(int i=0; i< 5 - disp; i++)
            {
                NameList.Add("No Date.");
            }
        }

        ArrayLength = NameList.Count;
        if(startIndex + next == ArrayLength)
        {
            inLastPage = true;
        }
    //更新Page資訊
         for(int i= startIndex;i<startIndex+next;i++)
         { 
            UserName[iName].text = NameList[i];
            iName++;           
         }
       

    }
}
