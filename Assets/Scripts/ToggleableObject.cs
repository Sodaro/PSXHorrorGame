using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ToggleableObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public void Toggle() => gameObject.SetActive(!gameObject.activeInHierarchy);

    // Update is called once per frame
    void Update()
    {
        
    }
}
