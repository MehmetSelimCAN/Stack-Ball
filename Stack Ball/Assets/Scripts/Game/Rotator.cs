using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    private float speed = 100f;

    private void Update() {
        if (!GameManager.gamePaused)
            transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
    }
}