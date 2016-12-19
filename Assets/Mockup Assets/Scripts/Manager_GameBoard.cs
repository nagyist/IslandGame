using UnityEngine;
using System;
using System.Collections.Generic;

public class Manager_GameBoard : MonoBehaviour {

    public GameObject hex_wood;
    public GameObject hex_wheat;
    public GameObject hex_stone;
    public GameObject hex_clay;
    public GameObject hex_sheep;
    public GameObject hex_desert;
    public GameObject edgePrefab;

    public GameObject[] hexList;
    public GameObject[] edgeList; 
    
	// Use this for initialization
	void Start () {

        createBoard();
        shuffleBoard();

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.S)) {
            shuffleBoard();
        }
	}

    void createBoard() {

        hexList = new GameObject[19];
        edgeList = new GameObject[6];

        hexList[0] = (GameObject)Instantiate(hex_wood, new Vector3(0, 0, 0), Quaternion.identity);
        hexList[1] = (GameObject)Instantiate(hex_wood, new Vector3(0, 0, 1.6092f), Quaternion.identity);
        hexList[2] = (GameObject)Instantiate(hex_wood, new Vector3(0, 0, 3.2184f), Quaternion.identity);
        hexList[3] = (GameObject)Instantiate(hex_wood, new Vector3(0, 0, -1.6092f), Quaternion.identity);
        hexList[4] = (GameObject)Instantiate(hex_wheat, new Vector3(0, 0, -3.2184f), Quaternion.identity);
        hexList[5] = (GameObject)Instantiate(hex_wheat, new Vector3(-1.394f, 0, 0.8046f), Quaternion.identity);
        hexList[6] = (GameObject)Instantiate(hex_wheat, new Vector3(-1.394f, 0, 2.4138f), Quaternion.identity);
        hexList[7] = (GameObject)Instantiate(hex_wheat, new Vector3(-1.394f, 0, -0.8046f), Quaternion.identity);
        hexList[8] = (GameObject)Instantiate(hex_sheep, new Vector3(-1.394f, 0, -2.4138f), Quaternion.identity);
        hexList[9] = (GameObject)Instantiate(hex_sheep, new Vector3(1.394f, 0, 0.8046f), Quaternion.identity);
        hexList[10] = (GameObject)Instantiate(hex_sheep, new Vector3(1.394f, 0, 2.4138f), Quaternion.identity);
        hexList[11] = (GameObject)Instantiate(hex_sheep, new Vector3(1.394f, 0, -0.8046f), Quaternion.identity);
        hexList[12] = (GameObject)Instantiate(hex_clay, new Vector3(1.394f, 0, -2.4138f), Quaternion.identity);
        hexList[13] = (GameObject)Instantiate(hex_clay, new Vector3(-2.788f, 0, 0), Quaternion.identity);
        hexList[14] = (GameObject)Instantiate(hex_clay, new Vector3(-2.788f, 0, 1.6092f), Quaternion.identity);
        hexList[15] = (GameObject)Instantiate(hex_stone, new Vector3(-2.788f, 0, -1.6092f), Quaternion.identity);
        hexList[16] = (GameObject)Instantiate(hex_stone, new Vector3(2.788f, 0, 0), Quaternion.identity);
        hexList[17] = (GameObject)Instantiate(hex_stone, new Vector3(2.788f, 0, 1.6092f), Quaternion.identity);
        hexList[18] = (GameObject)Instantiate(hex_desert, new Vector3(2.788f, 0, -1.6092f), Quaternion.identity);
        edgeList[0] = (GameObject)Instantiate(edgePrefab, new Vector3(-1.39338f, 0, 3.8674f), Quaternion.identity);
        edgeList[1] = (GameObject)Instantiate(edgePrefab, new Vector3(2.6527f, 0, 3.141f), Quaternion.identity);
        edgeList[1].transform.Rotate(0, 60, 0);
        edgeList[2] = (GameObject)Instantiate(edgePrefab, new Vector3(4.0469f, 0, -0.727f), Quaternion.identity);
        edgeList[2].transform.Rotate(0, 120, 0);
        edgeList[3] = (GameObject)Instantiate(edgePrefab, new Vector3(1.4f, 0, -3.87f), Quaternion.identity);
        edgeList[3].transform.Rotate(0, 180, 0);
        edgeList[4] = (GameObject)Instantiate(edgePrefab, new Vector3(-2.644f, 0, -3.143f), Quaternion.identity);
        edgeList[4].transform.Rotate(0, 240, 0);
        edgeList[5] = (GameObject)Instantiate(edgePrefab, new Vector3(-4.048f, 0, 0.725f), Quaternion.identity);
        edgeList[5].transform.Rotate(0, 300, 0);

        foreach (GameObject hex in hexList) {
            hex.transform.parent = this.transform;
        }

        foreach (GameObject edge in edgeList)
        {
            edge.transform.parent = this.transform;
        }
    }

    public void shuffleBoard() {

        List<GameObject> resources = new List<GameObject>();

        for (int i = 0; i < 4; i ++) {
            resources.Add(hex_wood);
            resources.Add(hex_sheep);
            resources.Add(hex_wheat);
        }

        for (int i = 0; i < 3; i++)
        {
            resources.Add(hex_clay);
            resources.Add(hex_stone);
        }

        resources.Add(hex_desert);
        Shuffle(resources);

        for (int i = 0; i < hexList.Length; i++) { 

            Vector3 localPos = hexList[i].transform.position;
            Destroy(hexList[i]);
            hexList[i] = (GameObject)Instantiate(resources[0], localPos, Quaternion.identity);
            hexList[i].transform.parent = this.transform;
            resources.RemoveAt(0);

        }
    }

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

}
