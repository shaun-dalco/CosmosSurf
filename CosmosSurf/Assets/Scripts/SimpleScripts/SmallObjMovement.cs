using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallObjMovement : MonoBehaviour
{
    public int direction = 0;
    public float speed = 0.05f;
    public bool running = true;

    void Start()
    {
        StartCoroutine(MoveObj());
    }

    IEnumerator MoveObj() {
        while(running) {
            transform.position = new Vector3(speed * direction + transform.position.x, transform.position.y, transform.position.z);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
