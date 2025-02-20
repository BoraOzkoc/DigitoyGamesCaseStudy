using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : HandController
{
    private bool isActive = false;

    public void SetActive()
    {
        isActive = true;
    }
}
