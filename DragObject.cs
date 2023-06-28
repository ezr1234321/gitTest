using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public Shader selectedShader;
    public Color outterColor;
    private Color myColor;
    private Shader myShader;
    private SkinnedMeshRenderer sRenderer;
    private bool Selected = false;//上述為變色系統的變數

    public UI_Follow_Mouse funcUI ;
    
    private Vector3 moffset;
    private float mZcoord;
    private Vector3 a;//上述為物件移動的變數

    private Vector3 b;
    private bool colToF;//上述為物件回復原位的變數
    void OnMouseDown()
    { //掛著此腳本的物件被點擊時觸發
        mZcoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        // Store offset = gameobject world pos - mouse world pos
        a = GetMouseWorldPos();
        float t;
        t = a.z;
        a.z = a.y;
        a.y = 0;
        Debug.Log(a);
        moffset = gameObject.transform.position - a;//存初始值

        b = gameObject.transform.position;//儲存原位

        //Destroy(GetComponent(typeof(Rigidbody)));
        Selected = true;
        myColor = gameObject.GetComponent<Renderer>().material.color;
        myShader = gameObject.GetComponent<Renderer>().material.shader;
        //滑鼠移入時要使用的shader
        selectedShader = Shader.Find("Unlit/NewUnlitShader");
        if (!selectedShader)
        {
            Debug.Log("false");
            enabled = false;
            return;
        }

    }

    private Vector3 GetMouseWorldPos()
    {
        //pixel coordinates (X,Y)
        Vector3 mousePoint = Input.mousePosition;

        //Z.coordinate of game object on screen
        mousePoint.z=mZcoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    void OnMouseUp()
    {
        gameObject.GetComponent<Collider>().isTrigger = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        /*gameObject.transform.position += new Vector3(0, 1f, 0);
        gameObject.GetComponent<Rigidbody>().AddForce(0, 5, 0, ForceMode.VelocityChange);*/
        if (colToF)
        {
            gameObject.transform.position = b;
        }
        gameObject.GetComponent<Renderer>().material.color = myColor;
        //恢復shader
        gameObject.GetComponent<Renderer>().material.shader = myShader;
        Debug.Log("recover"+gameObject.transform.position+ gameObject.GetComponent<Collider>().isTrigger);
    }
    void OnMouseDrag()
    {
        float timelost = 0;
        //替換Shader
        gameObject.GetComponent<Renderer>().material.shader = selectedShader;
        //設定邊緣光顏色值
        if (Selected)
        {
            outterColor = new Color(20, 1, 20, 1);
            gameObject.GetComponent<Renderer>().material.SetColor("_RimColor", outterColor);
            Selected = false;
        }
        //設定紋理顏色值
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", myColor);
        Vector3 c = GetMouseWorldPos();
        float t;
        t = c.z;
        c.z = c.y;
        c.y = 0;
        gameObject.transform.position = c + moffset;
        gameObject.GetComponent<Collider>().isTrigger = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //家具角度改變
        if (Input.GetKeyDown("z"))
        {
            transform.Rotate(0, 0, 5);
            if (Time.time < 0.5f)
            {
                Input.GetKey("z");
                transform.Rotate(0, 0, 5);
            }
        }
        if (Input.GetKeyDown("x"))
        {
            timelost = Time.time;
            if (Time.time  - timelost < 1)
            {

                if (!(Input.GetKeyUp("x"))) ;
                transform.Rotate(0, 0, -5);
            }

            transform.Rotate(0, 0, -5);
        }
        if (Input.GetKeyDown("c"))
        {
            transform.Rotate(5, 0, 0);
            if (Time.time < 0.5f)
            {
                Input.GetKey("c");
                transform.Rotate(5, 0, 0);
            }
        }
        if (Input.GetKeyDown("v"))
        {
            timelost = Time.time;
            if (Time.time  - timelost < 1)
            {
                if (!(Input.GetKeyUp("v"))) ;
                transform.Rotate(-5, 0, 0);
            }
            transform.Rotate(-5, 0, 0);
        }
        if (Input.GetKeyDown("b"))
        {
            transform.Rotate(0, 5, 0);
            if (Time.time < 0.5f)
            {
                Input.GetKey("b");
                transform.Rotate(0, 5, 0);
            }
        }
        if (Input.GetKeyDown("n"))
        {
            transform.Rotate(0, -5, 0);
            if (Time.time  - timelost < 1)
            {
                Input.GetKey("n");
                transform.Rotate(0, -5, 0);
            }
        }

        //放大縮小
        float x, z;
        if (Input.GetKeyDown("l"))
        {
            Debug.Log("l");
            x = Input.GetAxis("Horizontal") * 1;
            z = Input.GetAxis("Vertical") * 1;
            gameObject.transform.localScale += new Vector3(1, 1, 1);
            if (Time.time < 0.5f)
            {
                Input.GetKey("l");
                gameObject.transform.localScale += new Vector3(1, 1, 1);
            }
        }
        if (Input.GetKeyDown("k"))
        {
            Debug.Log("k");
            x = Input.GetAxis("Horizontal") * -1;
            z = Input.GetAxis("Vertical") * -1;
            gameObject.transform.localScale -= new Vector3(1, 1, 1);
            if (Time.time < 0.5f)
            {
                Input.GetKey("k");
                gameObject.transform.localScale -= new Vector3(1, 1, 1);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Plane")
        {
            Color thios = new Color(20, 1, 1, 1);
            gameObject.GetComponent<Renderer>().material.SetColor("_RimColor", thios);
            colToF = true;
            Debug.Log("Enter " + other.name + a);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name != "Plane")
        {
            Selected = true;
            colToF = false;
            Debug.Log("Exit " + other.name + a);
        }
    }
 
}