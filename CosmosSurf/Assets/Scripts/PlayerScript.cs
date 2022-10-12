using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameManager gManager;
    public SpriteRenderer surfboard;
    public Sprite[] surfboards;
    public GameObject[] characters;

    void Start() {
        surfboard.sprite = surfboards[CharacterScript.CurSurf];
        characters[CharacterScript.CurChar].SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Obstacle" && !GameManager.immunity)
        {
            gManager.GameOver();
        }

        if (otherObject.gameObject.tag == "Coin")
        {
            InvScript.PickUpCoin();
            Destroy(otherObject.gameObject);
        }

    }
}
