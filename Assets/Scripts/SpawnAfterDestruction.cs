using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAfterDestruction : MonoBehaviour
{
    public GameObject[] asteroids;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bolt"))
        {
            SpawnSmallAsteroids();
        }
    }

    void SpawnSmallAsteroids()
    {
        for (int i = 0; i < asteroids.Length; i++)
        {
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(asteroids[i], rb.position, spawnRotation);
        }
    }
}
