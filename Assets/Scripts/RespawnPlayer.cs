using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnPlayer : MonoBehaviour
{
    public ParticleSystem deathEffect;
    public static event Action PlayerRespawnedFromCheckpoint;
    public void Respawn()
    {
        if (Checkpoint.currentlyActiveCheckpoint == null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            StartCoroutine(WaitForRespawn());
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            //This script MUST be placed on the player gameObject
            transform.position = Checkpoint.currentlyActiveCheckpoint.transform.position;

            if (PlayerRespawnedFromCheckpoint != null)
            {
                StartCoroutine(WaitForRespawn());
                //PlayerRespawnedFromCheckpoint.Invoke();
                Invoke("PlayerRespawnedFromCheckpoint", 10);
            }
        }       
    }

    IEnumerator WaitForRespawn()
    {
        yield return new WaitForSeconds(3f);
    }
}