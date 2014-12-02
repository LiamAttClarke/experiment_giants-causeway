using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {
	MeshFilter planeMeshFilter; 
	Vector3[] vertices;
	float amplitude = 2.0f;
	float scale = 2.0f;
	float translate_x = 0;
	float translate_z = 0;
	float current = 0.01f;
	// Use this for initialization
	void Start () {
		planeMeshFilter = gameObject.GetComponent<MeshFilter> ();
		vertices = planeMeshFilter.mesh.vertices;
	}
	// Update is called once per frame
	void Update () {
		translate_x += current;
		translate_z += current;
		TranslateWater ();
	}
	void TranslateWater () {
		for (int i = 0; i < vertices.Length; i++) {
			vertices[i].y = (amplitude * Mathf.PerlinNoise (vertices[i].x / scale + translate_x, vertices[i].z / scale + translate_z)) - amplitude / 2;
		}
		planeMeshFilter.mesh.vertices = vertices;
		planeMeshFilter.mesh.RecalculateBounds ();
		planeMeshFilter.mesh.RecalculateNormals ();
	}
}
