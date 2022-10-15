using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RingPuzzle : MonoBehaviour
{
	PuzzleTools puzzleTools;
	GameObject selectedObject;

	[HideInInspector]
	public UnityEvent finishedEvent;
	[HideInInspector]
	public UnityEvent startedEvent;

	[SerializeField] Camera puzzleCamera = null;

	AudioSource audioSource;

	bool isActive = false;

	public GameObject GetCamera() => puzzleCamera.gameObject;

	void CheckIsSolved()
	{
		if (puzzleTools.piecesInRotations)
		{
			isActive = false;
			finishedEvent.Invoke();
		}
	}

	public void StartPuzzle()
	{
		startedEvent.Invoke();
		isActive = true;
	}

	// Start is called before the first frame update
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		puzzleTools = GetComponent<PuzzleTools>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!isActive)
			return;
		Cursor.visible = true;
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = puzzleCamera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100f))
			{
				if (puzzleTools.IsPartOfPuzzle(hit.collider.gameObject))
				{
					if (selectedObject)
					{
						selectedObject = null;
					}
					else
						selectedObject = hit.transform.gameObject;
				}
				else
					selectedObject = null;
			}
			else
				selectedObject = null;
		}
		if (selectedObject)
		{
			float mouseX = Input.GetAxis("Mouse X");
			if (mouseX != 0)
			{
				if (!audioSource.isPlaying)
					audioSource.Play();
				else
					print("audio is playing");
				selectedObject.transform.eulerAngles += new Vector3(0, mouseX, 0);
				if (puzzleTools.piecesInRotations)
				{
					isActive = false;
					finishedEvent.Invoke();
					audioSource.Stop();
				}
			}
			else
			{
				audioSource.Stop();
			}
		}
	}
}
