using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    [SerializeField]
    private Transform[] modelPrefabs;
    private static Transform currentModelPrefab;
    private static Transform playerPrefab;
    private static Transform rotator;

    private static int maximumObstacleCount;

    private void Awake() {
        currentModelPrefab = modelPrefabs[PlayerPrefs.GetInt("ObstacleModelNumber") - 1];
        rotator = GameObject.Find("Rotator").transform;
        playerPrefab = Resources.Load<Transform>("Prefabs/pfPlayer");
        maximumObstacleCount = 20 + PlayerPrefs.GetInt("LastLevel") * 2;
    }

    private void Start() {
        GenerateLevel();
    }

    public static void GenerateLevel() {
        for (int i = 0; i < maximumObstacleCount; i++) {
            Transform model = Instantiate(currentModelPrefab);
            if (PlayerPrefs.GetInt("LastLevel") < 20) {
                for (int j = 0; j < Random.Range(0, model.childCount - 2); j++) {
                    model.GetChild(j).GetComponent<MeshRenderer>().material.color = Color.black;
                    model.GetChild(j).tag = "Unbreakable";
                }

            }

            else if (PlayerPrefs.GetInt("LastLevel") > 20 && PlayerPrefs.GetInt("LastLevel") < 40) {
                for (int j = 0; j < Random.Range(1, model.childCount - 1); j++) {
                    model.GetChild(j).GetComponent<MeshRenderer>().material.color = Color.black;
                    model.GetChild(j).tag = "Unbreakable";
                }
            }

            else if (PlayerPrefs.GetInt("LastLevel") > 40 && PlayerPrefs.GetInt("LastLevel") < 60) {
                for (int j = 0; j < Random.Range(2, model.childCount); j++) {
                    model.GetChild(j).GetComponent<MeshRenderer>().material.color = Color.black;
                    model.GetChild(j).tag = "Unbreakable";
                }
            }

            else if (PlayerPrefs.GetInt("LastLevel") > 60) {
                for (int j = 0; j < Random.Range(3, model.childCount); j++) {
                    model.GetChild(j).GetComponent<MeshRenderer>().material.color = Color.black;
                    model.GetChild(j).tag = "Unbreakable";
                }
            }

            model.transform.position = new Vector3(0, (0.2f + i * 0.2f), 0);
            model.transform.eulerAngles = new Vector3(0, ((i / 10) * 180) + i * 8, 0);
            model.SetParent(rotator);
        }

        Instantiate(playerPrefab, new Vector3(0, rotator.GetChild(rotator.childCount - 1).position.y + 0.5f, -0.55f), Quaternion.identity);
    }

    public static int GetMaximumObstacleCount() {
        return maximumObstacleCount;
    }

    public static int GetCurrentObstacleCount() {
        return maximumObstacleCount - GameObject.FindGameObjectsWithTag("Obstacle").Length;
    }

}
