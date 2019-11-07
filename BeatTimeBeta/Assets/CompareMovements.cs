using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Kinect = Windows.Kinect;

public class CompareMovements : MonoBehaviour
{
    public RawImage Cali;
    public RawImage Cali2;
    public RawImage Exc;
    public RawImage Buen;
    public RawImage Regu;
    public RawImage Mal;
    public int TimeMusic = 1;
    public int NPlayer = 0;
    public int Scene = 1;
    Dictionary<int, List<Vector3>> positions = new Dictionary<int, List<Vector3>>();
    GameObject bodyObject1 = null;
    GameObject bodyObject2 = null;
    public BodySourceView bodies;
    List<Vector3> JointsPlayer1 = new List<Vector3>();
    List<Vector3> JointsPlayer2 = new List<Vector3>();
    List<Vector3> JointsMachine = new List<Vector3>();
    int cont = 0;
    int numberOfMovements = 0;
    int movementsPerSecond = 0;
    float dif1 = 2.5f;
    float dif2 = 5.5f;
    float dif3 = 7.8f;

    Vector3 Player1;
    Vector3 PlayerAux1;
    Vector3 Player2;
    Vector3 PlayerAux2;
    Vector3 Machine;
    //float xPlayer = 0;
    //float yPlayer = 0;
    //float zPlayer = 0;

    //float xPlayerAux = 0;
    //float yPlayerAux = 0;
    //float zPlayerAux = 0;

    //float xMachine = 0;
    //float yMachine = 0;
    //float zMachine = 0;

    float numberOfJoints = 25;
    int points1 = 0;
    int points2 = 0;

    float timeCompare = 2;
    float maxTimeCompare = 2;

    public Canvas franky;
    public Text txt_qualification1;
    public Text txt_qualification2;
    string qualification = "";

    static int totalPoints1 = 0;
    static int totalPoints2 = 0;

    public Image photo;

    private PhotoPlayerController photoPlayerController = new PhotoPlayerController();
    bool secondPlayer = false;

    public RawImage progressBar;
    float progressBarWidth;

    float totalPerfectPoints;

    ProgressBar barPlayer1;
    ProgressBar barPlayer2;
    public RawImage rawImageBar1;
    public RawImage rawImageBar2;

    bool changeScene = false;
    float timeChangeScene = 2;
    // Start is called before the first frame update
    private void Awake()
    {
        Cali.gameObject.SetActive(false);
        Cali2.gameObject.SetActive(false);
    }
    void Start()
    {
        txt_qualification1.text = "0";
        txt_qualification2.text = "0";
        ReadMovements leerMovimientos = new ReadMovements();
        positions = leerMovimientos.LoadData(Scene);
        
        numberOfMovements = positions.Count / (TimeMusic/2);
        cont = numberOfMovements;
        maxTimeCompare = timeCompare;
        movementsPerSecond = positions.Count / TimeMusic;

        if (!photoPlayerController.getTurnPlayerOne())
        {
            //secondPlayer = true;
        }
        progressBarWidth = progressBar.rectTransform.rect.width;
        totalPerfectPoints = 10 * 250;
        barPlayer1 = rawImageBar1.GetComponent<ProgressBar>();
        barPlayer2 = rawImageBar2.GetComponent<ProgressBar>();
        //LoadPhoto();
    }

