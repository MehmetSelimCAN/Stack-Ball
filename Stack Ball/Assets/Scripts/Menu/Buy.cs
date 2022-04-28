using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buy : MonoBehaviour {

    private Button button;
    private int price;
    private Transform priceMenu;
    private Sprite greenRadiant;
    private Sprite redRadiant;

    private void Awake() {
        greenRadiant = Resources.Load<Sprite>("Sprites/spGreenRadiant");
        redRadiant = Resources.Load<Sprite>("Sprites/spRedRadiant");

        priceMenu = transform.parent.Find("Price");
        price = int.Parse(priceMenu.Find("text").GetComponent<Text>().text);

        if (PlayerPrefs.GetInt(transform.parent.name) == 1) {       //If it is unlocked.
            Destroy(priceMenu.gameObject);          //Destroy price menu
            Destroy(gameObject);            //Destroy buy button
        }

        else if (PlayerPrefs.GetInt(transform.parent.name) != 1) {       //If it is not unlocked.
            if (price <= PlayerPrefs.GetInt("Diamond")) {
                GetComponent<Image>().sprite = greenRadiant;
            }
            else {
                GetComponent<Image>().sprite = redRadiant;
            }
        }
        
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {
        if (price <= PlayerPrefs.GetInt("Diamond")) {
                BuyModel();
            }
        });
    }

    private void BuyModel() {
        PlayerPrefs.SetInt(transform.parent.name, 1);

        if (transform.parent.tag == "PlayerModel") {
            PlayerPrefs.SetInt("PlayerModelNumber", int.Parse(transform.parent.name.Substring(11))); //select this model.
        }
        else if (transform.parent.tag == "ObstacleModel") {
            PlayerPrefs.SetInt("ObstacleModelNumber", int.Parse(transform.parent.name.Substring(13))); //select this model.
        }
        PlayerPrefs.SetInt("Diamond", PlayerPrefs.GetInt("Diamond") - price);
        MenuManager.RefreshDiamondUI();
        MenuManager.RefreshOutlineUI();
        MenuManager.RefreshAllBuyButtons();
        Destroy(priceMenu.gameObject);          //Destroy price menu
        Destroy(gameObject);            //Destroy buy button
    }

    public void RefreshButtonUI() {
        if (price <= PlayerPrefs.GetInt("Diamond")) {
            GetComponent<Image>().sprite = greenRadiant;
        }
        else {
            GetComponent<Image>().sprite = redRadiant;
        }
    }

}
