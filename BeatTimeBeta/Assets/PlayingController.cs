using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PlayingController : MonoBehaviour
{
    public RawImage player1;
    public RawImage player2;

    public Image photo1;
    public Image photo2;

    PhotoPlayerController photoPlayerController = new PhotoPlayerController();
    // Start is called before the first frame update
    private void Awake()
    {
        player1.gameObject.SetActive(false);
        player2.gameObject.SetActive(false);
    }
    void Start()
    {  
        if (photoPlayerController.getTurnPlayerOne())
        {
            player1.gameObject.SetActive(true);
            photo1.sprite = LoadPhoto("face1.png");
        }
        else
        {
            player1.gameObject.SetActive(true);
            player2.gameObject.SetActive(true);
            photo1.sprite = LoadPhoto("face1.png");
            photo2.sprite = LoadPhoto("face2.png");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Sprite LoadPhoto(string name)
    {
        Texture2D spriteTexture = null;
        string path = Application.dataPath + "//Resources//";
        if (File.Exists(path + name))
        {
            spriteTexture = LoadTexture(path + name);
        }
        else
        {
            spriteTexture = LoadTexture(path + "Images//noImage.png");
        }
        Sprite newSprite = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), Vector2.zero);
        return newSprite;
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
}
