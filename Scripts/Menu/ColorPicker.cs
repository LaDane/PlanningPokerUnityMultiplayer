using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour {

    public Image image;

    private float red = 1f;
    private float green = 1f;
    private float blue = 1f;

    private Color currentColor;

    private void Start() {
        SetColor();
    }

    public void SetRed(float r) {
        red = r;
        SetColor();
    }
    public void SetGreen(float g) {
        green = g;
        SetColor();
    }
    public void SetBlue(float b) {
        blue = b;
        SetColor();
    }

    public void SetColor() {
        currentColor = new Color(red, green, blue, 1f);
        image.color = currentColor;
        LobbyScene.Instance.SetPlayerColor(currentColor);
    }
}
