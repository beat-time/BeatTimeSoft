using UnityEngine;
using System.Collections;
using Windows.Kinect;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class CoordinateMapperView : MonoBehaviour
{
    public GameObject CoordinateMapperManager;
    private CoordinateMapperManager _CoordinateMapperManager;

    private ComputeBuffer depthBuffer;
    private ComputeBuffer bodyIndexBuffer;

    public int idPlayer;

    DepthSpacePoint[] depthPoints;
    byte[] bodyIndexPoints;

    static float index = -1;

    public BodySourceView bodySourceView;
    Body[] bodies;
    GameObject bodyPlayer1;
    GameObject bodyPlayer2;
    ulong trackingIdPlayer1 = 0;
    ulong trackingIdPlayer2 = 0;

    void Start()
    {
        ReleaseBuffers();

        if (CoordinateMapperManager == null)
        {
            return;
        }

        _CoordinateMapperManager = CoordinateMapperManager.GetComponent<CoordinateMapperManager>();

        Texture2D renderTexture = _CoordinateMapperManager.GetColorTexture();
        if (renderTexture != null)
        {
            gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", renderTexture);
        }

        depthPoints = _CoordinateMapperManager.GetDepthCoordinates();
        if (depthPoints != null)
        {
            depthBuffer = new ComputeBuffer(depthPoints.Length, sizeof(float) * 2);
            gameObject.GetComponent<Renderer>().material.SetBuffer("depthCoordinates", depthBuffer);
        }

        bodyIndexPoints = _CoordinateMapperManager.GetBodyIndexBuffer();
        if (bodyIndexPoints != null)
        {
            bodyIndexBuffer = new ComputeBuffer(bodyIndexPoints.Length, sizeof(float));
            gameObject.GetComponent<Renderer>().material.SetBuffer("bodyIndexBuffer", bodyIndexBuffer);
        }
    }

    void Update()
    {
        //TODO: fix perf on this call.

        bodies = bodySourceView._BodyManager.GetData();

        if (bodyPlayer1 == null)
        {
            trackingIdPlayer1 = 0;
        }
        if (bodyPlayer2 == null)
        {
            trackingIdPlayer2 = 0;
        }

        if (bodyPlayer1 == null)
        {
            if (bodyPlayer2 != null)
            {
                if (bodySourceView._Bodies.Count >= 2)
                {
                    bodyPlayer1 = bodySourceView._Bodies.Values.ElementAt(1);
                    trackingIdPlayer1 = bodySourceView._Bodies.Keys.ElementAt(1);
                }
            }
            else
            {
                if (bodySourceView._Bodies.Count >= 1)
                {
                    bodyPlayer1 = bodySourceView._Bodies.Values.ElementAt(0);
                    trackingIdPlayer1 = bodySourceView._Bodies.Keys.ElementAt(0);
                }
            }

            //bodyObject1 = bodies._Bodies.Values.First();
        }
        if (bodyPlayer2 == null)
        {
            if (bodyPlayer1 != null)
            {
                if (bodySourceView._Bodies.Count >= 2)
                {
                    bodyPlayer2 = bodySourceView._Bodies.Values.ElementAt(1);
                    trackingIdPlayer2 = bodySourceView._Bodies.Keys.ElementAt(1);
                }
            }
            else
            {
                if (bodySourceView._Bodies.Count >= 1)
                {
                    bodyPlayer2 = bodySourceView._Bodies.Values.ElementAt(0);
                    trackingIdPlayer2 = bodySourceView._Bodies.Keys.ElementAt(0);
                }
            }
        }

        depthBuffer.SetData(depthPoints);
        // ComputeBuffers do not accept bytes, so we need to convert to float.

        float[] buffer = new float[512 * 424];
        float tempPlayer1 = -1;
        float tempPlayer2 = -1;
        for (int i = 0; i < bodyIndexPoints.Length; i++)
        {
            float temp = (float)bodyIndexPoints[i];
            if (idPlayer == 1)
            {
                if (temp < 255)
                {
                    if (tempPlayer1 == -1)
                    {
                        if (bodies != null)
                        {
                            if (bodies[(int)temp].TrackingId == trackingIdPlayer1 && trackingIdPlayer1 != 0)
                            {
                                tempPlayer1 = temp;
                            }
                            else
                            {
                                temp = 255;
                            }
                        }
                    }
                    else if (temp != tempPlayer1)
                    {
                        temp = 255;
                    }
                }
            }
            else if (idPlayer == 2)
            {
                if (temp < 255)
                {
                    if (tempPlayer2 == -1)
                    {
                        if (bodies != null)
                        {
                            if (bodies[(int)temp].TrackingId == trackingIdPlayer2 && trackingIdPlayer2 != 0)
                            {
                                tempPlayer2 = temp;
                            }
                            else
                            {
                                temp = 255;
                            }
                        }
                    }
                    else if (temp != tempPlayer2)
                    {
                        temp = 255;
                    }
                }
            }
            //if (idPlayer == 1)
            //{
            //    if (temp < 255)
            //    {
            //        if (temp2 == -1)
            //        {
            //            temp2 = temp;
            //        }
            //        else if (temp != temp2)
            //        {
            //            temp = 255;
            //        }
            //    }
            //}
            //else
            //{
            //    if (temp < 255)
            //    {
            //        if (temp2 == -1)
            //        {
            //            temp2 = temp;
            //        }
            //        else if (temp == temp2)
            //        {
            //            temp = 255;
            //        }
            //    }
            //}
            buffer[i] = temp;
        }
        bodyIndexBuffer.SetData(buffer);
        buffer = null;
    }

    private void ReleaseBuffers()
    {
        if (depthBuffer != null) depthBuffer.Release();
        depthBuffer = null;

        if (bodyIndexBuffer != null) bodyIndexBuffer.Release();
        bodyIndexBuffer = null;

        depthPoints = null;
        bodyIndexPoints = null;
    }

    void OnDisable()
    {
        ReleaseBuffers();
    }
}
