using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	GameObject tile;
	void GenerateTerrain () {
		int mapSize = 32;
		float apothem = Mathf.Cos(30 * Mathf.PI / 180);
		int diameter = 2;
		float offsetZ;
		float offsetX = 0.5f;
		float scale = 6.5f;
		float amplitude = 10.0f;
		float random = Random.Range (0, 100);
		for (int x = 0; x < mapSize; x++) {
			for (int z = 0; z < mapSize; z++) {
				if (x % 2 != 0) {
					offsetZ = apothem;
				} else {
					offsetZ = 0;
				}
				float height = Mathf.PerlinNoise (random + x / scale, random + z / scale);
				GameObject tileClone = (GameObject)Instantiate (
					tile,
					new Vector3 (x * diameter - (offsetX * x), Mathf.Round (height * amplitude), (z * apothem) * diameter + offsetZ),
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
	
	}
}
