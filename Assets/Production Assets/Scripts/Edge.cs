using UnityEngine;
using System.Collections.Generic;

public class Edge {

    public EdgeTile edgeTile;
    public bool isOccupied = false;
    public Player owner = null;
    public RoadType currRoadType = RoadType.unbuilt;

    public List<Point> edgePoints = new List<Point>();
    public List<Hex> edgeHexes = new List<Hex>();

    public Edge(Point pointOne, Point pointTwo) {
        edgePoints.Add(pointOne);
        edgePoints.Add(pointTwo);
    }

    public override string ToString()
    {
        return edgePoints[0].ToString() + ", " + edgePoints[1].ToString();
    }

}
