using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Transform mainCamera;
    private Transform finalArea;
    private Vector2 cameraOffset;

    private void Awake() {
        mainCamera = Camera.main.transform;
        cameraOffset = new Vector2(0.5f, 2f);
        finalArea = GameObject.Find("FinalArea").transform;
    }

    private void Start() {
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, transform.position.y + cameraOffset.x, -2f);
    }

    private void Update() {
        if (mainCamera.transform.position.y > transform.position.y && mainCamera.transform.position.y > finalArea.position.y + cameraOffset.y) {
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, transform.position.y + cameraOffset.x, -2f);
        }
    }

}
