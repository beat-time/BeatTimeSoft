using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class ChangeMyPhoto : MonoBehaviour
{
    public KinectUICursor CursorRigth;
    //public ContPlayers contPlayers;
    public Button btn1;
    public Button btn2;

    PhotoPlayerController photoPlayerController;

    bool isGreen1 = false;
    bool isGreen2 = false;
    // Start is called before the first frame update
    void Start()
    {
        photoPlayerController = new PhotoPlayerController();
        string path = "Assets\\Resources\\Images\\";
        if (File.Exists(path + "photo1.png"))
        {
            File.Delete(path + "photo1.png");
        }
        if (File.Exists(path + "photo2.png"))
        {
            File.Delete(path + "photo2.png");
        }
        if (File.Exists(path + "face1.png"))
        {
            File.Delete(path + "face1.png");
        }
        if (File.Exists(path + "face2.png"))
        {
            File.Delete(path + "face2.png");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CursorRigth.transform.position.x > btn1.transform.position.x - 80 && CursorRigth.transform.position.x < btn1.transform.position.x + 80 && CursorRigth.transform.position.y > btn1.transform.position.y - 80 && CursorRigth.transform.position.y < btn1.transform.position.y + 80)
        {
            ChangeOnePlayer();
        }
        else
        {
            isGreen1 = false;
        }
        if (CursorRigth.transform.position.x > btn2.transform.position.x - 80 && CursorRigth.transform.position.x < btn2.transform.position.x + 80 && CursorRigth.transform.position.y > btn2.transform.position.y - 80 && CursorRigth.transform.position.y < btn2.transform.position.y + 80)
        {
            ChangeTwoPlayer();
        }
        else
        {
            isGreen2 = false;
        }
    }

    public void ChangeOnePlayer()
    {
        if (CursorRigth.HandGreen == true)
        {
            isGreen1 = true;
        }
        else
        {
            if (isGreen1)
            {
                photoPlayerController.ButtonOnePlayer();
                SceneManager.LoadScene("MyPhoto");
            }
        }
    }
    public void ChangeTwoPlayer()
    {
        if (CursorRigth.HandGreen == true)
        {
            isGreen2 = true;
        }
        else
        {
            if (isGreen2)
            {
                photoPlayerController.ButtonTwoPlayer();
                SceneManager.LoadScene("MyPhotoPlayer1");
            }
        }
    }
}
