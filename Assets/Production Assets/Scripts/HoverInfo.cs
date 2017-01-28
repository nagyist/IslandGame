using UnityEngine;
using UnityEngine.UI;

public class HoverInfo : MonoBehaviour {

    public Text infoText;

    public void Start() {

        infoText = GameObject.Find("Info_Panel_Text").GetComponent<Text>();

    }

    public void OnMouseEnter() {

        infoText.text = gameObject.name;

    }

    public void OnMouseExit() {

        infoText.text = "";
    }

}
