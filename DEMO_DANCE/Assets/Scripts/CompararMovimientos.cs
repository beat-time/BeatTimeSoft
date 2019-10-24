using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect;

public class CompararMovimientos : MonoBehaviour
{
    Dictionary<int, List<Vector3>> positions = new Dictionary<int, List<Vector3>>();
    GameObject bodyObject = null;
    List<Vector3> JointsPlayer = new List<Vector3>();
    List<Vector3> JointsMachine = new List<Vector3>();
    int cont = 1;
    float dif1 = 2;
    float dif2 = 5;
    float dif3 = 8;
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
    // Start is called before the first frame update
    void Start()
    {
        //LeerMovimientos leerMovimientos = new LeerMovimientos();
        //positions = leerMovimientos.LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        if (bodyObject == null)
        {
            bodyObject = GameObject.FindWithTag("body");
        }
        else
        {
            JointsPlayer = GetJoints(bodyObject);
            JointsMachine = positions[cont];

            for(int a = 0; a < numberOfJoints; a++)
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
            if (points == 250)
            {
                Debug.Log("Excellent");
            }
            else if(points >= 150 && points < 250)
            {
                Debug.Log("Good");
            }
            else if (points >= 50 && points < 150)
            {
                Debug.Log("Regular");
            }
            else
            {
                Debug.Log("Bad");
            }
            cont++;
            points = 0;
            if (cont > positions.Count)
            {
                //float precision = validPositions / positions.Count;
                //if (precision > 0.8)
                //{
                //    Debug.Log("Es bueno bailando. Tiene una precision de " + precision * 100);
                //}
                //else
                //{
                //    Debug.Log("No es bueno bailando. Tiene una precision de " + precision * 100);
                //}
                UnityEditor.EditorApplication.isPlaying = false;
            }
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
