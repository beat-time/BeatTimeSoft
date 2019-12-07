using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyController : MonoBehaviour
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
                animator.speed = 1.02f;
            }
            if (seconds < -37)
            {
                animator.speed = 0.85f;
            }
            if (seconds < -44)
            {
                animator.speed = 0.71f;
            }
            if (seconds < -54)
            {
                animator.speed = 0.65f;
            }

            if (seconds < -85)
            {
                animator.speed = 0.52f;
            }

            if (seconds < -140)
            {
                animator.speed = 0.41f;
            }
            if(seconds < -187)
            {
                animator.speed = 0.38f;
            }
        }
        seconds -= Time.deltaTime;
    }
}
