using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {
	MeshFilter planeMeshFilter; 
	Vector3[] vertices;
	float amplitude = 1.0f;
	float scale = 1.5f;
	float translate_x = 0;
	float translate_z = 0;
	float current = 0.015f;
	// Initialization
	void Start () {
		CreateWater ();
		planeMeshFilter = gameObject.GetComponent<MeshFilter> ();
		vertices = planeMeshFilter.mesh.vertices;
	}
	// Update is called once per frame
	void Update () {
		translate_x += current;
		translate_z += current;
		TranslateWater ();
	}
	void CreateWater () {
		Mesh waterMesh = gameObject.AddComponent<MeshFilter> ().mesh;
		MeshRenderer waterRender = gameObject.AddComponent<MeshRenderer> ();
		waterRender.material = (Material)Resources.Load ("Materials/Water");
		Vector3[,] vertices =  new Vector3[17,17];
		Vector3[] waterMeshVerts = new Vector3[1536];
		int vertIndex = 0;
		int xCoordInc, zCoordInc, xCoordMult, zCoordMult;
		int[] triangles = new int[waterMeshVerts.Length];
		Vector2[] uvs = new Vector2[waterMeshVerts.Length];
		/*
		 *  _________
		 * 1        2       Grid Size = 16x16
		 * |      /    /4
		 * |    /    /  |
		 * |  /    /    |
		 * 0/    /      |
		 *     3________5
		 * 
		 */
		for (int x = 0; x < 17; x++) {
			for (int z = 0; z < 17; z++) {
				vertices[x,z] = new Vector3 (x, 0, z);
			}
		}
		for (int x = 0; x < 16; x++) {
			for (int z = 0; z < 16; z++) {
				if (x == 0) {
					xCoordInc = 0;
					xCoordMult = 1;
				} else {
					xCoordInc = x - 1;
					xCoordMult = x;
				}
				if (z == 0) {
					zCoordInc = 0;
					zCoordMult = 1;
				} else {
					zCoordInc = z - 1;
					zCoordMult = z;
				}
				waterMeshVerts[vertIndex] = vertices[0 + xCoordInc, 0 + zCoordInc];
				triangles[vertIndex] = vertIndex;
				uvs[vertIndex] = new Vector2 (x / 10, z / 10);
				vertIndex++;
				waterMeshVerts[vertIndex] = vertices[0 + xCoordInc, 1 * zCoordMult];
				triangles[vertIndex] = vertIndex;
				uvs[vertIndex] = new Vector2 (x / 10, z / 10);
				vertIndex++;
				waterMeshVerts[vertIndex] = vertices[1 * xCoordMult, 1 * zCoordMult];
				triangles[vertIndex] = vertIndex;
				uvs[vertIndex] = new Vector2 (x / 10, z / 10);
				vertIndex++;
				waterMeshVerts[vertIndex] = vertices[0 + xCoordInc, 0 + zCoordInc];
				triangles[vertIndex] = vertIndex;
				uvs[vertIndex] = new Vector2 (x / 10, z / 10);
				vertIndex++;
				waterMeshVerts[vertIndex] = vertices[1 * xCoordMult, 1 * zCoordMult];
				triangles[vertIndex] = vertIndex;
				uvs[vertIndex] = new Vector2 (x / 10, z / 10);
				vertIndex++;
				waterMeshVerts[vertIndex] = vertices[1 * xCoordMult, 0 + zCoordInc];
				triangles[vertIndex] = vertIndex;
				uvs[vertIndex] = new Vector2 (x / 10, z / 10);
				vertIndex++;
			}
		}
		waterMesh.vertices = waterMeshVerts;
		waterMesh.triangles = triangles;
		waterMesh.RecalculateNormals ();
	}
	void TranslateWater () {
		for (int i = 0; i < vertices.Length; i++) {
			vertices[i].y = (amplitude * Mathf.PerlinNoise (vertices[i].x / scale + translate_x, vertices[i].z / scale + translate_z)) - amplitude / 2;
		}
		planeMeshFilter.mesh.vertices = vertices;
		planeMeshFilter.mesh.RecalculateNormals ();
	}
}
