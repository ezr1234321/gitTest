using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragWall : MonoBehaviour
{
    private Vector3 moffset; 
    private float mZcoord;//target.z

    void OnMouseDown()
    { 
        //掛著此腳本的物件被點擊時觸發
        mZcoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        // Store offset = gameobject world pos - mouse world pos
        //點
        moffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()//取得滑鼠的世界座標
    {
        //pixel coordinates (X,Y)
        Vector3 mousePoint = Input.mousePosition;
        //Z.coordinate of game object on screen
        mousePoint.z=mZcoord;//變為三維座標
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + moffset; 
    }

     void OnMouseUp()
     {
        if(transform.position.x>1290.0)
        {
            //超出範圍就刪除
            Destroy(gameObject);
        }
     }
}
