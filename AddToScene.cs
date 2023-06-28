using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class AddToScene : MonoBehaviour
{   
    //去Recourse把物件放入Scene
    public GameObject loadItem;
    public int NumAddToScene =0; // 讓furnitureMenu知道要改幾個Button
    public furnitureMaterialContral fmc;
    public btnMenu bm;
    public List<int> classNum = new List<int>();//紀錄每個類各加幾次
    public void AddFurnitureToScene(string FName,string folder)
    {
        //Create Furniture Sample
        loadItem = Instantiate(Resources.Load(folder+"/"+FName,typeof(GameObject))) as GameObject;
        loadItem.name = FName;
        loadItem.tag = "Sample";
        loadItem.transform.position = new Vector3(0f,0f,0f);
        //Add  script function
        BoxCollider bc = loadItem.AddComponent<BoxCollider>() as BoxCollider;
        MeshRenderer mr = loadItem.AddComponent<MeshRenderer>() as MeshRenderer;
        fmc = FindObjectOfType<furnitureMaterialContral>();
        //Add to furnitureSample List
        fmc.furnitureSample.Add(loadItem);
        NumAddToScene++;
    }
    public void AddtoClassNumList(int n){
        classNum.Add(n);
    }
}
