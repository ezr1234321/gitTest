using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMove : MonoBehaviour
{
    public float speed=5.0f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update () {
    //Move
    // float xDirection = Input.GetAxis("Horizontal");
    // float zDirection = Input.GetAxis("Vertical");
    // Vector3 moveDirection = new Vector3(xDirection,0.0f,zDirection);
    // transform.position+=moveDirection*speed;
    
    //依照自身座標軸移動(非世界座標)
    transform.position += transform.right * Input.GetAxis("Horizontal") * speed*20;
    transform.position += transform.forward * Input.GetAxis("Vertical") * speed*20;
    //Rotate
    if(Input.GetKey("q")){
       transform.Rotate(0,-10,0);
    }
    if(Input.GetKey("e")){
        transform.Rotate(0,10,0);
    }
    }

}
