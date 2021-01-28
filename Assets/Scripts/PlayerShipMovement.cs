using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipMovement : MonoBehaviour
{
    public float speed;
    public float tilt;

    public float xMin, xMax, zMin, zMax;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire;

    void Start()
    {
        
    }

	void Update()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play();
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;

		GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp(GetComponent<Rigidbody>().position.x, xMin, xMax),
			0.0f,
			Mathf.Clamp(GetComponent<Rigidbody>().position.z, zMin, zMax)
		);

		GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}
}
