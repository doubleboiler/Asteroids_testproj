using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;

	private GameController gameController;
	private HighScoreManager highScoreManager;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		highScoreManager = gameControllerObject.GetComponent<HighScoreManager>();

		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("EnemyBolt"))
		{
			return;
		}
		
		if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);
		}

		if (other.CompareTag("Player"))
		{
			gameController.DealDamage();
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);


		}
		else
		{
			highScoreManager.AddScore(scoreValue);
		}

		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
