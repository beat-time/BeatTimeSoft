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

    float crHeight;
    float crWidth;
    float clHeight;
    float clWidth;

    Vector3 Rinicio;
    Vector3 Rfin;
    bool RbInicio = false;
    bool RbFin = false;

    Vector3 Linicio;
    Vector3 Lfin;
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

    public float MinWidthChange = 40;
    public float MaxHeightChange = 100;

    float anchorPosition = 0;
    bool positionInicial = false;
    // Start is called before the first frame update
    void Start()
    {
        video_player_center = imageCenter.GetComponent<VideoPlayer>();
        audio_source_center = imageCenter.GetComponent<AudioSource>();


        //video_player_center.clip = v1.clip;
        video_player_center.clip = Resources.Load<VideoClip>("VideoMusica\\1");    
        video_player_center.Play();
        //audio_source_center = m1;
        audio_source_center.clip = Resources.Load<AudioClip>("VideoMusica\\1");
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
            float u = Mathf.MoveTowards(rectTransform.rect.width, 0, 100);
            rectTransform.sizeDelta = new Vector2(Mathf.Clamp(u, 0.0f, ImageCenterWidth), ImageCenterHeight);
            if (rectTransform.rect.width == 0)
            {
                newVideo = true;
                change = false;

                Change();
                renderTexture.Release();
                video_player_center.Play();
                video_player_center.Pause();

                if (rectTransform.pivot.x == 1)
                {
                    rectTransform.pivot = new Vector2(0, 0.5f);
                    rectTransform.anchoredPosition = new Vector3(-ImageCenterWidth / 2 + anchorPosition, rectTransform.anchoredPosition.y, 0);
                }
                else
                {
                    rectTransform.pivot = new Vector2(1, 0.5f);
                    rectTransform.anchoredPosition = new Vector3(ImageCenterWidth / 2 + anchorPosition, rectTransform.anchoredPosition.y, 0);
                }
            }
        }
        if (newVideo)
        {
            float u = Mathf.MoveTowards(rectTransform.rect.width, ImageCenterWidth, 100);
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
                    Rinicio = CursorRight.transform.position;
                    RbInicio = true;
                }
            }
            else if (!CursorRight.HandGreen)
            {
                if (RbInicio)
                {
                    Rfin = CursorRight.transform.position;
                    if (Mathf.Abs(Rinicio.y - Rfin.y) <= MaxHeightChange)
                    {
                        if (Rinicio.x - Rfin.x > MinWidthChange)
                        {
                            LbInicio = false;
                            Change_Next();
                        }
                        else if (Rfin.x - Rinicio.x > MinWidthChange)
                        {
                            LbInicio = false;
                            Change_Before();
                        }
                    }
                    RbInicio = false;
                }
            }
            if (CursorLeft.HandGreen)
            {
                if (!LbInicio)
                {
                    Linicio = CursorLeft.transform.position;
                    LbInicio = true;
                }
            }
            else if (!CursorLeft.HandGreen)
            {
                if (LbInicio)
                {
                    Lfin = CursorLeft.transform.position;
                    if (Mathf.Abs(Linicio.y - Lfin.y) <= MaxHeightChange)
                    {
                        if (Lfin.x - Linicio.x > MinWidthChange)
                        {
                            RbInicio = false;
                            Change_Before();
                        }
                        else if (Linicio.x - Lfin.x > MinWidthChange)
                        {
                            RbInicio = false;
                            Change_Next();
                        }
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
        video_player_center.Stop();
        audio_source_center.Stop();
        change = true;

        if (!positionInicial)
        {
            anchorPosition = rectTransform.anchoredPosition.x;
            positionInicial = true;
        }
        rectTransform.pivot = new Vector2(0, 0.5f);
        rectTransform.anchoredPosition = new Vector3(-ImageCenterWidth / 2 + anchorPosition, rectTransform.anchoredPosition.y, 0);
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
        video_player_center.Stop();
        audio_source_center.Stop();
        change = true;
        if (!positionInicial)
        {
            anchorPosition = rectTransform.anchoredPosition.x;
            positionInicial = true;
        }
        rectTransform.pivot = new Vector2(1, 0.5f);
        rectTransform.anchoredPosition = new Vector3(ImageCenterWidth / 2 + anchorPosition, rectTransform.anchoredPosition.y, 0);

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
            video_player_center.clip = Resources.Load<VideoClip>("VideoMusica\\1");
            audio_source_center.clip = Resources.Load<AudioClip>("VideoMusica\\1");

        }
        else if (contador == 2)
        {
            video_player_center.clip = Resources.Load<VideoClip>("VideoMusica\\2");
            audio_source_center.clip = Resources.Load<AudioClip>("VideoMusica\\2");
        }
        else if (contador == 3)
        {
            video_player_center.clip = Resources.Load<VideoClip>("VideoMusica\\3");
            audio_source_center.clip = Resources.Load<AudioClip>("VideoMusica\\3");
        }
        else if (contador == 4)
        {
            video_player_center.clip = Resources.Load<VideoClip>("VideoMusica\\4");
            audio_source_center.clip = Resources.Load<AudioClip>("VideoMusica\\4");
        }
        else if (contador == 5)
        {
            video_player_center.clip = Resources.Load<VideoClip>("VideoMusica\\5");
            audio_source_center.clip = Resources.Load<AudioClip>("VideoMusica\\5");
        }
    }
}

