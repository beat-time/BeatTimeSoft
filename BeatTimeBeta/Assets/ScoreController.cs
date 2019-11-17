using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public RawImage Winner1;
    public RawImage Winner2;

    public Image photo1;
    public Text txt_score1;
    public Image photo2;
    public Text txt_score2;

    public RawImage place1;
    public RawImage newRecord;

    int totalPoints1 = 0;
    int totalPoints2 = 0;

    float totalTime = 10;

    Image photoWinner;
    int totalPointsWinner = 0;
    int totalPointsWinner2 = 0;

    int Scene = 1;
    int placePlayer = 0;
    PhotoPlayerController photoPlayerController = new PhotoPlayerController();
    // Start is called before the first frame update
    private void Awake()
    {
        Winner1.gameObject.SetActive(false);
        Winner2.gameObject.SetActive(false);
        place1.gameObject.SetActive(false);
        newRecord.gameObject.SetActive(false);
    }
    void Start()
    {
        CompareMovements compareMovements = new CompareMovements();
        Scene = compareMovements.getScene();
        if (photoPlayerController.getTurnPlayerOne())
        {
            Winner1.transform.localPosition = new Vector3(0, Winner1.transform.localPosition.y, Winner1.transform.localPosition.z);
            Winner1.gameObject.SetActive(true);
            photo1.sprite = LoadPhoto("face1.png");
            totalPoints1 = compareMovements.GetTotalPoints();
            photoWinner = photo1;
            totalPointsWinner = totalPoints1;
            totalPointsWinner2 = totalPoints2;
        }
        else
        {
            Winner1.gameObject.SetActive(true);
            Winner2.gameObject.SetActive(true);
            place1.gameObject.SetActive(true);
            totalPoints1 = compareMovements.GetTotalPoints();
            totalPoints2 = compareMovements.GetTotalPointsPlayer2();
            if (totalPoints1 >= totalPoints2)
            {
                photo1.sprite = LoadPhoto("face1.png");
                photo2.sprite = LoadPhoto("face2.png");
                txt_score1.text = totalPoints1.ToString();
                txt_score2.text = totalPoints2.ToString();
                photoWinner = photo1;
                totalPointsWinner = totalPoints1;
                totalPointsWinner2 = totalPoints2;
            }
            else
            {
                photo1.sprite = LoadPhoto("face2.png");
                photo2.sprite = LoadPhoto("face1.png");
                txt_score1.text = totalPoints2.ToString();
                txt_score2.text = totalPoints1.ToString();
                photoWinner = photo1;
                totalPointsWinner = totalPoints2;
                totalPointsWinner2 = totalPoints1;
            }
        }
        SaveRanking();

        if(placePlayer == 1)
        {
            newRecord.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        txt_score1.text = totalPointsWinner.ToString();
        txt_score2.text = totalPointsWinner2.ToString();
        totalTime -= Time.deltaTime;
        if (totalTime < 0)
        {
            Initiate.Fade("Ranking", Color.black, 2.0f);
        }
    }

    public int getScene()
    {
        return Scene;
    }

    Sprite LoadPhoto(string name)
    {
        Texture2D spriteTexture = null;
        string path = Application.dataPath + "//Resources//";
        if (File.Exists(path + name)){
            spriteTexture = LoadTexture(path + name);
        }
        else
        {
            spriteTexture = LoadTexture(path + "Images//noImage.png");
        }
        Sprite newSprite = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), Vector2.zero);
        return newSprite;
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

        bool esTop = false;

        if (File.Exists(path + "ranking.txt"))
        {
            List<string> data = File.ReadAllLines(path + "ranking.txt").ToList();
            int numberOfPlayers = int.Parse(data[0]);
            if (numberOfPlayers > 0)
            {
                int points = 0;
                int menor = totalPointsWinner;
                string auxPath = path + "temporal.png";
                byte[] bytes = photoWinner.sprite.texture.EncodeToPNG();
                File.WriteAllBytes(auxPath, bytes);

                for (int a = 1; a <= numberOfPlayers; a++)
                {
                    points = int.Parse(data[a]);
                    if (menor > points)
                    {
                        if (placePlayer == 0)
                        {
                            placePlayer = a;
                        }

                        data[a] = menor.ToString();
                        menor = points; 

                        File.Move(path + "r" + a + ".png", path + "rank.png");
                        File.Move(auxPath, path + "r" + a + ".png");
                        File.Move(path + "rank.png", auxPath);
                        esTop = true;

                    }
                }
                if (numberOfPlayers < 5)
                {
                    numberOfPlayers++;
                    data[0] = (numberOfPlayers).ToString();
                    data[numberOfPlayers] = menor.ToString();

                    File.Move(auxPath, path + "r" + numberOfPlayers + ".png");
                }
                
                
                data[numberOfPlayers+1] = placePlayer.ToString();
                
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
        placePlayer = 1;
        string[] d = { "1", totalPointsWinner.ToString(), placePlayer.ToString()};
        File.WriteAllLines(Application.dataPath + "//Resources//Ranking//ranking.txt", d);

        byte[] bytes = photoWinner.sprite.texture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "//Resources//Ranking//r1.png", bytes);
    }
}
