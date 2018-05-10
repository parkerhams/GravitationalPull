using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;

	public Animator animator;

    public Animator buttonAnimator;

    [SerializeField]
    AudioSource selectAudio;
    [SerializeField]
    AudioSource typeAudio;
    //[SerializeField]
    //GameObject continueButton;

    private Queue<string> sentences;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
        //continueButton.SetActive(false);
        buttonAnimator.SetBool("isOpen", false);
	}

	public void StartDialogue (Dialogue dialogue)
	{
		animator.SetBool("isOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}
        typeAudio.Stop();
		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}
        selectAudio.Play();
		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
        typeAudio.Play();
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
        typeAudio.Stop();
		animator.SetBool("isOpen", false);
        buttonAnimator.SetBool("isOpen", true);
	}

}
