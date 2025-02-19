using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScreenController : MonoBehaviour
{
    [SerializeField]
    private Transform deckTransform;

    [SerializeField]
    private ProfileController profileController;

    [SerializeField]
    private TextMeshProUGUI betText;

    [SerializeField]
    private CanvasGroup canvasGroup;

    public void Start()
    {
        GameManager.OnGameStart += HandleOnGameStart;
    }

    public void OnDestroy()
    {
        GameManager.OnGameStart -= HandleOnGameStart;
    }

    private void HandleOnGameStart(int playerCount, int gameBet)
    {
        Debug.Log("bet amount :" + gameBet);
        betText.text = "Bet: " + gameBet.ToString();
    }

    public Transform GetDeckTransform()
    {
        return deckTransform;
    }

    public void Activate()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void Deactivate()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
