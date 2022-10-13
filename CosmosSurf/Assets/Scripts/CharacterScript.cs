using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScript : MonoBehaviour
{
    public static int CurChar = 0;
    public static int CurSurf = 0;
    public List<Image> CharStands;
    public List<Image> SurfStands;

    public List<Transform> skillBars;

    void Start() {
        CurChar = PlayerPrefs.GetInt("CurChar", 0);
        CurSurf = PlayerPrefs.GetInt("CurSurf", 0);
        SelectCharacter(CurChar);
        SelectSurfboard(CurSurf);
        UpdateColourBars();
    }

    public void UpdateCharacter(int index) {
        PlayerPrefs.SetInt("CurChar", index);
    }

    public void UpdateSurfboard(int index) {
        PlayerPrefs.SetInt("CurSurf", index);
    }
    
    public void SelectCharacter(int index) {
        foreach(Image c in CharStands) {
            c.color = new Color32(255,255,255,150);
        }

        CurChar = index;
        CharStands[index].color = new Color32(0,255,0,150);
        UpdateCharacter(index);
    }

    public void SelectSurfboard(int index) {
        foreach(Image c in SurfStands) {
            c.color = new Color32(255,255,255,150);
        }

        CurSurf = index;
        SurfStands[index].color = new Color32(0,255,0,150);
        UpdateSurfboard(index);
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
                        UpdateColourBars();
                    }
                }
                break;

            case 1:
                //buy swing
                if(swingSkill < 5) {
                    if(MenuController.ValidatePurchase((int) Mathf.Pow(2,swingSkill)*100)) {
                        swingSkill++;
                        PlayerPrefs.SetInt("swingSkill", swingSkill);
                        UpdateColourBars();
                    }
                }
                break;

            case 2:
                //buy size
                if(sizeSkill < 5) {
                    if(MenuController.ValidatePurchase((int) Mathf.Pow(2,sizeSkill)*100)) {
                        sizeSkill++;
                        PlayerPrefs.SetInt("sizeSkill", sizeSkill);
                        UpdateColourBars();
                    }
                }
                break;

        }
    }

    public void UpdateColourBars() {
        for(int i = 0; i < skillBars.Count; i++) {
            int skillPoints = InvScript.GetSkillPoints(i);

            for(int j = 0; j < skillPoints; j++) {
                skillBars[i].GetChild(j).GetComponent<Image>().color = new Color32(0,255,0,255);
            }
        }
    }
}
