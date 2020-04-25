using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Text debugText;
    public Animator animator;
    int counter = 0;
    // Start is called before the first frame update
    public void modifyHealth(int damage)
    {
        counter = damage;
        if (animator != null)
        {
            animator.SetInteger("health", damage);
        }
        else {
            Application.Quit();
        }
        
    }

    public void Update()
    {
        //debugText.text = "HELLOW WORLD";
        debugText.text = "current state " + counter;
    }
}
