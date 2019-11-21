using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PosesController : MonoBehaviour
{
    public RawImage camino;
    public RawImage poseTemplate;

    int contador = 1;

    List<Pose> posesData = new List<Pose>();

    float generalAT = 0;
    bool nextPose = true;
    int totalPoses = 0;

    Vector3 startPosition;
    Vector3 target;
    List<int> numberPoses = new List<int>();
    List<float> timeTargets = new List<float>();
    List<float> durations = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
        /*LoadDataPoses();
        totalPoses = posesData.Count - 1;
        startPosition = Vector3.zero;
        target = new Vector3(startPosition.x - (camino.rectTransform.rect.width - poseTemplate.rectTransform.rect.width) / 2, 0, 0);*/
    }

    void CreateRawImagePose(int numberOfPose, int index)
    {
        RawImage riPose = Instantiate(poseTemplate);
        riPose.name = "pose" + index;
        riPose.transform.SetParent(camino.transform, false);
        riPose.transform.localPosition = Vector3.zero;
        riPose.texture = LoadImagePose(numberOfPose + ".png");
        riPose.gameObject.SetActive(true);
    }

    void MovePoses()
    {
        for (int a = 0; a < numberPoses.Count; a++)
        {
            int np = numberPoses[a];
            float t = timeTargets[a];
            t += Time.deltaTime / durations[a];
            timeTargets[a] = t;
            GameObject.Find("pose" + np).transform.localPosition = Vector3.Lerp(startPosition, target, t);
            if (t >= 1)
            {
                Destroy(GameObject.Find("pose" + numberPoses[a]));
                numberPoses.RemoveAt(a);
                timeTargets.RemoveAt(a);
                durations.RemoveAt(a);
                a--;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (contador <= totalPoses)
        {
            if (nextPose)
            {
                generalAT = posesData[contador].appearingTime;
                nextPose = false;
            }
            generalAT -= Time.deltaTime;
            if (generalAT <= 0)
            {
                CreateRawImagePose(posesData[contador].pose, contador);
                numberPoses.Add(contador);
                timeTargets.Add(0);
                durations.Add(posesData[contador].duration);
                nextPose = true;
                contador++;
            }
        }
        MovePoses();*/
    }

    Texture2D LoadImagePose(string number)
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
        string[] data = File.ReadAllLines(Application.dataPath + "//Resources//Poses//Song1//pose.txt");
        int numberOfPoses = int.Parse(data[0]);
        posesData.Add(null);
        for (int a = 1; a <= numberOfPoses; a++)
        {
            string[] row = data[a].Split(char.Parse("\t"));
            float appear = float.Parse(row[0], System.Globalization.CultureInfo.InvariantCulture); //appearingTime
            float dur = float.Parse(row[1], System.Globalization.CultureInfo.InvariantCulture); //duration
            int p = int.Parse(row[2]); //pose           
            posesData.Add(new Pose(p, appear, dur));
        }
    }
}

public class Pose
{
    public int pose;
    public float appearingTime; //seconds
    public float duration; //seconds

    public Pose(int p, float at, float d)
    {
        pose = p;
        appearingTime = at;
        duration = d;
    }
}
