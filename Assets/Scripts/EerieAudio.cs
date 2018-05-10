using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EerieAudio : MonoBehaviour {
    //Get ready for CoRoutines: The Script

    [SerializeField]
    AudioSource overallMusic;

    [SerializeField]
    AudioSource explosion;

    [SerializeField]
    AudioSource electric;

    [SerializeField]
    AudioSource orchestra;

	// Use this for initialization
	void Start ()
    {
        overallMusic.Play();
        StartCoroutine(playTheElectric());
	}
	
	private IEnumerator playTheElectric()
    {
        yield return new WaitForSeconds(4f);
        electric.Play();
        StartCoroutine(playTheOrchestra());
    }

    private IEnumerator playTheOrchestra()
    {
        yield return new WaitForSeconds(3f);
        orchestra.Play();
    }
}
