using UnityEngine;
using System.Collections;

public class PlayerPhaseManager : MonoBehaviour
{
    public int validUnits;

    void OnEnable()
    {
        Debug.Log("Player Phase Manager Enabled");
        validUnits = GameManager.Instance.playerUnits.Length;
    }

    void Update()
    {
       
    }
}
