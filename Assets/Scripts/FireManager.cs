using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    public GameObject[] fires;
    private GameObject switchManager;
    private bool currentState;

    // Start is called before the first frame update
    void Start()
    {
        switchManager = GameObject.Find("SwitchManager");
        currentState = switchManager.GetComponent<SwitchManager>().state;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != switchManager.GetComponent<SwitchManager>().state)
        {
            currentState = switchManager.GetComponent<SwitchManager>().state;
            foreach (GameObject fire in fires)
            {
                fire.GetComponent<BoolStateTileScript>().BoolValue =
                    !fire.GetComponent<BoolStateTileScript>().BoolValue;
            }
        }
    }
}
