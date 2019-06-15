using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    private GameObject RightHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //is right hand is empty null it finding else it changing hand position
        if(RightHand == null)
        {
            RightHand = GameObject.Find("HandRight");
        }
        else
        {
            gameObject.transform.position = new Vector3(-RightHand.transform.position.x, RightHand.transform.position.y, transform.position.z);
        }
    }
}
