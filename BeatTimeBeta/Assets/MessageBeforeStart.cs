using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBeforeStart : MonoBehaviour
{
    public float time;
    float totalTime = 3;
    float timeStart = 1;
    float timeStart2 = 0.2f;
    public Canvas canvas;
    public Text txtContador;
    public Canvas canvasFranky;
    public AudioSource audio_mix;
    // Start is called before the first frame update
    void Start()
    {
        time = totalTime;
        canvas.enabled = true;
        txtContador.text = totalTime.ToString("f0");
        canvasFranky.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            timeStart -= Time.deltaTime;
            txtContador.text = "Start!";
            if (timeStart <= 0)
            {
                canvas.gameObject.SetActive(false);
                if (timeStart2 <= 0)
                {
                    canvasFranky.gameObject.SetActive(true);
                    audio_mix.Play();
                    GetComponent<PosesController>().enabled = true;
                    enabled = false;
                }
                timeStart2 -= Time.deltaTime;
            }
        }
        else
        {
            txtContador.text = Math.Ceiling(time).ToString("f0");
        }
    }
}
