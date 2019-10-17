using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoMessage : MonoBehaviour
{
    public float MaxTime = 0;
    public float Limit = 5;
    public Canvas Message;
    public Text txt_number;
    public GameObject eventSystem;
    // Start is called before the first frame update
    void Start()
    {
        MaxTime = Limit;
        txt_number.text = Limit.ToString("f0");
        eventSystem.GetComponent<PhotoController>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (MaxTime >= 0)
        {
            MaxTime -= Time.deltaTime;
            txt_number.text = MaxTime.ToString("f0");
        }
        else
        {
            Message.enabled = false;
            eventSystem.GetComponent<PhotoController>().enabled = true;
        }
    }
}
