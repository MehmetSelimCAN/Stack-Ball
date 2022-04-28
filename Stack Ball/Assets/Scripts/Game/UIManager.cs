using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    private static Transform scoreUI;
    private static Transform levelUI;
    private static Button resumeButton;
    private static Button pauseButton;
    private static Button backToMenuButton;
    private static Button menuButton;
    private static Button restartButton;
    private static Button nextLevelButton;
    private static Transform pauseMenu;


    private void Awake() {
        scoreUI = GameObject.Find("ScoreUI").transform;
        levelUI = GameObject.Find("LevelUI").transform;

        levelUI.Find("CurrentLevel").Find("CurrentLevelText").GetComponent<Text>().text = (PlayerPrefs.GetInt("LastLevel")).ToString();
        levelUI.Find("NextLevel").Find("NextLevelText").GetComponent<Text>().text = (PlayerPrefs.GetInt("LastLevel") + 1).ToString();

        GameManager.gamePaused = false;
        Time.timeScale = 1f;

        pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
        pauseButton.onClick.AddListener(() => {
            PauseGame();
        });

        resumeButton = GameObject.Find("ResumeButton").GetComponent<Button>();
        resumeButton.onClick.AddListener(() => {
            ResumeGame();
        });

        backToMenuButton = GameObject.Find("PauseMenu").transform.Find("MenuButton").GetComponent<Button>();
        backToMenuButton.onClick.AddListener(() => {
            BackToMenu();
        });

        menuButton = GameObject.Find("GameOverScreen").transform.Find("MenuButton").GetComponent<Button>();
        menuButton.onClick.AddListener(() => {
            BackToMenu();
        });

        restartButton = GameObject.Find("GameOverScreen").transform.Find("RestartButton").GetComponent<Button>();
        restartButton.onClick.AddListener(() => {
            Restart();
        });

        nextLevelButton = GameObject.Find("NextLevelButton").GetComponent<Button>();
        nextLevelButton.onClick.AddListener(() => {
            GameManager.NextLevel();
        });

        pauseMenu = GameObject.Find("PauseMenu").transform;
        pauseMenu.gameObject.SetActive(false);
    }

    public static void BackToMenu() {
        SceneManager.LoadScene("Menu");
    }

    public static void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void PauseGame() {
        Time.timeScale = 0f;
        GameManager.gamePaused = true;
        pauseMenu.gameObject.SetActive(true);
    }

    public static void ResumeGame() {
        Time.timeScale = 1f;
        GameManager.gamePaused = false;
        pauseMenu.gameObject.SetActive(false);

    }

    public static void RefreshUI() {
        RefreshScoreUI();
        RefreshLevelUI();
    }

    private static void RefreshScoreUI() {
        scoreUI.Find("text").GetComponent<Text>().text = PlayerPrefs.GetInt("Score").ToString();
    }

    private static void RefreshLevelUI() {
        float fillAmount = (float)(LevelGenerator.GetCurrentObstacleCount()) / LevelGenerator.GetMaximumObstacleCount();
        levelUI.Find("LevelSlider").Find("fill").GetComponent<Image>().fillAmount = fillAmount;
        levelUI.Find("NextLevel").Find("fill").GetComponent<Image>().fillAmount = fillAmount;
    }
}
