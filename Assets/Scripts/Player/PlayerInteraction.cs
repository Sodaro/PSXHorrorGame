using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionEvent : UnityEvent<GameObject> { };
public class PlayerInteraction : MonoBehaviour
{
    public InteractionEvent interactionEvent = new InteractionEvent();
    Camera _camera = null;
    LayerMask interactionLayer = 0;

    Light flashLight = null;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        interactionLayer = LayerMask.GetMask("Interaction");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        flashLight = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    public void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.L))
		{
            flashLight.enabled = !flashLight.enabled;
		}
        if (Input.GetMouseButtonDown(0))
		{
            RaycastHit hit;
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, 1.5f, interactionLayer))
			{
                interactionEvent.Invoke(hit.collider.gameObject);
			}
		}
    }
}
