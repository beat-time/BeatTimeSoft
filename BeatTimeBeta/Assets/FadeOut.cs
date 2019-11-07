
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOut : MonoBehaviour
{
    RawImage txt;
    Color txt_color;
    float time_fadeout = 1;
    // can ignore the update, it's just to make the coroutines get called for example
    void Start()
    {
        txt = GetComponent<RawImage>();
        txt_color = txt.color;
    }
    void Update()
    {
        if (txt.texture != null)
        {
            //StartCoroutine(FadeTextToZeroAlpha(1f, txt));
            if (txt.color.a > 0)
            {
                txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, txt.color.a - (Time.deltaTime / time_fadeout));
            }
            else
            {
                txt.texture = null;
                txt.color = txt_color;
                txt.gameObject.SetActive(false);
            }
        }
    }

    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        txt.texture = null;
        txt.color = txt_color;
        txt.gameObject.SetActive(false);
    }
}
