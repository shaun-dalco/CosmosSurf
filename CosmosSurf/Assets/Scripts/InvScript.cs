using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvScript : MonoBehaviour
{
    private int coins;
    private int gems;

    public GameObject[] surfboards;

    public static int GetHighScore() {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    public static void CompareHighScores(int score) {
        if(score > GetHighScore()) {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public static void PickUpCoin() {
        PlayerPrefs.SetInt("PickUpCoins", PlayerPrefs.GetInt("PickUpCoins", 0) + 1);
        AddCoin();
    }

    public static void AddCoin() {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 1);
    }

    public static void AddCoin(int amount) {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + amount);
    }

    public static int GetNumberOfBoosts(int type) {
        switch(type) {
            case 0:
                return PlayerPrefs.GetInt("Boost100m", 0);
                break;

            case 1:
                return PlayerPrefs.GetInt("Boost200m", 0);
                break;

            case 2:
                return PlayerPrefs.GetInt("Shield", 0);
                break;
        }
        return -1;
    }

    public static void AddBoost(int type) {
        switch(type) {
            case 0:
                PlayerPrefs.SetInt("Boost100m", PlayerPrefs.GetInt("Boost100m", 0) + 1);
                break;

            case 1:
                PlayerPrefs.SetInt("Boost200m", PlayerPrefs.GetInt("Boost200m", 0) + 1);
                break;

            case 2:
                PlayerPrefs.SetInt("Shield", PlayerPrefs.GetInt("Shield", 0) + 1);
                break;

        }
    }

    public static void RemoveBoost(int type) {
        switch(type) {
            case 0:
                PlayerPrefs.SetInt("Boost100m", PlayerPrefs.GetInt("Boost100m", 0) - 1);
                break;

            case 1:
                PlayerPrefs.SetInt("Boost200m", PlayerPrefs.GetInt("Boost200m", 0) - 1);
                break;

            case 2:
                PlayerPrefs.SetInt("Shield", PlayerPrefs.GetInt("Shield", 0) - 1);
                break;

        }
    }

    public static int GetCoins() {
        return PlayerPrefs.GetInt("Coins", 0);
    }

    public static int GetGems() {
        return PlayerPrefs.GetInt("Gems", 0);
    }

    public static int GetPickedUpCoins() {
        return PlayerPrefs.GetInt("PickUpCoins", 0);
    }

    public static void SubtractCoins(int amount) {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) - amount);
    }

    private void UpdateCoinsAndGems() {
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.SetInt("Gems", gems);
    }

    public void BuySkill(int index) {
        int speedSkill = PlayerPrefs.GetInt("speedSkill", 0);
        int swingSkill = PlayerPrefs.GetInt("swingSkill", 0);
        int sizeSkill = PlayerPrefs.GetInt("sizeSkill", 0);

        switch(index) {
            case 0:
                //buy speed
                if(speedSkill < 5) {
                    if(MenuController.ValidatePurchase((int) Mathf.Pow(2,speedSkill)*100)) {
                        speedSkill++;
                        PlayerPrefs.SetInt("speedSkill", speedSkill);
                    }
                }
                break;

            case 1:
                //buy swing
                if(swingSkill < 5) {
                    if(MenuController.ValidatePurchase((int) Mathf.Pow(2,swingSkill)*100)) {
                        swingSkill++;
                        PlayerPrefs.SetInt("speedSkill", swingSkill);
                    }
                }
                break;

            case 2:
                //buy size
                if(sizeSkill < 5) {
                    if(MenuController.ValidatePurchase((int) Mathf.Pow(2,sizeSkill)*100)) {
                        sizeSkill++;
                        PlayerPrefs.SetInt("speedSkill", sizeSkill);
                    }
                }
                break;

        }
    }
    

}
