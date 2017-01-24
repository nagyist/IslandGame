using UnityEngine;
using System.Collections.Generic;

public class GameBoard : MonoBehaviour {

    public GameObject townLocationGeo;
    public GameObject roadLocationGeo;
    public static GameObject[] hexObjects;

    private GameObject gameBoard;

    List<Point> pointList;
    List<Edge> edgeList;
    List<Hex> hexList;

    public void Start() {

        pointList = new List<Point>();
        edgeList = new List<Edge>();
        hexList = new List<Hex>();

        hexObjects = Resources.LoadAll<GameObject>("HexPrefabs");

        gameBoard = this.gameObject;

        initializePoints();
        initializeEdges();
        initializeHexes();

        shuffleBoard();

        initializeTownLocations();
        initializeRoadLocations();
        initializeHexLocations();

    }

    //Find a specific point in the points list.
    public Point findPoint(int x, int y) {

        Point tempPoint = null;

        foreach(Point point in pointList) {
            if (point.x == x && point.y == y) {
                tempPoint = point;
            }
        }

        return tempPoint;
    }

    //Find if a point exists
    public bool pointExists(int x, int y)
    {

        bool tempBool = false;

        foreach (Point point in pointList)
        {
            if (point.x == x && point.y == y)
            {
                tempBool = true;
            }
        }

        return tempBool;

    }

    //Find an edge from two points
    public Edge findEdge(Point point01, Point point02) {

        Edge tempEdge = null;

        foreach (Edge edge in edgeList) {
            if (edge.edgePoints.Contains(point01) && edge.edgePoints.Contains(point02)) {
                tempEdge = edge;
            }
        }

        return tempEdge;
    }       //Not tested

    //Remove a point from the points list
    private void removePoint(int x, int y) {

        //Debug.Log("remove: " + x + ", " + y);

        Point tempPoint = null;

        foreach (Point point in pointList)
        {
            if (point.x == x && point.y == y)
            {
                tempPoint = point;
            }
        }

        pointList.Remove(tempPoint);
    }

    //Populate points list
    private void initializePoints() {

        for (int x = 0; x < 6; x++) {
            for (int y = 0; y < 11; y++) {
                pointList.Add(new Point(x, y));
            }
        }

        //Bottom Left
        removePoint(0, 0);
        removePoint(1, 0);
        removePoint(0, 1);

        //Bottom Right
        removePoint(5, 0);
        removePoint(4, 0);
        removePoint(5, 1);

        //Top Left
        removePoint(0, 10);
        removePoint(1, 10);
        removePoint(0, 9);

        //Top Right
        removePoint(5, 10);
        removePoint(4, 10);
        removePoint(5, 9);

        Debug.Log("Initialized " + pointList.Count + " points.");

    }

    //Populate edges list
    private void initializeEdges() {

        foreach (Point point in pointList) {
            if (pointExists(point.x, point.y + 1)) {
                Edge tempEdge = new Edge(findPoint(point.x, point.y), findPoint(point.x, point.y + 1));
                edgeList.Add(tempEdge);
                point.pointEdges.Add(tempEdge);
                findPoint(point.x, point.y + 1).pointEdges.Add(tempEdge);
            }
        }

        foreach (Point point in pointList)
        {
            if (pointExists(point.x + 1, point.y))
            {
                if (point.x % 2 == 0 && point.y % 2 == 0) {
                    Edge tempEdge = new Edge(findPoint(point.x, point.y), findPoint(point.x + 1, point.y));
                    edgeList.Add(tempEdge);
                    point.pointEdges.Add(tempEdge);
                    findPoint(point.x + 1, point.y).pointEdges.Add(tempEdge);
                }
                if (point.x % 2 != 0 && point.y % 2 != 0)
                {
                    Edge tempEdge = new Edge(findPoint(point.x, point.y), findPoint(point.x + 1, point.y));
                    edgeList.Add(tempEdge);
                    point.pointEdges.Add(tempEdge);
                    findPoint(point.x + 1, point.y).pointEdges.Add(tempEdge);
                }
            }
        }

        Debug.Log("Initialized " + edgeList.Count + " edges.");
    }

