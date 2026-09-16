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

    public Unit[] playerUnits;
    public Unit[] enemyUnits;

    public GameState state; 

    void Start()
    {
        state = GameState.SetUp; 
    }

    void Awake()
    {
        if(_instance != null && _instance != this){ Destroy(gameObject); }
        else{ _instance = this; }
    }

    void Update()
    {
       
    }

    public void changeGameState(GameState newState)
    {
        state = newState; 
    }

}
