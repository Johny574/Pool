using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private GameState currentState = GameState.AI;

    public enum GameState
    {
        MainMenu,
        PVP,
        AI
    }
}