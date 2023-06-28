using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Login : MonoBehaviour
{
    public TMP_InputField UsernameInput;
    public TMP_InputField PasswordInput;
    public Button LoginBtn;
    public Button RegisterBtn;
    public Web web;
  
    void Start()
    {
        web = FindObjectOfType<Web>();
        LoginBtn.onClick.AddListener(() =>{

            StartCoroutine(Main.Instance.Web.Login(UsernameInput.text,PasswordInput.text));
            StartCoroutine(Main.Instance.Web.SQLToJosn(UsernameInput.text));// SQL 讀取json 在寫入text
        });
        RegisterBtn.onClick.AddListener(() =>{
            
            StartCoroutine(Main.Instance.Web.Register(UsernameInput.text,PasswordInput.text));
            
        });
    }
}
