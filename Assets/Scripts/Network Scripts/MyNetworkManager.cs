using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    public override void OnStartServer()
    {
        Debug.Log("Server started!");
    }

    public override void OnStopServer()
    {
        Debug.Log("Server stopped!");
    }

    public virtual void OnClientConnect(NetworkConnection connection)
    {
        Debug.Log("Connected to server!");
    }

    public virtual void OnClientDisconnect(NetworkConnection connection)
    {
        Debug.Log("Disconnected from server!");
    }

}


