using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControllers : MonoBehaviour
{
    private GameObject RighHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (RighHand == null)
        {
            RighHand = GameObject.Find("HandRight");
        }
        else
        {
            gameObject.transform.position = new Vector3(-RighHand.transform.position.x, RighHand.transform.position.y, transform.position.z);
        }
    }
}
