using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
public class ChangeVideo : MonoBehaviour
{
    public KinectUICursor CursorRight;
    public KinectUICursor CursorLeft;
    public Button BtnStart;
    public int contador = 1;
    int maxContador = 5;
  

    public RawImage imageCenter;
    
    VideoPlayer video_player_center;
    AudioSource audio_source_center;


    //Videos/Music
    public VideoPlayer v1;
    public VideoPlayer v2;
    public VideoPlayer v3;
    public VideoPlayer v4;
    public VideoPlayer v5;

    public AudioSource m1;
    public AudioSource m2;
    public AudioSource m3;
    public AudioSource m4;
    public AudioSource m5;


    float crHeight;
    float crWidth;
    float clHeight;
    float clWidth;

    float Rinicio = 0.0f;
    float Rfin = 0.0f;
    bool RbInicio = false;
    bool RbFin = false;

    float Linicio = 0.0f;
    float Lfin = 0.0f;
    bool LbInicio = false;
    bool LbFin = false;

    float btnWidth = 0.0f;
    float btnHeight = 0.0f;

    float btnPosX = 0.0f;
    float btnPosY = 0.0f;

    float timeTutorial = 10;
    public RawImage HandRightTutorial;
    public RawImage HandLeftTutorial;

    public RectTransform rectTransform;
    float ImageCenterWidth;
    float ImageCenterHeight;
    bool change = false;
    bool newVideo = false;

    public RenderTexture renderTexture;
    // Start is called before the first frame update
    void Start()
    {
        video_player_center = imageCenter.GetComponent<VideoPlayer>();
        audio_source_center = imageCenter.GetComponent<AudioSource>();


        video_player_center.clip = v1.clip;
        video_player_center.Play();
        audio_source_center = m1;
        audio_source_center.Play();

        crHeight = CursorRight.GetComponent<RectTransform>().rect.height;
        crWidth = CursorRight.GetComponent<RectTransform>().rect.width;
        clHeight = CursorLeft.GetComponent<RectTransform>().rect.height;
        clWidth = CursorLeft.GetComponent<RectTransform>().rect.width;

        btnWidth = BtnStart.GetComponent<RectTransform>().rect.width;
        btnHeight = BtnStart.GetComponent<RectTransform>().rect.width;
        btnPosX = BtnStart.transform.position.x;
        btnPosY = BtnStart.transform.position.y;

        rectTransform = imageCenter.GetComponent<RectTransform>();
        ImageCenterWidth = rectTransform.rect.width;
        ImageCenterHeight = rectTransform.rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        Click_Start();
        Update_All();

        CancelTutorial();

        if (change)
        {
            float u = Mathf.MoveTowards(rectTransform.rect.width, 0, 150.0f);
            rectTransform.sizeDelta = new Vector2(Mathf.Clamp(u, 0.0f, ImageCenterWidth), ImageCenterHeight);
            if (rectTransform.rect.width == 0)
            {
                newVideo = true;
                change = false;

                Change();
               
                video_player_center.Play();
                video_player_center.Pause();

                if (rectTransform.pivot.x == 1)
                {
                    rectTransform.pivot = new Vector2(0, 0.5f);
                    rectTransform.anchoredPosition = new Vector3(-ImageCenterWidth / 2, 40, 0);
                }
                else
                {
                    rectTransform.pivot = new Vector2(1, 0.5f);
                    rectTransform.anchoredPosition = new Vector3(ImageCenterWidth / 2, 40, 0);
                }
            }
        }
        if (newVideo)
        {
            float u = Mathf.MoveTowards(rectTransform.rect.width, ImageCenterWidth, 150.0f);
            rectTransform.sizeDelta = new Vector2(Mathf.Clamp(u, 0.0f, ImageCenterWidth), ImageCenterHeight);
            if (rectTransform.rect.width == ImageCenterWidth)
            {
                newVideo = false;
                video_player_center.Play();
                audio_source_center.Play();
            }
        }
        else
        {
            if (CursorRight.HandGreen)
            {
                if (!RbInicio)
                {
                    Rinicio = CursorRight.transform.position.x;
                    RbInicio = true;
                }
            }
            else if (!CursorRight.HandGreen)
            {
                if (RbInicio)
                {
                    Rfin = CursorRight.transform.position.x;
                    if (Rinicio - Rfin > 100)
                    {
                        video_player_center.Stop();
                        audio_source_center.Stop();
                        change = true;
                        rectTransform.pivot = new Vector2(0, 0.5f);
                        rectTransform.anchoredPosition = new Vector3(-ImageCenterWidth / 2, 40, 0);
                        Change_Next();
                    }
                    RbInicio = false;
                }
            }
            if (CursorLeft.HandGreen)
            {
                if (!LbInicio)
                {
                    Linicio = CursorLeft.transform.position.x;
                    LbInicio = true;
                }
            }
            else if (!CursorLeft.HandGreen)
            {
                if (LbInicio)
                {
                    Lfin = CursorLeft.transform.position.x;
                    if (Lfin - Linicio > 100)
                    {
                        video_player_center.Stop();
                        audio_source_center.Stop();
                        change = true;
                        rectTransform.pivot = new Vector2(1, 0.5f);
                        rectTransform.anchoredPosition = new Vector3(ImageCenterWidth / 2, 40, 0);
                        Change_Before();
                    }
                    LbInicio = false;
                }
            }
        }
    }

