using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class door0 : MonoBehaviour
{
    public GameObject enterE;
    private bool ifE=false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            enterE.SetActive(true);
            ifE = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            enterE.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ifE==true && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
        }

        
    }
}
