using UnityEngine;
using System.Collections.Generic;

public class GameBoard : MonoBehaviour {

    public GameObject townLocationGeo;
    public GameObject roadLocationGeo;
    public GameObject hexLocationGeo;

    private GameObject gameBoard;

    List<Point> pointList;
    List<Edge> edgeList;
    List<Hex> hexList;

    public void Start() {

        pointList = new List<Point>();
        edgeList = new List<Edge>();
        hexList = new List<Hex>();

        gameBoard = this.gameObject;

        initializePoints();
        initializeEdges();
        initializeHexes();

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
        }
    }

    //Place a gameobject on the board for each hex
    private void initializeHexLocations() {
        foreach (Hex hex in hexList) {
            GameObject go = (GameObject)Instantiate(hexLocationGeo, hex.location, new Quaternion());
        }
    }

}


