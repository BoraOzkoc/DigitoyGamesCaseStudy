using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class WarningTextController : MonoBehaviour
{
    public static WarningTextController Instance;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private TextMeshProUGUI warningText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        warningText
            .transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
        ClosePanel();
    }

    public void GiveWarning(string text, bool isGreen = false)
    {
        if (isGreen)
            warningText.color = Color.green;
        else
            warningText.color = Color.red;
        Activate();
        StartCoroutine(WarningCoroutine(text));
    }

    IEnumerator WarningCoroutine(string newText)
    {
        warningText.text = newText;

        yield return new WaitForSeconds(3);

        ClosePanel();
    }

    private void ClosePanel()
    {
        Deactivate();
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
