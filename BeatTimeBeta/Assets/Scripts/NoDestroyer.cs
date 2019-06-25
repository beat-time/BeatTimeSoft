using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDestroyer : MonoBehaviour
{
    public static NoDestroyer noDestroyer;
    private void Awake()
    {
        if (noDestroyer == null)
        {
            noDestroyer = this;
            DontDestroyOnLoad(noDestroyer);
        }
        /*else if (noDestroyer != this)
        {
            Destroy(transform.gameObject);
        }/*/
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
