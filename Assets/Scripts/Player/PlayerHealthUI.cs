using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    public void modifyHealth(int damage)
    {
        if (animator != null)
        {
            animator.SetInteger("health", damage);
        }
        
    }
}
