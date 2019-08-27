using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActiveUserController : MonoBehaviour
{
    public BodySourceView bodySourceView;
    public float time;
    float totalTime = 10.0f;
    public Canvas canvasAlert;
    public Text txt_number;
    // Start is called before the first frame update
    void Start()
    {
        time = totalTime;
        canvasAlert.enabled = false;
        txt_number.text = totalTime.ToString("f0");
    }

    // Update is called once per frame
    void Update()
    {
        if (!bodySourceView.Active)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                SceneManager.LoadScene("MainScene");
            }
            else if (time <= totalTime / 2)
            {
                canvasAlert.enabled = true;
            }
            txt_number.text = time.ToString("f0");
        }
        else
        {
            time = totalTime;
            canvasAlert.enabled = false;
        }
    }
}
