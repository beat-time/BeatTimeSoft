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
    GameObject bodyObject = null;
    public BodySourceView bodies;
    List<Vector3> JointsPlayer = new List<Vector3>();
    List<Vector3> JointsMachine = new List<Vector3>();
    int cont = 0;
    int numberOfMovements = 0;
    int movementsPerSecond = 0;
    float dif1 = 2.5f;
    float dif2 = 5.5f;
    float dif3 = 7.8f;
    float xPlayer = 0;
    float yPlayer = 0;
    float zPlayer = 0;

    float xPlayerAux = 0;
    float yPlayerAux = 0;
    float zPlayerAux = 0;

    float xMachine = 0;
    float yMachine = 0;
    float zMachine = 0;

    float numberOfJoints = 25;
    int points = 0;
    float points11 = 0;
    float points12 = 0;
    float points13 = 0;
    float points21 = 0;
    float points22 = 0;
    float points23 = 0;
    float validPositions = 0;

    float timeCompare = 2;
    float maxTimeCompare = 2;

    public Canvas franky;
    public Text txt_qualification;
    string qualification = "";

    static int totalPoints = 0;

    public Image photo;
    // Start is called before the first frame update
    void Start()
    {
        ReadMovements leerMovimientos = new ReadMovements();
        positions = leerMovimientos.LoadData();
        numberOfMovements = positions.Count / 28;
        cont = numberOfMovements;
        maxTimeCompare = timeCompare;
        movementsPerSecond = positions.Count / 56;

        LoadPhoto();
    }

    // Update is called once per frame
    void Update()
    {
        if (franky.isActiveAndEnabled)
        {
            if (bodyObject == null)
            {
                if (bodies._Bodies.Count >= 1)
                {
                    bodyObject = bodies._Bodies.Values.First();
                    bodyObject.SetActive(false);
                }
            }
            if (timeCompare <= 0)
            {
                if (bodyObject != null)
                {
                    JointsPlayer = GetJoints(bodyObject);

                    int inicio = numberOfMovements - movementsPerSecond;
                    int max = numberOfMovements + movementsPerSecond;
                    int auxPoints = 0;

                    if (inicio < 1)
                    {
                        inicio = 1;
                    }
                    if (max > positions.Count)
                    {
                        max = positions.Count;
                    }
                    //points11 = 45;
                    while (inicio <= max)
                    {
                        
                        auxPoints = 0;
                        JointsMachine = positions[inicio];
                        //points11 = positions[1][6].x -10;
                        
                        for (int b = 0; b < numberOfJoints; b++)
                        {
                            xPlayer = JointsPlayer[b].x;
                            yPlayer = JointsPlayer[b].y;
                            zPlayer = JointsPlayer[b].z;

                            xMachine = JointsMachine[b].x/1000000;
                            yMachine = JointsMachine[b].y/1000000;
                            zMachine = JointsMachine[b].z/1000000;

                            //Debug.Log(xMachine +" - " + yMachine + " - " + zMachine);

                            if (b == 0)
                            {
                                xPlayerAux = xPlayer;
                                yPlayerAux = yPlayer;
                                zPlayerAux = zPlayer;

                                xPlayer = 0;
                                yPlayer = 0;
                                zPlayer = 0;
                            }
                            else
                            {
                                Vector3 v = new Vector3(xPlayer, yPlayer, zPlayer);
                                v = TransformJoint(v, new Vector3(xPlayerAux, yPlayerAux, zPlayerAux));

                                xPlayer = v.x;
                                yPlayer = v.y;
                                zPlayer = v.z;

                            }
                            //float distanceBefore = Vector3.Distance(JointsPlayer[a], JointsMachine[a]);
                            float distance = Vector3.Distance(new Vector3(xPlayer, yPlayer, zPlayer), new Vector3(xMachine, yMachine, zMachine));
                            //float distanceAfter = Vector3.Distance(JointsPlayer[a], JointsMachine[a]);
                            
                            //distance = distance;
                            //points = (int)distance;
                            points11 = distance;
                            points12 = yPlayer;
                            points13 = zPlayer;

                            points21 = xMachine;
                            points22 = yMachine;
                            points23 = zMachine;

                            if (distance <= dif1)
                            {
                                auxPoints += 10;
                            }
                            else if (distance <= dif2)
                            {
                                auxPoints += 7;
                            }
                            else if (distance <= dif3)
                            {
                                auxPoints += 4;
                            }
                            else
                            {
                                auxPoints += 1;
                            }
                            /*if (xPlayer >= xMachine - dif1 && xPlayer <= xMachine + dif1 &&
                                 yPlayer >= yMachine - dif1 && yPlayer <= yMachine + dif1 &&
                                 zPlayer >= zMachine - dif1 && zPlayer <= zMachine + dif1)
                             {
                                 points += 10;
                             }
                             else if (xPlayer >= xMachine - dif2 && xPlayer <= xMachine + dif2 &&
                                 yPlayer >= yMachine - dif2 && yPlayer <= yMachine + dif2 &&
                                 zPlayer >= zMachine - dif2 && zPlayer <= zMachine + dif2)
                             {
                                 points += 5;
                             }
                             else if (xPlayer >= xMachine - dif3 && xPlayer <= xMachine + dif3 &&
                                 yPlayer >= yMachine - dif3 && yPlayer <= yMachine + dif3 &&
                                 zPlayer >= zMachine - dif3 && zPlayer <= zMachine + dif3)
                             {
                                 points += 2;
                             }*/
                        }
                        
                        if (auxPoints > points)
                        {
                            points = auxPoints;
                        }
                        inicio += 2;
                    }
                }
                if (points >= 190 && points <= 250)
                {
                    qualification = "Excellent!!";
                  
                }
                else if (points >= 180 && points < 190)
                {
                    qualification = "Good!!" ;
                }
                else if (points >= 160 && points < 180)
                {
                    qualification = "Regular";
                }
                else
                {
                    qualification = "Bad";
                }
                totalPoints += points;

                txt_qualification.text = qualification;
                cont += numberOfMovements;
                points = 0;
                if (cont > positions.Count)
                {
                    txt_qualification.text = "";
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

    List<Vector3> GetJoints(GameObject obj)
    {
        List<Vector3> joints = new List<Vector3>();
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            Transform jointObj = bodyObject.transform.Find(jt.ToString());
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
        return totalPoints;
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
