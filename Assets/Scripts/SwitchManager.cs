using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
   

    public GameObject[] switches;
    public bool state;
    private bool currentState;
    // Start is called before the first frame update


    void Start()
    {
        foreach (GameObject swtchs in switches)
        {
            swtchs.GetComponent<BoolStateTileScript>().BoolValue = state;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (state != currentState)
        {
            foreach (GameObject swtchs in switches)
            {
                currentState = state;
                swtchs.GetComponent<BoolStateTileScript>().BoolValue = currentState;
            }
        }
    }
}
