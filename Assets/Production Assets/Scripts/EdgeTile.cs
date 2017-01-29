using UnityEngine;
using UnityEngine.UI;

public class EdgeTile : MonoBehaviour {


    public Text infoText;
    public Edge edge;


    void Start () {
        infoText = GameObject.Find("Info_Panel_Text").GetComponent<Text>();
    }

    public void OnMouseEnter() {
        if (edge.owner != null)
        {
            infoText.text = gameObject.name + "\n" + "Owner: " + edge.owner;
        }
        else {
            infoText.text = gameObject.name + "\n" + "Unoccupied";
        }
    }

    public void OnMouseExit() {
        infoText.text = "";
    }

}