    // Update is called once per frame
    void Update()
    {
        if (franky.isActiveAndEnabled)
        {
            if (bodyObject1 == null)
            {
                if (bodyObject2 != null)
                {
                    if (bodies._Bodies.Count >= 2)
                    {
                        bodyObject1 = bodies._Bodies.Values.ElementAt(1);
                    }
                }
                else
                {
                    if (bodies._Bodies.Count >= 1)
                    {
                        bodyObject1 = bodies._Bodies.Values.ElementAt(0);
                    }
                }

                //bodyObject1 = bodies._Bodies.Values.First();
            }
            if (bodyObject2 == null && NPlayer==2)
            {
                if (bodyObject1 != null)
                {
                    if (bodies._Bodies.Count >= 2)
                    {
                        bodyObject2 = bodies._Bodies.Values.ElementAt(1);
                    }
                }
                else
                {
                    if (bodies._Bodies.Count >= 1)
                    {
                        bodyObject2 = bodies._Bodies.Values.ElementAt(0);
                    }
                }
            }
            if (timeCompare <= 0 && !changeScene)
            {
                if (bodyObject1 != null || bodyObject2 != null)
                {
                    if (bodyObject1 != null)
                    {
                        JointsPlayer1 = GetJoints(bodyObject1);
                    }
                    if(bodyObject2 != null && NPlayer==2)
                    {
                        JointsPlayer2 = GetJoints(bodyObject2);
                    }
                    int inicio = numberOfMovements - movementsPerSecond;
                    int max = numberOfMovements + movementsPerSecond;
                    int auxPoints1 = 0;
                    int auxPoints2 = 0;
                    if (inicio < 1)
                    {
                        inicio = 1;
                    }
                    if (max > positions.Count)
                    {
                        max = positions.Count;
                    }
                    while (inicio <= max)
                    {
                        
                        auxPoints1 = 0;
                        auxPoints2 = 0;
                        JointsMachine = positions[inicio];
                        
                        for (int b = 0; b < numberOfJoints; b++)
                        {
                            Machine = JointsMachine[b];
                            if (bodyObject1 != null)
                            {
                                Player1 = JointsPlayer1[b];
                                if (b == 0)
                                {
                                    PlayerAux1 = Player1;
                                    Player1 = Vector3.zero;
                                }
                                else
                                {
                                    Player1 = TransformJoint(Player1, PlayerAux1);
                                }
                                float distance = Vector3.Distance(Player1, Machine);
                                if (distance <= dif1)
                                {
                                    auxPoints1 += 10;
                                }
                                else if (distance <= dif2)
                                {
                                    auxPoints1 += 7;
                                }
                                else if (distance <= dif3)
                                {
                                    auxPoints1 += 4;
                                }
                                else
                                {
                                    auxPoints1 += 1;
                                }
                            }
                            if (bodyObject2 != null && NPlayer==2)
                            {
                                Player2 = JointsPlayer2[b];
                                if (b == 0)
                                {
                                    PlayerAux2 = Player2;
                                    Player2 = Vector3.zero;
                                }
                                else
                                {
                                    Player2 = TransformJoint(Player2, PlayerAux2);
                                }
                                float distance = Vector3.Distance(Player2, Machine);
                                if (distance <= dif1)
                                {
                                    auxPoints2 += 10;
                                }
                                else if (distance <= dif2)
                                {
                                    auxPoints2 += 7;
                                }
                                else if (distance <= dif3)
                                {
                                    auxPoints2 += 4;
                                }
                                else
                                {
                                    auxPoints2 += 1;
                                }
                            }
                        }

                        if (auxPoints1 > points1)
                        {
                            points1 = auxPoints1;
                        }

                        if (auxPoints2 > points2)
                        {
                            points2 = auxPoints2;
                        }

                        inicio += 2;
                    }
                }
                
                txt_qualification1.text = totalPoints1.ToString();//Calification(points1);

                Cali.texture = Calification(points1);

                if (!Cali.isActiveAndEnabled)
                {
                    Cali.gameObject.SetActive(true);
                }

                if (!barPlayer1.move)
                {
                    totalPoints1 += 2 * points1;
                }
                else
                {
                    totalPoints1 += points1;
                }
                barPlayer1.progress += points1 * progressBarWidth / totalPerfectPoints;

                if (NPlayer==2)
                {
                    txt_qualification2.text = totalPoints2.ToString();//Calification(points2);
                    Cali2.texture = Calification(points2);

                    if (!Cali2.isActiveAndEnabled)
                    {
                        Cali2.gameObject.SetActive(true);
                    }
                    if (!barPlayer2.move)
                    {
                        totalPoints2 += 2 * points2;
                    }
                    else
                    {
                        totalPoints2 += points2;
                    }
                    barPlayer2.progress += points2 * progressBarWidth / totalPerfectPoints;
                }

                cont += numberOfMovements;
                points1 = 0;
                points2 = 0;
                if (cont > positions.Count)
                {
                    changeScene = true;
                }
                timeCompare = maxTimeCompare;
            }
            timeCompare -= Time.deltaTime;

            if (changeScene)
            {
                if (timeChangeScene <= 0)
                {
                    txt_qualification1.text = "";
                    txt_qualification2.text = "";
                    if (NPlayer == 1)
                    {
                        Initiate.Fade("Score", Color.black, 2.0f);
                    }
                    else
                    {
                        Initiate.Fade("Score2", Color.black, 2.0f);
                    }
                    enabled = false;
                }
                timeChangeScene -= Time.deltaTime;
            }
        }
    }

    Texture2D Calification(int points)
    {
        if (points >= 210 && points <= 250)
        {
            return Exc.texture as Texture2D;
        }
        else if (points >= 190 && points < 210)
        {
            return Buen.texture as Texture2D;
        }
        else if (points >= 175 && points < 190)
        {
            return Regu.texture as Texture2D;
        }
        else
        {
            return Mal.texture as Texture2D;
        }
    }

    List<Vector3> GetJoints(GameObject obj)
    {
        List<Vector3> joints = new List<Vector3>();
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            Transform jointObj = obj.transform.Find(jt.ToString());
            joints.Add(jointObj.localPosition);
        }
        return joints;
    }

    Vector3 TransformJoint(Vector3 joint, Vector3 jointBase)
    {
        return new Vector3(joint.x - jointBase.x, joint.y - jointBase.y, joint.z - jointBase.z);
    }

    public int GetTotalPoints()
    {
        return totalPoints1;
    }

    public int GetTotalPointsPlayer2()
    {
        return totalPoints2;
    }

    void LoadPhoto()
    {
        Texture2D spriteTexture = null;
        string path = Application.dataPath + "//Resources//";
        if (File.Exists(path + "face1.png"))
        {
            spriteTexture = LoadTexture(path + "face1.png");
        }
        else
        {
            spriteTexture = LoadTexture(path + "Images//noImage.png");
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
}
