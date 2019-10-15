using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Image photo;
    public Text txt_score;

    int totalPoints = 0;

    float totalTime = 5;
    // Start is called before the first frame update
    void Start()
    {
        LoadPhoto();
        LoadScore();
        SaveRanking();
    }

    // Update is called once per frame
    void Update()
    {
        totalTime -= Time.deltaTime;
        if (totalTime < 0)
        {
            Initiate.Fade("Ranking", Color.black, 2.0f);
        }
    }

    void LoadScore()
    {
        CompareMovements compareMovements = new CompareMovements();
        totalPoints = compareMovements.GetTotalPoints();
        txt_score.text = "Puntaje: " + totalPoints.ToString();
    }
    void LoadPhoto()
    {
        Texture2D spriteTexture = null;
        string path = Application.dataPath + "//Resources//";
        if (File.Exists(path + "face1.png")){
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

    void SaveRanking()
    {
        string path = Application.dataPath + "//Resources//Ranking//";

        if (File.Exists(path + "ranking.txt"))
        {
            List<string> data = File.ReadAllLines(path + "ranking.txt").ToList();
            int numberOfPlayers = int.Parse(data[0]);
            if (numberOfPlayers > 0)
            {
                int points = 0;
                int menor = totalPoints;
                string auxPath = path + "temporal.png";
                byte[] bytes = photo.sprite.texture.EncodeToPNG();
                File.WriteAllBytes(auxPath, bytes);

                for (int a = 1; a <= numberOfPlayers; a++)
                {
                    points = int.Parse(data[a]);
                    if (menor > points)
                    {
                        data[a] = menor.ToString();
                        menor = points; ;

                        File.Move(path + "r" + a + ".png", path + "rank.png");
                        File.Move(auxPath, path + "r" + a + ".png");
                        File.Move(path + "rank.png", auxPath);
                    }
                }
                if (numberOfPlayers < 5)
                {
                    numberOfPlayers++;
                    data[0] = (numberOfPlayers).ToString();
                    data[numberOfPlayers] = menor.ToString();

                    File.Move(auxPath, path + "r" + numberOfPlayers + ".png");
                }

                string[] d = data.ToArray();
                File.WriteAllLines(path + "ranking.txt", d);
            }
            else
            {
                SaveFirstPlace();
            }
        }
        else
        {
            SaveFirstPlace();
        }
    }

    void SaveFirstPlace()
    {
        string[] d = { "1", totalPoints.ToString() };
        File.WriteAllLines(Application.dataPath + "Resources//Ranking//ranking.txt", d);

        byte[] bytes = photo.sprite.texture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "Resources//Ranking//r1.png", bytes);
    }
}
