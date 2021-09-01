using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using System;

public class Card : NetworkBehaviour {

    public bool raiseCard = false;
    public bool cardIsSelected = false;

    private bool originalPositionSet = false;
    private Vector3 originalPosition;
    private Vector3 hoverPosition;
    private float lerpTime = 0.1f;
    private float yHoverPlus = 0.4f;

    void Update() {
        if (raiseCard || cardIsSelected) {
            if (!originalPositionSet) {
                originalPosition = transform.position;
                hoverPosition = new Vector3(originalPosition.x, originalPosition.y + yHoverPlus, originalPosition.z);
                originalPositionSet = true;
            }
            transform.position = Vector3.Lerp(transform.position, hoverPosition, lerpTime);
        }

        if (originalPositionSet && !raiseCard && !cardIsSelected && transform.position != originalPosition) {
            transform.position = Vector3.Lerp(transform.position, originalPosition, lerpTime);
        }
    }
}


//cardOwner = (int)NetworkObject.OwnerClientId;

