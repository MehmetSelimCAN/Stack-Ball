using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody rb;
    private bool breaking;
    private float rotationSpeed = 250f;
    private Vector3 rotationVector;

    private Transform splashPrefab;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        splashPrefab = Resources.Load<Transform>("Prefabs/pfSplash");

        transform.GetChild(PlayerPrefs.GetInt("PlayerModelNumber") - 1).gameObject.SetActive(true);
    }

    private void Start() {
        GetComponent<MeshRenderer>().material.color = ColorManager.GetCurrentColor();
    }

    private void Update() {
        if (!GameManager.gamePaused) {
            if (!(EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.CompareTag("PauseButton"))) {
                if (Input.GetMouseButtonDown(0)) {
                    breaking = true;
                }
                else if (Input.GetMouseButtonUp(0)) {
                    breaking = false;
                }

                if (!Input.GetMouseButton(0)) {
                    RandomRotation();
                }
            }
        }
    }

    private void FixedUpdate() {
        if (Input.GetMouseButton(0)) {
            if (!(EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.CompareTag("PauseButton"))) {
                rb.velocity = new Vector3(0, -250 * Time.fixedDeltaTime, 0);
            }
        }
    }

    private void RandomRotation() {
        rotationVector = new Vector3(Random.value, Random.value, Random.value) * Time.deltaTime * rotationSpeed;
        transform.Rotate(rotationVector);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "FinalArea" && !GameManager.gamePaused) {
            GameManager.gamePaused = true;
            GameManager.LevelComplete();
        }

        if (!breaking) {
            rb.velocity = new Vector3(0, 150 * Time.deltaTime, 0);

            Transform splash = Instantiate(splashPrefab);
            splash.GetComponent<SpriteRenderer>().color = ColorManager.GetCurrentColor();
            splash.transform.position = transform.position;
            splash.SetParent(collision.transform);      //It should rotate with area.
            splash.transform.localPosition = new Vector3(splash.transform.localPosition.x, 0.0035f, splash.transform.localPosition.z);
            splash.transform.localEulerAngles = new Vector3(90, Random.Range(0, 359), 0);
            //spawn splashes
        }

        else if (breaking) {
            if (FireMode.fireModeOn) {
                if (collision.collider.tag == "Breakable" || collision.collider.tag == "Unbreakable") {
                    collision.transform.parent.GetComponent<PartController>().BreakAllParts();
                    GameManager.GainScore(10);
                    //break
                }
            }
            else if (!FireMode.fireModeOn) {
                if (collision.collider.tag == "Breakable") {
                    collision.transform.parent.GetComponent<PartController>().BreakAllParts();
                    GameManager.GainScore(5);
                    //break
                }

                if (collision.collider.tag == "Unbreakable") {
                    GameManager.GameOver();
                    //Die
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision) {
        if (collision.collider.tag == "FinalArea") {
            rb.velocity = new Vector3(0, 150 * Time.deltaTime, 0);
        }

        if (breaking) {
            if (collision.collider.tag == "Breakable") {
                collision.transform.parent.GetComponent<PartController>().BreakAllParts();
                GameManager.GainScore(5);
                //break
            }
        }
    }
}
