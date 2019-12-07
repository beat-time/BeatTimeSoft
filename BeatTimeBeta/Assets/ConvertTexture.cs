using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ConvertTexture : MonoBehaviour
{
    public Texture2D Convert(bool isTurnPlayerOne)
    {
        Texture2D sample = new Texture2D(2, 2);
        byte[] fileData;
        string path = Application.dataPath + "//Resources//";
        if (isTurnPlayerOne)
        {
            path = path + "photo1.png";
        }
        else{
            path = path + "photo2.png";
        }
        
        if (File.Exists(path))
        {
            fileData = File.ReadAllBytes(path);
            sample.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return sample;
    }
    public void SaveFace(Texture2D mTexture, int X, int Y, int width, int height, bool isTurnPlayerOne)
    {
        //Crop an image
        Color[] c = mTexture.GetPixels(X, mTexture.height - (Y + height), width, height);
        Texture2D m2Texture = new Texture2D(width, height);
        m2Texture.SetPixels(c);
        m2Texture.Apply();

        //then Save To Disk as PNG
        byte[] bytes = m2Texture.EncodeToPNG();
        var dirPath = Application.dataPath + "//Resources//"; // Application.dataPath + "/../SaveImages/";
        if (isTurnPlayerOne)
        {
            File.WriteAllBytes(dirPath + "face1" + ".png", bytes);
        }
        else
        {
            File.WriteAllBytes(dirPath + "face2" + ".png", bytes);
        }
        //if (!Directory.Exists(dirPath))
        //{
        //    Directory.CreateDirectory(dirPath);
        //}
    }
    public Sprite LoadFace(bool isTurnPlayerOne)
    {
        Texture2D spriteTexture = null;
        string path = Application.dataPath + "//Resources//"; ;
        if (isTurnPlayerOne)
        {
            spriteTexture = LoadTexture(path + "face1.png");
        }
        else
        {
            spriteTexture = LoadTexture(path + "face2.png");
        }
        Sprite newSprite = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), Vector2.zero);
        return newSprite;
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

    public void SaveNoImage(bool isTurnPlayerOne)
    {
        byte[] file = File.ReadAllBytes(Application.dataPath + "//Resources//Images//noImage.png");
        //video_player_center.clip = Resources.Load<VideoClip>("VideoMusica\\1");
        //Image textura = Resources.Load<Image>("Images\\noImage.png");

        //byte[] file = textura

        //Texture2D t = Resources.Load<Texture2D>("VideoMusica\\1");


        Texture2D t = new Texture2D(2, 2);
        t.LoadImage(file);
        SaveFace(t, 0, 0, t.width, t.height, isTurnPlayerOne);
    }
}
