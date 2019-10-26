using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public RectTransform rectTransform;
    public RawImage barra;
    private float width;
    private float height;
    public float progress;
    // Start is called before the first frame update
    void Start()
    {
        progress = 0;
        rectTransform = GetComponent<RectTransform>();
        width = barra.rectTransform.rect.width;
        height = barra.rectTransform.rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        //float updateBar = Mathf.MoveTowards(rectTransform.rect.width, progress, 5.0f);
        //rectTransform.sizeDelta = new Vector2(100f, Mathf.Clamp(updateBar, 0.0f, 100f));
        float updateBar = Mathf.MoveTowards(rectTransform.rect.width, progress, 0.1f);
        rectTransform.sizeDelta = new Vector2(Mathf.Clamp(updateBar, 0.0f, width), height);
    }
}
