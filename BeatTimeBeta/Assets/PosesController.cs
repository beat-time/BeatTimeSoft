using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PosesController : MonoBehaviour
{
    public RawImage camino;
    public RawImage pose;

    float timeTotal = 2;
    float maxTime = 0;
    int contadorPoses = 1;
    // Start is called before the first frame update
    void Start()
    {
        pose.gameObject.SetActive(false);
        maxTime = timeTotal;
    }

    // Update is called once per frame
    void Update()
    {
        timeTotal -= Time.deltaTime;
        if (timeTotal <= 0)
        {
            if (contadorPoses == 15)
            {
                contadorPoses = 1;
            }
            pose.gameObject.SetActive(true);
            pose.texture = LoadPose(contadorPoses.ToString() + ".png");
            contadorPoses++;
            timeTotal = maxTime;
        }
    }

    Texture2D LoadPose(string number)
    {
        Texture2D spriteTexture = null;
        string path = Application.dataPath + "//Resources//Poses//Song1//";
        if (File.Exists(path + number))
        {
            spriteTexture = LoadTexture(path + number);
        }
        else
        {
            spriteTexture = LoadTexture(path + "Images//noImage.png");
        }
        return spriteTexture;
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
