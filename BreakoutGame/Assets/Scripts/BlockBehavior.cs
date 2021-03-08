using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
{
    public GameObject powerUp;

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        Destroy(gameObject);
    }
}
