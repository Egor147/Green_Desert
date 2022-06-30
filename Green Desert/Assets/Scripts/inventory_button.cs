using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventory_button : MonoBehaviour
{
    public int action=2;
    public Button yourButton;
    TouchMe Click_reg;
    public string number="1";
    public int id;


    // Start is called before the first frame update
    void Start()
    {
        Click_reg = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TouchMe>();
        Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        id=PlayerPrefs.GetInt("But"+number+"id");
        if (Click_reg.ID!=id){
        Click_reg.ID=id;
        Click_reg.Action=action;
        } else {
            Click_reg.ID= -1;
            Click_reg.Action=0;
        }
    }
}
