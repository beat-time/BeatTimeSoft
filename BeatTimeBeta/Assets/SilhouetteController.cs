using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilhouetteController : MonoBehaviour
{
    public GameObject silhouettePlayer1;
    public GameObject silhouettePlayer2;
    // Start is called before the first frame update
    private void Awake()
    {
        silhouettePlayer1.SetActive(false);
        silhouettePlayer2.SetActive(false);
    }
    void Start()
    {
        PhotoPlayerController photoPlayerController = new PhotoPlayerController();
        if (photoPlayerController.getTurnPlayerOne())
        {
            silhouettePlayer1.SetActive(true);
        }
        else
        {
            silhouettePlayer1.SetActive(true);
            silhouettePlayer2.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
