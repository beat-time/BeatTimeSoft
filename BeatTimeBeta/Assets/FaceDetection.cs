using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FaceDetection : MonoBehaviour
{
    public Image photo;
    PhotoPlayerController photoPlayerController;
    // Start is called before the first frame update
    void Start()
    {
        photoPlayerController = new PhotoPlayerController();
        Texture2D spriteTexture = null;
        string path = "Assets\\Resources\\Images\\";
        if (photoPlayerController.getTurnPlayerOne())
        {
            spriteTexture = LoadTexture(path + "photo1.png");
        }
        else
        {
            spriteTexture = LoadTexture(path + "photo2.png");
        }
        Sprite newSprite = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), Vector2.zero);
        photo.sprite = newSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Texture2D LoadTexture(string FilePath)
    {

        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails

        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
            if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
                return Tex2D;                 // If data = readable -> return texture
        }
        return null;                     // Return null if load failed
    }
}
