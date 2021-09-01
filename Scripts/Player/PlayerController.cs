using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Connection;
using MLAPI.Messaging;

public class PlayerController : NetworkBehaviour {

    private GameObject localPlayerCard;
    private Text playerNameText;
    private Image playerColorImage;
    
    [SerializeField] private Camera playerCameraObject;
    [HideInInspector] public Camera playerCam;
    private bool cameraCreated = false;


    // Networked fields
    private NetworkVariableString playerNetworkName = new NetworkVariableString(new NetworkVariableSettings {
        WritePermission = NetworkVariablePermission.OwnerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });

    public NetworkVariableColor playerNetworkColor = new NetworkVariableColor(new NetworkVariableSettings {
        WritePermission = NetworkVariablePermission.OwnerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });

    public NetworkVariableBool playerNetworkReady = new NetworkVariableBool(new NetworkVariableSettings {
        WritePermission = NetworkVariablePermission.OwnerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });

    public NetworkVariableBool playerNetworkPokerCardsCreated = new NetworkVariableBool(new NetworkVariableSettings {
        WritePermission = NetworkVariablePermission.ServerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });

    public NetworkVariableInt playerNetworkTableRotation = new NetworkVariableInt(new NetworkVariableSettings {
        WritePermission = NetworkVariablePermission.Everyone,
        ReadPermission = NetworkVariablePermission.Everyone
    });


    public override void NetworkStart() {
        if (SceneManager.GetActiveScene().name.Equals("MainMenu")) {

            // Set player names
            localPlayerCard = Instantiate(LobbyScene.Instance.playerLobbyCardPrefab, Vector3.zero, Quaternion.identity);
            localPlayerCard.transform.SetParent(LobbyScene.Instance.playerCardsContainer, false);
            
            playerNameText = localPlayerCard.GetComponentInChildren<Text>();

            if (IsOwner) {
                playerNetworkName.Value = LobbyScene.Instance.playerName;
                playerNameText.text = playerNetworkName.Value;
            }
            else {
                playerNameText.text = playerNetworkName.Value;
            }
            ListenPlayerNameChange();            
            LobbyScene.Instance.lobbyStartButton.SetActive(IsServer);

            playerColorImage = localPlayerCard.transform.GetChild(2).GetComponent<Image>();
        }
    }

    void Update() {
        if (SceneManager.GetActiveScene().name.Equals("MainMenu")) {

            // Set player colors
            if (IsOwner) {
                playerNetworkColor.Value = LobbyScene.Instance.playerColor;
                playerColorImage.color = playerNetworkColor.Value;
            }
            else {
                playerColorImage.color = playerNetworkColor.Value;
            }
            ListenPlayerColorChange();
        }

        if (SceneManager.GetActiveScene().name.Equals("Game")) {
            
            // Move playerController object and create camera
            if (playerNetworkPokerCardsCreated.Value && IsLocalPlayer && !cameraCreated) {
                transform.position = new Vector3(0f, 9f, 15f);
                CreateCamera();
                transform.RotateAround(Vector3.zero, new Vector3(0f, 1f, 0f), playerNetworkTableRotation.Value);
                cameraCreated = true;

                
            }
        }
    }

    public void OnDestroy() {
        Destroy(localPlayerCard);
    }

    //public void CreateGameManager() {
    //    if (IsServer) {
    //        GameObject go = Instantiate(GameScene.Instance.gameManagerPrefab);
    //        go.GetComponent<NetworkObject>().Spawn(destroyWithScene: true);
    //    }
    //}

    private void CreateCamera() {
        playerCam = Instantiate(playerCameraObject);
        playerCam.transform.position = transform.position;
        playerCam.transform.Rotate(50, 180, 0);
        playerCam.transform.parent = transform;
    }

    // Events
    // Must be used to update player cards when a new player joins the lobby
    void ListenPlayerNameChange() {
        playerNetworkName.OnValueChanged += PlayerNameValueChanged;
    }

    void PlayerNameValueChanged(string prevS, string newS) {
        playerNameText.text = newS;
    }

    void ListenPlayerColorChange() {
        playerNetworkColor.OnValueChanged += PlayerColorValueChanged;
    }

    void PlayerColorValueChanged(Color prevC, Color newC) {
        playerColorImage.color = newC;
    }
}
