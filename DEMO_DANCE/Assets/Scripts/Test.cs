
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect;
using System.IO;

public class Test : MonoBehaviour
{
    //private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
    //{
    //    { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
    //    { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
    //    { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
    //    { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase }
    //};
    GameObject bodyObject = null;
    private Dictionary<int, Transform> bodyParts = new Dictionary<int, Transform>();
    private List<Vector3> parts = new List<Vector3>();
    float totalTime = 3;
    int contador = 0;
    int cont = 0;
    void Start()
    {
        //string[] filas = File.ReadAllLines(@"D:\ejemplo.txt");
    }

    // Update is called once per frame
    void Update()
    {
        /*if (bodyObject == null)
        {
            bodyObject = GameObject.FindWithTag("body");
            //if (bodyObject != null)
            //{
            //    GetChildren(bodyObject);
            //}
        }
        else
        {
            totalTime -= Time.deltaTime;
            if (totalTime <= 0)
            {
                GetChildren(bodyObject);
                contador++;
                totalTime = 3;
            }
        }
        if (contador == 3)
        {
            int h = 0;
        }*/
    }

    void GetChildren(GameObject obj)
    {
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            Transform jointObj = bodyObject.transform.Find(jt.ToString());
            bodyParts.Add(cont, jointObj);
            cont++;
            //Debug.Log(jointObj);
            //Debug.Log(jointObj.position);
        }
    }
    GameObject GetChildWithName2(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }
    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }
}
