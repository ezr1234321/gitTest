using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCanvas : MonoBehaviour
{
    public GameObject ManagerUI;
    public GameObject LoginUI;
    public GameObject OpenMenu;
    public GameObject LOpenMenu;
    bool isOpen = false;
    bool LisOpen = false;
    // Update is called once per frame
    public void EnterManagerMode()
    {
        LoginUI.SetActive(false);
        ManagerUI.SetActive(true);
    }
    public void ReturnLoginMode()
    {
        ManagerUI.SetActive(false);
        LoginUI.SetActive(true);
    }
    public void MoveIn(){
        if(!isOpen){
            OpenMenu.SetActive(true);
            isOpen = true;
        }
        else{
            OpenMenu.SetActive(false);
            isOpen = false;
        }    
        
    }
    public void LMoveIn(){
        if(!isOpen){
            LOpenMenu.SetActive(true);
            LisOpen = true;
        }
        else{
            LOpenMenu.SetActive(false);
            LisOpen = false;
        }    
        
    }
}
