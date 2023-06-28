using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class btnMenu : MonoBehaviour
{
    public Button Btn;
    public Button BtnSample;
    public TMP_Text bMessage;
    public furnitureMaterialContral fmc;
    public AddToScene ats;
    public SelectTypes st;

//Create Menu's button for Manager's import furniture 
    public void AddtoBtnMenu(int index,string name,int val)
    {
        Btn = GetComponent<Button>();
        fmc = FindObjectOfType<furnitureMaterialContral>();
        ats = FindObjectOfType<AddToScene>();
        st = FindObjectOfType<SelectTypes>();
        Btn = GameObject.Find("freeBtn"+index.ToString()+name).GetComponent<Button>();
        bMessage = GameObject.Find("freeBtn"+index.ToString()+"T"+name).GetComponent<TMP_Text>();
        int disp=0;
        if(val != 0 ){
            disp = st.is_in[val] -ats.classNum[val];
        }
        else disp  = st.is_in[val]-ats.classNum[val];       
        int target = fmc.originalSampleCount+(index -1) + disp;
    
        bMessage.text = fmc.furnitureSample[target].name;
  
       // add click event  to button
        Btn.onClick.AddListener(()=>{
            fmc.AddFurniture(target);  
        });
     
        //index++;
    }

}
