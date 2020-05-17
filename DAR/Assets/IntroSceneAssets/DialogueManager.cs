using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;
	public Animator animator;
	public bool inDialogue = false;
	private Queue<string> sentences;
	public bool nowTyping;
	private string currentText;

	// Use this for initialization
	void Awake () {
		sentences = new Queue<string>();
	}

	public void StartDialogue (Dialogue dialogue)
	{
		inDialogue = true;
		
		if (animator) { animator.SetBool("IsOpen", true); }

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		currentText = sentence;
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		nowTyping = true;
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
		nowTyping = false;
	}
	public void TypeEverything()
	{
		StopAllCoroutines();
		dialogueText.text = currentText;
		nowTyping = false;
	}

	void EndDialogue()
	{
		inDialogue = false;

		if (animator) { animator.SetBool("IsOpen", false); }

		currentText = null;
	}

}
