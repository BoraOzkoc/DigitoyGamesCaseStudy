using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TableOptionController : MonoBehaviour
{
    [SerializeField]
    private Button toggleButton;

    [SerializeField]
    private Transform panel;

    [SerializeField]
    private CanvasGroup canvasGroup;
    private bool isOpen = true;

    public void Toggle()
    {
        if (isOpen)
        {
            ClosePanel();
        }
        else
        {
            OpenPanel();
        }
    }

    void Start()
    {
        Toggle();
        Deactivate();
    }

    public void Activate()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        ClosePanel();
    }

    public void Deactivate()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void OpenPanel()
    {
        isOpen = true;
        panel.DOLocalMoveX(180, 0.5f);
    }

    private void ClosePanel()
    {
        isOpen = false;
        panel.DOLocalMoveX(1100, 0.5f);
    }

    public void BackToLobby()
    {
        int betAmount = GameFlowController.Instance.GetCurrentBet();
        PlayerDataManager.Instance.SubtractPlayerScore(betAmount);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NewGame()
    {
        GameManager.Instance.ResetGame();
    }
}
