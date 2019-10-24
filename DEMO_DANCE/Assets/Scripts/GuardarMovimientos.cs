using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Kinect = Windows.Kinect;

public class GuardarMovimientos : MonoBehaviour
{
    private GameObject bodyObject = null;
    private Dictionary<int, List<Vector3>> positions= new Dictionary<int, List<Vector3>>();
    int cont = 1;
    public float time1 = 5;
    public float time2 = 56;
    float time3;
    bool save = false;
    // Start is called before the first frame update
    void Start()
    {
        time3 = time2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!File.Exists(Application.dataPath + "\\data.txt"))
        {
            if (bodyObject == null)
            {
                bodyObject = GameObject.FindWithTag("body");
            }
            else
            {
                time1 -= Time.deltaTime;
                if (time1 <= 0)
                {
                    Debug.Log("Comienza");
                    time2 -= Time.deltaTime;
                    if (time2 >= 0)
                    {
                        //if (time3 - time2 >= 0.1)
                        //{
                          //  time3 = time2;
                            GetChildren(bodyObject);
                        //}
                    }
                    else
                    {
                        save = true;
                    }
                }
            }
            if (save)
            {
                SaveData();
                UnityEditor.EditorApplication.isPlaying = false;
                //Application.Quit();
            }
        }
    }

    void GetChildren(GameObject obj)
    {
        List<Vector3> bodyParts = new List<Vector3>();
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            Transform jointObj = bodyObject.transform.Find(jt.ToString());
            bodyParts.Add(jointObj.localPosition);
        }
        positions.Add(cont, bodyParts);
        cont++;
    }

    void SaveData()
    {
        List<Vector3> temp = new List<Vector3>();
        float x = 0;
        float y = 0;
        float z = 0;

        float xAux = 0;
        float yAux = 0;
        float zAux = 0;

        List<string> chain = new List<string>();
        chain.Add(positions.Count.ToString());

        for (int a = 1; a <= positions.Count; a++)
        {
            temp = positions[a];
            chain.Add(temp.Count.ToString());
            for (int b = 0; b < temp.Count; b++)
            {
                x = temp[b].x;
                y = temp[b].y;
                z = temp[b].z;
                /*if (b == 0)
                {
                    xAux = x;
                    yAux = y;
                    zAux = z;

                    x = 0;
                    y = 0;
                    z = 0;
                }
                else
                {
                    Vector3 v = new Vector3(x, y, z);
                    v = TransformJoint(v, new Vector3(xAux, yAux, zAux));

                    x = v.x;
                    y = v.y;
                    z = v.z;
                }*/
                chain.Add(x.ToString() + "\t" + y.ToString() + "\t" + z.ToString());
            }
        }

        File.WriteAllLines(Application.dataPath + "\\data.txt", chain);
    }

    Vector3 TransformJoint( Vector3 joint, Vector3 jointBase)
    {
        return new Vector3(joint.x - jointBase.x, joint.y - jointBase.y, joint.z - jointBase.z);
    }
}
