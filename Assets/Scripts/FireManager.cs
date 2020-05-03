using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    public GameObject[] fires;
    private GameObject switchManager;
    private bool currentState;

    private GameObject player;

    public float fireSoundProx;
    private AudioSource fireSound;
    public float volumeNorm;

    // Start is called before the first frame update
    void Start()
    {
        switchManager = GameObject.Find("SwitchManager");
        currentState = switchManager.GetComponent<SwitchManager>().state;
        player = GameObject.FindGameObjectWithTag("Player");
        fireSound = GetComponent<AudioSource>();
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
        Debug.Log(DistanceToPlayer());
        float distance = DistanceToPlayer();
        if (distance < fireSoundProx) {
            fireSound.volume = (fireSoundProx - distance) * volumeNorm;
            if (!fireSound.isPlaying) {
                fireSound.UnPause();
            }
        } else {
            fireSound.Pause();
        }
    }

    float DistanceToPlayer() {
        float distance = float.PositiveInfinity;
        foreach (GameObject fire in fires) {
            if (fire.GetComponent<BoolStateTileScript>().BoolValue) {
                float thisDist = (fire.transform.position - player.transform.position).magnitude;
                if (thisDist < distance)
                    distance = thisDist;
            }
        }
        return distance;
    }
}
