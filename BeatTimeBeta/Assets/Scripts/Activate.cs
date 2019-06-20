using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Activate : MonoBehaviour
{
    public Image ImgButton;
    public BodySourceView bodySourceView;

    // Start is called before the first frame update
    void Start()
    {
        ImgButton = GameObject.Find("BtnDesactivado").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bodySourceView.Active == true)
        {
            ImgButton.sprite = Resources.Load<Sprite>("Images/btnON");
        }
        else {
            ImgButton.sprite = Resources.Load<Sprite>("Images/btnOF");
        }
        
    }
}
