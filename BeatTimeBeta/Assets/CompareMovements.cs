using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
    float dif1 = 2f;
    float dif2 = 4f;
    float dif3 = 5f;
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
    float points = 0;
    float validPositions = 0;

    float timeCompare = 2;
    float maxTimeCompare = 2;

    public Canvas franky;
    public Text txt_qualification;
    string qualification = "";
    // Start is called before the first frame update
    void Start()
    {
        ReadMovements leerMovimientos = new ReadMovements();
        positions = leerMovimientos.LoadData();
        numberOfMovements = positions.Count / 28;
        cont = numberOfMovements;
        maxTimeCompare = timeCompare;
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
                    JointsMachine = positions[cont];

                    for (int a = 0; a < numberOfJoints; a++)
                    {
                        xPlayer = JointsPlayer[a].x;
                        yPlayer = JointsPlayer[a].y;
                        zPlayer = JointsPlayer[a].z;

                        xMachine = JointsMachine[a].x;
                        yMachine = JointsMachine[a].y;
                        zMachine = JointsMachine[a].z;

                        if (a == 0)
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

                        if (xPlayer >= xMachine - dif1 && xPlayer <= xMachine + dif1 &&
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
                        }
                    }
                }
                if (points == 250)
                {
                    qualification = "Excellent!!!";
                }
                else if (points >= 200 && points < 250)
                {
                    qualification = "Good!!!";
                }
                else if (points >= 100 && points < 200)
                {
                    qualification = "Regular";
                }
                else
                {
                    qualification = "Bad";
                }
                txt_qualification.text = qualification;
                cont += numberOfMovements;
                points = 0;
                if (cont > positions.Count)
                {
                    txt_qualification.text = "";
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
}
