using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTileInteraction : MonoBehaviour
{
    private bool closeToSwitch;
    // Start is called before the first frame update
    private GameObject switchManager;
    AudioSource audioSource ;
    
    void Start()
    {
        closeToSwitch = false;
        switchManager = GameObject.Find("SwitchManager");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("space") && closeToSwitch)
        {
            switchManager.GetComponent<SwitchManager>().state = !switchManager.GetComponent<SwitchManager>().state;
            audioSource.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            closeToSwitch = true;
        }
        
    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            closeToSwitch = false;
        }
    }
}
