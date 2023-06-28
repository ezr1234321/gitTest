using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance;
    public Web Web;
    public GetUserName GetUserName;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Web = GetComponent<Web>();
        GetUserName = GetComponent<GetUserName>();
    }
}
