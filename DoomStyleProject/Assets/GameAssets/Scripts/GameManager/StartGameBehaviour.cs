using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameBehaviour : IGameManagerBehaviour
{
    public void RunBehaviour(bool enabled, GameObject gameMenu)
    {
        gameMenu.SetActive(enabled);
    }
}