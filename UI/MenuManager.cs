using System.Collections;
using UnityEngine;
using System;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject timeOverMenu;
    public GameStateManager stateManager;
    private MenuAnimations mainMenuAnim;
    public bool menuEnabled {get; private set;} = true;
    public bool pauseEnabled {get; private set;} = false;
    public bool timeOverEnabled {get; private set;} = false;
    public bool newGameStarted {get; private set;} = false;
    public bool mainMenuLoad {get; private set;} = false;
    public bool ressurecting {get; private set;} = false;
    public event Action OnPauseEnabled;

    private void Awake() 
    {
        if(instance == null)
            instance = this;
        else    
            Destroy(gameObject);
    }

    private void OnEnable() 
    {
        mainMenuAnim = mainMenu.GetComponent<MenuAnimations>();
        stateManager = FindObjectOfType<GameStateManager>();
    }

    private void Start() 
    {
        menuEnabled = true;
        pauseEnabled = false;
    }

    public void GameStart()
    {
        mainMenuAnim.HideMainMenu();
        newGameStarted = true;
    }

    public void ButtonClickSound()
    {
        AudioManager.instance.Play("MenuClick");
    }

    #region MainMenu
    public void DisableMainMenu(float animationTime)
    {
        StartCoroutine(mainDisable(animationTime));
    }
    public void EnableMainMenu(float animationTime)
    {
        StartCoroutine(mainEnable(animationTime));
        newGameStarted = false;
    }
    IEnumerator mainDisable(float animationTime)
    {
        yield return new WaitForSeconds(animationTime);
        menuEnabled = false;
    }

    IEnumerator mainEnable(float animationTime)
    {
        yield return new WaitForSeconds(animationTime);
        menuEnabled = true;
        mainMenuLoad = false;
    }
    #endregion

    #region Pause
    public void MainMenuLoadFromPause()
    {
        DisablePauseMenu();
        mainMenuAnim.ShowMainMenu();
        mainMenuLoad = true;
    }
    public void EnablePauseMenu()
    {
        if(stateManager.State.GetType() == typeof(PlayState) || stateManager.State.GetType() == typeof(SummaryState))
        {
            mainMenuAnim.ShowInGameMenu(pauseMenu.GetComponent<InGameMenu>());
            OnPauseEnabled?.Invoke();
            pauseEnabled = true;
        }
    }

    public void DisablePauseMenu()
    {
        mainMenuAnim.HideInGameMenu(pauseMenu.GetComponent<InGameMenu>());
        pauseEnabled = false;
    }
    #endregion

    #region TimeOver
    public void MainMenuLoadFromTO()
    {
        DisableTimeOverMenu();
        mainMenuAnim.ShowMainMenu();
        mainMenuLoad = true;
    }
    public void EnableTimeOverMenu()
    {
        mainMenuAnim.ShowInGameMenu(timeOverMenu.GetComponent<InGameMenu>());
        timeOverMenu.GetComponent<TimeOverMenu>().CheckMedals();
        timeOverMenu.GetComponent<TimeOverMenu>().CheckHighscore();
        timeOverEnabled = true;
    }

    public void DisableTimeOverMenu()
    {
        mainMenuAnim.HideInGameMenu(timeOverMenu.GetComponent<InGameMenu>());
        timeOverEnabled = false;
    }

    IEnumerator RessurectionRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        ressurecting = false;
    }
    #endregion

    public void ExitButtonClick()
    {
        Application.Quit();
    }
}