    void CancelTutorial()
    {
        if (timeTutorial <= 0)
        {
            HandRightTutorial.enabled = false;
            HandLeftTutorial.enabled = false;
        }
        else
        {
            timeTutorial -= Time.deltaTime;
        }

    }

    public void Update_All()
    {
        btnPosX = BtnStart.transform.position.x;
        btnPosY = BtnStart.transform.position.y;
    }
    public void Change_Next()
    {
        if (contador < maxContador)
        {
            contador++;
        }
        else if (contador == maxContador)
        {
            contador = 1;
        }
    }

    public void Change_Before()
    {
        if (contador > 1)
        {
            contador--;
        }
        else if (contador == 1)
        {
            contador = maxContador;
        }
    }
    public void Click_Start()
    {
        if (CursorRight.HandGreen || CursorLeft.HandGreen)
        {
            if (CursorRight.transform.position.x - crWidth / 2 >= btnPosX - btnWidth / 2 &&
                CursorRight.transform.position.x + crWidth / 2 <= btnPosX + btnWidth / 2 &&
                CursorRight.transform.position.y - crHeight / 2 >= btnPosY - btnHeight / 2 &&
                CursorRight.transform.position.y + crHeight / 2 <= btnPosY + btnHeight / 2)
            {
                SceneManager.LoadScene("Playing");
            }
            else if (CursorLeft.transform.position.x - clWidth / 2 >= btnPosX - btnWidth / 2 &&
                CursorLeft.transform.position.x + clWidth / 2 <= btnPosX + btnWidth / 2 &&
                CursorLeft.transform.position.y - clHeight / 2 >= btnPosY - btnHeight / 2 &&
                CursorLeft.transform.position.y + clHeight / 2 <= btnPosY + btnHeight / 2)
            {
                SceneManager.LoadScene("Playing");
            }
        }
    }
    public void Change()
    {
        if (contador == 1)
        {
            video_player_center.clip = v1.clip;
            audio_source_center = m1;

        }
        else if (contador == 2)
        {
            video_player_center.clip = v2.clip;
            audio_source_center = m2;
        }
        else if (contador == 3)
        {
            video_player_center.clip = v3.clip;
            audio_source_center = m3;
        }
        else if (contador == 4)
        {
            video_player_center.clip = v4.clip;
            audio_source_center = m4;
        }
        else if (contador == 5)
        {
            video_player_center.clip = v5.clip;
            audio_source_center = m5;
        }
    }
}

