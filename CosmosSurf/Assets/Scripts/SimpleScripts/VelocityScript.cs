using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityScript : MonoBehaviour
{

    public Vector3 vel;
    public bool running = true;

    private float speed = 0.01f;

    void Start() {
        StartCoroutine(Velcoity());
    }

    IEnumerator Velcoity() {
        while(running) {
            transform.position = new Vector3(transform.position.x, transform.position.y-speed, transform.position.z);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
