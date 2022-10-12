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

    void Start() {
        CurChar = PlayerPrefs.GetInt("CurChar", 0);
        CurSurf = PlayerPrefs.GetInt("CurSurf", 0);
        SelectCharacter(CurChar);
        SelectSurfboard(CurSurf);
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
}
