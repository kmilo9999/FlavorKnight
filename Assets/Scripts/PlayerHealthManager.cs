using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    
    // called when the player dies. should reset the level once the level system is implemented.
    public void KillPlayer() {
        Camera.main.GetComponent<CameraFollow>().enabled = false;
        this.gameObject.SetActive(false);
    }

}
