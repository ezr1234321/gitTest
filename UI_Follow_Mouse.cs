using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Follow_Mouse : MonoBehaviour
{
    // [SerializeField] private RectTransform canvasRectTransform;
   
    private RectTransform backgroundRectTransform;
    private RectTransform rectTransform;
    public bool funcMenuIsTrue = false;
    public GameObject DeleteMode;
    // public bool IsDelet = false;
    // public bool IsMaterial = false;
    // public bool IsFurniture = false; 
    public int state = 0;
    public GameObject funcUI;
    public GameObject MaterialMenuUI;
    public GameObject FurnitureMenuUI;
    public GameObject SaveAndExitUI;

    public GameObject SizeUI;
    public GameObject RotUI;
    void Awake()
    {
        rectTransform = transform.GetComponent<RectTransform>();
    }

    // Update is called once per frame.
    void Update()
    { /**/ 
      if(Input.GetMouseButtonDown(1)){
        funcMenuIsTrue = true;
        state = 0;
        CloseFuncChild();
        UIMove();
      }
    }
    public void UIMove(){
      //  UI.SetActive(true);
      if(funcMenuIsTrue){
        funcUI.SetActive(true);
        rectTransform.anchoredPosition = Input.mousePosition ;
       // funcMenuIsTrue = true;
      }
    }
    public void CloseFuncMenu (){
      funcUI.SetActive(false);
      funcMenuIsTrue = false;
    }
    public void OpenFunction(int choose){
      CloseFuncMenu();
      switch (choose)
      {
        case 1://Delete
          state = 1;
          DeleteMode.SetActive(true);
          break;

        case 2:// material
          MaterialMenuUI.SetActive(true);
          state =2;
          break;

        case 3://furniture
          state =3;
          FurnitureMenuUI.SetActive(true);          
          break;
        case 4:
          state = 4;
          SaveAndExitUI.SetActive(true);
          break;
        case 5:
          state = 5;
          SizeUI.SetActive(true);
          break;
        case 6:
          state = 6;
          RotUI.SetActive(true);
          break;
        default:
          break;
      }
    }
    public void CloseFuncChild(){
      MaterialMenuUI.SetActive(false);
      FurnitureMenuUI.SetActive(false);
      DeleteMode.SetActive(false);
      SaveAndExitUI.SetActive(false);
      SizeUI.SetActive(false);
      RotUI.SetActive(false);
    }
}
