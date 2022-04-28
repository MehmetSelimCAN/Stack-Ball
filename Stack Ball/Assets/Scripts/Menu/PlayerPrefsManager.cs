using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour {

    private void Awake() {
        if (!PlayerPrefs.HasKey("LastLevel")) PlayerPrefs.SetInt("LastLevel", 1);
        if (!PlayerPrefs.HasKey("PlayerModel1")) PlayerPrefs.SetInt("PlayerModel1", 1);
        if (!PlayerPrefs.HasKey("PlayerModelNumber")) PlayerPrefs.SetInt("PlayerModelNumber", 1);
        if (!PlayerPrefs.HasKey("ObstacleModel1")) PlayerPrefs.SetInt("ObstacleModel1", 1);
        if (!PlayerPrefs.HasKey("ObstacleModelNumber")) PlayerPrefs.SetInt("ObstacleModelNumber", 1);
    }

}
