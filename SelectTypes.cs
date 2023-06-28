using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

//選擇家具類別
public class SelectTypes : MonoBehaviour
{   
   
  //  string path = "C:\\Users\\HUANG HUNG CHIN\\UnityPrject\\3d_Object\\Assets\\json\\List.txt";
    public GameObject[] furnitureType;
    // Start is called before the first frame update
    public TMPro.TMP_Dropdown mydrop;
    public int preType = -1;
    public furnitureMaterialContral fmc;
    public AddToScene ats;
    string[] t_name = new string[]{"Sofa","Table","Chair","Desk","Cabinet","Bed","Other"};
    public int[] is_in = new int[]{0,0,0,0,0,0,0};
    public btnMenu bm;
    bool indexCompute = false;
    public void TypeSelect(int val){//開啟List
        if (preType != -1) furnitureType[preType].SetActive(false);
        countIndex();
        ats = FindObjectOfType<AddToScene>();
        switch(val){
            case 0:
                furnitureType[0].SetActive(true);
                if(ats.classNum[0]!=0)//有額外匯入的(家具
                    useBM(0);
                preType = 0;
                break;
            case 1:
                furnitureType[1].SetActive(true);
                if(ats.classNum[1]!=0)//有額外匯入的(家具
                    useBM(1);
                preType = 1 ;
                break;
            case 2:
                furnitureType[2].SetActive(true);
                if(ats.classNum[2]!=0)//有額外匯入的(家具
                    useBM(2);
                preType = 2 ;
                break;
            case 3:
                furnitureType[3].SetActive(true);
                if(ats.classNum[3]!=0)//有額外匯入的(家具
                    useBM(3);
                preType = 3;
                break;
            case 4:
                furnitureType[4].SetActive(true);
                if(ats.classNum[4]!=0)//有額外匯入的(家具
                    useBM(4);
                preType = 4;
                break;
            case 5:
                furnitureType[5].SetActive(true);
                if(ats.classNum[5]!=0)//有額外匯入的(家具
                    useBM(5);
                preType = 5;
                break;
            case 6:
                furnitureType[6].SetActive(true);
                if(ats.classNum[6]!=0)//有額外匯入的(家具
                    useBM(6);
                preType = 6;
                break;

        }
    }
    void countIndex(){
        int total =0;
        ats = FindObjectOfType<AddToScene>();
        for(int i=0;i<7;i++){
            total+= ats.classNum[i];
            is_in[i] = total; 
        }

    }
    void useBM(int val){
        ats = FindObjectOfType<AddToScene>();
        bm = FindObjectOfType<btnMenu>();
        for(int i=1;i<=ats.classNum[val];i++)
        {
            bm.AddtoBtnMenu(i,t_name[val],val);
        }
    
    }
}

