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
    public Text txt_time_controller;
    // Start is called before the first frame update
    void Start()
    {
        time = maxTime;
        txt_time_controller.text = maxTime.ToString("f0");
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        txt_time_controller.text = time.ToString("f0");
        if (time <= 0)
        {
            Texture2D photoTexture = rawImage.texture as Texture2D;
            photoTexture = Rotation180(photoTexture);
            byte[] bytes = photoTexture.EncodeToPNG();
            File.WriteAllBytes("Assets\\Resources\\Images\\" + "photo" + ".png", bytes);
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
}
