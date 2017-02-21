using UnityEngine;
using UnityEngine.EventSystems;

public class EdgeTile : MonoBehaviour, IPointerClickHandler {

    public GameLoop gameController;
    public AssetLibrary assetLibrary;
    public Edge edge;


    void Start () {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameLoop>();
        assetLibrary = GameObject.FindGameObjectWithTag("AssetLibrary").GetComponent<AssetLibrary>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {

        switch (gameController.gameState) {
            case GameStates.setupTurn01:

                if (edge.currRoadType == RoadType.unbuilt)
                {
                    if (checkBuildable() && nextToTown(gameController.currPlayer.firstTown) && !gameController.currPlayer.firstRoadBuilt)
                    {
                        GameObject go = (GameObject)Instantiate(assetLibrary.buildPanel, Camera.main.WorldToScreenPoint(edge.edgeTile.transform.position), new Quaternion());
                        go.transform.SetParent(GameObject.FindGameObjectWithTag("InfoUI").transform, false);
                        go.GetComponent<BuildPanel>().buildfunction = buildFirstRoad;
                    }
                }
                break;

            case GameStates.setupTurn02:

                if (edge.currRoadType == RoadType.unbuilt)
                {
                    if (checkBuildable() && nextToTown(gameController.currPlayer.secondTown) && !gameController.currPlayer.secondRoadBuilt)
                    {
                        GameObject go = (GameObject)Instantiate(assetLibrary.buildPanel, Camera.main.WorldToScreenPoint(edge.edgeTile.transform.position), new Quaternion());
                        go.transform.SetParent(GameObject.FindGameObjectWithTag("InfoUI").transform, false);
                        go.GetComponent<BuildPanel>().buildfunction = buildSecondRoad;
                    }
                }
                break;

            case GameStates.mainGame:

                if (edge.currRoadType == RoadType.unbuilt)
                {
                    if (checkBuildable() && gameController.currPlayer.checkHasRes(1, 1, 0, 0, 0))
                    {
                        GameObject go = (GameObject)Instantiate(assetLibrary.buildPanel, Camera.main.WorldToScreenPoint(edge.edgeTile.transform.position), new Quaternion());
                        go.transform.SetParent(GameObject.FindGameObjectWithTag("InfoUI").transform, false);
                        go.GetComponent<BuildPanel>().buildfunction = buildRoad;
                    }
                }
                break;
        }        
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

        gameController.currPlayer.deductCost(1, 1, 0, 0, 0);
    }

    public void buildFirstRoad()
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

        gameController.currPlayer.firstRoadBuilt = true;
    }

    public void buildSecondRoad()
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

        gameController.currPlayer.secondRoadBuilt = true;
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

    private bool nextToTown(Point town) {

        bool tempBool = false;

        foreach (Point point in edge.edgePoints) {

            if (point == town) {
                tempBool = true;
            }

        }

        return tempBool;

    }

}
