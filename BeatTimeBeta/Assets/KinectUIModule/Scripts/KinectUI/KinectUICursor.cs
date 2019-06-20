using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Windows.Kinect;
using System.Collections.Generic;

public class KinectUICursor : AbstractKinectUICursor
{
    public Color normalColor = new Color(1f, 1f, 1f, 0.5f);
    public Color hoverColor = new Color(1f, 1f, 1f, 1f);
    public Color clickColor = new Color(1f, 1f, 1f, 1f);
    public Vector3 clickScale = new Vector3(.8f, .8f, .8f);

    public BodySourceView bodySourceView;

    //public List<int> HandsPress = new List<int>();

    //private int HandType = -1;
    public bool HandGreen = false;
    private bool HandExisting = false;
    private Vector3 _initScale;
    private Vector3 posIni = new Vector3(1200,233,20);

    public override void Start()
    {
        base.Start();
        _initScale = transform.localScale;
        _image.color = new Color(1f, 1f, 1f, 0f);

        /*HandsPress.Add(0);
        HandsPress.Add(0);
        if (_data.handType == JointType.HandLeft)
        {
            HandType = 0;
        }
        else
        {
            HandType = 1;
        }*/
    }

    public override void ProcessData()
    {
        if (bodySourceView.Active == false)
        {
            if(HandExisting == false) {
                transform.position = posIni;
            }           
        }
        else
        {
            HandExisting = true;
            // update pos
            transform.position = _data.GetHandScreenPosition();
            
            if (_data.IsPressing)
            {
                _image.color = clickColor;

                //HandsPress[HandType] = 1;
                HandGreen = true;
                
                //_image.transform.localScale = clickScale;
                return;
            }
            if (_data.IsHovering)
            {
                HandGreen = false;
                _image.color = hoverColor;
            }
            else
            {
                //HandsPress[HandType] = 0;
                HandGreen = false;
                HandExisting = false;
                _image.color = normalColor;
            }
            _image.transform.localScale = _initScale;
        }
    }
}
