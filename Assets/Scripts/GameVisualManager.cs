using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering;

public class GameVisualManager : NetworkBehaviour
{
    private const float GRID_SIZE = 2.45f;
    [SerializeField] private Transform crossPrefab;
    [SerializeField] private Transform circlePrefab;

    private void Start()
    {
        GameManager.Instance.OnClickedOnGridPosition += GameManager_OnClickedOnGridPosition;
    }

    private void GameManager_OnClickedOnGridPosition(object sender, GameManager.OnClickedOnGridPositionEventArgs e)
    {
        SpawnObjectRpc(e.x, e.y);
    }
    [Rpc(SendTo.Server)]
    private void SpawnObjectRpc(int x,int y)
    {
        Transform spawnCrossTransform = Instantiate(crossPrefab, GetGridWorldPosition(x, y), Quaternion.identity);
        spawnCrossTransform.GetComponent<NetworkObject>().Spawn(true);
    }
    private Vector2 GetGridWorldPosition(int x,int y)
    {
        return new Vector2 (-GRID_SIZE + x * GRID_SIZE, -GRID_SIZE + y * GRID_SIZE);
    }
}
