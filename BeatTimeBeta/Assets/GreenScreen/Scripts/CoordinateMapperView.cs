using UnityEngine;
using System.Collections;
using Windows.Kinect;
using System.Collections.Generic;
using System.IO;

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

	void Start ()
	{
        ReleaseBuffers ();
		
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

		depthPoints = _CoordinateMapperManager.GetDepthCoordinates ();
		if (depthPoints != null)
		{
			depthBuffer = new ComputeBuffer(depthPoints.Length, sizeof(float) * 2);
			gameObject.GetComponent<Renderer>().material.SetBuffer("depthCoordinates", depthBuffer);
		}

		bodyIndexPoints = _CoordinateMapperManager.GetBodyIndexBuffer ();
		if (bodyIndexPoints != null)
		{
			bodyIndexBuffer = new ComputeBuffer(bodyIndexPoints.Length, sizeof(float));
			gameObject.GetComponent<Renderer>().material.SetBuffer ("bodyIndexBuffer", bodyIndexBuffer);
		}
	}

	void Update()
	{
		//TODO: fix perf on this call.
		depthBuffer.SetData(depthPoints);
        // ComputeBuffers do not accept bytes, so we need to convert to float.
        float[] buffer = new float[512 * 424];
        float temp2 = -1;
        for (int i = 0; i < bodyIndexPoints.Length; i++)
        {
            float temp = (float)bodyIndexPoints[i];
            if (idPlayer == 1)
            {
                if (temp < 255)
                {
                    if (temp2 == -1)
                    {
                        temp2 = temp;
                    }
                    else if (temp != temp2)
                    {
                        temp = 255;
                    }
                }
            }
            else
            {
                if (temp < 255)
                {
                    if (temp2 == -1)
                    {
                        temp2 = temp;
                    }
                    else if (temp == temp2)
                    {
                        temp = 255;
                    }
                }
            }
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
		ReleaseBuffers ();
	}
}
