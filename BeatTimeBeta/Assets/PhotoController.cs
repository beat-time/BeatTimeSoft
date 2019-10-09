using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotoController : MonoBehaviour
{
    public BodySourceView bodySourceView;
    float time;
    float maxTime = 5.0f;
    public RawImage rawImage;
    public RawImage rawImageSnip;
    //public Text txt_time_controller;
    PhotoPlayerController photoPlayerController;
    // Start is called before the first frame update
    void Start()
    {
        time = maxTime;
        //txt_time_controller.text = maxTime.ToString("f0");
        photoPlayerController = new PhotoPlayerController();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        //txt_time_controller.text = time.ToString("f0");
        if (time <= 0)
        {
            Texture2D photoTexture = rawImage.texture as Texture2D;
            //photoTexture = SnipTexture(photoTexture, rawImage, rawImageSnip);
            photoTexture = Rotation180(photoTexture);
            byte[] bytes = photoTexture.EncodeToPNG();
            string path = Application.dataPath + "//Resources//"; //"Assets\\Resources\\Images\\";
            if (photoPlayerController.getTurnPlayerOne())
            {
                File.WriteAllBytes(path + "photo1" + ".png", bytes);
            }
            else
            {
                File.WriteAllBytes(path + "photo2" + ".png", bytes);
            }
            SceneManager.LoadScene("MyPhoto2");
        }
    }
    public Texture2D Rotation180(Texture2D texture)
    {
        int width = texture.width;
        int height = texture.height;
        Texture2D snap = new Texture2D(width, height);
        Color[] pixels = texture.GetPixels();
        Color[] pixelsFlipped = new Color[pixels.Length];

        for (int i = 0; i < height; i++)
        {
            Array.Copy(pixels, i * width, pixelsFlipped, (height - i - 1) * width, width);
        }

        snap.SetPixels(pixelsFlipped);
        snap.Apply();
        return snap;
    }

    public Texture2D SnipTexture(Texture2D photo,RawImage rawImage, RawImage snip)
    {
        Color[] c = photo.GetPixels((int)(rawImage.rectTransform.rect.width - snip.rectTransform.rect.width)/2 + (int)rawImageSnip.rectTransform.rect.x,
            (int)(rawImage.rectTransform.rect.height - snip.rectTransform.rect.height) / 2+ (int)rawImageSnip.rectTransform.rect.y,
            (int)snip.rectTransform.rect.width, 
            (int)snip.rectTransform.rect.height);
        // x=7; y=-15
        Texture2D m2Texture = new Texture2D((int)snip.rectTransform.rect.width, (int)snip.rectTransform.rect.height);
        m2Texture.SetPixels(c);
        m2Texture.Apply();
        return m2Texture;
    }
}
