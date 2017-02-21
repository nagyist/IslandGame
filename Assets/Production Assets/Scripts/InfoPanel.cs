using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

    private GameLoop gameController;
    private Text infoText;

    private void Start() {

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameLoop>();
        infoText = GetComponentInChildren<Text>();

    }

    private void Update() {

        Player currPlayer = gameController.currPlayer;

        int clayCount;
        int woodCount;
        int sheepCount;
        int wheatCount;
        int stoneCount;

        currPlayer.resourceList.TryGetValue(TileType.clay, out clayCount);
        currPlayer.resourceList.TryGetValue(TileType.wood, out woodCount);
        currPlayer.resourceList.TryGetValue(TileType.sheep, out sheepCount);
        currPlayer.resourceList.TryGetValue(TileType.wheat, out wheatCount);
        currPlayer.resourceList.TryGetValue(TileType.stone, out stoneCount);

        infoText.text = "Player: " + currPlayer.playerName + "\n" + "Clay: " + clayCount + " Wood: " + woodCount + " Sheep: " + sheepCount + " Wheat: " + wheatCount + " Stone: " + stoneCount;

    }

}
