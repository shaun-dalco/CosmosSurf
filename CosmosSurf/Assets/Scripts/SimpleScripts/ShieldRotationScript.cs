using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRotationScript : MonoBehaviour
{
    float z = 0;
    void Update()
    {
        z = z + 0.3f;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, z);
    }
}
