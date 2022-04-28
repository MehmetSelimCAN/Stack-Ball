using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireMode : MonoBehaviour {

    public static bool fireModeOn;
    private float neededTimeForFireMode = 0.75f;
    private float currentTimeForFireMode;

    private Transform fireModeUI;
    private Transform fireModeTimer;

    private void Awake() {
        fireModeTimer = GameObject.Find("FireModeTimer").transform;
        fireModeUI = GameObject.Find("FireModeUI").transform;
        fireModeUI.gameObject.SetActive(false);
    }

    private void Update() {
        if (currentTimeForFireMode > 0 && !fireModeUI.gameObject.activeInHierarchy) {
            fireModeUI.gameObject.SetActive(true);
        }

        else if (currentTimeForFireMode < 0 && fireModeUI.gameObject.activeInHierarchy) {
            fireModeUI.gameObject.SetActive(false);
        }

        if (!fireModeOn) {
            if (Input.GetMouseButton(0)) {
                if (currentTimeForFireMode < neededTimeForFireMode) {
                    currentTimeForFireMode += Time.deltaTime;
                    fireModeTimer.GetComponent<Image>().fillAmount = currentTimeForFireMode / neededTimeForFireMode;
                }

                if (currentTimeForFireMode > neededTimeForFireMode) {
                    fireModeOn = true;
                    fireModeTimer.GetComponent<Image>().color = Color.red;
                }
            }

            else if (!Input.GetMouseButton(0)) {
                if (currentTimeForFireMode < neededTimeForFireMode && currentTimeForFireMode > 0) {
                    currentTimeForFireMode -= Time.deltaTime / 10f;
                    fireModeTimer.GetComponent<Image>().fillAmount = currentTimeForFireMode / neededTimeForFireMode;
                }
            }
        }

        else if (fireModeOn) {
            currentTimeForFireMode -= Time.deltaTime / 2f;
            fireModeTimer.GetComponent<Image>().fillAmount = currentTimeForFireMode / neededTimeForFireMode;

            if (currentTimeForFireMode < 0f) {
                fireModeOn = false;
                fireModeTimer.GetComponent<Image>().color = new Color32(80, 155, 255, 255);
            }
        }
    }

}
