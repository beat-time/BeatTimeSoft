using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShadowPLayer : MonoBehaviour
{
    public BodySourceView bodySourceView;
    Windows.Kinect.Body[] data;
    public int indicadorplayer;
    ulong IdTrack;
    ulong IdTrack2;
    int contador;

    // Start is called before the first frame update
    void Start()
    {
        //data = bodySourceView._BodyManager.GetData();
        contador = 0;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }
            if (indicadorplayer == 1)
            {
                if (body.IsTracked)
                {
                    IdTrack = body.TrackingId;
                    contador++;
                }
            }
            if (indicadorplayer == 2)
            {
                if (body.IsTracked)
                {
                    IdTrack2 = body.TrackingId;
                    contador++;
                }
            }
        }
        contador = 0;

        if (indicadorplayer == 1)
        {
            foreach (var body in data)
            {
                if (body == null)
                {
                    continue;
                }
                if (body.TrackingId == IdTrack)
                {
                }
                else {
                    bodySourceView._Bodies[body.TrackingId].GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }
        else {
            if (indicadorplayer == 2)
            {
                foreach (var body in data)
                {
                    if (body == null)
                    {
                        continue;
                    }
                    if (body.TrackingId == IdTrack2)
                    {
                    }
                    else
                    {
                        bodySourceView._Bodies[body.TrackingId].GetComponent<MeshRenderer>().enabled = false;
                    }
                }
            }
        }
    }


}

