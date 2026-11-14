using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkGamePlayer : NetworkBehaviour
{
    [SyncVar] private string displayName = "Loading...";


    private NetworkManagerLobby room;

    public NetworkManagerLobby Room
    {
        get
        {
            if (room != null)
            {
                return room;
            }
            return room = NetworkManager.singleton as NetworkManagerLobby;
        }
    }

    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject); //if using rounds of games

        Room.GamePlayers.Add(this);
    }

    public override void OnStopClient()
    {
        Room.GamePlayers.Remove(this);
    }

    [Server]
    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }
}
