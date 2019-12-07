using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmigosConDerechoController : MonoBehaviour
{
    Animator animator;
    public AudioSource audio;
    public float seconds = 3;
    public bool finish = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audio.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (seconds <= 0)
        {
            if (!finish)
            {
                audio.gameObject.SetActive(true);
                audio.Play();
                finish = true;
                animator.speed = 0.94f;
            }
            if (seconds < -47)
            {
                animator.speed = 0.81f;
            }

            if (seconds < -72)
            {
                animator.speed = 0.64f;
            }

            if (seconds < -76)
            {
                animator.speed = 0.67f;
            }

            if (seconds < -91)
            {
                animator.speed = 0.56f;
            }
            if (seconds < -128)
            {
                animator.speed = 0.49f;
            }
            if (seconds < -145)
            {
                animator.speed = 0.48f;
            }
            if (seconds < -165)
            {
                animator.speed = 0.44f;
            }
            if (seconds < -189)
            {
                animator.speed = 0.41f;
            }
        }
        seconds -= Time.deltaTime;
    }
}
