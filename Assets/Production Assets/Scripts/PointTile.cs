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
        gameObject.GetComponent<MeshFilter>().mesh = assetLibrary.townPrefab.GetComponent<MeshFilter>().sharedMesh;
        gameObject.transform.localScale = new Vector3(.1f, .1f, .1f);
    }

    public void buildCity() {
        point.isOccupied = true;
        point.currCityType = CityType.town;
        point.owner = gameController.currPlayer;
        gameObject.GetComponent<MeshFilter>().mesh = assetLibrary.cityPrefab.GetComponent<MeshFilter>().sharedMesh;
        gameObject.transform.localScale = new Vector3(.1f, .1f, .1f);
    }

    public void OnMouseUp() {
        if (point.currCityType == CityType.unbuilt) {
            buildTown();
        }
        else if (point.currCityType == CityType.unbuilt) {
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

}
