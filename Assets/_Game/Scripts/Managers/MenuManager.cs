using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GameManager.OnGameStart += HandleOnGameStart;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStart -= HandleOnGameStart;
    }

    private void HandleOnGameStart(int playerCount, int gameBet)
    {
        CloseMainMenu();
    }

    [SerializeField]
    private ProfileController profileController;

    [SerializeField]
    private CreateTableController createTableController;

    public ProfileController GetProfileController()
    {
        return profileController;
    }

    public CreateTableController GetCreateTableController()
    {
        return createTableController;
    }

    public void CloseMainMenu()
    {
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
