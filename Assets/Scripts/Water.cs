using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {
	Mesh waterMesh;
	Vector3[] vertices;
	float amplitude = 1.5f;
	float scale = 2.0f;
	float translate_x = 0;
	float translate_z = 0;
	float current = 0.01f;
	int gridSize = 16;
	int vertLen;
	// Initialization
	void Start () {
		vertLen = ((gridSize * gridSize) * 2) * 3;
		waterMesh = gameObject.GetComponent<MeshFilter> ().mesh;
		CreateWater ();
		vertices = waterMesh.vertices;

	}
	// Update is called once per frame
	void Update () {
		translate_x += current;
		translate_z += current;
		TranslateWater ();
	}
	void CreateWater () {
		Vector3[,] verts =  new Vector3[gridSize + 1, gridSize + 1];
		Vector3[] meshVerts = new Vector3[vertLen];
		int vertIndex = 0;
		int xCoordInc, zCoordInc, xCoordMult, zCoordMult;
		int[] meshTris = new int[vertLen];
		Vector2[] meshUVs = new Vector2[vertLen];
		/*
		 *  _________
		 * 1        2
		 * |      /    /4
		 * |    /    /  |
		 * |  /    /    |
		 * 0/    /      |
		 *     3________5
		 * 
		 */
		for (int x = 0; x < gridSize; x++) {
			for (int z = 0; z < gridSize; z++) {
				verts[x,z] = new Vector3 (x, 0, z);
			}
		}
		for (int x = 0; x < gridSize; x++) {
			for (int z = 0; z < gridSize; z++) {
				if (x == 0) {
					xCoordInc = 0;
					xCoordMult = 1;
				} else {
					xCoordInc = x;
					xCoordMult = x + 1;
				}
				if (z == 0) {
					zCoordInc = 0;
					zCoordMult = 1;
				} else {
					zCoordInc = z;
					zCoordMult = z + 1;
				}
				meshVerts[vertIndex] = verts[xCoordInc, zCoordInc];
				meshTris[vertIndex] = vertIndex;
				meshUVs[vertIndex] = new Vector2 (x * (1 / gridSize), z * (1 / gridSize));
				vertIndex++;
				meshVerts[vertIndex] = verts[xCoordInc, zCoordMult];
				meshTris[vertIndex] = vertIndex;
				meshUVs[vertIndex] = new Vector2 (x * (1 / gridSize), z * (1 / gridSize));
				vertIndex++;
				meshVerts[vertIndex] = verts[xCoordMult, zCoordMult];
				meshTris[vertIndex] = vertIndex;
				meshUVs[vertIndex] = new Vector2 (x * (1 / gridSize), z * (1 / gridSize));
				vertIndex++;
				meshVerts[vertIndex] = verts[xCoordInc, zCoordInc];
				meshTris[vertIndex] = vertIndex;
				meshUVs[vertIndex] = new Vector2 (x * (1 / gridSize), z * (1 / gridSize));
				vertIndex++;
				meshVerts[vertIndex] = verts[xCoordMult, zCoordMult];
				meshTris[vertIndex] = vertIndex;
				meshUVs[vertIndex] = new Vector2 (x * (1 / gridSize), z * (1 / gridSize));
				vertIndex++;
				meshVerts[vertIndex] = verts[xCoordMult, zCoordInc];
				meshTris[vertIndex] = vertIndex;
				meshUVs[vertIndex] = new Vector2 (x * (1 / gridSize), z * (1 / gridSize));
				vertIndex++;
			}
		}
		waterMesh.vertices = meshVerts;
		waterMesh.triangles = meshTris;
		waterMesh.uv = meshUVs;
		waterMesh.RecalculateNormals ();
	}
	void TranslateWater () {
		for (int i = 0; i < vertices.Length; i++) {
			vertices[i].y = (amplitude * Mathf.PerlinNoise (vertices[i].x / scale + translate_x, vertices[i].z / scale + translate_z)) - amplitude / 2;
		}
		waterMesh.vertices = vertices;
		waterMesh.RecalculateNormals ();
	}
}
