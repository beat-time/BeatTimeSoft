using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour
{
    float speed = 4f;
    Vector3 pointA;
    Vector3 pointB;
    bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdatePoints();
        if (isActiveAndEnabled)
        {
            //PingPong between 0 and 1
            float time = Mathf.PingPong(Time.time * speed, 1);
            transform.localPosition = Vector3.Lerp(pointA, pointB, time);
            isActive = true;
        }

    }

    void UpdatePoints()
    {
        if (!isActive)
        {
            pointA = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
            pointB = new Vector3(transform.localPosition.x + 10, transform.localPosition.y, 0);
        }

    }
}
