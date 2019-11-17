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
    public int Scene = 3;
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
    static int SceneNow = 0;

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

    float porcentaje = 0.7f;
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
        SceneNow = Scene;
        
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
        /*if (franky.isActiveAndEnabled)
        {
            if (bodyObject1 == null)
            {
                if (bodyObject2 != null)
                {
                    if (bodies._Bodies.Count >= 2)
                    {
                        bodyObject1 = bodies._Bodies.Values.ElementAt(0);
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
                        bodyObject2 = bodies._Bodies.Values.ElementAt(1);
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
        }*/
        SegundaParte();
    }

    Texture2D Calification(int points)
    {
        if (points >= 218 && points <= 250)
        {
            return Exc.texture as Texture2D;
        }
        else if (points >= 198 && points < 218)
        {
            return Buen.texture as Texture2D;
        }
        else if (points >= 180 && points < 198)
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

    public int getScene()
    {
        return SceneNow;
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
    void SegundaParte()
    {
        if (franky.isActiveAndEnabled)
        {
            if (bodyObject1 == null)
            {
                if (bodyObject2 != null)
                {
                    if (bodies._Bodies.Count >= 2)
                    {
                        bodyObject1 = bodies._Bodies.Values.ElementAt(0);
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
                        bodyObject2 = bodies._Bodies.Values.ElementAt(1);
                    }
                }
            }
            if (timeCompare <= 0 && !changeScene)
            {
                int auxValidParts1 = 0;
                int auxValidParts2 = 0;
                if (bodyObject1 != null || bodyObject2 != null)
                {
                    if (bodyObject1 != null)
                    {
                        JointsPlayer1 = GetJoints(bodyObject1);
                    }
                    if (bodyObject2 != null && NPlayer==2)
                    {
                        JointsPlayer2 = GetJoints(bodyObject2);
                    }
                    int inicio = numberOfMovements - movementsPerSecond;
                    int max = numberOfMovements + movementsPerSecond;
                    auxValidParts1 = 0;
                    auxValidParts2 = 0;

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
                        JointsMachine = positions[inicio];
                        int auxVP1 = 0;
                        int auxVP2 = 0;
                        int auxPoints1 = 0;
                        int auxPoints2 = 0;
                        if (bodyObject1 != null)
                        {
                            PlayerAux1 = JointsPlayer1[0];

                            List<int> p1 = CompararTronco(JointsPlayer1, JointsMachine, PlayerAux1);
                            List<int> p2 = CompararBrazoDerecho(JointsPlayer1, JointsMachine, PlayerAux1);
                            List<int> p3 = CompararBrazoIzquierdo(JointsPlayer1, JointsMachine, PlayerAux1);
                            List<int> p4 = CompararPiernaDerecha(JointsPlayer1, JointsMachine, PlayerAux1);
                            List<int> p5 = CompararPiernaIzquierda(JointsPlayer1, JointsMachine, PlayerAux1);

                            auxVP1 = auxVP1 + ((p1[0] == 1) ? 1 : 0);
                            auxVP1 = auxVP1 + ((p2[0] == 1) ? 1 : 0);
                            auxVP1 = auxVP1 + ((p3[0] == 1) ? 1 : 0);
                            auxVP1 = auxVP1 + ((p4[0] == 1) ? 1 : 0);
                            auxVP1 = auxVP1 + ((p5[0] == 1) ? 1 : 0);

                            auxPoints1 = p1[1] + p2[1] + p3[1] + p4[1] + p5[1];
                        }
                        if (bodyObject2 != null)
                        {
                            PlayerAux2 = JointsPlayer2[0];

                            List<int> p11 = CompararTronco(JointsPlayer2, JointsMachine, PlayerAux2);
                            List<int> p21 = CompararBrazoDerecho(JointsPlayer2, JointsMachine, PlayerAux2);
                            List<int> p31 = CompararBrazoIzquierdo(JointsPlayer2, JointsMachine, PlayerAux2);
                            List<int> p41 = CompararPiernaDerecha(JointsPlayer2, JointsMachine, PlayerAux2);
                            List<int> p51 = CompararPiernaIzquierda(JointsPlayer2, JointsMachine, PlayerAux2);

                            auxVP2 = auxVP2 + ((p11[0] == 1) ? 1 : 0);
                            auxVP2 = auxVP2 + ((p21[0] == 1) ? 1 : 0);
                            auxVP2 = auxVP2 + ((p31[0] == 1) ? 1 : 0);
                            auxVP2 = auxVP2 + ((p41[0] == 1) ? 1 : 0);
                            auxVP2 = auxVP2 + ((p51[0] == 1) ? 1 : 0);

                            auxPoints2 = p11[1] + p21[1] + p31[1] + p41[1] + p51[1];
                        }

                        if (auxVP1 > auxValidParts1)
                        {
                            auxValidParts1 = auxVP1;
                            points1 = auxPoints1;
                        }

                        if (auxVP2 > auxValidParts2)
                        {
                            auxValidParts2 = auxVP2;
                            points2 = auxPoints2;
                        }

                        inicio += 2;
                    }
                }
                txt_qualification1.text = totalPoints1.ToString();//Calification(points1);

                Cali.texture = PartsCalification(auxValidParts1);

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

                if (NPlayer == 2)
                {
                    txt_qualification2.text = totalPoints2.ToString();//Calification(points2);
                    Cali2.texture = PartsCalification(auxValidParts2);

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
                    Initiate.Fade("Score", Color.black, 2.0f);
                    enabled = false;
                }
                timeChangeScene -= Time.deltaTime;
            }
        }
    }


    //p = player
    //m = machine
    //aux = auxiliar

    List<int> CompararTronco(List<Vector3> p, List<Vector3> m, Vector3 aux)
    {
        int nParts = 0;
        List<int> list = new List<int>();
        nParts = nParts + CompareDistance(p[0], m[0], aux);
        nParts = nParts + CompareDistance(p[1], m[1], aux);
        nParts = nParts + CompareDistance(p[2], m[2], aux);
        nParts = nParts + CompareDistance(p[3], m[3], aux);
        nParts = nParts + CompareDistance(p[20], m[20], aux);

        int r = (nParts == 0) ? 0 : (nParts / 5 >= porcentaje) ? 1 : 0;
        list.Add(r);
        list.Add(nParts * 10); //Points
        return list;

        //Vector3 SpineBase = JointsMachine[0];
        //Vector3 SpineMiddle = JointsMachine[1];
        //Vector3 Neck = JointsMachine[2];
        //Vector3 Head = JointsMachine[3];
        //Vector3 ShoulderMid = JointsMachine[20];
    }

    List<int> CompararBrazoDerecho(List<Vector3> p, List<Vector3> m, Vector3 aux)
    {
        int nParts = 0;
        List<int> list = new List<int>();
        nParts = nParts + CompareDistance(p[20], m[20], aux);
        nParts = nParts + CompareDistance(p[8], m[8], aux);
        nParts = nParts + CompareDistance(p[9], m[9], aux);
        nParts = nParts + CompareDistance(p[10], m[10], aux);
        nParts = nParts + CompareDistance(p[11], m[11], aux);
        nParts = nParts + CompareDistance(p[24], m[24], aux);
        nParts = nParts + CompareDistance(p[23], m[23], aux);

        int r = (nParts == 0) ? 0 : (nParts / 7 >= porcentaje) ? 1 : 0;
        list.Add(r);
        list.Add(nParts * 10); //Points
        return list;

        //Vector3 RightShoulderMid = JointsMachine[20];
        //Vector3 RightShoulder = JointsMachine[8];
        //Vector3 RightElbow = JointsMachine[9];
        //Vector3 RightWrist = JointsMachine[10];
        //Vector3 RightHand = JointsMachine[11];
        //Vector3 RightThumb = JointsMachine[24];
        //Vector3 RightHandTip = JointsMachine[23];
    }

    List<int> CompararBrazoIzquierdo(List<Vector3> p, List<Vector3> m, Vector3 aux)
    {
        int nParts = 0;
        List<int> list = new List<int>();
        nParts = nParts + CompareDistance(p[20], m[20], aux);
        nParts = nParts + CompareDistance(p[4], m[4], aux);
        nParts = nParts + CompareDistance(p[5], m[5], aux);
        nParts = nParts + CompareDistance(p[6], m[6], aux);
        nParts = nParts + CompareDistance(p[7], m[7], aux);
        nParts = nParts + CompareDistance(p[22], m[22], aux);
        nParts = nParts + CompareDistance(p[21], m[21], aux);

        int r = (nParts == 0) ? 0 : (nParts / 7 >= porcentaje) ? 1 : 0;
        list.Add(r);
        list.Add(nParts * 10); //Points
        return list;

        //Vector3 LeftShoulderMid = JointsMachine[20];
        //Vector3 LeftShoulder = JointsMachine[4];
        //Vector3 LeftElbow = JointsMachine[5];
        //Vector3 LeftWrist = JointsMachine[6];
        //Vector3 LeftHand = JointsMachine[7];
        //Vector3 LeftThumb = JointsMachine[22];
        //Vector3 LeftHandTip = JointsMachine[21];
    }

    List<int> CompararPiernaDerecha(List<Vector3> p, List<Vector3> m, Vector3 aux)
    {
        int nParts = 0;
        List<int> list = new List<int>();
        nParts = nParts + CompareDistance(p[0], m[0], aux);
        nParts = nParts + CompareDistance(p[16], m[16], aux);
        nParts = nParts + CompareDistance(p[17], m[17], aux);
        nParts = nParts + CompareDistance(p[18], m[18], aux);
        nParts = nParts + CompareDistance(p[19], m[19], aux);

        int r = (nParts == 0) ? 0 : (nParts / 5 >= porcentaje) ? 1 : 0;
        list.Add(r);
        list.Add(nParts * 10); //Points
        return list;

        //Vector3 RightSpineBase = JointsMachine[0];
        //Vector3 RightHip = JointsMachine[16];
        //Vector3 RightKnee = JointsMachine[17];
        //Vector3 RightAnkle = JointsMachine[18];
        //Vector3 RightFoot = JointsMachine[19];
    }
    List<int> CompararPiernaIzquierda(List<Vector3> p, List<Vector3> m, Vector3 aux)
    {
        int nParts = 0;
        List<int> list = new List<int>();
        nParts = nParts + CompareDistance(p[0], m[0], aux);
        nParts = nParts + CompareDistance(p[12], m[12], aux);
        nParts = nParts + CompareDistance(p[13], m[13], aux);
        nParts = nParts + CompareDistance(p[14], m[14], aux);
        nParts = nParts + CompareDistance(p[15], m[15], aux);

        int r = (nParts == 0) ? 0 : (nParts / 5 >= porcentaje) ? 1 : 0;
        list.Add(r);
        list.Add(nParts * 10); //Points
        return list;

        //Vector3 LeftSpineBase = JointsMachine[0];
        //Vector3 LeftHip = JointsMachine[12];
        //Vector3 LeftKnee = JointsMachine[13];
        //Vector3 LeftAnkle = JointsMachine[14];
        //Vector3 LeftFoot = JointsMachine[15];
    }

    int CompareDistance(Vector3 vP, Vector3 vM, Vector3 vAux)
    {
        Vector3 vec = TransformJoint(vP, vAux);
        if (Vector3.Distance(vec, vM) <= 3.5f)
        {
            return 1;
        }
        return 0;
    }

    Texture2D PartsCalification(int numberOfParts)
    {
        if (numberOfParts >= 4)
        {
            return Exc.texture as Texture2D;
        }
        else if (numberOfParts == 3)
        {
            return Buen.texture as Texture2D;
        }
        else if (numberOfParts == 2)
        {
            return Regu.texture as Texture2D;
        }
        else
        {
            return Mal.texture as Texture2D;
        }
    }
    
}
