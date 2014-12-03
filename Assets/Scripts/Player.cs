using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	GameObject map;
	// Initialization
	void Start () {
		map = GameObject.Find ("Map");
	}
	Vector3 mouseOrigin = new Vector3 (0.5f, 0.5f, 0);
	Vector3 mousePos;
	float mouseDiff;
	Vector3 mouseDir;
	float translated_x = 0;
	float translated_z = 0;
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			mouseOrigin = Camera.main.ScreenToViewportPoint (Input.mousePosition);
			translated_x = 0;
			translated_z = 0;
		}
		if (Input.GetMouseButton(0)) {
			mousePos = Camera.main.ScreenToViewportPoint (Input.mousePosition);
			mouseDiff = (mousePos - mouseOrigin).magnitude;
			mouseDir = (mousePos - mouseOrigin).normalized;
			if (translated_x < mouseDiff / 2) {
				map.GetComponent<Map> ().translate_x += -mouseDir.x / 20;
				translated_x += mouseDiff / 10;
			}
			if (translated_z < mouseDiff / 2) {
				map.GetComponent<Map> ().translate_z += -mouseDir.y / 20;
				translated_z += mouseDiff / 10;
			}
			map.GetComponent<Map> ().TranslateTerrain ();
		}
	}
}