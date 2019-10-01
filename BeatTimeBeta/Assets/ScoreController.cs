using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public KinectUICursor CursorRight;
    public KinectUICursor CursorLeft;

    public Image photo;

    public Button btnRepeatDance;
    public Button btnChooseSong;
    public Button btnChangePlayers;

    float crHeight;
    float crWidth;
    float clHeight;
    float clWidth;

    float btnRDWidth = 0.0f;
    float btnRDHeight = 0.0f;
    float btnRDPosX = 0.0f;
    float btnRDPosY = 0.0f;

    float btnCSWidth = 0.0f;
    float btnCSHeight = 0.0f;
    float btnCSPosX = 0.0f;
    float btnCSPosY = 0.0f;

    float btnCPWidth = 0.0f;
    float btnCPHeight = 0.0f;
    float btnCPPosX = 0.0f;
    float btnCPPosY = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        LoadPhoto();
        crHeight = CursorRight.GetComponent<RectTransform>().rect.height;
        crWidth = CursorRight.GetComponent<RectTransform>().rect.width;
        clHeight = CursorLeft.GetComponent<RectTransform>().rect.height;
        clWidth = CursorLeft.GetComponent<RectTransform>().rect.width;

        btnRDWidth = btnRepeatDance.GetComponent<RectTransform>().rect.width;
        btnRDHeight = btnRepeatDance.GetComponent<RectTransform>().rect.width;
        btnRDPosX = btnRepeatDance.transform.position.x;
        btnRDPosY = btnRepeatDance.transform.position.y;

        btnCSWidth = btnChooseSong.GetComponent<RectTransform>().rect.width;
        btnCSHeight = btnChooseSong.GetComponent<RectTransform>().rect.width;
        btnCSPosX = btnChooseSong.transform.position.x;
        btnCSPosY = btnChooseSong.transform.position.y;

        btnCPWidth = btnChangePlayers.GetComponent<RectTransform>().rect.width;
        btnCPHeight = btnChangePlayers.GetComponent<RectTransform>().rect.width;
        btnCPPosX = btnChangePlayers.transform.position.x;
        btnCPPosY = btnChangePlayers.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();

        Click_RepeatDance();
        Click_ChooseAnotherSong();
        Click_ChangePLayers();
    }

    void UpdatePosition()
    {
        btnRDPosX = btnRepeatDance.transform.position.x;
        btnRDPosY = btnRepeatDance.transform.position.y;

        btnCSPosX = btnChooseSong.transform.position.x;
        btnCSPosY = btnChooseSong.transform.position.y;

        btnCPPosX = btnChangePlayers.transform.position.x;
        btnCPPosY = btnChangePlayers.transform.position.y;
    }
    void LoadPhoto()
    {
        Texture2D spriteTexture = null;
        string path = "Assets\\Resources\\Images\\";
        if (File.Exists(path + "face1.png")){
            spriteTexture = LoadTexture(path + "face1.png");
        }
        else
        {
            spriteTexture = LoadTexture(path + "noImage.png");
        }
        Sprite newSprite = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), Vector2.zero);
        photo.sprite = newSprite;
    }
    Texture2D LoadTexture(string FilePath)
    {
        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);           
            if (Tex2D.LoadImage(FileData))         
                return Tex2D;                 
        }
        return null;                     
    }

    public void Click_RepeatDance()
    {
        if (CursorRight.HandGreen || CursorLeft.HandGreen)
        {
            if (CursorRight.transform.position.x - crWidth / 2 >= btnRDPosX - btnRDWidth / 2 &&
                CursorRight.transform.position.x + crWidth / 2 <= btnRDPosX + btnRDWidth / 2 &&
                CursorRight.transform.position.y - crHeight / 2 >= btnRDPosY - btnRDHeight / 2 &&
                CursorRight.transform.position.y + crHeight / 2 <= btnRDPosY + btnRDHeight / 2)
            {
                SceneManager.LoadScene("Playing");
            }
            else if (CursorLeft.transform.position.x - clWidth / 2 >= btnRDPosX - btnRDWidth / 2 &&
                CursorLeft.transform.position.x + clWidth / 2 <= btnRDPosX + btnRDWidth / 2 &&
                CursorLeft.transform.position.y - clHeight / 2 >= btnRDPosY - btnRDHeight / 2 &&
                CursorLeft.transform.position.y + clHeight / 2 <= btnRDPosY + btnRDHeight / 2)
            {
                SceneManager.LoadScene("Playing");
            }
        }
    }
    
    public void Click_ChooseAnotherSong()
    {
        if (CursorRight.HandGreen || CursorLeft.HandGreen)
        {
            if (CursorRight.transform.position.x - crWidth / 2 >= btnCSPosX - btnCSWidth / 2 &&
                CursorRight.transform.position.x + crWidth / 2 <= btnCSPosX + btnCSWidth / 2 &&
                CursorRight.transform.position.y - crHeight / 2 >= btnCSPosY - btnCSHeight / 2 &&
                CursorRight.transform.position.y + crHeight / 2 <= btnCSPosY + btnCSHeight / 2)
            {
                SceneManager.LoadScene("SelecThemes");
            }
            else if (CursorLeft.transform.position.x - clWidth / 2 >= btnCSPosX - btnCSWidth / 2 &&
                CursorLeft.transform.position.x + clWidth / 2 <= btnCSPosX + btnCSWidth / 2 &&
                CursorLeft.transform.position.y - clHeight / 2 >= btnCSPosY - btnCSHeight / 2 &&
                CursorLeft.transform.position.y + clHeight / 2 <= btnCSPosY + btnCSHeight / 2)
            {
                SceneManager.LoadScene("SelecThemes");
            }
        }
    }

    public void Click_ChangePLayers()
    {
        if (CursorRight.HandGreen || CursorLeft.HandGreen)
        {
            if (CursorRight.transform.position.x - crWidth / 2 >= btnCPPosX - btnCSWidth / 2 &&
                CursorRight.transform.position.x + crWidth / 2 <= btnCPPosX + btnCSWidth / 2 &&
                CursorRight.transform.position.y - crHeight / 2 >= btnCPPosY - btnCSHeight / 2 &&
                CursorRight.transform.position.y + crHeight / 2 <= btnCPPosY + btnCSHeight / 2)
            {
                SceneManager.LoadScene("SelecPlayers");
            }
            else if (CursorLeft.transform.position.x - clWidth / 2 >= btnCPPosX - btnCPWidth / 2 &&
                CursorLeft.transform.position.x + clWidth / 2 <= btnCPPosX + btnCPWidth / 2 &&
                CursorLeft.transform.position.y - clHeight / 2 >= btnCPPosY - btnCPHeight / 2 &&
                CursorLeft.transform.position.y + clHeight / 2 <= btnCPPosY + btnCPHeight / 2)
            {
                SceneManager.LoadScene("SelecPlayers");
            }
        }
    }
}
