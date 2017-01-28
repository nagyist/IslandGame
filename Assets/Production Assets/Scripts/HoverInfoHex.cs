using UnityEngine;
using UnityEngine.UI;

public class HoverInfoHex : MonoBehaviour {

    public Text infoText;

    public void Start() {
        infoText = GameObject.Find("Info_Panel_Text").GetComponent<Text>();
    }

    public void OnMouseEnter() {
        infoText.text = gameObject.name.ToString();
    }

    public void OnMouseExit() {
        infoText.text = "";
    }

}
