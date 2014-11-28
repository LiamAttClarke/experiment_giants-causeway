using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	GameObject tile;
	float random;
	float height;
	GameObject[] tileArray;
	public int mapSize = 16;
	public float scale = 10.0f;
	public float amplitude = 5.0f;
	public float translate_x = 0;
	public float translate_z = 0;
	void Start () {
		tile = (GameObject)Resources.Load ("Prefabs/Tile");
		GenerateTerrain ();
		tileArray = GameObject.FindGameObjectsWithTag ("Tile");
	}
	void Update () {
		ModulateTerrain();
	}
	void GenerateTerrain () {
		float radius = 0.5f;
		float apothem = radius * Mathf.Sin (60 * Mathf.PI / 180);
		float offset_z;
		float offset_x = 0.25f;
		random = Random.Range (0, 100);
		// Generate hexagonal grid
		for (int x = 0; x < mapSize; x++) {
			for (int z = 0; z < mapSize; z++) {
				// For every odd row, offset by apothem along z
				if (x % 2 != 0) {
					offset_z = apothem;
				} else {
					offset_z = 0;
				}
				// Set height of tile
				height = Mathf.PerlinNoise (x / scale + random, z / scale + random);
				GameObject tileClone = (GameObject)Instantiate (
					tile, 
					new Vector3 (x - (offset_x * x), height * amplitude, (z * apothem) * 2 + offset_z), 
					Quaternion.identity
					);
				// Parent tile to Map gameObject
				tileClone.transform.parent = GameObject.Find ("Map").transform;
			}
		}
	}
	void ModulateTerrain () {
		foreach (GameObject tile in tileArray) {
			height = Mathf.PerlinNoise ((tile.transform.position.x / 2) / scale + random + translate_x, (tile.transform.position.z / 2) / scale + random + translate_z);
			tile.transform.position = new Vector3 (tile.transform.position.x, height * amplitude, tile.transform.position.z);
		}
	}
}
