using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class MouseController : MonoBehaviour
{
    Tilemap tilemap;
    void Start()
    {
        tilemap = MapManager.Instance.tilemap;
        Vector2Int startPos = findPosinMap(gameObject.transform.position); 
        moveController(startPos); 
        Debug.Log("Snap to Position");
    }

    public void moveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2Int pos2d = findPosinMap(gameObject.transform.position) + Vector2Int.CeilToInt(context.ReadValue<Vector2>());
            if (!MapManager.Instance.map.ContainsKey(pos2d))
            {
                Debug.Log("No Tile Found");
                return; 
            }
            moveController(pos2d);
        }
    }

    public void moveController(Vector2Int pos){ gameObject.transform.position = MapManager.Instance.map[pos].transform.position; }

    public Vector2Int findPosinMap(Vector3 worldPos)
    {
        Vector3Int pos3d = tilemap.WorldToCell(worldPos); 
        return new Vector2Int(pos3d.x, pos3d.y); 
    }
}
