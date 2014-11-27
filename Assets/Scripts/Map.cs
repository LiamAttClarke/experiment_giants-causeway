using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	GameObject tile;
	float random;
	float height;
	public int mapSize = 16;
	public float scale = 10.0f;
	public float amplitude = 5.0f;
	public float translate_x = 0;
	public float translate_z = 0;
	void GenerateTerrain () {
		float apothem = Mathf.Cos(30 * Mathf.PI / 180);
		int diameter = 2;
		float offsetZ;
		float offsetX = 0.5f;
		random = Random.Range (0, 100);
		for (int x = 0; x < mapSize; x++) {
			for (int z = 0; z < mapSize; z++) {
				if (x % 2 != 0) {
					offsetZ = apothem;
				} else {
					offsetZ = 0;
				}
				height = Mathf.PerlinNoise (x / scale + random, z / scale + random);
				GameObject tileClone = (GameObject)Instantiate (
					tile, 
					new Vector3 (x * diameter - (offsetX * x), height * amplitude, (z * apothem) * diameter + offsetZ), 
					Quaternion.identity
				);
			}
		}
	}
	void Start () {
		tile = (GameObject)Resources.Load ("Prefabs/Tile");
		GenerateTerrain ();
	}
	void Update () {
		GameObject[] tileArray = GameObject.FindGameObjectsWithTag ("Tile");
		foreach (GameObject tile in tileArray) {
			height = Mathf.PerlinNoise ((tile.transform.position.x / 2) / scale + random + translate_x, (tile.transform.position.z / 2) / scale + random + translate_z);
			tile.transform.position = new Vector3 (tile.transform.position.x, height * amplitude, tile.transform.position.z);
		}
		translate_x += 0.25f;
	}
}
