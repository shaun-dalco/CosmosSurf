using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoostCount : MonoBehaviour
{
    public int type;
    public TextMeshProUGUI text;
    private int amount = 0;

    void Update() {
        switch(type) {
            case 0:
                amount = PlayerPrefs.GetInt("Boost100m", 0);
                break;

            case 1:
                amount = PlayerPrefs.GetInt("Boost200m", 0);
                break;

            case 2:
                amount = PlayerPrefs.GetInt("Shield", 0);
                break;

        }
        text.text = "" + amount;
    }
}
