using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionManager : NetworkBehaviour {

    private PlayerController pc;
    
    private Transform hoveredTransform;
    private Card hoveredCard;

    private Card selectedCard;
    private Transform selectedTransform;
    private Outline selectedOutline;


    void Start() {
        pc = GetComponent<PlayerController>();
    }

    void Update() {
        if (pc.playerCam != null && pc.playerNetworkPokerCardsCreated.Value) {

        // HOVER
            // Lower card
            if (hoveredTransform != null && hoveredCard != null) {
                hoveredCard.raiseCard = false;
                hoveredTransform = null;
                hoveredCard = null;
            }

            var ray = pc.playerCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f)) {
                hoveredTransform = hit.transform;

                // Raise card (mouse is hovering over card)
                hoveredCard = hoveredTransform.GetComponent<Card>();
                if (hoveredCard != null && hoveredCard.IsOwner) {
                    hoveredCard.raiseCard = true;
                }
            }

        // SELECTED
            if (Input.GetMouseButtonDown(0)) {
                if (hoveredTransform != selectedTransform && selectedOutline != null && selectedCard != null) {
                    selectedCard.cardIsSelected = false;
                    selectedOutline.enabled = false;
                    selectedTransform = null;
                    selectedOutline = null;
                }

                if (hoveredCard != null && hoveredCard.IsOwner) {
                    selectedCard = hoveredCard;
                    selectedCard.cardIsSelected = true;
                    selectedTransform = hoveredTransform;
                    selectedOutline = hoveredTransform.GetComponent<Outline>();
                    selectedOutline.enabled = true;
                }
            }
        }
    }
}
