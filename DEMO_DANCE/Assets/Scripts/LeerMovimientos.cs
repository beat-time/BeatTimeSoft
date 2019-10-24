using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LeerMovimientos : MonoBehaviour
{
    Dictionary<int, List<Vector3>> positions = new Dictionary<int, List<Vector3>>();
    string[] data = null;
    string path = "";
    public GameObject cubo;
    int n = 1;
    int cont = 0;
    // Start is called before the first frame update
    void Start()
    {
        positions = LoadData();
        n = positions.Count;
    }

    // Update is called once per frame
    void Update()
    {
        for(int a=1; a <= n; a++)
        {
            for(int b = 0; b < 25; b++)
            {
                Vector3 v = positions[a][b];
                cubo.transform.position = v;
            }
        }
    }

    public Dictionary<int, List<Vector3>> LoadData()
    {
        path = Application.dataPath + "\\data.txt";
        if (File.Exists(path))
        {
            data = File.ReadAllLines(path);
        }
        int numberOfPositions = int.Parse(data[0]);
        int numberOfJoints = 0;
        int a = 1;
        int cont = 1;
        List<Vector3> listOfJoints = new List<Vector3>();
        float x = 0;
        float y = 0;
        float z = 0;
        while (a < data.Length)
        {
            numberOfJoints = int.Parse(data[a]);
            a++;
            while (numberOfJoints > 0)
            {
                string[] positionJoint = data[a].Split(char.Parse("\t"));
                x = float.Parse(positionJoint[0]);
                y = float.Parse(positionJoint[1]);
                z = float.Parse(positionJoint[2]);
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
}
