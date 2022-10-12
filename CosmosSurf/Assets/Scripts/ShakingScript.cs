using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingScript : MonoBehaviour
{
    public float speed = 1.0f; //how fast it shakes
    public float amount = 1.0f; //how much it shakes

    void Update()
    {
        transform.position = new Vector3(Mathf.Sin(Time.time * speed) * amount, Mathf.Sin(Time.time * speed) * amount, transform.position.z);
    }

}
