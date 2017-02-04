using UnityEngine;
using UnityEngine.UI;

public class EdgeTile : MonoBehaviour {


    public Text infoText;
    public GameLoop gameController;
    public Edge edge;


    void Start () {
        infoText = GameObject.Find("Info_Panel_Text").GetComponent<Text>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameLoop>();
    }

    public void buildRoad() {
        edge.isOccupied = true;
        edge.currRoadType = RoadType.road;
        edge.owner = gameController.currPlayer;
    }

    public void OnMouseUp() {
        buildRoad();
    }

    public void OnMouseEnter() {
        if (edge.owner != null)
        {
            infoText.text = gameObject.name + "\n" + "Owner: " + edge.owner.playerName + "\n" + edge.currRoadType;
        }
        else {
            infoText.text = gameObject.name + "\n" + "Unoccupied" + "\n" + edge.currRoadType;
        }
    }

    public void OnMouseExit() {
        infoText.text = "";
    }

}
