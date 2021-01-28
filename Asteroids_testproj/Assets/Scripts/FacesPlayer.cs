using UnityEngine;

public class FacesPlayer : MonoBehaviour
{
	public float rotSpeed = 90f;
	private Transform player;

	void Update ()
	{
		if(player == null)
		{
			GameObject go = GameObject.FindWithTag("Player");

			if(go != null)
			{
				player = go.transform;
			}
		}

		if(player == null)
			return;

		Vector3 dir = player.position - transform.position;
		dir.Normalize();

		float yAngle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg + 90;

		Quaternion desiredRot = Quaternion.Euler(0, -yAngle, 0);

		transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, rotSpeed * Time.deltaTime);
	}
}
