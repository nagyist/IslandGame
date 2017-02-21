using UnityEngine;
using UnityEngine.EventSystems;

public class PointTile : MonoBehaviour, IPointerClickHandler {


    public GameLoop gameController;
    public AssetLibrary assetLibrary;
    public Point point;


    void Start() {

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameLoop>();
        assetLibrary = GameObject.FindGameObjectWithTag("AssetLibrary").GetComponent<AssetLibrary>();

    }


    public void OnPointerClick(PointerEventData eventData)
    {
        switch (gameController.gameState) {
            case GameStates.setupTurn01:

                if (point.currCityType == CityType.unbuilt && gameController.currPlayer.firstTown == null)
                {
                    if (checkBuildable())
                    {
                        GameObject go = (GameObject)Instantiate(assetLibrary.buildPanel, Camera.main.WorldToScreenPoint(point.location), new Quaternion());
                        go.transform.SetParent(GameObject.FindGameObjectWithTag("InfoUI").transform, false);
                        go.GetComponent<BuildPanel>().buildfunction = buildFirstTown;
                    }
                }

                break;
            case GameStates.setupTurn02:

                if (point.currCityType == CityType.unbuilt && gameController.currPlayer.secondTown == null)
                {
                    if (checkBuildable())
                    {
                        GameObject go = (GameObject)Instantiate(assetLibrary.buildPanel, Camera.main.WorldToScreenPoint(point.location), new Quaternion());
                        go.transform.SetParent(GameObject.FindGameObjectWithTag("InfoUI").transform, false);
                        go.GetComponent<BuildPanel>().buildfunction = buildSecondTown;
                    }
                }

                break;
            case GameStates.mainGame:

                if (point.currCityType != CityType.city) {
                    if (point.currCityType == CityType.unbuilt && gameController.currPlayer.checkHasRes(1,1,1,1,0)) {
                        if (checkBuildable()) {
                            GameObject go = (GameObject)Instantiate(assetLibrary.buildPanel, Camera.main.WorldToScreenPoint(point.location), new Quaternion());
                            go.transform.SetParent(GameObject.FindGameObjectWithTag("InfoUI").transform, false);
                            go.GetComponent<BuildPanel>().buildfunction = buildTown;
                        }
                    }
                    else if (point.currCityType == CityType.town && point.owner == gameController.currPlayer && gameController.currPlayer.checkHasRes(0,0,0,2,3)) {
                        GameObject go = (GameObject)Instantiate(assetLibrary.buildPanel, Camera.main.WorldToScreenPoint(point.location), new Quaternion());
                        go.transform.SetParent(GameObject.FindGameObjectWithTag("InfoUI").transform, false);
                        go.GetComponent<BuildPanel>().buildfunction = buildCity;
                    }
                }
                break;

        }        
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

    public void buildFirstTown()
    {

        point.isOccupied = true;
        point.currCityType = CityType.town;
        point.owner = gameController.currPlayer;
        GameObject go = (GameObject)Instantiate(assetLibrary.townPrefab, point.location, new Quaternion());
        go.transform.name = this.transform.name;
        go.transform.parent = this.transform.parent;
        go.transform.GetComponent<MeshRenderer>().material.color = gameController.currPlayer.playerColour;
        point.pointTile = go.GetComponent<PointTile>();
        point.pointTile.point = this.point;
        gameController.currPlayer.firstTown = this.point;
        Destroy(this.gameObject);

    }

    public void buildSecondTown()
    {

        point.isOccupied = true;
        point.currCityType = CityType.town;
        point.owner = gameController.currPlayer;
        GameObject go = (GameObject)Instantiate(assetLibrary.townPrefab, point.location, new Quaternion());
        go.transform.name = this.transform.name;
        go.transform.parent = this.transform.parent;
        go.transform.GetComponent<MeshRenderer>().material.color = gameController.currPlayer.playerColour;
        point.pointTile = go.GetComponent<PointTile>();
        point.pointTile.point = this.point;
        gameController.currPlayer.secondTown = this.point;
        Destroy(this.gameObject);

    }

    public void buildTown()
    {

        point.isOccupied = true;
        point.currCityType = CityType.town;
        point.owner = gameController.currPlayer;
        GameObject go = (GameObject)Instantiate(assetLibrary.townPrefab, point.location, new Quaternion());
        go.transform.name = this.transform.name;
        go.transform.parent = this.transform.parent;
        go.transform.GetComponent<MeshRenderer>().material.color = gameController.currPlayer.playerColour;
        point.pointTile = go.GetComponent<PointTile>();
        point.pointTile.point = this.point;
        Destroy(this.gameObject);

        gameController.currPlayer.deductCost(1, 1, 1, 1, 0);
    }

    public void buildCity()
    {

        point.isOccupied = true;
        point.currCityType = CityType.city;
        point.owner = gameController.currPlayer;
        GameObject go = (GameObject)Instantiate(assetLibrary.cityPrefab, point.location, new Quaternion());
        go.transform.name = this.transform.name;
        go.transform.parent = this.transform.parent;
        go.transform.GetComponent<MeshRenderer>().material.color = gameController.currPlayer.playerColour;
        point.pointTile = go.GetComponent<PointTile>();
        point.pointTile.point = this.point;
        Destroy(this.gameObject);

        gameController.currPlayer.deductCost(0, 0, 0, 2, 3);

    }   

}
