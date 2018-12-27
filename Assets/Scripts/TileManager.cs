using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

	//public GameObject leftTilePrefab;

	//public GameObject topTilePrefab;

	public GameObject[] tilePrefabs;

	public GameObject currentTile;

	private static TileManager instance;

	private Stack<GameObject> leftTiles = new Stack<GameObject>();

	public Stack<GameObject> LeftTiles {
		get {
			return leftTiles;
		}
		set {
			leftTiles = value;
		}
	}

	private Stack<GameObject> topTiles = new Stack<GameObject>();

	public Stack<GameObject> TopTiles {
		get {
			return topTiles;
		}
		set {
			topTiles = value;
		}
	}

	public static TileManager Instance {
		get {
			if (instance == null) {
				instance = GameObject.FindObjectOfType<TileManager> ();
			}

			return instance;
		}
	}

	// Use this for initialization
	void Start () {
		CreateTiles (100);

		for (int i = 0; i < 50; i++) {
			SpawnTile ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CreateTiles(int amount)
	{
		for (int i = 0; i < amount; i++) {
			leftTiles.Push(Instantiate(tilePrefabs[0]));
			topTiles.Push(Instantiate(tilePrefabs[1]));
			topTiles.Peek().name = "TopTile";
			topTiles.Peek ().SetActive (false);
			leftTiles.Peek().name = "LeftTile";
			leftTiles.Peek ().SetActive (false);

		}
	}

	public void SpawnTile()
	{

		if (leftTiles.Count == 0 || topTiles.Count == 0) {
			CreateTiles (10);
		}

		int randomindex = Random.Range (0, 2);

		if (randomindex == 0) {
			GameObject tmp = leftTiles.Pop ();
			tmp.SetActive (true);
			tmp.transform.position = currentTile.transform.GetChild (0).transform.GetChild (randomindex).position;
			currentTile = tmp;
		}

		else if (randomindex == 1) {
			GameObject tmp = topTiles.Pop ();
			tmp.SetActive (true);
			tmp.transform.position = currentTile.transform.GetChild (0).transform.GetChild (randomindex).position;
			currentTile = tmp;
		} 

		int spawnPickup = Random.Range(0, 20);
		if (spawnPickup == 0) {
			currentTile.transform.GetChild (1).gameObject.SetActive (true);
		}
		//currentTile = (GameObject)Instantiate (tilePrefabs[randomindex], currentTile.transform.GetChild (0).transform.GetChild (randomindex).position, Quaternion.identity);
	}

	public void ResetGame(){
		Application.LoadLevel (Application.loadedLevel);
	}

    public void QuitGame()
    {
        Debug.Log("has quit game");
        Application.Quit();
    }
}
