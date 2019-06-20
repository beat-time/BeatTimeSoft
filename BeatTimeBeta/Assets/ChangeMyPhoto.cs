using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMyPhoto : MonoBehaviour
{
    public KinectUICursor CursorRigth;
    public ContPlayers contPlayers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeOnePlayer()
    {
        if (CursorRigth.HandGreen == true)
        {
            SceneManager.LoadScene("MyPhotoP1");
            contPlayers.Players = 1;
        }
    }
    public void ChangeTwoPlayer()
    {
        if (CursorRigth.HandGreen == true)
        {
            contPlayers.Players = 2;
            SceneManager.LoadScene("MyPhotoP1");
        }
    }
}
