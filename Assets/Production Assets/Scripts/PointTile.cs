using UnityEngine;
using UnityEngine.UI;

public class PointTile : MonoBehaviour {

    public Text infoText;
    public GameLoop gameController;
    public AssetLibrary assetLibrary;
    public Point point;


    void Start() {
        infoText = GameObject.Find("Info_Panel_Text").GetComponent<Text>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameLoop>();
        assetLibrary = GameObject.FindGameObjectWithTag("AssetLibrary").GetComponent<AssetLibrary>();
    }
    
    public void buildTown() {
        point.isOccupied = true;
        point.currCityType = CityType.town;
        point.owner = gameController.currPlayer;
        GameObject go = (GameObject)Instantiate(assetLibrary.townPrefab, point.location, new Quaternion());
        go.transform.name = this.transform.name;
        point.pointTile = go.GetComponent<PointTile>();
        point.pointTile.point = this.point;
        Destroy(this.gameObject);
    }

    public void buildCity() {
        point.isOccupied = true;
        point.currCityType = CityType.city;
        point.owner = gameController.currPlayer;
        GameObject go = (GameObject)Instantiate(assetLibrary.cityPrefab, point.location, new Quaternion());
        go.transform.name = this.transform.name;
        point.pointTile = go.GetComponent<PointTile>();
        point.pointTile.point = this.point;
        Destroy(this.gameObject);
    }

    public void OnMouseUp() {
        if (point.currCityType == CityType.unbuilt) {
            if (checkBuildable()) {
                buildTown();
            }
        }
        else if (point.currCityType == CityType.town) {
            buildCity();
        }
    }

    public void OnMouseEnter()
    {
        if (point.owner != null)
        {
            infoText.text = gameObject.name + "\n" + "Owner: " + point.owner.playerName + "\n" + point.currCityType;
        }
        else {
            infoText.text = gameObject.name + "\n" + "Unoccupied" + "\n" + point.currCityType;
        }
    }

    public void OnMouseExit()
    {
        infoText.text = "";
    }

    public bool checkBuildable() {

        bool tempBool = true;

        foreach (Edge pEdge in point.pointEdges) {
            foreach (Point ePoint in pEdge.edgePoints) {
                if (ePoint.isOccupied == true) {
                    tempBool = false;
                }
            }
        }

        return tempBool;
    }

}
