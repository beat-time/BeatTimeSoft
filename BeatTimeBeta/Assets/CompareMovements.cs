﻿using System;
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
    // Start is called before the first frame update
    void Start()
    {
        ReadMovements leerMovimientos = new ReadMovements();
        positions = leerMovimientos.LoadData();
        numberOfMovements = positions.Count / 28;
        cont = numberOfMovements;
        maxTimeCompare = timeCompare;
        movementsPerSecond = positions.Count / 56;

        if (!photoPlayerController.getTurnPlayerOne())
        {
            secondPlayer = true;
        }
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
            if (bodyObject2 == null && secondPlayer)
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
            if (timeCompare <= 0)
            {
                if (bodyObject1 != null || bodyObject2 != null)
                {
                    if (bodyObject1 != null)
                    {
                        JointsPlayer1 = GetJoints(bodyObject1);
                    }
                    if(bodyObject2 != null && secondPlayer)
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
                            if (bodyObject2 != null && secondPlayer)
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
                txt_qualification1.text = Calification(points1);
                totalPoints1 += points1;

                if(secondPlayer)
                {
                    txt_qualification2.text = Calification(points2);
                    totalPoints2 += points2;
                }

                cont += numberOfMovements;
                points1 = 0;
                points2 = 0;
                if (cont > positions.Count)
                {
                    txt_qualification1.text = "";
                    txt_qualification2.text = "";
                    //SceneManager.LoadScene("Score");
                    Initiate.Fade("Score", Color.black, 2.0f);
                    enabled = false;
                    //UnityEditor.EditorApplication.isPlaying = false;
                }
                timeCompare = maxTimeCompare;
            }
            timeCompare -= Time.deltaTime;
        }
    }

    string Calification(int points)
    {
        if (points >= 190 && points <= 250)
        {
            return "Excellent!!";

        }
        else if (points >= 180 && points < 190)
        {
            return "Good!!";
        }
        else if (points >= 160 && points < 180)
        {
            return "Regular";
        }
        else
        {
            return "Bad";
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
