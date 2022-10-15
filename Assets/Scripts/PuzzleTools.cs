using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTools : MonoBehaviour
{

    [SerializeField] List<GameObject> pieces = new List<GameObject>();

    [ContextMenu("Reset Positions")]
    void ResetPositions()
    {
        foreach (GameObject piece in pieces)
            piece.transform.position = piece.GetComponent<PuzzlePiece>().GetStoredPosition();
    }

    [ContextMenu("Reset Rotations")]
    void ResetRotations()
    {
        foreach (GameObject piece in pieces)
            piece.transform.eulerAngles = piece.GetComponent<PuzzlePiece>().GetStoredRotation();
    }

    [ContextMenu("Store Positions")]
    void StorePositions()
    {
        foreach (GameObject piece in pieces)
            piece.GetComponent<PuzzlePiece>().StorePosition();
    }

  //  [ContextMenu("Store Rotations")]
  //  void StoreRotations()
  //  {
  //      foreach (GameObject piece in pieces)
		//{
  //          piece.GetComponent<PuzzlePiece>().StoreRotation();
  //      }
  //  }

    [ContextMenu("Shuffle Positions")]
    void ShufflePositions()
    {
        for (int i = 0; i < pieces.Count - 1; i++)
        {
            pieces[i].SwapPosition(pieces[i + 1]);
        }
        Debug.Log("Positions Shuffled");
    }

    [ContextMenu("Check Positions")]
    void CheckPositions()
    {
        if (piecesInPositions)
		{
            Debug.Log("Objects in stored positions");
        }
        else
		{
            Debug.Log("Objects NOT in stored positions");
        }
    }
    //[ContextMenu("Check Rotations")]
    //void CheckRotations()
    //{
    //    if (piecesInRotations)
    //    {
    //        Debug.Log("Objects in stored rotations");
    //    }
    //    else
    //    {
    //        Debug.Log("Objects NOT in stored rotations");
    //    }
    //}

    [ContextMenu("Randomize Y Rotations")]
    void RandomizeYRotations()
    {
        foreach (GameObject piece in pieces)
        {
            piece.transform.eulerAngles = new Vector3(0, Random.Range(0, 359), 0);
        }
    }

    public bool piecesInPositions
	{
		get
		{
            foreach (GameObject piece in pieces)
            {
                if (Vector3.Distance(piece.transform.position, piece.GetComponent<PuzzlePiece>().GetStoredPosition()) > 0.5f)
                {
                    Debug.Log("Objects NOT in stored positions");
                    return false;
                }
            }
            return true;
        }
    }
    public bool piecesInRotations
    {
        get
        {
            for (int i = 1; i < pieces.Count; i++)
            {
                if (Quaternion.Angle(pieces[i].transform.rotation, pieces[i-1].transform.rotation) > 5f)
				{
                    Debug.Log("Objects NOT same rotations");
                    return false;
                }
            }
            return true;
        }
    }


    public bool IsPartOfPuzzle(GameObject gameObject)
	{
        return pieces.Contains(gameObject);
	}

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
