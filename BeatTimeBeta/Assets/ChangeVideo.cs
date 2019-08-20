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
    public bool slide = false;
    public RawImage imageLeft;
    public RawImage imageCenter;
    public RawImage imageRight;
    VideoPlayer video_player_center;
    AudioSource audio_source_center;

    VideoPlayer video_player_left;
    VideoPlayer video_player_right;

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

    public RenderTexture rt1;
    public RenderTexture rt2;
    public RenderTexture rt3;

    float widthLeft;
    float heightLeft;
    float widthRight;
    float heightRight;
    float widthCenter;
    float heightCenter;
    float posXLeft;
    float posYLeft;
    float posXRight;
    float posYRight;
    float posXCenter;
    float posYCenter;

    public float crHeight;
    public float crWidth;
    public float clHeight;
    public float clWidth;

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
    // Start is called before the first frame update
    void Start()
    {
        rt1.Release();

        video_player_left = imageLeft.GetComponent<VideoPlayer>();
        video_player_right = imageRight.GetComponent<VideoPlayer>();
        video_player_center = imageCenter.GetComponent<VideoPlayer>();
        audio_source_center = imageCenter.GetComponent<AudioSource>();


        video_player_center.clip = v1.clip;
        video_player_right.clip = v2.clip;
        video_player_right.Play();
        video_player_right.Pause();
        video_player_center.Play();
        audio_source_center = m1;
        audio_source_center.Play();

        widthLeft = imageLeft.rectTransform.rect.width;
        heightLeft = imageLeft.rectTransform.rect.height;
        widthRight = imageRight.rectTransform.rect.width;
        heightRight = imageRight.rectTransform.rect.height;
        widthCenter = imageCenter.rectTransform.rect.width;
        heightCenter = imageCenter.rectTransform.rect.height;
        posXLeft = imageLeft.transform.position.x;
        posYLeft = imageLeft.transform.position.y;
        posXRight = imageRight.transform.position.x;
        posYRight = imageRight.transform.position.y;
        posXCenter = imageCenter.transform.position.x;
        posYCenter = imageCenter.transform.position.y;

        crHeight = CursorRight.GetComponent<RectTransform>().rect.height;
        crWidth = CursorRight.GetComponent<RectTransform>().rect.width;
        clHeight = CursorLeft.GetComponent<RectTransform>().rect.height;
        clWidth = CursorLeft.GetComponent<RectTransform>().rect.width;

        btnWidth = BtnStart.GetComponent<RectTransform>().rect.width;
        btnHeight = BtnStart.GetComponent<RectTransform>().rect.width;
        btnPosX = BtnStart.transform.position.x;
        btnPosY = BtnStart.transform.position.y;
    }

    // Update is called once per frame
    void Update()
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
                    Change_Before();
                }
                LbInicio = false;
            }
        }
        Click_Start();
        Update_All();

    }

    public void Update_All()
    {
        widthLeft = imageLeft.rectTransform.rect.width;
        heightLeft = imageLeft.rectTransform.rect.height;
        widthRight = imageRight.rectTransform.rect.width;
        heightRight = imageRight.rectTransform.rect.height;
        widthCenter = imageCenter.rectTransform.rect.width;
        heightCenter = imageCenter.rectTransform.rect.height;
        posXLeft = imageLeft.transform.position.x;
        posYLeft = imageLeft.transform.position.y;
        posXRight = imageRight.transform.position.x;
        posYRight = imageRight.transform.position.y;
        posXCenter = imageCenter.transform.position.x;
        posYCenter = imageCenter.transform.position.y;

        btnPosX = BtnStart.transform.position.x;
        btnPosY = BtnStart.transform.position.y;
    }
    public void Change_Next()
    {
        if (contador < maxContador)
        {
            video_player_center.Stop();
            audio_source_center.Stop();
            contador++;
            Change();
            video_player_center.Play();
            audio_source_center.Play();
        }
    }

    public void Change_Before()
    {
        if (contador > 1)
        {
            video_player_center.Stop();
            audio_source_center.Stop();
            contador--;
            Change();
            video_player_center.Play();
            audio_source_center.Play();
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
            rt1.Release();
            video_player_left.clip = null;
            video_player_center.clip = v1.clip;
            video_player_right.clip = v2.clip;
            video_player_right.Play();
            video_player_right.Pause();
            audio_source_center = m1;

        }
        else if (contador == 2)
        {
            video_player_left.clip = v1.clip;
            video_player_left.Play();
            video_player_left.Pause();
            video_player_center.clip = v2.clip;
            video_player_right.clip = v3.clip;
            video_player_right.Play();
            video_player_right.Pause();
            audio_source_center = m2;
        }
        else if (contador == 3)
        {
            video_player_left.clip = v2.clip;
            video_player_left.Play();
            video_player_left.Pause();
            video_player_center.clip = v3.clip;
            video_player_right.clip = v4.clip;
            video_player_right.Play();
            video_player_right.Pause();
            audio_source_center = m3;
        }
        else if (contador == 4)
        {
            video_player_left.clip = v3.clip;
            video_player_left.Play();
            video_player_left.Pause();
            video_player_center.clip = v4.clip;
            video_player_right.clip = v5.clip;
            video_player_right.Play();
            video_player_right.Pause();
            audio_source_center = m4;
        }
        else if (contador == 5)
        {
            video_player_left.clip = v4.clip;
            video_player_left.Play();
            video_player_left.Pause();
            video_player_center.clip = v5.clip;
            video_player_right.clip = null;
            audio_source_center = m5;

            rt3.Release();
        }
    }
}

