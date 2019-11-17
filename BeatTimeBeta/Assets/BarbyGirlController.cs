using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbyGirlController : MonoBehaviour
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
                animator.speed = 0.99f;
            }
            if (seconds < -46)
            {
                animator.speed = 0.67f;
            }

            if (seconds < -92)
            {
                animator.speed = 0.51f;
            }

            if (seconds < -145)
            {
                animator.speed = 0.44f;
            }
        }
        seconds -= Time.deltaTime;
    }
}
