using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour {
    [SerializeField]
    AudioSource audioSource;

    public void Start()
    {
        audioSource.GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioSource.Play();
            collision.gameObject.GetComponent<RespawnPlayer>().Respawn();
        }
    }

}
