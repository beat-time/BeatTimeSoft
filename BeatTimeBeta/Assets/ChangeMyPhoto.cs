using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeMyPhoto : MonoBehaviour
{
    public KinectUICursor CursorRigth;
    //public ContPlayers contPlayers;
    public Button btn1;
    public Button btn2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CursorRigth.transform.position.x > btn1.transform.position.x - 80 && CursorRigth.transform.position.x < btn1.transform.position.x + 80 && CursorRigth.transform.position.y > btn1.transform.position.y - 80 && CursorRigth.transform.position.y < btn1.transform.position.y + 80)
        {
            ChangeOnePlayer();
        }
        if (CursorRigth.transform.position.x > btn2.transform.position.x - 80 && CursorRigth.transform.position.x < btn2.transform.position.x + 80 && CursorRigth.transform.position.y > btn2.transform.position.y - 80 && CursorRigth.transform.position.y < btn2.transform.position.y + 80)
        {
            ChangeTwoPlayer();
        }
    }

    public void ChangeOnePlayer()
    {
        if (CursorRigth.HandGreen == true)
        {
            SceneManager.LoadScene("MyPhoto");
            //contPlayers.Players = 1;
        }
    }
    public void ChangeTwoPlayer()
    {
        if (CursorRigth.HandGreen == true)
        {
            //contPlayers.Players = 2;
            SceneManager.LoadScene("MyPhoto");
        }
    }
}
