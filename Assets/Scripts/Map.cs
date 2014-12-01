using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	GameObject tileAsset;
	float random;
	GameObject[] tileArray;
	enum Biome {forest, plains, desert, taiga, tundra, swamp, ocean};
	Biome value;
	// 
	public int mapSize = 16;
	public float scale = 10.0f;
	public float amplitude = 5.0f;
	public float translate_x = 0;
	public float translate_z = 0;
	// Initialize
	void Start () {
		tileAsset = (GameObject)Resources.Load ("Prefabs/Tile");
		random = Random.Range (0, 100);
		GenerateTerrain ();
		tileArray = GameObject.FindGameObjectsWithTag ("Tile");
		TranslateTerrain ();
	}
	// Generate tile grid
	void GenerateTerrain () {
		float radius = 0.5f;
		float apothem = radius * Mathf.Sin (60 * Mathf.PI / 180);
		float offset_z;
		float offset_x = 0.25f;
		float tileGap = 1.1f;
		// Generate hexagonal grid
		for (int x = 0; x < mapSize + 2; x++) {
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
				tileClone.transform.parent = GameObject.Find ("Map").transform;
				// Add Tile MeshRenderer
				tileClone.AddComponent<MeshRenderer> ();
			}
		}
	}
	// Modulate grid tile height
	public void TranslateTerrain () {
		Vector3 tilePos;
		float height;
		float highFreqNoise;
		float lowFreqNoise;
		foreach (GameObject tile in tileArray) {
			tilePos = tile.transform.position;
			// Calculate noise 
			highFreqNoise = (amplitude * Mathf.PerlinNoise (tilePos.x / scale + random + translate_x, tilePos.z / scale + random + translate_z)) - amplitude / 2;
			lowFreqNoise = ((amplitude * 5) * Mathf.PerlinNoise (tilePos.x / (scale * 5) + random + translate_x, tilePos.z / (scale * 5) + random + translate_z)) - (amplitude * 5) / 2;
			// Overlap high and low frequency noise
			height = highFreqNoise + lowFreqNoise;
			// Set tile elevation
			tile.transform.position = new Vector3 (tilePos.x, height, tilePos.z);
			SetTileBiome (tile);
			// Paint tile
			PaintTile (tile, height);
		}
	}
	// Set tile biome
	void SetTileBiome (GameObject tile) {
		Vector3 tilePos = tile.transform.position;
		float scaleX = 30;
		float amplitudeX = 20;
		float temperatureMap = (amplitudeX *(Mathf.PerlinNoise (tilePos.x / scaleX + random + translate_x, tilePos.z / scaleX + random + translate_z))) - amplitudeX / 2;
		float humidityMap = (amplitudeX * (Mathf.PerlinNoise (tilePos.x / scaleX + random  + translate_x + 100, tilePos.z / scaleX + random + translate_z + 100))) - amplitudeX / 2;
		if (temperatureMap > 1 && humidityMap > 1) {
			value = Biome.forest;
		} else if (temperatureMap > 1 && humidityMap < 1) {
			value = Biome.desert;
		} else if (temperatureMap < 1 && humidityMap > 1 && tilePos.y > 3) {
			value = Biome.taiga;
		} else if (temperatureMap < 1 && humidityMap < 1 && tilePos.y < 3) {
			value = Biome.tundra;
		} else if (temperatureMap > 1 && humidityMap < 1) {
			value = Biome.desert;
		} else {
			value = Biome.plains;
		}
	}
	// Set tile Material
	void PaintTile (GameObject tile, float elevation) {
		MeshRenderer tileMeshRenderer = tile.GetComponent<MeshRenderer> ();
		// Set Material based on elevation
		switch (value) {
			case Biome.forest:
				tileMeshRenderer.material = (Material)Resources.Load ("Materials/Tile_Grass");
				break;
			case Biome.desert:
				tileMeshRenderer.material = (Material)Resources.Load ("Materials/Tile_Sand");
				break;
			case Biome.taiga:
				tileMeshRenderer.material = (Material)Resources.Load ("Materials/Tile_Stone");
				break;
			case Biome.plains:
				tileMeshRenderer.material = (Material)Resources.Load ("Materials/Tile_Grass");
				break;
		}
	}
}
