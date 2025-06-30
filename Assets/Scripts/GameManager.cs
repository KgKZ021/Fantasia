using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver,
        LevelUp
    }

    public GameState currentState;

    public GameState previousState;

    [Header("Screen")]
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject resultScreen;
    [SerializeField] private GameObject levelUpScreen;

    [Header("Current Stats Display")]
    public TextMeshProUGUI currentHealthDisplay;
    public TextMeshProUGUI currentRecoveryDisplay;
    public TextMeshProUGUI currentMoveSpeedDisplay;
    public TextMeshProUGUI currentMightDisplay;
    public TextMeshProUGUI currentProjectileSpeedDisplay;
    public TextMeshProUGUI currentMagnetDisplay;

    [Header("Result Screen Display")]
    [SerializeField] private Image chosenCharacterImage;
    [SerializeField] private TextMeshProUGUI chosenCharacterName;
    [SerializeField] private TextMeshProUGUI levelReachedDisplay;
    [SerializeField] private TextMeshProUGUI timeSurvivedDisplay;
    [SerializeField] private List<Image> chosenWeaponUI = new List<Image>(6);
    [SerializeField] private List<Image> chosenPassiveItemsUI = new List<Image>(6);

    [Header("StopWatch")]
    [SerializeField] private float timeLimit;
    private float stopWatchTime; // current time elapsed
    [SerializeField] private TextMeshProUGUI stopWatchDisplay;


    public bool isGameOver = false;

    public bool choosingUpgrade;

    [SerializeField] GameObject playerObject;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("EXTRA" + this + "DELETED");
            Destroy(gameObject);
        }

        DisableScreen();
    }

    private void Start()
    {
        SaveManager.Load();
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameState.Gameplay:
                CheckForPauseAndResume();
                UpdateStopWatch();
                break;
            case GameState.Paused:
                CheckForPauseAndResume();
                break;
            case GameState.GameOver:
                if (!isGameOver)
                {
                    isGameOver = true;
                    Time.timeScale = 0f; //Stops the time after game over

                    Debug.Log("GAME OVER");
                    DisplayResults();
                }
                break;
            case GameState.LevelUp:
                if(!choosingUpgrade)
                {
                    choosingUpgrade = true;
                    Time.timeScale = 0f;

                    Debug.Log("Upgrades shown");

                    levelUpScreen.SetActive(true);
                }
                break;
            default:
                Debug.LogWarning("State does not exit");
                break;

        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

   public void PauseGame()
   {
        if(currentState != GameState.Paused)
        {
            previousState = currentState;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f;

            pauseScreen.SetActive(true);

            Debug.Log("Game is Paused");
        }
        
   }

    public void ResumeGame()
    {
        if(currentState == GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1f;

            pauseScreen.SetActive(false);

            Debug.Log("Game is resumed");
        }
    }

    private void CheckForPauseAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void DisableScreen()
    {
        pauseScreen.SetActive(false);
        resultScreen.SetActive(false);
        levelUpScreen.SetActive(false);
    }

    public void GameOver()
    {
        timeSurvivedDisplay.text = stopWatchDisplay.text;

        ChangeState(GameState.GameOver);
    }

    private void DisplayResults()
    {
        resultScreen.SetActive(true);
    }

    public void AssignChosenCharacterUI(PlayerSO chosenPlayerSO)
    {
        chosenCharacterImage.sprite = chosenPlayerSO.Icon;
        chosenCharacterName.text = chosenPlayerSO.Name;
    }

    public void AssignLevelReached(int levelReached)
    {
        levelReachedDisplay.text = levelReached.ToString();
    }

    public void AssignChosenWeaponAndPassiveItemUI(List<Image> chosenWeaponData,List<Image> chosenPassiveItemsData)
    {
        if (chosenWeaponData.Count != chosenWeaponUI.Count || chosenPassiveItemsData.Count != chosenPassiveItemsUI.Count)
        {
            Debug.Log("Lists have different lengths");
            return;
        }

        for (int i = 0; i < chosenWeaponUI.Count; i++)
        {
            if (chosenWeaponData[i].sprite)
            {
                chosenWeaponUI[i].enabled = true;
                chosenWeaponUI[i].sprite = chosenWeaponData[i].sprite;
            }
            else
            {
                chosenWeaponUI[i].enabled = false;
            }
        }

        for (int i = 0; i < chosenPassiveItemsUI.Count; i++)
        {
            if (chosenPassiveItemsData[i].sprite)
            {
                chosenPassiveItemsUI[i].enabled = true;
                chosenPassiveItemsUI[i].sprite = chosenPassiveItemsData[i].sprite;
            }
            else
            {
                chosenPassiveItemsUI[i].enabled = false;
            }
        }

    }

    private void UpdateStopWatch()
    {
        stopWatchTime += Time.deltaTime;

        UpdateStopWatchDisplay();

        if(stopWatchTime >= timeLimit)
        {
            playerObject.SendMessage("Kill");
        }
    }

    private void UpdateStopWatchDisplay()
    {
        int minutes = Mathf.FloorToInt(stopWatchTime / 60);
        int seconds = Mathf.FloorToInt(stopWatchTime % 60);

        stopWatchDisplay.text = string.Format("{0:00}:{1:00}" , minutes, seconds);

    }

    public void StartLevelUp()
    {
        ChangeState(GameState.LevelUp);

        playerObject.SendMessage("RemoveAndApplyUpgrade");
    }

    public void EndLevelUp()
    {
        choosingUpgrade = false;
        Time.timeScale = 1.0f;
        levelUpScreen.SetActive(false);

        ChangeState(GameState.Gameplay);
    }
}
