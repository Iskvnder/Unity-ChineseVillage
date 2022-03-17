using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicBoss : MonoBehaviour
{

    public AudioSource playSound1;
    public AudioSource playSound2;

    void OnTriggerEnter(Collider other)
    {
        playSound1.Play();
        playSound2.Stop();
    }

}
