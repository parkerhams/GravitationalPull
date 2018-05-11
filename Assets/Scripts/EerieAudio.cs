using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    GameObject theTitle;

	// Use this for initialization
	void Start ()
    {
        theTitle.SetActive(true);
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
        StartCoroutine(playTheExplosion());
    }

    public IEnumerator playTheExplosion()
    {
        StopCoroutine(playTheOrchestra());
        StopCoroutine(playTheElectric());
        overallMusic.Stop();
        yield return new WaitForSeconds(0f);
        explosion.Play();
        StartCoroutine(cutToBlack());
    }

    public IEnumerator cutToBlack()
    {
        theTitle.SetActive(false);
        yield return new WaitForSeconds(3f);
        NextSceneTimeDammit();
        
        
    }

    public void NextSceneTimeDammit()
    {
        SceneManager.LoadScene("Wake");
    }
}
