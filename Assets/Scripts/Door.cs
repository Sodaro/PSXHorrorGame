using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ScenePicker))]
public class Door : Interactible
{
    private string sceneToLoad;
    [SerializeField] string requiredItem = "";
    // Start is called before the first frame update
    void Start()
    {
        sceneToLoad = GetComponent<ScenePicker>().scenePath;
    }

    public string GetRequiredItem() => requiredItem;
    public string GetSceneToLoad() => sceneToLoad;

    // Update is called once per frame
    void Update()
    {
        
    }
}
