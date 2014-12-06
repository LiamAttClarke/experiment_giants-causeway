using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	enum Biome {forest, desert};
	Biome value;
	GameObject map;
	float translate_x, translate_z;
	float random;
	float scale;
	Vector3 tilePos;
	GameObject[] trees;
	// Initialization
	void Start () {
		// Create tile
		CreateTile();
		map = GameObject.Find ("Map");
		random = map.GetComponent<Map> ().random;
		scale = map.GetComponent<Map> ().scale;
		GameObject spruceTree = (GameObject)Resources.Load ("Prefabs/Tree_Spruce");
		GameObject oakTree = (GameObject)Resources.Load ("Prefabs/Tree_Oak");
		Vector3 treeOffset = new Vector3 (0, 1.25f, 0);
		trees = new GameObject[] {
			(GameObject)Instantiate (spruceTree, transform.position + treeOffset, spruceTree.transform.rotation),
			(GameObject)Instantiate (oakTree, transform.position + treeOffset, spruceTree.transform.rotation)
		};
		foreach (GameObject tree in trees) {
			tree.SetActive (false);
			tree.transform.parent = transform;
		}
		SetTileBiome ();
	}
	void Update () {
		translate_x = map.GetComponent<Map> ().translate_x;
		translate_z = map.GetComponent<Map> ().translate_z;
	}
	void CreateTile () {
		/*       TOP              SIDE        
		 *     2 -- 1        6 --- 7   10--- 11  14--- 15
		 *   / |     \       |     |   |     |   |     |
		 *  3  |      0      |     |   |     |   |     |
		 *  |  |     /       |     |   |     |   |     |
		 *  | 4 --- 5        |  1  |   |  2  |   |  3  |
		 *  | |*    |        |     |   |     |   |     |
		 *  |/|     |        |     |   |     |   |     |
		 *  * |     |        |     |   |     |   |     |
		 *   \|     |        |     |   |     |   |     |
		 *    * --- *        9 --- 8   13--- 12  17--- 16
		 */ 
		// Create Mesh
		Mesh tileMesh = gameObject.AddComponent<MeshFilter> ().mesh;
		// Add Tile MeshRenderer
		gameObject.AddComponent<MeshRenderer> ();
		// mesh.vertices
		float radius = 0.5f;
		float apothem = radius * Mathf.Sin (60 * Mathf.PI / 180);
		float component_x = radius * Mathf.Cos (60 * Mathf.PI / 180);
		tileMesh.vertices = new Vector3[] {
			// TOP
			/*0*/new Vector3 (radius, radius, 0),
			/*1*/new Vector3 (component_x, radius, apothem),
			/*2*/new Vector3 (-component_x, radius, apothem),
			/*3*/new Vector3 (-radius, radius, 0),
			/*4*/new Vector3 (-component_x, radius, -apothem),
			/*5*/new Vector3 (component_x, radius, -apothem),
			// SIDE 1
			/*6*/new Vector3 (-component_x, radius, apothem),
			/*7*/new Vector3 (-radius, radius, 0),
			/*8*/new Vector3 (-radius, -radius, 0),
			/*9*/new Vector3 (-component_x, -radius, apothem),
			// SIDE 2
			/*10*/new Vector3 (-radius, radius, 0),
			/*11*/new Vector3 (-component_x, radius, -apothem),
			/*12*/new Vector3 (-component_x, -radius, -apothem),
			/*13*/new Vector3 (-radius, -radius, 0),
			// SIDE 3
			/*14*/new Vector3 (-component_x, radius, -apothem),
			/*15*/new Vector3 (component_x, radius, -apothem),
			/*16*/new Vector3 (component_x, -radius, -apothem),
			/*17*/new Vector3 (-component_x, -radius, -apothem),
		};
		// mesh.triangles
		tileMesh.triangles = new int[] {
			// TOP
			0,5,4,
			0,4,3,
			0,3,2,
			0,2,1,
			// SIDE 1
			6,7,9,
			9,7,8,
			// SIDE 2
			10,11,13,
			13,11,12,
			//SIDE 3
			14,15,17,
			17,15,16
		};
		float radiusUV = 0.25f;
		float apothemUV = radiusUV * Mathf.Sin (60 * Mathf.PI /180);
		float componentUV_x = apothemUV * Mathf.Tan (30 * Mathf.PI /180);
		//mesh.uv
		tileMesh.uv = new Vector2[] {
			// From L -> R && B -> T
			/*0*/new Vector2 (1, apothemUV),
			/*1*/new Vector2 (1 - componentUV_x, apothemUV * 2),
			/*2*/new Vector2 (0.5f + componentUV_x, apothemUV * 2),
			/*3*/new Vector2 (0.5f, apothemUV),
			/*4*/new Vector2 (0.5f + componentUV_x, 0),
			/*5*/new Vector2 (1 - componentUV_x, 0),
			/*6*/new Vector2 (0, 1),
			/*7*/new Vector2 (0.25f, 1),
			/*8*/new Vector2 (0.5f, 1),
			/*9*/new Vector2 (0.75f, 1),
			/*10*/new Vector2 (1, 1),
			/*11*/new Vector2 (0, 0.5f),
			/*12*/new Vector2 (0.25f, 0.5f),
			/*13*/new Vector2 (0.5f, 0.5f),
			/*14*/new Vector2 (0.75f, 0.5f),
			/*15*/new Vector2 (1, 0.5f),
			/*16*/new Vector2 (0, 0.5f),
			/*17*/new Vector2 (0.25f, 0.5f)
		};
		tileMesh.RecalculateNormals ();
		tileMesh.Optimize();
		SetTileBiome();
	}

	// Set tile biome
	public void SetTileBiome () {
		tilePos = transform.position;
		float biomeNoiseAmpl = 20;
		float tempNoise = (biomeNoiseAmpl *(Mathf.PerlinNoise (tilePos.x / scale + translate_x + random, tilePos.z / scale + translate_z + random))) - biomeNoiseAmpl / 2;
		float humidNoise = (biomeNoiseAmpl * (Mathf.PerlinNoise (tilePos.x / scale + translate_x + random, tilePos.z / scale + translate_z + random))) - biomeNoiseAmpl / 2;
		if (tempNoise > 3 && humidNoise < 3) {
			value = Biome.desert;
		} else {
			value = Biome.forest;
		}
		PaintTile ();
	}
	
	// Set tile Material
	void PaintTile () {
		MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer> ();
		// Set Material based on elevation
		if (meshRenderer) {
			switch (value) {
			case Biome.forest:
				meshRenderer.material = (Material)Resources.Load ("Materials/Tile_Grass");
				break;
			case Biome.desert:
				meshRenderer.material = (Material)Resources.Load ("Materials/Tile_Sand");
				break;
			}
			if (tilePos.y > -1 && tilePos.y < 0) {
				meshRenderer.material = (Material)Resources.Load ("Materials/Tile_Sand");
			} else if (tilePos.y < -1) {
				meshRenderer.material = (Material)Resources.Load ("Materials/Tile_Stone");
			}
		}
		PlantTrees ();
	}

	// Plant trees
	void PlantTrees () {
		if (trees != null) {
			float scaler = 12;
			float foliageScale = scale / scaler;
			float foliageNoiseAmpl = 2;
			int randomTree = Random.Range (0, trees.Length);
			float foliageNoise = foliageNoiseAmpl * Mathf.PerlinNoise (tilePos.x / foliageScale + (translate_x * scaler), tilePos.z / foliageScale + (translate_z * scaler)) - (foliageNoiseAmpl / 2);
			if (foliageNoise > 0 && value == Biome.forest && tilePos.y > 1) {
				if (!trees[randomTree].activeInHierarchy) {
					trees[randomTree].SetActive(true);
				}
				if (randomTree == 0) {
					trees[1].SetActive (false);
				} else if (randomTree == 1) {
					trees[0].SetActive (false);
				}
			} else {
				for (int i = 0; i < trees.Length; i++) {
					trees[i].SetActive (false);
				}
			}
		}
	}
}
