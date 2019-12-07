using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public KinectUICursor CursorLeft;
    public KinectUICursor CursorRigth;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CursorLeft.HandGreen == true && CursorRigth.HandGreen == true)
        {
            CursorLeft.HandGreen = false;
            CursorRigth.HandGreen = false;
            SceneManager.LoadScene("SelecPlayers");
        }
    }

    
}
