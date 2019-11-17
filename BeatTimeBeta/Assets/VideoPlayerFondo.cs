using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerFondo : MonoBehaviour
{
    VideoPlayer vfondo;
    RawImage ImageFondo;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        vfondo = GetComponent<VideoPlayer>();
        ImageFondo = GetComponent<RawImage>();
        
        vfondo.targetTexture.Release();
        ImageFondo.texture = vfondo.targetTexture;
        vfondo.Play();
        vfondo.loopPointReached += EndReached;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void EndReached(VideoPlayer v)
    {
        v.frame = 0;
    }
}
