using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTileScript : MonoBehaviour
{

    public Animator animator;
    public bool isOFF ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isOFF", isOFF);
    }
}
