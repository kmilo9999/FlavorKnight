using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour
{

    public AudioSource musicStart;
    public AudioSource musicLoop;

    void Update() {
        if (!musicStart.isPlaying && !musicLoop.isPlaying) {
            musicLoop.Play();
        }
    }

}
