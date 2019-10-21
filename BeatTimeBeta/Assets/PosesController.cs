using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PosesController : MonoBehaviour
{
    public RawImage camino;
    public RawImage pose1;
    public RawImage pose2;
    public RawImage pose3;

    int contadorPoses = 1;
    Vector3 startPosition1;
    Vector3 target1;
    Vector3 startPosition2;
    Vector3 target2;
    Vector3 startPosition3;
    Vector3 target3;
    float timeToReachTarget;
    float t = 0;
    int contador = 1;

    List<Pose> posesData = new List<Pose>();
    Pose objPose = null;
    int numImages = 0;
    // Start is called before the first frame update
    void Start()
    {
        LoadDataPoses();
        pose1.transform.localPosition = Vector3.zero;
        pose1.gameObject.SetActive(true);
        ChangePose(1);
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime / timeToReachTarget;

        //startPosition = Vector3.zero;
        //target = new Vector3(startPosition.x - (camino.rectTransform.rect.width - pose1.rectTransform.rect.width) / 2, 0, 0);
        //pose1.transform.localPosition = Vector3.Lerp(startPosition, target, t);
        for (int a = 1; a <= numImages; a++)
        {
            if (a == 1)
            {
                pose1.transform.localPosition = Vector3.Lerp(startPosition1, target1, t);
            }
            else if (a == 2)
            {
                pose2.transform.localPosition = Vector3.Lerp(startPosition2, target2, t);
            }
            else
            {
                pose3.transform.localPosition = Vector3.Lerp(startPosition3, target3, t);
            }
        }
        if (t >= 1)
        {
            contador++;
            if (contador > posesData.Count - 1)
            {
                PosesSetActive(false);
            }
            else
            {
                ChangePose(contador);
            }
        }
    }

    void ChangePose(int number)
    {
        pose1.transform.localPosition = Vector3.zero;
        pose2.transform.localPosition = Vector3.zero;
        pose3.transform.localPosition = Vector3.zero;
        PosesSetActive(false);
        objPose = posesData[number];
        numImages = objPose.poses.Count;
        for(int a = 1; a <= numImages; a++)
        {
            float width = pose1.rectTransform.rect.width;
            float widthCamino = camino.rectTransform.rect.width;
            if (a == 1)
            {
                pose1.gameObject.SetActive(true);
                startPosition1 = new Vector3(pose1.transform.localPosition.x - (numImages - a) * width, 0, 0);
                target1 = new Vector3(0 - (widthCamino - width) / 2, 0, 0);
                pose1.transform.localPosition = startPosition1;
                pose1.texture = LoadPose(objPose.poses[a - 1] + ".png");
            }
            else if (a == 2)
            {
                pose2.gameObject.SetActive(true);
                startPosition2 = new Vector3(pose2.transform.localPosition.x - (numImages - a) * width, 0, 0);
                target2 = new Vector3(0 - (widthCamino - 3 * width) / 2, 0, 0);
                pose2.transform.localPosition = startPosition2;
                pose2.texture = LoadPose(objPose.poses[a - 1] + ".png");
            }
            else
            {
                pose3.gameObject.SetActive(true);
                startPosition3 = new Vector3(pose3.transform.localPosition.x - (numImages - a) * width, 0, 0);
                target3 = new Vector3(0 - (widthCamino - 5 * width) / 2, 0, 0);
                pose3.transform.localPosition = startPosition3;
                pose3.texture = LoadPose(objPose.poses[a - 1] + ".png");
            }
        }
        
        //pose1.transform.localPosition = Vector3.zero;
        //pose1.texture = LoadPose(number + ".png");
        timeToReachTarget = posesData[number].seconds;
        t = 0;
    }

    void PosesSetActive(bool active)
    {
        pose1.gameObject.SetActive(active);
        pose2.gameObject.SetActive(active);
        pose3.gameObject.SetActive(active);
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
            spriteTexture = LoadTexture(Application.dataPath + "//Resources//Images//noImage.png");
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

    void LoadDataPoses()
    {
        string[] data = File.ReadAllLines(Application.dataPath + "//Resources//Poses//Song1//data.txt");
        int numberOfPoses = int.Parse(data[0]);
        posesData.Add(null);
        for(int a = 1; a <= numberOfPoses; a++)
        {
            string[] row = data[a].Split(char.Parse("\t"));
            int temp = int.Parse(row[0]);
            float s = 0;
            List<int> p = new List<int>();
            for(int b = 1; b <= temp; b++)
            {
                p.Add(int.Parse(row[b]));
            }
            s = float.Parse(row[temp + 1], System.Globalization.CultureInfo.InvariantCulture);
            posesData.Add(new Pose(p, s));
        }
    }
}

public class Pose
{
    public List<int> poses;
    public float seconds;

    public Pose(List<int> p, float s)
    {
        poses = p;
        seconds = s;
    }
}
