using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static bool gamePaused;
    private static Transform levelCompleteScreen;
    private static Transform gameOverScreen;
    private static Transform gameUI;
    private static int score;

    private void Awake() {
        levelCompleteScreen = GameObject.Find("LevelCompleteScreen").transform;
        gameOverScreen = GameObject.Find("GameOverScreen").transform;
        gameUI = GameObject.Find("GameUI").transform;
    }

    private void Start() {
        levelCompleteScreen.gameObject.SetActive(false);
        gameOverScreen.gameObject.SetActive(false);
    }

    public static void GainScore(int score) {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + score);
        UIManager.RefreshUI();
    }

    public static void DoubleDiamond() {
        gameOverScreen.Find("Score").Find("text").GetComponent<Text>().text = (score * 2).ToString();
        PlayerPrefs.SetInt("Diamond", PlayerPrefs.GetInt("Diamond") + score);
        gameOverScreen.Find("VideoButton").gameObject.SetActive(false);
    }
 
    public static void LevelComplete() {
        gameUI.gameObject.SetActive(false);
        levelCompleteScreen.gameObject.SetActive(true);
    }

    public static void NextLevel() {
        PlayerPrefs.SetInt("LastLevel", PlayerPrefs.GetInt("LastLevel") + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void GameOver() {
        gameUI.gameObject.SetActive(false);


        score = PlayerPrefs.GetInt("Score");
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Diamond", PlayerPrefs.GetInt("Diamond") + score);

        gameOverScreen.Find("Score").Find("text").GetComponent<Text>().text = score.ToString();
        gameOverScreen.gameObject.SetActive(true);
        gamePaused = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = true;
    }
}
