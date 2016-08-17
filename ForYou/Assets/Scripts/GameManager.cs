using UnityEngine;
using System.Collections;
using UnityEngine.UI; // include UI namespace so can reference UI elements
using UnityStandardAssets.ImageEffects;

public class GameManager : MonoBehaviour {

    // static reference to game manager so can be called from other scripts directly (not just through gameobject component)
    public static GameManager gm;

    // levels to move to on after completion or restart or quit
    public string levelAfterComplete;
    public string levelAfterRestart;
    public string levelAfterQuit;

    // UI elements to control
    public GameObject UIGamePaused;
    public GameObject UIGamePausedText;
    public Text UILevel;

    // private variables
    GameObject _player;
    Vector3 _spawnLocation;
    BlurOptimized _blur;

    // setup
    void Awake ()
    {
        // setup reference to game manager
        if (gm == null)
            gm = this.GetComponent<GameManager>();

        Time.timeScale = 1f; // this unpauses the game action (ie. normal)

        _blur = Camera.main.gameObject.GetComponent<BlurOptimized>();
        if (_blur == null)
        { // if blur is missing
            Debug.LogWarning("Blur component missing from this camera. Adding one.");
            // let's just add the b;ur component dynamically
            _blur = Camera.main.gameObject.AddComponent<BlurOptimized>();
            Camera.main.gameObject.GetComponent<BlurOptimized>().enabled = false;
        }


        // setup all the variables, the UI, and provide errors if things not setup properly.
        setupDefaults();
    }

	// Update is called once per frame
	void Update ()
    {
        // if ESC pressed then pause the game -- updated to use the keys assigned to the Pause button and to blur screen
        if (Input.GetButtonDown("Pause"))
        {
            PauseGame();    
        }

        // add option to reset game by pressing "r" while not paused
        if (Input.GetButtonDown("Reset") && Time.timeScale > 0f)
        {
            ResetGame();
        }
    }

    void setupDefaults()
    {
        // setup reference to player
        if (_player == null)
            _player = GameObject.FindGameObjectWithTag("Player");

        if (_player == null)
            Debug.LogError("Player not found in Game Manager");

        // get initial _spawnLocation based on initial position of player
        _spawnLocation = _player.transform.position;

        // if levels not specified, default to current level
        if (levelAfterComplete == "")
        {
            Debug.LogWarning("levelAfterComplete not specified, defaulted to current level");
            levelAfterComplete = Application.loadedLevelName;
        }

        // if levels not specified, default to current level
        if (levelAfterRestart == "")
        {
            Debug.LogWarning("levelAfterRestart not specified, defaulted to current level");
            levelAfterRestart = Application.loadedLevelName;
        }

        if (UILevel == null)
            Debug.LogError("Need to set UILevel on Game Manager.");

        if (UIGamePaused == null)
            Debug.LogError("Need to set UIGamePaused on Game Manager.");

        // get stored player prefs
        refreshPlayerState();

        // get the UI ready for the game
        refreshGUI();

    }

    // get stored Player Prefs if they exist, otherwise go with defaults set on gameObject
    void refreshPlayerState()
    {
        // save that this level has been accessed so the MainMenu can enable it
        PlayerPrefManager.UnlockLevel();
    }

    // refresh all the GUI elements
    void refreshGUI()
    {
        // set the text elements of the UI
        UILevel.text = Application.loadedLevelName;
    }

    // public function to reset game accordingly
    public void ResetGame()
    {
        Application.LoadLevel(levelAfterRestart);
    }

    // public function for level complete
    public void LevelCompete()
    {
        // use a coroutine to allow the player to get fanfare before moving to next level
        StartCoroutine(LoadNextLevel());
    }

    // load the nextLevel after delay
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(1.0f);
        Application.LoadLevel(levelAfterComplete);
    }

    // public funtion to pause and unpause game
    public void PauseGame()
    {
        if (Time.timeScale > 0f)
        {
            UIGamePaused.SetActive(true); // this brings up the pause UI
            UIGamePausedText.SetActive(true);
            Time.timeScale = 0f; // this pauses the game action
            Camera.main.gameObject.GetComponent<BlurOptimized>().enabled = true;
        }
        else
        {
            Camera.main.gameObject.GetComponent<BlurOptimized>().enabled = false;
            Time.timeScale = 1f; // this unpauses the game action (ie. back to normal)
            UIGamePausedText.SetActive(false);
            UIGamePaused.SetActive(false); // remove the pause UI
        }
    }

    // public funtion to quit level
    public void QuitGame()
    {
        // load the main menu
        Application.LoadLevel(levelAfterQuit);
    }

}
