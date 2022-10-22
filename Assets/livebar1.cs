using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class livebar1 : MonoBehaviour
{
    public GameObject presswasd;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            presswasd.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            presswasd.SetActive(false);
        }
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
