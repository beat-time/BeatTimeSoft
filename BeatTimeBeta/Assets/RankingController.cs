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
    // Start is called before the first frame update
    void Start()
    {
        ClearScreen();
        LoadRanking();
    }

    // Update is called once per frame
    void Update()
    {
        
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
                for(int a = 1; a <= numberOfPlayers; a++)
                {
                    int score = int.Parse(data[a]);
                    if (a == 1)
                    {
                        r1.gameObject.SetActive(true);
                        r1.GetComponentsInChildren<RawImage>()[2].texture = LoadPhoto("r1.png");
                        r1.GetComponentInChildren<Text>().text = score.ToString() + " PTS.";
                    }
                    else if (a == 2)
                    {
                        r2.gameObject.SetActive(true);
                        r2.GetComponentsInChildren<RawImage>()[2].texture = LoadPhoto("r2.png");
                        r2.GetComponentInChildren<Text>().text = score.ToString() + " PTS.";
                    }
                    else if (a == 3)
                    {
                        r3.gameObject.SetActive(true);
                        r3.GetComponentsInChildren<RawImage>()[2].texture = LoadPhoto("r3.png");
                        r3.GetComponentInChildren<Text>().text = score.ToString() + " PTS.";
                    }
                    else if (a == 4)
                    {
                        r4.gameObject.SetActive(true);
                        r4.GetComponentsInChildren<RawImage>()[2].texture = LoadPhoto("r4.png");
                        r4.GetComponentInChildren<Text>().text = score.ToString() + " PTS.";
                    }
                    else
                    {
                        r5.gameObject.SetActive(true);
                        r5.GetComponentsInChildren<RawImage>()[2].texture = LoadPhoto("r5.png");
                        r5.GetComponentInChildren<Text>().text = score.ToString() + " PTS.";
                    }
                }
                LoadArrowPlayer(int.Parse(data[numberOfPlayers+1]));
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
        if (playerPosicion == 0)
        {
            arrowPlayer.gameObject.SetActive(false);
        }
        else if(playerPosicion == 1)
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
        arrowPlayer.gameObject.SetActive(true);
        arrowPlayer.transform.SetParent(rawImage.transform);
        arrowPlayer.rectTransform.localPosition = new Vector3(-235, 0, 0);
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
