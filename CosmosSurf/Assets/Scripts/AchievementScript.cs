using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementScript : MonoBehaviour
{
    public readonly int ACH_COUNT = 2;
    public List<Transform> progressBar;
    public List<Image> background;
    public List<TextMeshProUGUI> text;
    public List<Button> buttons;
    public int[] completion; // 0,1,2,3,4 or 5 (5 for complete)
    public bool test = false;


    private string[] textMatrix = { "Reach 200m", "Reach 400m", "Reach 750m", "Reach 1500m", "Reach 3000m",
        "Collect 100 Coins In-Game", "Collect 200 Coins In-Game", "Collect 350 Coins In-Game", "Collect 500 Coins In-Game", "Collect 1000 Coins In-Game" };
    
    void Start()
    {
        completion = new int[ACH_COUNT];
        CheckAchievements();
    }

    void Update() {
        if(test) {
            completion[0] = 3;
            UpdateGUI();
            test = false;
        }
    }

    void CheckAchievements() {
        // distance achievement
        if(InvScript.GetHighScore() >= 200) {
            PlayerPrefs.SetInt("ACH_Completion_"+1,1);
        }

        if(InvScript.GetHighScore() >= 400) {
            PlayerPrefs.SetInt("ACH_Completion_"+1,2);
        }

        if(InvScript.GetHighScore() >= 750) {
            PlayerPrefs.SetInt("ACH_Completion_"+1,3);
        }

        if(InvScript.GetHighScore() >= 1500) {
            PlayerPrefs.SetInt("ACH_Completion_"+1,4);
        }

        if(InvScript.GetHighScore() >= 3000) {
            PlayerPrefs.SetInt("ACH_Completion_"+1,5);
        }

        //Coin Pick Up Achievement
        if(InvScript.GetPickedUpCoins() >= 100) {
            PlayerPrefs.SetInt("ACH_Completion_"+2,1);
        }

        if(InvScript.GetPickedUpCoins() >= 200) {
            PlayerPrefs.SetInt("ACH_Completion_"+2,2);
        }

        if(InvScript.GetPickedUpCoins() >= 350) {
            PlayerPrefs.SetInt("ACH_Completion_"+2,3);
        }

        if(InvScript.GetPickedUpCoins() >= 500) {
            PlayerPrefs.SetInt("ACH_Completion_"+2,4);
        }

        if(InvScript.GetPickedUpCoins() >= 1000) {
            PlayerPrefs.SetInt("ACH_Completion_"+2,5);
        }

        UpdateAchievements();
        CheckAwards();
        UpdateGUI();
    }

    void CheckAwards() {
        for(int i = 0; i < ACH_COUNT; i++) {
            if(PlayerPrefs.GetInt("ACH_Completion_"+i,0) > PlayerPrefs.GetInt("ACH_Awards_"+i,0)) { // ie the awards claimed has not progressed past completion
                background[i].GetComponent<Image>().color = new Color32(0,255,0,200);
                text[i].color = new Color32(0,0,0,100);
                text[i].text = "Claim Reward !";
                buttons[i].enabled = true;
            } else {
                background[i].GetComponent<Image>().color = new Color32(0,0,0,200);
                text[i].text = textMatrix[5*i + completion[i]];
                text[i].color = new Color32(0,0,0,200);
                buttons[i].enabled = false;
            }
        }
    }

    void UpdateAchievements() {
        for(int i = 0; i < ACH_COUNT; i++) {
            completion[i] = PlayerPrefs.GetInt("ACH_Completion_"+i,0);
        }
    }

    void UpdateGUI() {
        for(int i = 0; i < ACH_COUNT; i++) {
            if(completion[i] >= 1) {
                progressBar[i].GetChild(0).GetComponent<Image>().color = new Color32(0,255,0,200); 
            }

            if(completion[i] >= 2) {
                progressBar[i].GetChild(1).GetComponent<Image>().color = new Color32(0,255,0,200); 
            }

            if(completion[i] >= 3) {
                progressBar[i].GetChild(2).GetComponent<Image>().color = new Color32(0,255,0,200); 
            }

            if(completion[i] >= 4) {
                progressBar[i].GetChild(3).GetComponent<Image>().color = new Color32(0,255,0,200); 
            }

            if(completion[i] >= 5) {
                progressBar[i].GetChild(4).GetComponent<Image>().color = new Color32(0,255,0,200); 
            }

            if(PlayerPrefs.GetInt("ACH_Completion_"+i,0) <= PlayerPrefs.GetInt("ACH_Awards_"+i,0)) { //ie no active awards
                text[i].text = textMatrix[5*i + completion[i]];
                text[i].color = new Color32(0,0,0,200);
            }
            
        }
    }

    public void ClaimAward(int achievement) {
        PlayerPrefs.SetInt("ACH_Awards_"+achievement, PlayerPrefs.GetInt("ACH_Awards_"+achievement,0) + 1);
        InvScript.AddCoin(500);
        CheckAwards();
    }
}
