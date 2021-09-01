using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHighlight : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, ISelectHandler {

    private Vector3 orginalPos;
    private Image image;
    private Color imageOriginalColor;
    private Color pressedColor = new Color32(217, 217, 217, 100);

    void Start() {
        orginalPos = transform.position;
        image = GetComponent<Image>();
        imageOriginalColor = image.color;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Vector3 highlightPos = new Vector3(orginalPos.x + 10f, orginalPos.y - 10f, orginalPos.z);
        transform.position = highlightPos;
    }

    public void OnPointerExit(PointerEventData eventData) {
        transform.position = orginalPos;
        image.color = imageOriginalColor;
    }

    public void OnSelect(BaseEventData eventData) {
        // image.color = pressedColor;
    }
}
