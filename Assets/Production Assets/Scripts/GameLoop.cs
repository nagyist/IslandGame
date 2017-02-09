using UnityEngine;
using System.Collections.Generic;

public class GameLoop : MonoBehaviour {

    private GameBoard gameBoard;
    public Player currPlayer;
    public List<Player> playerList;
    public turnState currState;

    void Start() {

        gameBoard = GameObject.FindGameObjectWithTag("GameBoard").GetComponent<GameBoard>();
        playerList = new List<Player>();
        currState = turnState.setup;

        createPlayers();

    }

	void Update () {

        if (Input.GetKey("escape"))
            Application.Quit();

        if (Input.GetKeyUp("t"))
            startTurn();

        if (Input.GetKeyUp("g"))
            nextPlayerInOrder();

        if(Input.GetKeyUp("q"))
            Debug.Log(currPlayer.playerColour.ToString());

    }

    public void createPlayers() {

        Player player01 = new Player("James");
        player01.playerColour = Color.green;
        playerList.Add(player01);
        Player player02 = new Player("Stefan");
        player02.playerColour = Color.blue;
        playerList.Add(player02);
        Player player03 = new Player("Tom");
        player03.playerColour = Color.red;
        playerList.Add(player03);
        Player player04 = new Player("Alex");
        player04.playerColour = Color.magenta;
        playerList.Add(player04);

        currPlayer = playerList[0];

    }

    public int rollDice() {

        int tempTotal = 0;

        int dieOne = 0;
        int dieTwo = 0;

        dieOne = Random.Range(1, 6);
        dieTwo = Random.Range(1, 6);

        tempTotal = dieOne + dieTwo;

        return tempTotal;
    }

    public void allocateResources(Hex hex) {

        foreach (Point point in hex.hexPoints) {

            if (point.isOccupied) {

                if (point.currCityType == CityType.town) {
                    point.owner.resourceList[hex.tileType] += 1;
                }
                else if (point.currCityType == CityType.city) {
                    point.owner.resourceList[hex.tileType] += 2;
                }         

            }

        }

    }

    public void startTurn() {

        int turnRoll = rollDice();
        Debug.Log("Dice roll: " + turnRoll.ToString());

        if (turnRoll == 7) {
            //Players with more than 7 cards discard half, rounded down
            //Player moves the knight (block hex, steal from player)
        }
        else {

            foreach (Hex hex in gameBoard.hexList) {

                if (hex.tileNumber == turnRoll) {

                    allocateResources(hex);

                }

            }

        }

        foreach (KeyValuePair<TileType, int> kvp in currPlayer.resourceList) {
            Debug.Log(string.Format("Resource: {0}, Total: {1}", kvp.Key, kvp.Value));
        }

    }                   //Temporarily started.

    public void nextPlayerInOrder() {

        int playerNum = playerList.IndexOf(currPlayer);
        int numPlayers = playerList.Count;

        playerNum += 1;

        if (playerNum == numPlayers)
            playerNum = 0;

        currPlayer = playerList[playerNum];

    }

    public void nextPlayerReverseOrder()
    {

        int playerNum = playerList.IndexOf(currPlayer);
        int numPlayers = playerList.Count;

        playerNum -= 1;

        if (playerNum == 0)
            playerNum = numPlayers - 1;

        currPlayer = playerList[playerNum];

    }

}
