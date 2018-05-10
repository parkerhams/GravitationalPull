using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Checkpoint currentlyActiveCheckpoint;

    public Animator checkpointNotificationAnim;

    [SerializeField]
    private float activatedScale;
    [SerializeField]
    private float deactivatedScale;
    [SerializeField]
    private Color activatedColor;
    [SerializeField]
    private Color deactivatedColor;

    [SerializeField]
    ParticleSystem checkpointParticles;

    private bool isActive = false;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        checkpointNotificationAnim.SetBool("isPlaying", false);
        spriteRenderer = GetComponent<SpriteRenderer>();
        DeactivateCheckpoint();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isActive)
        {

            ActivateCheckpoint();
            
        }
    }

    private void DeactivateCheckpoint()
    {
        isActive = false;
        transform.localScale = Vector3.one * deactivatedScale;
        spriteRenderer.color = deactivatedColor;
        StopCoroutine(ParticleEffectStopPlaying());
    }

    private void ActivateCheckpoint()
    {
        if(currentlyActiveCheckpoint != null)
        {
            currentlyActiveCheckpoint.DeactivateCheckpoint();
        }
        
        isActive = true;
        currentlyActiveCheckpoint = this;
        Instantiate(checkpointParticles, transform.position, Quaternion.identity);
        StartCoroutine(CallCheckpointTextAnim());
        StartCoroutine(ParticleEffectStopPlaying());
        transform.localScale = Vector3.one * activatedScale;
        spriteRenderer.color = activatedColor;
    }

    IEnumerator CallCheckpointTextAnim()
    {
        checkpointNotificationAnim.SetBool("isPlaying", true);
        yield return new WaitForSeconds(3f);
        checkpointNotificationAnim.SetBool("isPlaying", false);
    }

    IEnumerator ParticleEffectStopPlaying()
    {
        yield return new WaitForSeconds(3);
        checkpointParticles.gameObject.SetActive(false);
    }
}
