using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public RectTransform rectTransform;
    public GameObject barra;
    private float width;
    private float height;
    public float progress;

    float timeSeconds = 5;
    float maxTime = 0;
    public bool move = true;
    // Start is called before the first frame update
    void Start()
    {
        progress = 0;
        rectTransform = GetComponent<RectTransform>();
        width = barra.GetComponent<RawImage>().rectTransform.rect.width;
        height = barra.GetComponent<RawImage>().rectTransform.rect.height;
        rectTransform.sizeDelta = new Vector2(0, height);
        maxTime = timeSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (rectTransform.rect.width >= width && timeSeconds >= maxTime)
        {         
            progress = 0;
            InvokeRepeating("FlashBar", 0.1f, 1f);
            //blink(barra, 0.1f, 5);
            move = false;
        }
        else if(move)
        {
            float updateBar = Mathf.MoveTowards(rectTransform.rect.width, progress, 5f);
            rectTransform.sizeDelta = new Vector2(Mathf.Clamp(updateBar, 0.0f, width), height);
        }

        if (!move)
        {
            timeSeconds -= Time.deltaTime;
            if (timeSeconds < 1)
            {
                CancelInvoke("FlashBar");
                timeSeconds = maxTime;
                rectTransform.sizeDelta = new Vector2(0, height);
                move = true;
                progress = 0;
            }
        }
    }

    void FlashBar()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            barra.gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            barra.gameObject.SetActive(true);
        }
    }

    void CountDown()
    {
        timeSeconds--;
        if (timeSeconds < 1)
        {
            CancelInvoke("FlashBar");
            timeSeconds = maxTime;
        }
    }

    void blink(GameObject obj, float blinkSpeed, float duration)
    {
        StartCoroutine(_blinkCOR(obj, blinkSpeed, duration));
    }

    IEnumerator _blinkCOR(GameObject obj, float blinkSpeed, float duration)
    {
        obj.SetActive(true);
        Color defualtColor = obj.GetComponent<RawImage>().color; //GetComponent<MeshRenderer>().material.color;

        float counter = 0;
        float innerCounter = 0;

        bool visible = false;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            innerCounter += Time.deltaTime;

            //Toggle and reset if innerCounter > blinkSpeed
            if (innerCounter > blinkSpeed)
            {
                visible = !visible;
                innerCounter = 0f;
            }

            if (visible)
            {
                //Show
                show(obj);
            }
            else
            {
                //Hide
                hide(obj);
            }

            //Wait for a frame
            yield return null;
        }

        //Done Blinking, Restore default color then Disable the GameObject
        //obj.GetComponent<MeshRenderer>().material.color = defualtColor;
        obj.GetComponent<RawImage>().color = defualtColor;
        gameObject.GetComponent<RawImage>().color = defualtColor;
        obj.SetActive(true);
    }

    void show(GameObject obj)
    {
        //Color currentColor = obj.GetComponent<MeshRenderer>().material.color;
        //currentColor.a = 1;
        //obj.GetComponent<MeshRenderer>().material.color = currentColor;
        Color currentColor = obj.GetComponent<RawImage>().color;
        currentColor.a = 1;
        obj.GetComponent<RawImage>().color = currentColor;

        Color currentColor2 = gameObject.GetComponent<RawImage>().color;
        currentColor2.a = 1;
        obj.GetComponent<RawImage>().color = currentColor2;
    }

    void hide(GameObject obj)
    {
        //Color currentColor = obj.GetComponent<MeshRenderer>().material.color;
        //currentColor.a = 0;
        //obj.GetComponent<MeshRenderer>().material.color = currentColor;
        Color currentColor = obj.GetComponent<RawImage>().color;
        currentColor.a = 0;
        obj.GetComponent<RawImage>().color = currentColor;

        Color currentColor2 = gameObject.GetComponent<RawImage>().color;
        currentColor2.a = 0;
        obj.GetComponent<RawImage>().color = currentColor2;
    }
}
