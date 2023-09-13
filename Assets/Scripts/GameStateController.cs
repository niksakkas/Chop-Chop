using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    private static GameStateController _instance;
    public static GameStateController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameStateController>();
            }
            return _instance;
        }
    }

    public GameState gameState;

}

public enum GameState
{
    chopping,
    selecting
}
