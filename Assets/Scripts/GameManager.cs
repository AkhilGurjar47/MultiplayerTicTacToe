using System;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler<OnClickedOnGridPositionEventArgs> OnClickedOnGridPosition;
    public class OnClickedOnGridPositionEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    public enum playerType
    {
        None,
        Cross,
        Circle,
    }

    private playerType localPlayerType;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Another GameManager");
        }
        Instance = this;
    }

    public override void OnNetworkSpawn()
    {
        
    }
    public void ClickedOnGridPosition(int x,int y)
    {
        Debug.Log("ClickedOnGridPosition" +x+","+y);
        OnClickedOnGridPosition?.Invoke(this, new OnClickedOnGridPositionEventArgs
        {
            x = x, 
            y = y
        });
    }
}
