using UnityEngine;
using System.Collections.Generic;

public class Player {


    public string playerName;
    public Color playerColour;

    public Dictionary<TileType, int> resourceList;

    public Player(string name) {

        playerName = name;

        resourceList = new Dictionary<TileType, int>();
        resourceList.Add(TileType.clay, 0);
        resourceList.Add(TileType.sheep, 0);
        resourceList.Add(TileType.stone, 0);
        resourceList.Add(TileType.wheat, 0);
        resourceList.Add(TileType.wood, 0);

    }

    public override string ToString() {

        return playerName;
    }


}
