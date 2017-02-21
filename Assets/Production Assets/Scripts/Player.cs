using UnityEngine;
using System.Collections.Generic;

public class Player {


    public string playerName;
    public Color playerColour;

    public Point firstTown = null;
    public Point secondTown = null;

    public bool firstRoadBuilt = false;
    public bool secondRoadBuilt = false;

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

    public bool checkHasRes(int reqClay, int reqWood, int reqSheep, int reqWheat, int reqStone) {

        bool tempBool = false;

        int clayCount;
        int woodCount;
        int sheepCount;
        int wheatCount;
        int stoneCount;

        resourceList.TryGetValue(TileType.clay, out clayCount);
        resourceList.TryGetValue(TileType.wood, out woodCount);
        resourceList.TryGetValue(TileType.sheep, out sheepCount);
        resourceList.TryGetValue(TileType.wheat, out wheatCount);
        resourceList.TryGetValue(TileType.stone, out stoneCount);

        if (reqClay <= clayCount && reqWood <= woodCount && reqSheep <= sheepCount && reqWheat <= wheatCount && reqStone <= stoneCount) {
            tempBool = true;
        }

        return tempBool;

    }

    public void deductCost(int costClay, int costWood, int costSheep, int costWheat, int costStone) {

        resourceList[TileType.clay] -= costClay;
        resourceList[TileType.wood] -= costWood;
        resourceList[TileType.sheep] -= costSheep;
        resourceList[TileType.wheat] -= costWheat;
        resourceList[TileType.stone] -= costStone;

    }


}
