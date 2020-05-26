using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	private bool dialogueActivated = false;
	public bool autoStart;
	public bool noButton;
	private DialogueToFade fadeComponent;
	private DialogueManager dialogueMng;
	private bool m_isAxisInUse = false;
	public Dialogue dialogue;

	void Start()
	{
		dialogueMng = FindObjectOfType<DialogueManager>();
		if (autoStart)
		{
			TriggerDialogue();
		}
		if(GetComponent<DialogueToFade>() != null)
		{
			fadeComponent = GetComponent<DialogueToFade>();
		}
	}

	void Update()
	{
		if(noButton && (Input.GetAxisRaw("Submit") != 0) || Input.GetKeyDown(GameManager.GM.put))
		{
			if(m_isAxisInUse == false)
			{
				if (dialogueMng.nowTyping)
				{
					dialogueMng.TypeEverything();
				}
				else TriggerNextSentence();
				
				m_isAxisInUse = true;
				if (dialogueActivated && !dialogueMng.inDialogue && fadeComponent != null)
				{
					fadeComponent.GoToLoad();
				}
			}
		}
		if (Input.GetAxisRaw("Submit") == 0)
		{
			m_isAxisInUse = false;
		}
	}

	public void TriggerDialogue()
	{
		dialogueActivated = true;
		dialogueMng.StartDialogue(dialogue);
	}

	public void TriggerNextSentence()
	{
		dialogueMng.DisplayNextSentence();
	}
}
