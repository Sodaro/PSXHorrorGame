using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[SelectionBase]
public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject player = null;

    float nextInteractionTime;
    float delay = 1f;
    bool ReadyForInteraction => Time.time >= nextInteractionTime;

    PlayerInventory inventory;
    //[SerializeField]
    //GameObject descriptionField;
    Interactible lastInteractible;

    [SerializeField] GameObject[] buttons = null;

    [SerializeField] GameObject panel = null;

    [SerializeField] Image spriteDisplayer = null;

    [SerializeField] BlockPuzzleChecker blockPuzzle = null;
    [SerializeField] RingPuzzle ringPuzzle = null;

    [SerializeField] Fader fader = null;

    Typewriter typewriter;
    List<Entity> entities = new List<Entity>();


    GameObject activePuzzle = null;

    static GameManager instance;
    void Awake()
    {
        if (instance == null)
		{
            instance = this;
            DontDestroyOnLoad(gameObject);
            //Application.targetFrameRate = 60;
            Screen.SetResolution(320, 240, true);
        }
        else
		{
            Destroy(gameObject);
		}

    }

    // Start is called before the first frame update
    void Start()
    {
        entities = FindObjectsOfType<Entity>().ToList();

        panel.SetActive(false);

        player.GetComponent<PlayerInteraction>().interactionEvent.AddListener(OnInteraction);
        inventory = player.GetComponent<PlayerInventory>();
        typewriter = GetComponent<Typewriter>();
        typewriter.finishedEvent.AddListener(OnFinishedTyping);
        spriteDisplayer.enabled = false;

        blockPuzzle.finishedEvent.AddListener(delegate { OnPuzzleFinished(blockPuzzle.gameObject); });
        ringPuzzle.finishedEvent.AddListener(delegate { OnPuzzleFinished(ringPuzzle.gameObject); });
        blockPuzzle.startedEvent.AddListener(delegate { OnPuzzleStarted(blockPuzzle.gameObject); });
        ringPuzzle.startedEvent.AddListener(delegate { OnPuzzleStarted(ringPuzzle.gameObject); });
        fader.fadeFinished.AddListener(delegate { SwitchCamera(); });
    }

	private void Update()
	{
        if (spriteDisplayer.enabled && panel.activeInHierarchy == false)
		{
            if (Input.GetMouseButtonDown(0))
                StartTyping();
        }
	}

	void OnInteraction(GameObject gameObject)
	{
        if (!ReadyForInteraction)
            return;
        Door door = gameObject.GetComponent<Door>();
        if (door)
		{
			if (inventory.HasItem(door.GetRequiredItem()))
				SceneManager.LoadScene(door.GetSceneToLoad());
			else
			{
				TogglePause(true);
				panel.SetActive(true);
				lastInteractible = gameObject.GetComponent<Interactible>();
				typewriter.TypeText(lastInteractible.GetDescription());
			}

		}
        else
		{
            TogglePause(true);
            lastInteractible = gameObject.GetComponent<Interactible>();
            Sprite sprite = lastInteractible.GetDisplaySprite();
            if (sprite)
			{
                DisplaySprite(sprite);
            }
            else
			{
                StartTyping();
            }
        }
    }
    void DisplaySprite(Sprite sprite)
	{
        spriteDisplayer.sprite = sprite;
        spriteDisplayer.enabled = true;
    }
    void StartTyping()
	{
        panel.SetActive(true);
        string textToType = lastInteractible.GetDescription();
        if (textToType == "" && textToType == null)
            textToType = lastInteractible.GetDialogText();
        typewriter.TypeText(textToType);
        
    }

    void OnFinishedTyping()
	{
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        string dialogText = lastInteractible.GetDialogText();
        if (dialogText != "" && dialogText != null)
		{
            typewriter.SetText(dialogText);
            foreach (GameObject button in buttons)
			{
                button.SetActive(true);
			}
		}
        else
		{
            HideUI();
		}
        nextInteractionTime = Time.time + delay;

    }
    void HideUI()
	{
        TogglePause(false);
        panel.SetActive(false);
        spriteDisplayer.enabled = false;
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
        }
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
    public void OnAccept()
	{
        lastInteractible.FireEvents();
        //playerHasKey = true;
        //print("yes");
        HideUI();
	}
    
    public void OnDecline()
	{
        //print("no");
        HideUI();
	}

    void OnCloseInteraction()
	{
        TogglePause(false);
        panel.SetActive(false);
    }

    void TogglePause(bool value)
	{
        foreach (var entity in entities)
		{
            entity.canMove = !value;
		}
	}
    void OnPuzzleStarted(GameObject puzzle)
    {
        activePuzzle = puzzle;
        TogglePause(true);
        fader.FadeInAndOut(1f);
    }

    void OnPuzzleFinished(GameObject puzzle)
	{
        TogglePause(false);
        fader.FadeInAndOut(1f);
        Cursor.visible = false; 
    }

    void SwitchCamera()
	{
        GameObject playerCam = player.GetComponent<Player>().GetCamera();
        GameObject puzzleCam = null;
        if (activePuzzle == blockPuzzle.gameObject)
		{
            puzzleCam = blockPuzzle.GetCamera();
        }
        else if (activePuzzle == ringPuzzle.gameObject)
		{
            puzzleCam = ringPuzzle.GetCamera();
        } 
        playerCam.SetActive(!playerCam.activeInHierarchy);
        puzzleCam?.SetActive(!puzzleCam.activeInHierarchy);
    } //is broadcasted in fade script
}
