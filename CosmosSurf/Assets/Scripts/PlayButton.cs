using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public bool running = true;
    public RectTransform navBar;

    public void OnClick() {
        StartCoroutine(PlayAnim());
    }

    IEnumerator PlayAnim() {
        float timeElapsed = 0f;
        float sF = 1f;

        while(running) {
            timeElapsed += Time.deltaTime;
            sF = (0.1f - timeElapsed) / 0.1f;
            GetComponent<RectTransform>().eulerAngles += new Vector3(0f, 0f, 5f);
            GetComponent<RectTransform>().localScale = new Vector3(sF, sF, 1f);
            navBar.position -= new Vector3(0f, 15f, 0f);

            if(timeElapsed >= 0.1f) {
                running = false;
            }
            yield return new WaitForSeconds(0.05f);
        }

        SceneManager.LoadScene("MainScene");
    }
}
