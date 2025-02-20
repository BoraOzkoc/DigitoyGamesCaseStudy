using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    [SerializeField]
    private ProfileStatsController profileStatsController;

    [SerializeField]
    private CreateTableController createTableController;

    [SerializeField]
    private GameScreenController gameScreenController;

    [SerializeField]
    private TableOptionController tableOptionController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameScreenController GetGameScreenController()
    {
        return gameScreenController;
    }

    private void Start()
    {
        GameManager.OnGameStart += HandleOnGameStart;
        DeactivateGameScreen();
    }

    private void OnDestroy()
    {
        GameManager.OnGameStart -= HandleOnGameStart;
    }

    public TableOptionController GetTableOptionController()
    {
        return tableOptionController;
    }

    private void HandleOnGameStart(int playerCount, int gameBet)
    {
        DeactivateProfilePanel();
        DeactivateCreateTablePanel();
        gameScreenController.Activate();
        tableOptionController.Activate();
    }

    public ProfileStatsController GetProfileStatsController()
    {
        return profileStatsController;
    }

    public CreateTableController GetCreateTableController()
    {
        return createTableController;
    }

    private void DeactivateProfilePanel()
    {
        profileStatsController.Deactivate();
    }

    private void DeactivateCreateTablePanel()
    {
        createTableController.Deactivate();
    }

    private void DeactivateGameScreen()
    {
        gameScreenController.Deactivate();
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
