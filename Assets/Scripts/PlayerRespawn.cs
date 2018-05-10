using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour {
    public static event Action PlayerRespawnedFromCheckpoint;
    public ParticleSystem deathEffect;
    private void Respawn(Transform playerTransform)
    {
        if (Checkpoint.currentlyActiveCheckpoint == null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            StartCoroutine(WaitForRespawn());
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            
            playerTransform.position = Checkpoint.currentlyActiveCheckpoint.transform.position;

            if (PlayerRespawnedFromCheckpoint != null)
            {
                PlayerRespawnedFromCheckpoint.Invoke();
            }
        }
    }
    void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
            StartCoroutine(WaitForRespawn());
            Respawn(other.transform);
		}
	}

    IEnumerator WaitForRespawn()
    {
        yield return new WaitForSeconds(2f);
    }
}
