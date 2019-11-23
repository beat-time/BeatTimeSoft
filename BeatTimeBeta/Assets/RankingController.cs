using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RankingController : MonoBehaviour
{
    public RawImage r1;
    public RawImage r2;
    public RawImage r3;
    public RawImage r4;
    public RawImage r5;

    public RawImage arrowPlayer;

    bool r1Active = false;
    bool r2Active = false;
    bool r3Active = false;
    bool r4Active = false;
    bool r5Active = false;
    bool arrowActive = false;

    float speed = 4f;
    Vector3 startPosition;
    Vector3 target;

    float timeToReachTarget = 0.6f;
    float t = 0;
    float maxX = 560;
    // Start is called before the first frame update
    void Start()
    {
        ClearScreen();
        LoadRanking();
    }

    bool DefinePositions(RawImage ri)
    {
        if (t == 0)
        {
            startPosition = new Vector3(ri.transform.localPosition.x + maxX, ri.transform.localPosition.y, ri.transform.localPosition.z);
            target = ri.transform.localPosition;
            ri.gameObject.SetActive(true);
        }
        t += Time.deltaTime / timeToReachTarget;
        ri.transform.localPosition = Vector3.Lerp(startPosition, target, t);
        if (t >= 1)
        {
            t = 0;
            return false;
        }
        return true;
    }
    // Update is called once per frame
    void Update()
    {
        if (r5Active)
        {
            r5Active = DefinePositions(r5);
        }
        else if (r4Active)
        {
            r4Active = DefinePositions(r4);
        }
        else if (r3Active)
        {
            r3Active = DefinePositions(r3);
        }
        else if (r2Active)
        {
            r2Active = DefinePositions(r2);
        }
        else if (r1Active)
        {
            r1Active = DefinePositions(r1);
        }
        else if (arrowActive)
        {
            t += Time.deltaTime;
            if (t >= 0.3)
            {
                arrowPlayer.gameObject.SetActive(true);
                arrowActive = false;
            }
        }
    }

    void LoadRanking()
    {
        string path = Application.dataPath + "//Resources//Ranking//";

        if (File.Exists(path + "ranking.txt"))
        {
            string[] data = File.ReadAllLines(path + "ranking.txt");
            int numberOfPlayers = int.Parse(data[0]);

            if (numberOfPlayers > 0)
            {
                for (int a = 1; a <= numberOfPlayers; a++)
                {
                    int score = int.Parse(data[a]);
                    if (a == 1)
                    {
                        r1Active = true;
                        r1.GetComponentsInChildren<RawImage>()[2].texture = LoadPhoto("r1.png");
                        r1.GetComponentInChildren<Text>().text = score.ToString() + " PTS.";
                    }
                    else if (a == 2)
                    {
                        r2Active = true;
                        r2.GetComponentsInChildren<RawImage>()[2].texture = LoadPhoto("r2.png");
                        r2.GetComponentInChildren<Text>().text = score.ToString() + " PTS.";
                    }
                    else if (a == 3)
                    {
                        r3Active = true;
                        r3.GetComponentsInChildren<RawImage>()[2].texture = LoadPhoto("r3.png");
                        r3.GetComponentInChildren<Text>().text = score.ToString() + " PTS.";
                    }
                    else if (a == 4)
                    {
                        r4Active = true;
                        r4.GetComponentsInChildren<RawImage>()[2].texture = LoadPhoto("r4.png");
                        r4.GetComponentInChildren<Text>().text = score.ToString() + " PTS.";
                    }
                    else
                    {
                        r5Active = true;
                        r5.GetComponentsInChildren<RawImage>()[2].texture = LoadPhoto("r5.png");
                        r5.GetComponentInChildren<Text>().text = score.ToString() + " PTS.";
                    }
                }
                LoadArrowPlayer(int.Parse(data[numberOfPlayers + 1]));
            }
        }
    }

    void ClearScreen()
    {
        r1.gameObject.SetActive(false);
        r2.gameObject.SetActive(false);
        r3.gameObject.SetActive(false);
        r4.gameObject.SetActive(false);
        r5.gameObject.SetActive(false);

        arrowPlayer.gameObject.SetActive(false);
    }

    void LoadArrowPlayer(int playerPosicion)
    {
        playerPosicion = 3;
        if (playerPosicion == 1)
        {
            SetArrowPlayer(r1);
        }
        else if (playerPosicion == 2)
        {
            SetArrowPlayer(r2);
        }
        else if (playerPosicion == 3)
        {
            SetArrowPlayer(r3);
        }
        else if (playerPosicion == 4)
        {
            SetArrowPlayer(r4);
        }
        else if (playerPosicion == 5)
        {
            SetArrowPlayer(r5);
        }
    }
    void SetArrowPlayer(RawImage rawImage)
    {
        arrowActive = true;
        arrowPlayer.transform.SetParent(rawImage.transform);
        arrowPlayer.rectTransform.localPosition = new Vector3(-245, 0, 0);
    }
    Texture2D LoadPhoto(string name)
    {
        Texture2D spriteTexture = null;
        string path = Application.dataPath + "//Resources//Ranking//";
        if (File.Exists(path + name))
        {
            spriteTexture = LoadTexture(path + name);
        }
        else
        {
            spriteTexture = LoadTexture(path + "Images//noImage.png");
        }
        return spriteTexture;
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
