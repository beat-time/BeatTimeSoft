using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveUserController : MonoBehaviour
{
    public BodySourceView bodySourceView;
    public float time;
    float totalTime = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        time = totalTime;
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
        }
        else
        {
            time = totalTime;
        }
    }
}
