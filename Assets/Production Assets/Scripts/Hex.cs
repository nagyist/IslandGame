using UnityEngine;
using System.Collections.Generic;

public class Hex {

    public List<Hex> hexNeighbours = new List<Hex>();
    public List<Point> hexPoints = new List<Point>();
    public List<Edge> hexEdges = new List<Edge>();

    public Vector3 location;    

    public Hex(Point point01, Point point02, Point point03, Point point04, Point point05, Point point06) {

        hexPoints.Add(point01);
        hexPoints.Add(point02);
        hexPoints.Add(point03);
        hexPoints.Add(point04);
        hexPoints.Add(point05);
        hexPoints.Add(point06);

        findEdges();
        calcLocation();
    }

    private void findEdges() {
        foreach (Point point in hexPoints) {
            foreach (Edge edge in point.pointEdges) {
                if (hexPoints.Contains(edge.edgePoints[0]) && hexPoints.Contains(edge.edgePoints[1])) {
                    if (!hexEdges.Contains(edge)) {
                        hexEdges.Add(edge);
                        edge.edgeHexes.Add(this);
                    }
                }
            }
        }
    }

    private void calcLocation() {

        Vector3 tempLoc = Vector3.zero;

        foreach (Point point in hexPoints) {
            tempLoc += point.location;
        }

        location = tempLoc / hexPoints.Count;
    }

}
