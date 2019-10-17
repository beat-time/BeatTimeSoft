using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GifAnimation : MonoBehaviour
{
    public Texture2D[] frames;
    public float framesPorSegundo = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float index = Time.time * framesPorSegundo;
        index = index % frames.Length;
        GetComponent<RawImage>().texture = frames[(int)index];
    }
}
