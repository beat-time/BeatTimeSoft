using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateBody : MonoBehaviour
{
    public GameObject Body;
    // Start is called before the first frame update
    void Start()
    {
        Body = GameController.kinect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
