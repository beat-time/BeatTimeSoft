using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlingPoses : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Blink", 0.1f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Blink()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
