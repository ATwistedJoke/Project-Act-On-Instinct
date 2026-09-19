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

    public PlayerPhaseManager playerPhaseManager;
    public EnemyPhaseManager enemyPhaseManager;

    public Unit[] playerUnits;
    public Unit[] enemyUnits;

    public GameState state; 

     void Awake()
    {
        if(_instance != null && _instance != this){ Destroy(gameObject); }
        else{ _instance = this; }

        playerPhaseManager = gameObject.GetComponentInChildren<PlayerPhaseManager>();
        enemyPhaseManager = gameObject.GetComponentInChildren<EnemyPhaseManager>();
    }

    void Start()
    {
        state = GameState.SetUp; 
        playerPhaseManager.enabled = false;
        enemyPhaseManager.enabled = false;
    }


    void Update()
    {
       
    }

    public void changeGameState(GameState newState)
    {
        state = newState; 
    }

    public void SwitchState()
    {
        if(state == GameState.SetUp)
        {
            changeGameState(GameState.PlayerPhase);
            playerPhaseManager.enabled = true; 
            enemyPhaseManager.enabled = false;
        }
        else if (state == GameState.PlayerPhase)
        {
            changeGameState(GameState.EnemyPhase);
            playerPhaseManager.enabled = false; 
            enemyPhaseManager.enabled = true; 
        }
        else if (state == GameState.EnemyPhase)
        {
            changeGameState(GameState.PlayerPhase);
            enemyPhaseManager.enabled = false; 
            playerPhaseManager.enabled = true; 
        }
    }

    public void UnitUsed()
    {
        playerPhaseManager.validUnits -= 1; 
    }
}
