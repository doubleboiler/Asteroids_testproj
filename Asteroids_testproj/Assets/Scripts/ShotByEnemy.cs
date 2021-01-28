using UnityEngine;

public class ShotByEnemy : MonoBehaviour
{
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate = 0.5F;

    private float time = 0.0F;
    private float nextFire = 0.5F;

    void Update()
    {
        time += Time.deltaTime;

        if (time > nextFire)
        {
            nextFire = time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

            nextFire -= time;
            time = 0.0F;
        }
    }
}
