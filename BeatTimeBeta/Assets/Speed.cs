using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    public float time;
    float totalTime = 2;
    float timeStart = 0;
    float timeStart2 = 0.2f;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        time = totalTime;
        anim.speed = 0.0001f;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {

            timeStart -= Time.deltaTime;
            if (timeStart <= 0)
            {
                if (timeStart2 <= 0)
                {
                    enabled = false;
                    anim.speed = 0.66f;
                }
                else
                {
                    anim.speed = 0.000001f;
                }
                timeStart2 -= Time.deltaTime;
            }
            else
            {
                anim.speed = 0.000001f;
            }

        }


    }
}
