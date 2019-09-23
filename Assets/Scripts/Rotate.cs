using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 0.2f;

    void Update()
    {
        gameObject.transform.Rotate(0, 0, speed);
    }
}
