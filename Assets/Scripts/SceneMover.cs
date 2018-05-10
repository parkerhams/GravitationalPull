using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
    [SerializeField]
    Canvas creditsCanvas;
    [SerializeField]
    Canvas titleCanvas;

    void Start()
    {
        creditsCanvas.gameObject.SetActive(false);
        titleCanvas.gameObject.SetActive(true);
    }

    public void StartButtonClicked()
    {
        SceneManager.LoadScene("Introduction");
    }

    public void ContinueButtonClicked()
    {
        SceneManager.LoadScene("Wake");
    }

    public void CreditsButtonClicked()
    {
        creditsCanvas.gameObject.SetActive(true);
        titleCanvas.gameObject.SetActive(false);
    }

    public void BackButtonClicked()
    {
        creditsCanvas.gameObject.SetActive(false);
        titleCanvas.gameObject.SetActive(true);
    }

    public void TitleButtonClicked()
    {
        SceneManager.LoadScene("TitleScreen");
    }


}
