using UnityEngine;

public enum GameState
{
    SetUp,
    PlayerPhase, 
    EnemyPhase,
    AllyPhase
}

public class GameManager : MonoBehaviour
{
    public static GameManager _instance; 
    public static GameManager Instance {get {return _instance; } }

    public GameState state; 

    void Start()
    {
        state = GameState.SetUp; 
    }

    public void changeGameState(GameState newState)
    {
        state = newState; 
    }
}
