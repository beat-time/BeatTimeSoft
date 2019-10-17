using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapPaying : MonoBehaviour
{
    public void ChangePlaying()
    {
        SceneManager.LoadScene("Playing");
    }
}
