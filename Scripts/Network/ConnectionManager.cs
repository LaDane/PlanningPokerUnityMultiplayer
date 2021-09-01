using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Spawning;
using MLAPI.Transports.UNET;

public class ConnectionManager : MonoBehaviour {

    private string playerName = "";
    private string ipAddress = "127.0.0.1";
    private string port = "9998";
    
    private UNetTransport transport;

    public void Host() {
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkManager.Singleton.StartHost(Spawn(), Quaternion.identity);
    }

    private void ApprovalCheck(byte[] connectionData, ulong clientId, MLAPI.NetworkManager.ConnectionApprovedDelegate callback) {
        // Check the incomming data
        bool approve = System.Text.Encoding.ASCII.GetString(connectionData) == "123";
        callback(true, null, approve, Spawn(), Quaternion.identity);
    }
    
    public void Join() {
        transport = NetworkManager.Singleton.GetComponent<UNetTransport>();
        transport.ConnectAddress = ipAddress;
        transport.ConnectPort = int.Parse(port);

        NetworkManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes("123");
        NetworkManager.Singleton.StartClient();
    }

    private Vector3 Spawn() {
        return new Vector3(1, 1, 1);
    }

    public void SetPlayerName(string newPlayerName) {
        this.playerName = newPlayerName;
    }

    public void SetIPAddress(string newAddress) {
        this.ipAddress = newAddress;
    }

    public void SetPort(string newPort) {
        this.port = newPort;
    }

}
