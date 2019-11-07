using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Activate : MonoBehaviour
{
    public Image ImgButton;
    public BodySourceView bodySourceView;
    public RawImage logo;
    public RawImage Indicaciones;

    // Start is called before the first frame update
    void Start()
    {
        ImgButton = GameObject.Find("BtnDesactivado").GetComponent<Image>();
        logo.gameObject.SetActive(true);
        Indicaciones.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (bodySourceView.Active == true)
        {
            ImgButton.sprite = Resources.Load<Sprite>("Images/btnON");
            logo.gameObject.SetActive(false);
            Indicaciones.gameObject.SetActive(true);
        }
        else {
            ImgButton.sprite = Resources.Load<Sprite>("Images/btnOF");
            logo.gameObject.SetActive(true);
            Indicaciones.gameObject.SetActive(false);
        }
        
    }
}
