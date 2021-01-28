using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float fireRate = 0.5F;
	public float speed = 5F;
	public float rotationSpeed = 150F;
	public float tilt;
	public GameObject shot;
	public Transform shotSpawn;

	private float myTime = 0.0F;
	private float nextFire = 0.5F;

	void Update()
	{
		Quaternion rot = transform.rotation;
		float y = rot.eulerAngles.y;

		y += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

		rot = Quaternion.Euler(0, y, 0);
		transform.rotation = rot;
		Vector3 position = transform.position;

		Vector3 velocity = new Vector3(0, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime);

		position += rot * velocity;
		transform.position = position;

		myTime += Time.deltaTime;

		if (Input.GetButton("Fire1") && myTime > nextFire)
		{
			nextFire = myTime + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

			nextFire -= myTime;
			myTime = 0.0F;
		}
	}
}
