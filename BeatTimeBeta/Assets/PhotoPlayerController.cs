using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoPlayerController : MonoBehaviour
{
    public static int NPlayer = 0;
    public Text Txt_Player;
    public static bool Player1 = false;
    // Start is called before the first frame update
    void Start()
    {
        if (Txt_Player != null)
        {
            if (Player1)
            {
                Txt_Player.text = "Player 1";
            }
            else
            {
                Txt_Player.text = "Player 2";
            }
        }
        else
        {
            NPlayer = 0;
        }
    }

    public void ChangePhotoPlayer()
    {
        if (Player1)
        {
            Player1 = false;
        }
        else
        {
            Player1 = true;
        }
        NPlayer++;
    }
    public void IsPlayer1()
    {
        Player1 = true;
        NPlayer++;
    }
    public void IsPlayer2()
    {
        Player1 = false;
        NPlayer++;
    }
    public bool ChangeToThemes()
    {
        if (NPlayer >= 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