    //Populate hexes list
    private void initializeHexes() {

        foreach (Point point in pointList)
        {
            if (pointExists(point.x + 1, point.y) && pointExists(point.x, point.y + 2))
            {
                if (point.x % 2 == 0 && point.y % 2 == 0)
                {
                    hexList.Add(new Hex(findPoint(point.x, point.y),
                                        findPoint(point.x, point.y + 1),
                                        findPoint(point.x, point.y + 2),
                                        findPoint(point.x + 1, point.y),
                                        findPoint(point.x + 1, point.y + 1),
                                        findPoint(point.x + 1, point.y + 2)
                                        ));
                }
                if (point.x % 2 != 0 && point.y % 2 != 0)
                {
                    hexList.Add(new Hex(findPoint(point.x, point.y),
                                        findPoint(point.x, point.y + 1),
                                        findPoint(point.x, point.y + 2),
                                        findPoint(point.x + 1, point.y),
                                        findPoint(point.x + 1, point.y + 1),
                                        findPoint(point.x + 1, point.y + 2)
                                        ));
                }
            }
        }

        Debug.Log("Initialized " + hexList.Count + " hexes.");
    }

    //Place a gameobject on the board for each town location
    private void initializeTownLocations() {
        foreach (Point point in pointList) {
            GameObject go = (GameObject)Instantiate(townLocationGeo, point.location, new Quaternion());
            go.transform.name = "Point: " + point.ToString();
            go.transform.parent = gameBoard.transform;
        }
    }

    //Place a gameobject on the board for each road location
    private void initializeRoadLocations() {
        foreach (Edge edge in edgeList) {
            GameObject go = (GameObject)Instantiate(roadLocationGeo, new Vector3((edge.edgePoints[0].location.x + edge.edgePoints[1].location.x) / 2, 0, (edge.edgePoints[0].location.z + edge.edgePoints[1].location.z) / 2), new Quaternion());
            go.transform.parent = gameBoard.transform;
            go.transform.name = "Road: " + edge.edgePoints[0] +" to " + edge.edgePoints[1];
            go.transform.Rotate(new Vector3(0, bearing(edge.edgePoints[0].location, edge.edgePoints[1].location), 0));
        }
    }

    //Place a gameobject on the board for each hex
    private void initializeHexLocations() {
        foreach (Hex hex in hexList) {
            GameObject go = (GameObject)Instantiate(hexObjects[(int)hex.tileType], hex.location, new Quaternion());
            go.transform.name = "Hex: " + hex.tileType.ToString();
            go.transform.parent = gameBoard.transform;
        }
    }

    //Add a random tile type to each hex
    public void shuffleBoard()
    {

        List<TileType> resources = new List<TileType>();

        for (int i = 0; i < 4; i++)
        {
            resources.Add(TileType.wood);
            resources.Add(TileType.sheep);
            resources.Add(TileType.wheat);
        }

        for (int i = 0; i < 3; i++)
        {
            resources.Add(TileType.clay);
            resources.Add(TileType.stone);
        }

        resources.Add(TileType.desert);
        Shuffle(resources);

        for (int i = 0; i < hexList.Count; i++)
        {
            hexList[i].tileType = resources[0];
            resources.RemoveAt(0);
        }
    }

    //Randomize a list of objects
    public void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        System.Random rnd = new System.Random();
        while (n > 1)
        {
            int k = (rnd.Next(0, n) % n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    //Find bearing between two points
    float bearing(Vector3 point1, Vector3 point2) {

        const float TWOPI = 6.2831853071795865f;
        const float RAD2DEG = 57.2957795130823209f;

        if (point1 == point2) {
            return 0.0f;
        }

        float theta = Mathf.Atan2(point1.x - point2.x, point1.z - point2.z);

        if (theta < 0.0) {
            theta += TWOPI;
        }

        return RAD2DEG * theta;
    }

}


