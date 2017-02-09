using UnityEngine;
using UnityEngine.UI;

public class EdgeTile : MonoBehaviour {


    public Text infoText;
    public GameLoop gameController;
    public AssetLibrary assetLibrary;
    public Edge edge;


    void Start () {
        infoText = GameObject.Find("Info_Panel_Text").GetComponent<Text>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameLoop>();
        assetLibrary = GameObject.FindGameObjectWithTag("AssetLibrary").GetComponent<AssetLibrary>();
    }

    public void buildRoad()
    {
        edge.isOccupied = true;
        edge.currRoadType = RoadType.road;
        edge.owner = gameController.currPlayer;
        GameObject go = (GameObject)Instantiate(assetLibrary.roadPrefab, this.transform.position, this.transform.rotation);
        go.transform.name = this.transform.name;
        go.transform.parent = this.transform.parent;
        go.transform.GetComponent<MeshRenderer>().material.color = gameController.currPlayer.playerColour;
        edge.edgeTile = go.GetComponent<EdgeTile>();
        edge.edgeTile.edge = this.edge;
        Destroy(this.gameObject);
    }

    public void OnMouseUp() {
        if (edge.currRoadType == RoadType.unbuilt) {
            if (checkBuildable()) {
                buildRoad();
            }
        }
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

    private bool checkBuildable() {

        bool tempBool = false;

        foreach (Point lPoint in edge.edgePoints) {
            if ((lPoint.isOccupied == true  && lPoint.owner == gameController.currPlayer)) {
                tempBool = true;
            }
            
            foreach (Edge lEdge in lPoint.pointEdges) {
                if (lEdge.isOccupied == true && lEdge.owner == gameController.currPlayer && lPoint.isOccupied != true) {
                    tempBool = true;
                }
            }
        }

        return tempBool;

    }

}
