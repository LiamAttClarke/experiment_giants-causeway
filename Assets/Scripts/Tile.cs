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
		 */ 
		Mesh tileMesh = new Mesh();
		// mesh.vertices
		float componentY = Mathf.Sin (60 * Mathf.PI / 180);
		Vector3[] vertices = new Vector3[] {
			new Vector3 (0, 0, 0), // 0
			new Vector3 (1, 0, 0), // 1
			new Vector3 (0.5f, componentY, 0), // 2
			new Vector3 (-0.5f, componentY, 0), // 3
			new Vector3 (-1, 0, 0), // 4
			new Vector3 (-0.5f, -componentY, 0), // 5
			new Vector3 (0.5f, -componentY, 0) // 6
		};
		// mesh.triangles
		int[] triangles = new int[] {
			0,2,1, // 1
			0,3,2, // 2
			0,4,3, // 3
			0,5,4, // 4
			0,6,5, // 5
			0,1,6  // 6
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
	}
	void Start () {
		// Create tile
		CreateTile();
		// Orient tile
		transform.Rotate (new Vector3 (90,0,0));
	}
}
