using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StringEvent : UnityEvent<string>
{
}

public class Interactible : MonoBehaviour
{
    [SerializeField] UnityEvent dialogYesEvent = null;
    [SerializeField] StringEvent itemEvent = null;

    [SerializeField][Multiline] string description = "";
    [SerializeField] string dialogText = "";
    [SerializeField] Sprite displaySprite = null;

    [SerializeField] string itemName = "";
    // Start is called before the first frame update
    void Start()
    {
        //print(description);
    }

    public void FireEvents()
	{
        dialogYesEvent?.Invoke();
        itemEvent?.Invoke(itemName);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetDescription() => description;
    public string GetDialogText() => dialogText;
    public Sprite GetDisplaySprite() => displaySprite;
}
