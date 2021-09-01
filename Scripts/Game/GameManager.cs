using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Connection;
using MLAPI.Messaging;
// using MLAPI.NetworkVariable;

public class GameManager : NetworkBehaviour {
    
    [SerializeField] private GameObject pokerCard_0;
    [SerializeField] private GameObject pokerCard_05;
    [SerializeField] private GameObject pokerCard_1;
    [SerializeField] private GameObject pokerCard_2;
    [SerializeField] private GameObject pokerCard_3;
    [SerializeField] private GameObject pokerCard_5;
    [SerializeField] private GameObject pokerCard_8;
    [SerializeField] private GameObject pokerCard_13;
    [SerializeField] private GameObject pokerCard_20;
    [SerializeField] private GameObject pokerCard_40;
    [SerializeField] private GameObject pokerCard_100;
    [SerializeField] private GameObject pokerCard_Question;
    private GameObject[] pokerCards = new GameObject[12];

    // [SerializeField] private Camera playerCamera;

    private int numberOfPlayersAssigned = 0;

    private float cardStartX = 4.6f;
    private float cardOffset = -0.7f;

    // Networked fields
    // private NetworkVariableBool playerPokerCardsCreated = new NetworkVariableBool(new NetworkVariableSettings {
    //     WritePermission = NetworkVariablePermission.OwnerOnly,
    //     ReadPermission = NetworkVariablePermission.Everyone
    // });


    public override void NetworkStart() {
        AssignPokerCards();
        if (IsServer)
            SpawnAllPokerCards();
    }

    private void SpawnAllPokerCards() {
        int tableRotateAmount = 360 / NetworkManager.Singleton.ConnectedClients.Count;
        foreach(KeyValuePair<ulong, NetworkClient> nc in NetworkManager.Singleton.ConnectedClients) {
            PlayerController pc = nc.Value.PlayerObject.GetComponent<PlayerController>();
            pc.playerNetworkTableRotation.Value = tableRotateAmount * numberOfPlayersAssigned;

            float cardLastX = cardStartX;
            foreach(GameObject card in pokerCards) {
                GameObject pokerCard = GameObject.Instantiate(card);

                cardLastX += cardOffset;
                pokerCard.transform.position = new Vector3(cardLastX, 1f, 13f);
                pokerCard.transform.Rotate(-50f, 0, 0);
                pokerCard.transform.RotateAround(Vector3.zero, new Vector3(0f, 1f, 0f), tableRotateAmount * numberOfPlayersAssigned);

                pokerCard.GetComponent<NetworkObject>().SpawnWithOwnership(nc.Key);
                //Debug.Log(nc.Key);
            }
            pc.playerNetworkPokerCardsCreated.Value = true;
            numberOfPlayersAssigned++;
        }
    }

    private void AssignPokerCards() {
        pokerCards[0] = pokerCard_0;
        pokerCards[1] = pokerCard_05;
        pokerCards[2] = pokerCard_1;
        pokerCards[3] = pokerCard_2;
        pokerCards[4] = pokerCard_3;
        pokerCards[5] = pokerCard_5;
        pokerCards[6] = pokerCard_8;
        pokerCards[7] = pokerCard_13;
        pokerCards[8] = pokerCard_20;
        pokerCards[9] = pokerCard_40;
        pokerCards[10] = pokerCard_100;
        pokerCards[11] = pokerCard_Question;
    }
}
