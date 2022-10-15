using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool immunity = false;

    public GameObject player;
    public Transform surfboard;
    public GameObject shieldObj;
    public TextMeshProUGUI[] badges;

    public float speed;
    public GameObject gameEnv;
    public GameObject[] obstacles;
    public GameObject[] vertObstacles;
    public GameObject coin;
    public RectTransform boostPanel;
    public GameObject reviveObj;

    public bool running = true;
    public bool paused = false;

    public TextMeshProUGUI distanceText;

    private int direction = 0;
    private float distance = 0;
    private float timeElapsed = 0;

    private float speedStat;
    private float swingStat;
    private float sizeStat;

    private float height;
    private float width;

    void Start()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
        
        badges[0].text = "" + InvScript.GetNumberOfBoosts(0);
        badges[1].text = "" + InvScript.GetNumberOfBoosts(1);
        badges[2].text = "" + InvScript.GetNumberOfBoosts(2);
        GetStats();
        player.transform.localScale = new Vector3(sizeStat, sizeStat, 1);
        StartCoroutine(GameEnv());
        StartCoroutine(PlayBoostHideAnim());
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 pos = touch.position;
            if(pos.x > width/2) {
                direction = -2;
                surfboard.eulerAngles = new Vector3(0f, 0f, 225f);
                surfboard.localScale = new Vector3(0.2f, 0.3f, 1f);

            } else {
                direction = 2;
                surfboard.eulerAngles = new Vector3(0f, 0f, 135f);
                surfboard.localScale = new Vector3(0.2f, 0.3f, 1f);

            }
            
        }
        
        if (Input.GetKeyDown("left"))
        {
            direction = 2;
            surfboard.eulerAngles = new Vector3(0f, 0f, 135f);
            surfboard.localScale = new Vector3(0.2f, 0.3f, 1f);

        }

        if (Input.GetKeyDown("right"))
        {
            direction = -2;
            surfboard.eulerAngles = new Vector3(0f, 0f, 225f);
            surfboard.localScale = new Vector3(0.2f, 0.3f, 1f);

        }
    }

    private void GetStats() {
        speedStat = 1 - PlayerPrefs.GetInt("speedSkill", 0)/10; 
        swingStat = 1 + PlayerPrefs.GetInt("swingSkill", 0)/10;
        sizeStat = 1 - PlayerPrefs.GetInt("sizeSkill", 0)/10;
    }

    IEnumerator GameEnv() {
        bool runOnce = true;
        float realSpeed = speed;
        while(running) {

            // Move env
            realSpeed = (distance/50 + speed) * speedStat;
            gameEnv.transform.position += new Vector3(0.9f * direction * Time.deltaTime * swingStat, realSpeed * Time.deltaTime, 0f);

            timeElapsed += Time.deltaTime;
            if(timeElapsed > 4) {
                int ran = Random.Range(0, obstacles.Length);
                float rand = Random.Range(-4f, 4f);
                GameObject newObj = (GameObject) Instantiate(obstacles[ran]) as GameObject;
                newObj.transform.parent = gameEnv.transform;
                newObj.transform.position = new Vector3(rand, -20f, -0.5f);

                ran = Random.Range(0, obstacles.Length);
                newObj = (GameObject) Instantiate(obstacles[ran]) as GameObject;
                newObj.transform.parent = gameEnv.transform;
                newObj.transform.position = new Vector3(rand+10f, -20f, -0.5f);

                ran = Random.Range(0, obstacles.Length);
                newObj = (GameObject) Instantiate(obstacles[ran]) as GameObject;
                newObj.transform.parent = gameEnv.transform;
                newObj.transform.position = new Vector3(rand-10f, -20f, -0.5f);

                // coins
                newObj = (GameObject) Instantiate(coin) as GameObject;
                newObj.transform.parent = gameEnv.transform;
                newObj.transform.position = new Vector3(rand+2f, -20f, -0.5f);
                newObj = (GameObject) Instantiate(coin) as GameObject;
                newObj.transform.parent = gameEnv.transform;
                newObj.transform.position = new Vector3(rand-2f, -20f, -0.5f);

                if(distance > 50) {
                    ran = Random.Range(0, obstacles.Length);
                    newObj = (GameObject) Instantiate(obstacles[ran]) as GameObject;
                    newObj.transform.parent = gameEnv.transform;
                    newObj.transform.position = new Vector3(rand*1.5f, -30f, -0.5f);
                }

                if(distance > 120) {
                    ran = Random.Range(0, obstacles.Length);
                    newObj = (GameObject) Instantiate(obstacles[ran]) as GameObject;
                    newObj.transform.parent = gameEnv.transform;
                    newObj.transform.position = new Vector3(rand-10f, -25f, -0.5f);

                    ran = Random.Range(0, obstacles.Length);
                    newObj = (GameObject) Instantiate(obstacles[ran]) as GameObject;
                    newObj.transform.parent = gameEnv.transform;
                    newObj.transform.position = new Vector3(rand+10f, -25f, -0.5f);
                }

                if(distance > 170) {
                    ran = Random.Range(0, obstacles.Length);
                    newObj = (GameObject) Instantiate(obstacles[ran]) as GameObject;
                    newObj.transform.parent = gameEnv.transform;
                    newObj.transform.position = new Vector3(rand*1.5f, -28f, -0.5f);
                    newObj.transform.localScale = new Vector3(0.5f,0.5f,0.5f);

                    ran = Random.Range(0, obstacles.Length);
                    newObj = (GameObject) Instantiate(obstacles[ran]) as GameObject;
                    newObj.transform.parent = gameEnv.transform;
                    newObj.transform.position = new Vector3(rand*1.5f, -20f, -0.5f);
                    newObj.transform.localScale = new Vector3(0.5f,0.5f,0.5f);

                    ran = Random.Range(0, obstacles.Length);
                    newObj = (GameObject) Instantiate(obstacles[ran]) as GameObject;
                    newObj.transform.parent = gameEnv.transform;
                    newObj.transform.position = new Vector3(rand*1.12f, -27f, -0.5f);
                    newObj.transform.localScale = new Vector3(0.5f,0.5f,0.5f);

                    ran = Random.Range(0, obstacles.Length);
                    newObj = (GameObject) Instantiate(obstacles[ran]) as GameObject;
                    newObj.transform.parent = gameEnv.transform;
                    newObj.transform.position = new Vector3(rand*1.12f, -31f, -0.5f);
                    newObj.transform.localScale = new Vector3(0.5f,0.5f,0.5f);

                    ran = Random.Range(0, obstacles.Length);
                    newObj = (GameObject) Instantiate(obstacles[ran]) as GameObject;
                    newObj.transform.parent = gameEnv.transform;
                    newObj.transform.position = new Vector3(rand*1.3f, -35f, -0.5f);
                    newObj.transform.localScale = new Vector3(0.5f,0.5f,0.5f);

                    ran = Random.Range(0, obstacles.Length);
                    newObj = (GameObject) Instantiate(obstacles[ran]) as GameObject;
                    newObj.transform.parent = gameEnv.transform;
                    newObj.transform.position = new Vector3(rand*1.3f, -35f, -0.5f);
                    newObj.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
                }

                if(distance > 220) {
                    ran = Random.Range(0, obstacles.Length);
                    newObj = (GameObject) Instantiate(obstacles[ran]) as GameObject;
                    newObj.transform.parent = gameEnv.transform;
                    newObj.transform.position = new Vector3(rand*2f+2, -30f, -0.5f);

                    ran = Random.Range(0, obstacles.Length);
                    newObj = (GameObject) Instantiate(obstacles[ran]) as GameObject;
                    newObj.transform.parent = gameEnv.transform;
                    newObj.transform.position = new Vector3(rand*2f-2, -30f, -0.5f);
                }

                if(distance > 250) {
                    ran = Random.Range(0, obstacles.Length);
                    newObj = (GameObject) Instantiate(obstacles[ran]) as GameObject;
                    newObj.transform.parent = gameEnv.transform;
                    newObj.transform.position = new Vector3(rand+5f, -20f, -0.5f);
                    newObj.transform.localScale = new Vector3(0.3f,0.3f,0.3f);

                    ran = Random.Range(0, obstacles.Length);
                    newObj = (GameObject) Instantiate(obstacles[ran]) as GameObject;
                    newObj.transform.parent = gameEnv.transform;
                    newObj.transform.position = new Vector3(rand-5f, -20f, -0.5f);
                }

                if(distance > 320) {
                    ran = Random.Range(0, obstacles.Length);
                    newObj = (GameObject) Instantiate(obstacles[ran]) as GameObject;
                    newObj.transform.parent = gameEnv.transform;
                    newObj.transform.position = new Vector3(rand+10f, -24f, -0.5f);
                    newObj.AddComponent<VelocityScript>();

                    ran = Random.Range(0, obstacles.Length);
                    newObj = (GameObject) Instantiate(obstacles[ran]) as GameObject;
                    newObj.transform.parent = gameEnv.transform;
                    newObj.transform.position = new Vector3(rand-10f, -24f, -0.5f);
                    newObj.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
                    newObj.AddComponent<VelocityScript>();
                }

                timeElapsed = 0;
            }

            if(distance > 99 & distance < 101 && runOnce) {
                int ranSmallObj;
                GameObject newSmallObj;
                for(int i = 0; i<8; i++) {

                    // left to right
                    ranSmallObj = Random.Range(0, vertObstacles.Length);
                    newSmallObj = (GameObject) Instantiate(vertObstacles[ranSmallObj]) as GameObject;
                    newSmallObj.transform.parent = gameEnv.transform;
                    newSmallObj.transform.position = new Vector3(-5f-4*i, 3f-i*7, -0.5f);
                    newSmallObj.GetComponent<SmallObjMovement>().direction = 1;

                    // right to left
                    ranSmallObj = Random.Range(0, vertObstacles.Length);
                    newSmallObj = (GameObject) Instantiate(vertObstacles[ranSmallObj]) as GameObject;
                    newSmallObj.transform.parent = gameEnv.transform;
                    newSmallObj.transform.position = new Vector3(5f+4*i, 3f-i*7, -0.5f);
                    newSmallObj.GetComponent<SmallObjMovement>().direction = -1;
                }

                runOnce = false;
            }

            distance += speed * Time.deltaTime;
            distanceText.text = "Distance\n" + (int)distance + "m";
            yield return new WaitForEndOfFrame();
            while(paused) {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    IEnumerator PlayBoostHideAnim() {
        float timeElapsed = 0f;
        float waitTimeElapsed = 0f;
        bool waiting = true;
        bool running1 = true;

        while(running1) {
            while(waiting) {
                waitTimeElapsed += Time.deltaTime;
                if(waitTimeElapsed >= 1f) {
                    waiting = false;
                }
                yield return new WaitForSeconds(0.05f);
            }

            timeElapsed += Time.deltaTime;
            boostPanel.position -= new Vector3(-12f, 0f, 0f);

            if(timeElapsed >= 0.2f) {
                running1 = false;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void StartBoost(int boost) {
        switch(boost) {
            case 0: // 100m head start
                if(InvScript.GetNumberOfBoosts(0)>1)
                    StartCoroutine(Boost(100f));
            break;
            case 1: // 200m head start
                if(InvScript.GetNumberOfBoosts(1)>1)
                    StartCoroutine(Boost(200f));
            break;
            case 2: // shield
                if(InvScript.GetNumberOfBoosts(2)>1)
                    StartCoroutine(ActivateShield(5f));

            break;
        }
    }

    IEnumerator ActivateShield(float duration) {
        shieldObj.SetActive(true);
        immunity = true;
        yield return new WaitForSeconds(duration);
        immunity = false;
        shieldObj.SetActive(false);
    }

    IEnumerator Boost(float dist) {
        bool boosting = true;
        shieldObj.SetActive(true);
        immunity = true;
        while(boosting) {
            gameEnv.transform.position += new Vector3(0.9f * direction * Time.deltaTime, speed * 5 * Time.deltaTime, 0f);
            timeElapsed += 5 * Time.deltaTime;
            distance += speed * 5 * Time.deltaTime;
            if(distance > dist) {
                boosting = false;
            }
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(ActivateShield(2f));
    }

    public void GameOver() {
        EndGame();
        /*
        paused = true;
        reviveObj.SetActive(true);
        */

    }

    public void Revive() {
        paused = false;
        StartCoroutine(ActivateShield(2f));
        reviveObj.SetActive(false);
    }

    public void EndGame() {
        PlayerPrefs.SetInt("DistanceTravelled", (int)distance);
        PlayerPrefs.SetInt("GameOver", 1);
        SceneManager.LoadScene("MenuScene");
    }
}
