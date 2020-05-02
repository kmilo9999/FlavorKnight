using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushSoundPlayer : MonoBehaviour
{
    Rigidbody2D rb2d;
    public AudioSource pushSound;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb2d.velocity.magnitude > 0) {
            if (!pushSound.isPlaying) {
                pushSound.Play();
            }
        } else {
            pushSound.Stop();
        }
    }
}
