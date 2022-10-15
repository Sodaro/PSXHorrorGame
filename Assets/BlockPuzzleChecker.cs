using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlockPuzzleChecker : MonoBehaviour
{
	PuzzleTools puzzleTools;
	GameObject selectedObject;
	[SerializeField] GameObject positionIndicator = null;
	List<GameObject> indicators = new List<GameObject>();
	private Coroutine coroutine;

	[HideInInspector]
	public UnityEvent finishedEvent;
	[HideInInspector]
	public UnityEvent startedEvent;

	[SerializeField] Camera puzzleCamera = null;

	bool isActive = false;

	public GameObject GetCamera() => puzzleCamera.gameObject;

	void Start()
    {
		puzzleTools = GetComponent<PuzzleTools>();
		for (int i = 0; i < 4; i++)
		{
			indicators.Add(Instantiate(positionIndicator));
		}
        //pieces.Shuffle();
    }

	void MoveIndicators(Vector3 center)
	{
		Vector3 rightPos = new Vector3(center.x + 1, center.y, center.z);
		Vector3 leftPos = new Vector3(center.x - 1, center.y, center.z);
		Vector3 upPos = new Vector3(center.x, center.y, center.z + 1);
		Vector3 downPos = new Vector3(center.x, center.y, center.z - 1);
		Vector3[] allPositions = { rightPos, leftPos, upPos, downPos };

		for (int i = 0; i < allPositions.Length; i++)
		{
			if (!Physics.CheckBox(allPositions[i], new Vector3(0.25f, 0.25f, 0.25f)))
			{
				indicators[i].transform.position = allPositions[i];
				indicators[i].SetActive(true);
			}	
		}
	}
	void DisableIndicators()
	{
		foreach (GameObject indicator in indicators)
		{
			indicator.SetActive(false);
		}
	}

	void CheckIsSolved()
	{
		if (puzzleTools.piecesInPositions)
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
				if (puzzleTools.IsPartOfPuzzle(hit.collider.gameObject) && coroutine == null)
				{
					if (selectedObject)
					{
						selectedObject = null;
						DisableIndicators();
					}
					selectedObject = hit.transform.gameObject;
					MoveIndicators(selectedObject.transform.position);
				}
				else
				{
					if (selectedObject && coroutine == null)
					{
						foreach (GameObject indicator in indicators)
						{
							if (hit.transform.gameObject == indicator)
							{
								coroutine = StartCoroutine(MoveFromTo(selectedObject.transform, selectedObject.transform.position, indicator.transform.position, 1));
								break;
							}
						}
						DisableIndicators();
						selectedObject = null;
					}
				}
			}
			else
				selectedObject = null;
		}
	}

	IEnumerator MoveFromTo(Transform objectToMove, Vector3 a, Vector3 b, float speed)
	{
		float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
		float t = 0;
		while (t <= 1.0f)
		{
			t += step; // Goes from 0 to 1, incrementing by step each time
			objectToMove.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
			yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
		}
		objectToMove.position = b;
		coroutine = null;
		CheckIsSolved();
	}
}
