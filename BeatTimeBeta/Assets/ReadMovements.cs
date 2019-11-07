using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadMovements : MonoBehaviour
{
    int Scene = 1;
    Dictionary<int, List<Vector3>> positions = new Dictionary<int, List<Vector3>>();
    string[] data = null;
    string path = "";
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setScene(int TempScene)
    {
        Scene = TempScene;
    }

    public Dictionary<int, List<Vector3>> LoadData(int nScene)
    {
        path = Application.dataPath + "//data"+ nScene + ".txt";
        if (File.Exists(path))
        {
            data = File.ReadAllLines(path);
            int numberOfPositions = int.Parse(data[0]);
            int numberOfJoints = 0;
            int a = 1;
            int cont = 1;
            List<Vector3> listOfJoints = new List<Vector3>();
            float x = 0;
            float y = 0;
            float z = 0;

            float xAux = 0;
            float yAux = 0;
            float zAux = 0;
            while (a < data.Length)
            {
                numberOfJoints = int.Parse(data[a]);
                a++;
                while (numberOfJoints > 0)
                {
                    string[] positionJoint = data[a].Split(char.Parse("\t"));
                    x = float.Parse(positionJoint[0].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                    y = float.Parse(positionJoint[1].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                    z = float.Parse(positionJoint[2].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                    if (numberOfJoints == 25)
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
                        Vector3 v = TransformJoint(new Vector3(x, y, z), new Vector3(xAux, yAux, zAux));
                        x = v.x;
                        y = v.y;
                        z = v.z;
                    }
                    
                    numberOfJoints--;
                    a++;
                    listOfJoints.Add(new Vector3(x, y, z));
                }
                positions.Add(cont, listOfJoints);
                listOfJoints = new List<Vector3>();
                cont++;
            }
            return positions;
        }
        return null;
    }

    Vector3 TransformJoint(Vector3 joint, Vector3 jointBase)
    {
        return new Vector3(joint.x - jointBase.x, joint.y - jointBase.y, joint.z - jointBase.z);
    }
}
