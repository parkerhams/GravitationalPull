using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EZCameraShake;

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
            CameraShaker.Instance.ShakeOnce(4f, 3f, .1f, 1.5f);
            collision.gameObject.GetComponent<RespawnPlayer>().Respawn();
        }
    }

}
