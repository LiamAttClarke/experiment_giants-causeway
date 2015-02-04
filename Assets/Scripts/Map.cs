using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	GameObject tileAsset;
	GameObject[] tileArray;
	float moveSpeed = 0.005f;
	public float scale = 32.0f;
	public float translate_x = 0;
	public float translate_z = 0;
	public float random;

	// Initialization
	void Start () {
		tileAsset = (GameObject)Resources.Load ("Prefabs/Tile");
		random = Random.Range (0, 100);
		GenerateTerrain ();
		tileArray = GameObject.FindGameObjectsWithTag ("Tile");
		TranslateTerrain ();
	}

	//
	void Update () {
		if (Input.GetKey (KeyCode.UpArrow)) {
			translate_x += moveSpeed;
			translate_z += moveSpeed;
			TranslateTerrain ();
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			translate_x -= moveSpeed;
			translate_z -= moveSpeed;
			TranslateTerrain ();
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			translate_x -= moveSpeed;
			translate_z += moveSpeed;
			TranslateTerrain ();
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			translate_x += moveSpeed;
			translate_z -= moveSpeed;
			TranslateTerrain ();
		}
	}

	// Generate tile grid
	void GenerateTerrain () {
		int mapSize = 28;
		float radius = 0.5f;
		float apothem = radius * Mathf.Sin (60 * Mathf.PI / 180);
		float offset_z;
		float offset_x = 0.25f;
		float tileGap = 1.1f;
		// Generate hexagonal grid
		for (int x = 0; x < mapSize + 4; x++) {
			for (int z = 0; z < mapSize; z++) {
				// For every odd row, offset by apothem along z
				if (x % 2 != 0) {
					offset_z = apothem;
				} else {
					offset_z = 0;
				}
				// Set tile position in grid
				GameObject tileClone = (GameObject)Instantiate (tileAsset, new Vector3 ((x - (x * offset_x)) * tileGap, 0, ((z * apothem) * 2 + offset_z) * tileGap), Quaternion.identity);
				// Parent tile to Map gameObject
				tileClone.transform.parent = gameObject.transform;
			}
		}
	}

	// Modulate tile height
	void TranslateTerrain () {
		Vector3 tilePos;
		float noiseOne, noiseTwo;
		float noiseOneAmpl = 16.0f;
		float noiseTwoAmpl = 4.0f;
		float translateDiff = 4;
		float noiseTwoScale = scale / translateDiff;
		float height;
		foreach (GameObject tile in tileArray) {
			tilePos = tile.transform.position;
			// Calculate noise 
			noiseOne = (noiseOneAmpl * Mathf.PerlinNoise (tilePos.x / scale + random + translate_x, tilePos.z / scale + random + translate_z)) - noiseOneAmpl / 2;
			noiseTwo = (noiseTwoAmpl * Mathf.PerlinNoise (tilePos.x / noiseTwoScale + random + (translate_x * translateDiff), tilePos.z / noiseTwoScale + random + (translate_z * translateDiff))) - noiseTwoAmpl / 2;
			// Overlap high and low frequency noise
			height = noiseOne + noiseTwo;
			// Set tile elevation
			tile.transform.position = new Vector3 (tilePos.x, height, tilePos.z);
			tile.GetComponent<Tile> ().SetTileBiome ();
		}
	}
}
