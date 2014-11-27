using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	void CreateTile () {
		// Add Components
		MeshFilter tileMeshFilter = gameObject.AddComponent<MeshFilter> ();
		gameObject.AddComponent<MeshRenderer> ();
		// Create Mesh
		/*     3 -- 2   radius = 1
		 *   /   |   \
		 *  4 -- 0 -- 1
		 *   \   |   /
		 *    5 --- 6
		 * 
		 *     10 -- 9
		 *   /   |   \
		 *  11 -- 7 -- 8
		 *   \   |   /
		 *    12 --- 13
		 */ 
		Mesh tileMesh = new Mesh();
		// mesh.vertices
		float componentY = Mathf.Sin (60 * Mathf.PI / 180);
		Vector3[] vertices = new Vector3[] {
			// Front
			new Vector3 (0, 0, 0), // 0
			new Vector3 (1, 0, 0), // 1
			new Vector3 (0.5f, componentY, 0), // 2
			new Vector3 (-0.5f, componentY, 0), // 3
			new Vector3 (-1, 0, 0), // 4
			new Vector3 (-0.5f, -componentY, 0), // 5
			new Vector3 (0.5f, -componentY, 0), // 6
			// Back
			new Vector3 (0, 0, 1), // 7
			new Vector3 (1, 0, 1), // 8
			new Vector3 (0.5f, componentY, 1), // 9
			new Vector3 (-0.5f, componentY, 1), // 10
			new Vector3 (-1, 0, 1), // 11
			new Vector3 (-0.5f, -componentY, 1), // 12
			new Vector3 (0.5f, -componentY, 1) // 13
		};
		// mesh.triangles
		int[] triangles = new int[] {
			// Front
			0,2,1, // 1
			0,3,2, // 2
			0,4,3, // 3
			0,5,4, // 4
			0,6,5, // 5
			0,1,6,  // 6
//			// Back
//			7,8,9,
//			7,9,10,
//			7,10,11,
//			7,11,12,
//			7,12,13,
//			7,13,8,
			// Sides
			1,2,9,
			1,9,8,
			2,3,10,
			2,10,9,
			3,4,11,
			3,11,10,
			4,5,12,
			4,12,11,
			5,6,13,
			5,13,12,
			6,1,8,
			6,8,13
		};
		//mesh.uv
		Vector2[] uv = new Vector2[] {
			new Vector2 (0.25f, 0), // 0
			new Vector2 (0.75f, 0), // 1
			new Vector2 (0, 0.5f), // 2
			new Vector2 (0.5f, 0.5f), // 3
			new Vector2 (1, 0.5f), // 4
			new Vector2 (0.25f, 1), // 5
			new Vector2 (0.75f, 1) // 6
		};
		// Set mesh
		tileMeshFilter.mesh.vertices = vertices;
		tileMeshFilter.mesh.triangles = triangles;
		tileMeshFilter.mesh.uv = uv;
		tileMeshFilter.mesh.RecalculateNormals ();
		tileMeshFilter.mesh.Optimize(); 
		Material ground = (Material)Resources.Load("Material/Ground");
		renderer.material = ground;
	}
	void Start () {
		// Create tile
		CreateTile();
		// Orient tile
		transform.Rotate (new Vector3 (90,0,0));
		transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, 6);
	}
}
