using UnityEngine;
using UnityEngine.UI;

public class HexTile : MonoBehaviour {

    public Hex hex;
    public Text infoText;

    public void Start() {
        infoText = GameObject.Find("Info_Panel_Text").GetComponent<Text>();
    }

    public void OnMouseEnter() {
        infoText.text = hex.tileType.ToString() + "\n" + "Tile number: " + hex.tileNumber.ToString();
    }

    public void OnMouseExit() {
        infoText.text = "";
    }

}
