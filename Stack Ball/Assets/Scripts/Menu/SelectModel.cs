using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectModel : MonoBehaviour {

    private Button selectButton;

    private void Awake() {
        selectButton = GetComponent<Button>();

        selectButton.onClick.AddListener(() => { 
            if (PlayerPrefs.GetInt(transform.parent.name) == 1) {       //If it is unlocked, user can select.
                ModelSelect();
            }
        });
    }

    private void ModelSelect() {
        if (transform.parent.tag == "PlayerModel") {
            PlayerPrefs.SetInt("PlayerModelNumber", int.Parse(transform.parent.name.Substring(11)));
        }
        else if (transform.parent.tag == "ObstacleModel") {
            PlayerPrefs.SetInt("ObstacleModelNumber", int.Parse(transform.parent.name.Substring(13)));
        }

        MenuManager.RefreshOutlineUI();
    }
}
