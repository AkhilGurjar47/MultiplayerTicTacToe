using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering;

public class GameVisualManager : MonoBehaviour
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
        Transform spawnCrossTransform = Instantiate(crossPrefab);
        spawnCrossTransform.GetComponent<NetworkObject>().Spawn(true);
        spawnCrossTransform.position = GetGridWorldPosition(e.x, e.y);

    }
    private Vector2 GetGridWorldPosition(int x,int y)
    {
        return new Vector2 (-GRID_SIZE + x * GRID_SIZE, -GRID_SIZE + y * GRID_SIZE);
    }
}
