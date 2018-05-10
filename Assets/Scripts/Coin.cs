using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Coin : MonoBehaviour
{
    public float respawnTime = 5f;

    static int coinCount = 0;

    private Text coinCountText;

    private AudioSource audioSource;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    private void OnPlayerRespawnedFromCheckpoint()
    {
        ReenableCoin();
        coinCount = 0;
        UpdateCoinText();
    }

    private void ReenableCoin()
    {
        spriteRenderer.enabled = true;
        boxCollider2D.enabled = true;
    }

    private void OnEnable()
    {
        RespawnPlayer.PlayerRespawnedFromCheckpoint += OnPlayerRespawnedFromCheckpoint;
    }

    private void OnDisable()
    {
        RespawnPlayer.PlayerRespawnedFromCheckpoint -= OnPlayerRespawnedFromCheckpoint;
    }

    private void UpdateCoinText()
    {
        coinCountText.text = "Data Packs: " + coinCount;
    }

    private void Start()
    {
        coinCountText = GameObject.Find("CoinCountText").GetComponent<Text>();
        UpdateCoinText();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Based on what we know, when OnTriggerEnter2D is called we have a collision parameter is called.
        if (collision.gameObject.tag == "Player")
        {
            audioSource.Play();
            //increments that coin count
            coinCount++;
            UpdateCoinText();
            spriteRenderer.enabled = false;
            boxCollider2D.enabled = false;
            StartCoroutine(RespawnPickupTime());
        }


    }

    private IEnumerator RespawnPickupTime()
    {
        yield return new WaitForSeconds(respawnTime);
        spriteRenderer.enabled = true;
        boxCollider2D.enabled = true;
    }
}
