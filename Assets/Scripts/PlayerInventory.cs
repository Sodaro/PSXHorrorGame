using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	[SerializeField] private List<string> items = new List<string>();

	public void AddItem(string item) => items.Add(item);
	public bool HasItem(string item) => items.Contains(item); 
}
