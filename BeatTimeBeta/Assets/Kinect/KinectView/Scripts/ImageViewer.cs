using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageViewer : MonoBehaviour
{
    public MultiSourceManager mMultiSource;

    public RawImage mRawImage;
    
    void Update()
    {
        mRawImage.texture = mMultiSource.GetColorTexture();
    }
}
