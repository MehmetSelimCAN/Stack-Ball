using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    private static Button playButton;
    private static Button playerModelsMenuButton;
    private static Button obstacleModelsMenuButton;
    private static Transform playerModelsMenu;
    private static Transform obstacleModelsMenu;
    private static Sprite greenRadiant;
    private static Sprite blueRadiant;

    private static GameObject[] playerModels;
    private static GameObject[] obstacleModels;

    private static Text diamondText;
    private static Button backMenuButton;
    private static Button shopMenuButton;
    private static Transform shopMenu;


    private void Awake() {
        playerModels = GameObject.FindGameObjectsWithTag("PlayerModel");
        obstacleModels = GameObject.FindGameObjectsWithTag("ObstacleModel");

        greenRadiant = Resources.Load<Sprite>("Sprites/spGreenRadiant");
        blueRadiant = Resources.Load<Sprite>("Sprites/spBlueRadiant");

        playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        playerModelsMenuButton = GameObject.Find("PlayerModelsMenuButton").GetComponent<Button>();
        obstacleModelsMenuButton = GameObject.Find("ObstacleModelsMenuButton").GetComponent<Button>();
        playerModelsMenu = GameObject.Find("PlayerModelsMenu").transform;
        obstacleModelsMenu = GameObject.Find("ObstacleModelsMenu").transform;
        obstacleModelsMenu.gameObject.SetActive(false);

        playButton.onClick.AddListener(() => {
            Play();
        });

        playerModelsMenuButton.onClick.AddListener(() => {
            PlayerModelsMenu();
        });

        obstacleModelsMenuButton.onClick.AddListener(() => {
            ObstacleModelsMenu();
        });

        diamondText = GameObject.Find("Diamond").transform.Find("text").GetComponent<Text>();

        shopMenu = GameObject.Find("ShopMenu").transform;
        shopMenuButton = GameObject.Find("ShopMenuButton").GetComponent<Button>();

        shopMenuButton.onClick.AddListener(() => {
            ShopMenu();
        });

        backMenuButton = shopMenu.Find("BackMenuButton").GetComponent<Button>();

        backMenuButton.onClick.AddListener(() => {
            BackToMenu();
        });

        shopMenu.gameObject.SetActive(false);
    }

    private void Start() {
        RefreshOutlineUI();
        RefreshDiamondUI();
        RefreshAllBuyButtons();
    }

    public static void Play() {
        SceneManager.LoadScene("GameScene");
    }

    public static void ShopMenu() {
        shopMenu.gameObject.SetActive(true);
    }

    public static void BackToMenu() {
        shopMenu.gameObject.SetActive(false);
    }

    public static void PlayerModelsMenu() {
        playerModelsMenuButton.transform.GetComponent<Image>().sprite = greenRadiant;
        obstacleModelsMenuButton.transform.GetComponent<Image>().sprite = blueRadiant;
        playerModelsMenu.gameObject.SetActive(true);
        obstacleModelsMenu.gameObject.SetActive(false);
    }

    public static void ObstacleModelsMenu() {
        playerModelsMenuButton.transform.GetComponent<Image>().sprite = blueRadiant;
        obstacleModelsMenuButton.transform.GetComponent<Image>().sprite = greenRadiant;
        playerModelsMenu.gameObject.SetActive(false);
        obstacleModelsMenu.gameObject.SetActive(true);
    }

    public static void RefreshDiamondUI() {
        diamondText.text = PlayerPrefs.GetInt("Diamond").ToString();
    }

    public static void RefreshOutlineUI() {
        for (int i = 0; i < playerModels.Length; i++) {
            if (string.Equals("PlayerModel" + PlayerPrefs.GetInt("PlayerModelNumber"), playerModels[i].name)) {
                playerModels[i].transform.Find("outline").gameObject.SetActive(true);
            }
            else {
                playerModels[i].transform.Find("outline").gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < obstacleModels.Length; i++) {
            if (string.Equals("ObstacleModel" + PlayerPrefs.GetInt("ObstacleModelNumber"), obstacleModels[i].name)) {
                obstacleModels[i].transform.Find("outline").gameObject.SetActive(true);
            }
            else {
                obstacleModels[i].transform.Find("outline").gameObject.SetActive(false);
            }
        }
    }

    public static void RefreshAllBuyButtons() {
        var buyButtons = Resources.FindObjectsOfTypeAll<Buy>();

        foreach (var button in buyButtons) {
            button.RefreshButtonUI();
        }
    }
}
