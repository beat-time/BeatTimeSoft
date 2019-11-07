using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    public float time;
    float totalTime = 46;
    float time0 = 2;
    float timeStart = 0;
    float timeStart2 = 2.0f;
    float cont = 0;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        time = totalTime;
        //anim.speed = 0.69f;
        anim.speed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (time0 > 0)
        {
            time0 -= Time.deltaTime;
            anim.speed = 0.0f;
        }
        else
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
                        anim.speed = 0.7f;

                        if (timeStart2 <= -75)
                        {
                            anim.speed = 0.00001f;
                        }
                        else
                        {
                            if (timeStart2 <= -65)
                            {
                                anim.speed = 0.26f;
                            }
                        }
                        
                        
                        cont += Time.deltaTime;
                    }
                    else
                    {
                        anim.speed = 1.0f;
                    }

                    /*else
                    {
                        anim.speed = 1.0f;
                    }*/
                    timeStart2 -= Time.deltaTime;
                }

            }
            else {
                anim.speed = 1.0f;
            }
        }


    }
}
