using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerFondo : MonoBehaviour
{
    VideoPlayer vfondo;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        vfondo = GetComponent<VideoPlayer>();
        vfondo.targetTexture.Release();
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
