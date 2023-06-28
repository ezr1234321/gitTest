using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class makeWall : MonoBehaviour
{
     public GameObject[] wallSet;//all家具物件範本
     public GameObject[] Walls;

    private float mZ;
    Vector3 mouseP;

    // Update is called once per frame
    int NonRepeat = 0;
    public void AddWall(int i)
    {
        mouseP = GetMouseWorldPos();
        mouseP += new Vector3(0.0f,-397.5039f,0.0f);
        GameObject wallClone = Instantiate(wallSet[i],mouseP,wallSet[i].transform.rotation);
        wallClone.transform.parent = gameObject.transform;
        wallClone.name = wallSet[i].name + "-" + NonRepeat.ToString();
        NonRepeat++;
        wallClone.tag = "room";         
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePosition = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }


    public void SwitchScene()
    {
        Walls=GameObject.FindGameObjectsWithTag("room");
        foreach (GameObject f in  Walls)
        {
            Destroy(f.GetComponent<DragWall>());//固定牆壁，去除移動腳本
        }
        SceneManager.LoadScene(2);
    }
}
