using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(SmoothMouseLook))]
[RequireComponent(typeof(PlayerInteraction))]
public class Player : Entity
{
    PlayerMovement playerMovement;
    SmoothMouseLook mouseLook;
    PlayerInteraction playerInteraction;
    GameObject playerCam;

    static Player playerInstance;
	private void Awake()
	{
        if (playerInstance == null)
        {
            DontDestroyOnLoad(gameObject);
            playerInstance = this;
            playerMovement = GetComponent<PlayerMovement>();
            mouseLook = GetComponent<SmoothMouseLook>();
            playerInteraction = GetComponent<PlayerInteraction>();
            playerCam = GetComponentInChildren<Camera>().gameObject;
        }
        else
            Destroy(gameObject);

	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.Confined;
            else if (Cursor.lockState == CursorLockMode.Confined)
                Cursor.lockState = CursorLockMode.None;
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

        }
        //safety so player cant move if cam disabled
        if (!playerCam.activeInHierarchy || !canMove)
            return;
        //playerMovement.enabled = canMove;
        //mouseLook.enabled = canMove;
        //playerInteraction.enabled = canMove;
        playerMovement.HandleMovement();
        mouseLook.ProcessMouseMovement();
        playerInteraction.HandleInputs();

    }

    public GameObject GetCamera() => playerCam;
}
