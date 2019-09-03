using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carvament : MonoBehaviour
{

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(audioSource.clip);
            Destroy(this.gameObject);
        }
    }

}
