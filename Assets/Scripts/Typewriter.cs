using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Typewriter : MonoBehaviour
{
	float typingSpeed = 0.07f;
	float dramaticSpeed = 0.12f;
	public UnityEvent finishedEvent;
    public Text descriptionField;
	int index = 0;
	string[] linesToType;
	bool isActive;

	private void Awake()
	{
		Wipe();
	}
	// Start is called before the first frame update
	void Start()
    {
        
    }


    public void Wipe()
	{
		descriptionField.text = "";
	}

	public void SetText(string text)
	{
		StopAllCoroutines();
		descriptionField.text = text;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && isActive)
		{
			if (index < linesToType.Length -1)
			{
				StopAllCoroutines();
				Wipe();
				index++;
				StartCoroutine(TypeOverTime(linesToType[index]));
			}
			else if (index == linesToType.Length - 1)
			{
				finishedEvent.Invoke();
				isActive = false;
			}

		}
	}

	public void TypeText(string text)
	{
		StopAllCoroutines();
		Wipe();
		index = 0;
        linesToType = text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
		if (linesToType.Length > 0)
		{
			StartCoroutine(TypeOverTime(linesToType[index]));
			isActive = true;
		}
		else
		{
			finishedEvent.Invoke();
			isActive = false;
		}
	}

	IEnumerator TypeOverTime(string text)
	{
		//yield return StartCoroutine(ClearOverTime());

		foreach (char c in text)
		{
			descriptionField.text += c;
			if (c == '.')
				yield return new WaitForSeconds(dramaticSpeed);
			else
				yield return new WaitForSeconds(typingSpeed);
		}
		yield return null;
	}
}
