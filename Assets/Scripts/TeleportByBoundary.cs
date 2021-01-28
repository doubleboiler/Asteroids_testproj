using UnityEngine;

public class TeleportByBoundary : MonoBehaviour
{
	void OnTriggerExit(Collider other)
	{
		var otherPos = other.transform.position;

		if(otherPos.x < Boundary.xMin)
        {
			otherPos.x = Boundary.xMax;
		}

		if(otherPos.x > Boundary.xMax)
        {
			otherPos.x = Boundary.xMin;
		}

		if(otherPos.z < Boundary.zMin)
        {
			otherPos.z = Boundary.zMax;
		}

		if(otherPos.z > Boundary.zMax)
        {
			otherPos.z = Boundary.zMin;
		}

		other.transform.position = otherPos;
	}
}
