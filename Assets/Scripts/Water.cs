using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {
	Mesh waterMesh;
	Vector3[] vertices;
	float amplitude = 1.0f;
	float scale = 1.5f;
	float translate_x = 0;
	float translate_z = 0;
	float current = 0.015f;
	int gridSize = 16;
	// Initialization
	void Start () {
		waterMesh = gameObject.AddComponent<MeshFilter> ().mesh;
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
		MeshRenderer waterRender = gameObject.AddComponent<MeshRenderer> ();
		waterRender.material = (Material)Resources.Load ("Materials/Water");
		Vector3[,] verts =  new Vector3[17,17];
		Vector3[] meshVerts = new Vector3[1536];
		int vertIndex = 0;
		int xCoordInc, zCoordInc, xCoordMult, zCoordMult;
		int[] triangles = new int[meshVerts.Length];
		Vector2[] uvs = new Vector2[meshVerts.Length];
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
		for (int x = 0; x < gridSize + 1; x++) {
			for (int z = 0; z < gridSize + 1; z++) {
				verts[x,z] = new Vector3 (x, 0, z);
			}
		}
		for (int x = 0; x < gridSize; x++) {
			for (int z = 0; z < gridSize; z++) {
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
				meshVerts[vertIndex] = verts[0 + xCoordInc, 0 + zCoordInc];
				triangles[vertIndex] = vertIndex;
				uvs[vertIndex] = new Vector2 (x / gridSize, z / gridSize);
				vertIndex++;
				meshVerts[vertIndex] = verts[0 + xCoordInc, 1 * zCoordMult];
				triangles[vertIndex] = vertIndex;
				uvs[vertIndex] = new Vector2 (x / gridSize, z / gridSize);
				vertIndex++;
				meshVerts[vertIndex] = verts[1 * xCoordMult, 1 * zCoordMult];
				triangles[vertIndex] = vertIndex;
				uvs[vertIndex] = new Vector2 (x / gridSize, z / gridSize);
				vertIndex++;
				meshVerts[vertIndex] = verts[0 + xCoordInc, 0 + zCoordInc];
				triangles[vertIndex] = vertIndex;
				uvs[vertIndex] = new Vector2 (x / gridSize, z / gridSize);
				vertIndex++;
				meshVerts[vertIndex] = verts[1 * xCoordMult, 1 * zCoordMult];
				triangles[vertIndex] = vertIndex;
				uvs[vertIndex] = new Vector2 (x / gridSize, z / gridSize);
				vertIndex++;
				meshVerts[vertIndex] = verts[1 * xCoordMult, 0 + zCoordInc];
				triangles[vertIndex] = vertIndex;
				uvs[vertIndex] = new Vector2 (x / gridSize, z / gridSize);
				vertIndex++;
			}
		}
		waterMesh.vertices = meshVerts;
		waterMesh.triangles = triangles;
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
