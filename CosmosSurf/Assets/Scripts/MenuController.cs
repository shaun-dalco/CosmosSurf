using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    public GameObject[] MainMenu;
    public GameObject[] ShopMenu;
    public GameObject[] AchievementMenu;
    public GameObject[] CustomizeMenu;
    public GameObject GameOver;
    public GameObject[] CharMenu;
    public TextMeshProUGUI HighScore;
    public TextMeshProUGUI GameOverDistanceText;
    public Text coinsText;
    public Text gemsText;
    public TextMeshProUGUI[] badges;
    public bool add10000Coins;

    public bool running = true;
    public RectTransform navBar;

    void Start() {
        OnClick(0);
        if(PlayerPrefs.GetInt("GameOver", 0) == 1) {
            GameOver.SetActive(true);
            int dist = PlayerPrefs.GetInt("DistanceTravelled", 0);
            GameOverDistanceText.text = "You travelled " + dist + "m";
            PlayerPrefs.SetInt("DistanceTravelled", 0);
            PlayerPrefs.SetInt("GameOver", 0);
            InvScript.CompareHighScores(dist);

        }
        HighScore.text = "Best\n"+InvScript.GetHighScore()+"m";
        UpdateCoinsAndGems();
    }

    void Update() {
        if(add10000Coins) {
            InvScript.AddCoin();
        }
        UpdateCoinsAndGems();
    }

    public void OnClick(int menuID) {
        DeactivateAllMenus();
        switch(menuID) {
            case 0: // main menu
                foreach(GameObject t in MainMenu) {
                    t.SetActive(true);
                }
                break;

            case 1:
                foreach(GameObject t in ShopMenu) {
                    t.SetActive(true);
                }
                break;
            
            case 2:
                foreach(GameObject t in AchievementMenu) {
                    t.SetActive(true);
                }
                break;

            case 3:
                foreach(GameObject t in CustomizeMenu) {
                    t.SetActive(true);
                }
                break;
            
            
        }
    }

    public void DeactivateAllMenus() {
        foreach(GameObject t in MainMenu) {
            t.SetActive(false);
        }
        foreach(GameObject t in ShopMenu) {
            t.SetActive(false);
        }
        foreach(GameObject t in AchievementMenu) {
            t.SetActive(false);
        }
        foreach(GameObject t in CustomizeMenu) {
            t.SetActive(false);
        }


        GameOver.SetActive(false);
            
    }

    public void UpdateCoinsAndGems() {
        coinsText.text = ""+InvScript.GetCoins();
        gemsText.text = ""+InvScript.GetGems();
    }

    public void StartGame() {
        StartCoroutine(PlayAnim());
    }

    IEnumerator PlayAnim() {
        float timeElapsed = 0f;
        float sF = 1f;

        while(running) {
            timeElapsed += Time.deltaTime;
            sF = (0.1f - timeElapsed) / 0.1f;
            MainMenu[0].GetComponent<RectTransform>().eulerAngles += new Vector3(0f, 0f, 5f);
            MainMenu[0].GetComponent<RectTransform>().localScale = new Vector3(sF, sF, 1f);
            navBar.position -= new Vector3(0f, 18f, 0f);

            if(timeElapsed >= 0.1f) {
                running = false;
            }
            yield return new WaitForSeconds(0.05f);
        }

        SceneManager.LoadScene("MainScene");
    }

    public void BuyBoost(int type) {
        Debug.Log("Buying Boost, " + type);
        switch(type) {
            case 0:
                if(ValidatePurchase(100)) {
                    InvScript.AddBoost(0);
                }
                break;

            case 1:
                if(ValidatePurchase(300)) {
                    InvScript.AddBoost(1);
                }
                break;

            case 2:
                if(ValidatePurchase(500)) {
                    InvScript.AddBoost(2);
                }
                break;
        }

    }

    public static bool ValidatePurchase(int amount) {
        if(InvScript.GetCoins() > amount) {
            InvScript.SubtractCoins(amount);
            return true;
        } else {
            return false;
        }
        
    }

    public void UpdateBoostBadges() {
        for(int i = 0; i < badges.Length; i++) {
            badges[i].text = "" + InvScript.GetNumberOfBoosts(i);
        }
    }

    public void NavCustomizationMenu(int menuID) {
        CharMenu[menuID].SetActive(true);
        CharMenu[(menuID+1)%CharMenu.Length].SetActive(false);
    }
}
