using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectSongChance : MonoBehaviour
{
    public KinectUICursor CursorRigth;
    public Button btnok;
    public Button btnBack;
    PhotoPlayerController photoPlayerController;

    bool isGreen1 = false;
    bool isGreen2 = false;
    // Start is called before the first frame update
    void Start()
    {
        photoPlayerController = new PhotoPlayerController();
    }

    // Update is called once per frame
    void Update()
    {
        if (CursorRigth.transform.position.x > btnok.transform.position.x - 50 && CursorRigth.transform.position.x < btnok.transform.position.x + 50 && CursorRigth.transform.position.y > btnok.transform.position.y - 50 && CursorRigth.transform.position.y < btnok.transform.position.y + 50)
        {
            ChangeScene();
        }
        else
        {
            isGreen1 = false;
        }

        if (CursorRigth.transform.position.x > btnBack.transform.position.x - 50 && CursorRigth.transform.position.x < btnBack.transform.position.x + 50 && CursorRigth.transform.position.y > btnBack.transform.position.y - 50 && CursorRigth.transform.position.y < btnBack.transform.position.y + 50)
        {
            ChangeBack();
        }
        else
        {
            isGreen2 = false;
        }
    }

    public void ChangeScene()
    {
        
            if (CursorRigth.HandGreen == true)
            {
                isGreen1 = true;
            }
            else
            {
                if (isGreen1)
                {
                    if (photoPlayerController.ChangeToThemes())
                    {
                        SceneManager.LoadScene("SelecThemes");
                    }
                    else
                    {
                        photoPlayerController.ChangeTurn();
                        SceneManager.LoadScene("MyPhoto");
                    }
                }
            }
            //contPlayers.Players = 1;
        
    }
    public void ChangeBack()
    {
        if (CursorRigth.HandGreen == true)
        {
            isGreen2 = true;
        }
        else
        {
            if (isGreen2)
            {
                SceneManager.LoadScene("MyPhoto");
            }
        }
    }
}
