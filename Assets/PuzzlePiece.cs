using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] Vector3 storedPosition;
    [SerializeField] Quaternion storedRotation;

    public void StorePosition() => storedPosition = transform.position;

    public Vector3 GetStoredPosition() => storedPosition;

    public void StoreRotation() => storedRotation.eulerAngles = transform.eulerAngles;

    public Vector3 GetStoredRotation() => storedRotation.eulerAngles;
}
