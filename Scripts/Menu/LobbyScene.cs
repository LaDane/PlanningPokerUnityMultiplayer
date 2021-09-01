using UnityEngine;
using MLAPI;
using MLAPI.Transports.UNET;
using MLAPI.SceneManagement;

public class LobbyScene : MonoSingleton<LobbyScene> {

    public string playerName = "";
    public string ipAddress = "127.0.0.1";
    public string port = "9998";
    public Color playerColor = Color.white;

    private UNetTransport transport;
    private MenuChange menuChange;

    // Lobby UI
    [SerializeField] public Transform playerCardsContainer;
    [SerializeField] public GameObject playerLobbyCardPrefab; 
    [SerializeField] public GameObject lobbyStartButton;

    void Start() {
        menuChange = GameObject.FindWithTag("MenuChange").GetComponent<MenuChange>();
    }

    public void OnMainHostButton() {
        menuChange.EnableMenu("Lobby");

        transport = NetworkManager.Singleton.GetComponent<UNetTransport>();
        transport.ConnectPort = int.Parse(port);
        transport.ServerListenPort = int.Parse(port);

        NetworkManager.Singleton.StartHost();
    }

    public void OnMainConnectButton() {
        menuChange.EnableMenu("Lobby");

        transport = NetworkManager.Singleton.GetComponent<UNetTransport>();
        transport.ConnectAddress = ipAddress;
        transport.ConnectPort = int.Parse(port);
        transport.ServerListenPort = int.Parse(port);

        NetworkManager.Singleton.StartClient();
    }

    public void OnMainStartGameButton() {
        Debug.Log("Start Game Button Pressed");
        NetworkSceneManager.SwitchScene("Game");
    }

    public void onMainBackButton() {
        NetworkManager.Singleton.Shutdown();
        menuChange.EnableMenu("Start");
    }

    // Input fields
    public void OnSetPlayerName(string newPlayerName) {
        this.playerName = newPlayerName;
    }

    public void OnSetIPAddress(string newAddress) {
        this.ipAddress = newAddress;
    }

    public void OnSetPort(string newPort) {
        this.port = newPort;
    }

    public void SetPlayerColor(Color color) {
        this.playerColor = color;
    }
}
