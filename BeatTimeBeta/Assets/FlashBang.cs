using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashBang : MonoBehaviour
{
    CanvasGroup myCG;
    public AudioSource AudioFlash;
    private bool flash = false;
    public bool makeFlash = false;
    void Start()
    {
        
        myCG = GetComponent<CanvasGroup>();    
    }

    void Update()
    {
        if (flash)
        {
            myCG.alpha = myCG.alpha - Time.deltaTime;
            if (myCG.alpha <= 0)
            {
                myCG.alpha = 0;
                flash = false;
                makeFlash = true;
            }
        }
    }

    public void MineHit()
    {
        AudioFlash.Play();
        flash = true;
        myCG.alpha = 1;
        
    }
}
