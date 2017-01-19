using UnityEngine;
using System.Collections.Generic;

public class Point {

    public int x;
    public int y;

    public float locX;
    public float locY;

    public List<Edge> pointEdges = new List<Edge>();

    public Point(int x, int y) {

        this.x = x;
        this.y = y;

        calcLocation();

    }

    public override string ToString()
    {
        return x + ", " + y;
    }

    private void calcLocation() {

        locX = x * 2;
        locY = ((float)y / 2) * 1.73205f;

        if ((float)y % 2 == 0) {
            if ((float)x % 2 == 0)
            {
                locX += 0.5f;
            } else {
                locX -= 0.5f;
            }
        }

        if (x > 3) {
            locX = locX - 1.0f;
        }

        if (x > 1) {
            locX = locX - 1.0f;
        }

    }

}
