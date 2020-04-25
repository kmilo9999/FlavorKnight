using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolStateTileScript : MonoBehaviour
{

    
    public Animator animator;
    public bool BoolValue;
    private bool currentState;
    // Start is called before the first frame update
    void Start()
    {
        updateState();
    }

    // Update is called once per frame
    void Update()
    {
        updateState();
    }

    void updateState()
    {
        if (!BoolValue)
        {
            animator.SetBool("isOFF", true);
        }
        else
        {
            animator.SetBool("isOFF", false);
        }
    }


}
