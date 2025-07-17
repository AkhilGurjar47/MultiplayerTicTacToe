using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering;

public class GameVisualManager : NetworkBehaviour
{
    private const float GRID_SIZE = 2.45f;
    [SerializeField] private Transform crossPrefab;
    [SerializeField] private Transform circlePrefab;
    [SerializeField] private Transform lineCompletePrefab;

    private void Start()
    {
        GameManager.Instance.OnClickedOnGridPosition += GameManager_OnClickedOnGridPosition;
        GameManager.Instance.OnGameWin += GameManager_OnGameWin;
    }

    private void GameManager_OnGameWin(object sender, GameManager.OnGameWinEventArgs e)
    {
        float eulerZ = 0f;
        switch(e.line.orientation)
        {
            default:
            case GameManager.Orientation.Horizontal:
                eulerZ = 0f;
                break;
            case GameManager.Orientation.Vertical:
                eulerZ = 90f;
                break;
            case GameManager.Orientation.DiagonalA:
                eulerZ = 45f;
                break;
            case GameManager.Orientation.DiagonalB:
                eulerZ = -45f;
                break;

        }
        Transform lineCompleteTransform = 
            Instantiate(
                lineCompletePrefab, 
                GetGridWorldPosition(e.line.centerGridPosition.x, e.line.centerGridPosition.y),
                Quaternion.Euler(0f,0f,eulerZ)
            );
        lineCompleteTransform.GetComponent<NetworkObject>().Spawn(true);
    }

    private void GameManager_OnClickedOnGridPosition(object sender, GameManager.OnClickedOnGridPositionEventArgs e)
    {
        SpawnObjectRpc(e.x, e.y, e.playerType);
    }
    [Rpc(SendTo.Server)]
    private void SpawnObjectRpc(int x,int y, GameManager.PlayerType playerType)
    {
        Transform prefab;
        switch(playerType)
        {
            default:
            case GameManager.PlayerType.Cross:
                prefab = crossPrefab;
                break;
            case GameManager.PlayerType.Circle:
                prefab = circlePrefab;
                break;
        }
        Transform spawnCrossTransform = Instantiate(prefab, GetGridWorldPosition(x, y), Quaternion.identity);
        spawnCrossTransform.GetComponent<NetworkObject>().Spawn(true);
    }
    private Vector2 GetGridWorldPosition(int x,int y)
    {
        return new Vector2 (-GRID_SIZE + x * GRID_SIZE, -GRID_SIZE + y * GRID_SIZE);
    }
}
