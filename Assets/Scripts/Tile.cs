using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	// Initialization
	void Start () {
		// Create tile
		CreateTile();
	}
	void CreateTile () {
		/*       TOP              SIDE        
		 *     2 -- 1        6 -- 7   10-- 11  14-- 15
		 *   / |     \       |    |   |    |   |    |
		 *  3  |      0      |    |   |    |   |    |
		 *  |  |     /       |    |   |    |   |    |
		 *  | 4 --- 5        | 1  |   | 2  |   | 3  |
		 *  | |*    |        |    |   |    |   |    |
		 *  |/|     |        |    |   |    |   |    |
		 *  * |     |        |    |   |    |   |    |
		 *   \|     |        |    |   |    |   |    |
		 *    * --- *        9 -- 8   13-- 12  17-- 16
		 */ 
		// Create Mesh
		Mesh tileMesh = gameObject.AddComponent<MeshFilter> ().mesh;
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
	}
}
