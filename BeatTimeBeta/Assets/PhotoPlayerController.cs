using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoPlayerController : MonoBehaviour
{
    public static int NPlayer = 0;
    public Text Txt_Player;
    public static bool turnPlayerOne = false;
    public static bool turnPlayerTwo = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Txt_Player != null)
        {
            if (turnPlayerOne)
            {
                Txt_Player.text = "Player 1";
            }
            else if(turnPlayerTwo)
            {
                Txt_Player.text = "Player 2";
            }
        }
    }
    public void ChangeTurn()
    {
        turnPlayerTwo = true;
        turnPlayerOne = false;
    }

    public void ButtonOnePlayer()
    {
        NPlayer = 1;
        turnPlayerOne = true;
    }
    public void ButtonTwoPlayer()
    {
        NPlayer = 2;
        turnPlayerOne = true;
    }
    public bool ChangeToThemes()
    {
        if (NPlayer == 1)
        {
            return true;
        }
        else 
        {
            if (turnPlayerTwo)
            {
                turnPlayerTwo = false;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public bool getTurnPlayerOne()
    {
        return turnPlayerOne;
    }
}
