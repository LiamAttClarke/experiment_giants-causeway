using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	void Start () {
		// Create tile
		CreateTile();
	}
	void CreateTile () {
		// Add Components
		Mesh tileMesh = gameObject.AddComponent<MeshFilter> ().mesh;
		// Create Mesh
		/*       TOP                 SIDEx4                SIDEx2
		 *     3 -- 2        7 -- 8 -- 9 -- 10-- 11     17-- 18-- 19
		 *   / |    |\       |    |    |    |    |      |    |    |
		 *  4  |    | 1      |    |    |    |    |      |    |    |
		 *  |  |    |/|      |    |    |    |    |      |    |    |
		 *  | 5 --- 6 |      |    |    |    |    |      |    |    |
		 *  | |* -- | |      |    |    |    |    |      |    |    |
		 *  |/|     |\|      |    |    |    |    |      |    |    |
		 *  * |     | *      |    |    |    |    |      |    |    |
		 *   \|     |/       |    |    |    |    |      |    |    |
		 *    * --- *        12-- 13-- 14-- 15-- 16    20 -- 21-- 22
		 */ 
		// mesh.vertices
		float radius = 0.5f;
		float apothem = radius * Mathf.Sin (60 * Mathf.PI / 180);
		float component_x = radius * Mathf.Cos (60 * Mathf.PI / 180);
		tileMesh.vertices = new Vector3[] {
			// TOP
			/*1*/new Vector3 (radius, radius, 0),
			/*2*/new Vector3 (component_x, radius, apothem),
			/*3*/new Vector3 (-component_x, radius, apothem),
			/*4*/new Vector3 (-radius, radius, 0),
			/*5*/new Vector3 (-component_x, radius, -apothem),
			/*6*/new Vector3 (component_x, radius, -apothem),
			// SIDEx4
			/*7*/new Vector3 (radius, radius, 0),
			/*8*/new Vector3 (component_x, radius, apothem),
			/*9*/new Vector3 (-component_x, radius, apothem),
			/*10*/new Vector3 (-radius, radius, 0),
			/*11*/new Vector3 (-component_x, radius, -apothem),
			/*12*/new Vector3 (radius, -radius, 0),
			/*13*/new Vector3 (component_x, -radius, apothem),
			/*14*/new Vector3 (-component_x, -radius, apothem),
			/*15*/new Vector3 (-radius, -radius, 0),
			/*16*/new Vector3 (-component_x, -radius, -apothem),
			//SIDEx2
			/*17*/new Vector3 (-component_x, radius, -apothem),
			/*18*/new Vector3 (component_x, radius, -apothem),
			/*19*/new Vector3 (radius, radius, 0),
			/*20*/new Vector3 (-component_x, -radius, -apothem),
			/*21*/new Vector3 (component_x, -radius, -apothem),
			/*22*/new Vector3 (radius, -radius, 0),
		};
		// mesh.triangles
		tileMesh.triangles = new int[] {
			// TOP
			0,5,4,
			0,4,3,
			0,3,2,
			0,2,1,
			// SIDEx4
			6,7,11,
			7,12,11,
			7,8,12,
			8,13,12,
			8,9,13,
			9,14,13,
			9,10,14,
			10,15,14,
			// SIDEx2
			16,17,19,
			17,20,19,
			17,18,20,
			18,21,20
		};
		float radiusUV = 0.25f;
		float apothemUV = radiusUV * Mathf.Sin (60 * Mathf.PI /180);
		float componentUV_x = apothemUV * Mathf.Tan (30 * Mathf.PI /180);
		//mesh.uv
		tileMesh.uv = new Vector2[] {
			// From L -> R && B -> T
			/*1*/new Vector2 (1, apothemUV),
			/*2*/new Vector2 (1 - componentUV_x, apothemUV * 2),
			/*3*/new Vector2 (0.5f + componentUV_x, apothemUV * 2),
			/*4*/new Vector2 (0.5f, apothemUV),
			/*5*/new Vector2 (0.5f + componentUV_x, 0),
			/*6*/new Vector2 (1 - componentUV_x, 0),
			/*7*/new Vector2 (0, 1),
			/*8*/new Vector2 (0.25f, 1),
			/*9*/new Vector2 (0.5f, 1),
			/*10*/new Vector2 (0.75f, 1),
			/*11*/new Vector2 (1, 1),
			/*12*/new Vector2 (0, 0.5f),
			/*13*/new Vector2 (0.25f, 0.5f),
			/*14*/new Vector2 (0.5f, 0.5f),
			/*15*/new Vector2 (0.75f, 0.5f),
			/*16*/new Vector2 (1, 0.5f),
			/*17*/new Vector2 (0, 0.5f),
			/*18*/new Vector2 (0.25f, 0.5f),
			/*19*/new Vector2 (0.5f, 0.5f),
			/*20*/new Vector2 (0, 0),
			/*21*/new Vector2 (0.25f, 0),
			/*22*/new Vector2 (0.5f, 0)
		};
		tileMesh.RecalculateNormals ();
		tileMesh.Optimize(); 
	}
}
