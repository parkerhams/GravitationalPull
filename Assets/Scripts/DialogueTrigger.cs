using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;
    [SerializeField]
    AudioSource selectButton;
    [SerializeField]
    GameObject continueButton;

    private void Start()
    {
        continueButton.SetActive(false);
    }

    public void TriggerDialogue ()
	{
        this.gameObject.SetActive(false);
        continueButton.SetActive(true);
        selectButton.Play();
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

}
