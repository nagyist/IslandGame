using UnityEngine;
using System.Collections.Generic;

public class GameLoop : MonoBehaviour {

    private GameBoard gameBoard;
    public Player currPlayer;
    public List<Player> playerList;

    void Start() {

        gameBoard = GameObject.FindGameObjectWithTag("GameBoard").GetComponent<GameBoard>();
        playerList = new List<Player>();

        createPlayers();

    }

	void Update () {

        if (Input.GetKey("escape"))
            Application.Quit();

        if (Input.GetKeyUp("t"))
            startTurn();

    }

    public void createPlayers() {

        Player player01 = new Player("James");
        playerList.Add(player01);
        Player player02 = new Player("Stefan");
        playerList.Add(player02);
        Player player03 = new Player("Tom");
        playerList.Add(player03);
        Player player04 = new Player("Alex");
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

    }                   //Temporarily started.  Need to define players objects, currently have multiple player 01s.

}
