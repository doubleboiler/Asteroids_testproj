using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public enum SpawnState { Spawning, Waiting, Counting };

	[System.Serializable]
	public class Wave
	{
		public string name;
		public Transform enemy;
		public int count;
		public float rate;
	}

	public GameObject playerShip;
	public Wave[] waves;
	public Transform[] spawnPoints;
	public float timeBetweenWaves = 5f;
	public Text restartText, gameOverText;
	public GameObject heart1, heart2, heart3;
	public int health;

	private Vector3 playersStartPos;
	private int nextWave = 0;
	private float waveCountdown;
	private float searchCountdown = 1f;
	private SpawnState state = SpawnState.Counting;
	private bool restart;
	private bool gameOver;

	public float WaveCountdown
	{
		get { return waveCountdown; }
	}

	public int NextWave
	{
		get { return nextWave + 1; }
	}

	public SpawnState State
	{
		get { return state; }
	}

    void Start()
	{
		playersStartPos = new Vector3(0, 0, -8);
		SpawnPlayer();

		heart1.gameObject.SetActive(true);
		heart2.gameObject.SetActive(true);
		heart3.gameObject.SetActive(true);

		gameOver = false;
		gameOverText.text = "";

		restart = false;
		restartText.text = "";

		waveCountdown = timeBetweenWaves;
	}

	void Update()
	{
		if (restart)
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				SceneManager.LoadScene("Game", LoadSceneMode.Single);
			}
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("Menu", LoadSceneMode.Single);
		}

		if (gameOver)
		{
			restart = true;
			restartText.text = "Press 'R' to Restart or 'Esc' to go to Menu";
		}

		if (state == SpawnState.Waiting)
		{
			if (!EnemyIsAlive())
			{
				WaveCompleted();
			}
			else
			{
				return;
			}
		}

		if (waveCountdown <= 0)
		{
			if (state != SpawnState.Spawning)
			{
				StartCoroutine(SpawnWave(waves[nextWave]));
			}
		}
		else
		{
			waveCountdown -= Time.deltaTime;
		}

	}

	public void SpawnPlayer()
	{
		Instantiate(playerShip, playersStartPos, new Quaternion());
	}

	void WaveCompleted()
	{
		state = SpawnState.Counting;
		waveCountdown = timeBetweenWaves;

		if (nextWave + 1 > waves.Length - 1)
		{
			nextWave = 0;
		}
		else
		{
			nextWave++;
		}
	}

	bool EnemyIsAlive()
	{
		searchCountdown -= Time.deltaTime;

		if (searchCountdown <= 0f)
		{
			searchCountdown = 1f;
			if (GameObject.FindGameObjectWithTag("Enemy") == null)
			{
				return false;
			}
		}

		return true;
	}

	IEnumerator SpawnWave(Wave _wave)
	{
        state = SpawnState.Spawning;

		for (int i = 0; i < _wave.count; i++)
		{
			SpawnEnemy(_wave.enemy);
			yield return new WaitForSeconds(1f / _wave.rate);
		}

		state = SpawnState.Waiting;
		yield break;
	}

	void SpawnEnemy(Transform _enemy)
	{
		Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
		Instantiate(_enemy, _sp.position, _sp.rotation);
	}

	private void GameOver()
	{
		gameOver = true;
		gameOverText.text = "GAME OVER";
	}

	public void DealDamage()
    {
		health--;
		HealthCounter();

	}

	private void HealthCounter()
    {
		switch (health)
		{
			case 3:
				heart1.gameObject.SetActive(true);
				heart2.gameObject.SetActive(true);
				heart3.gameObject.SetActive(true);
				break;
			case 2:
				heart1.gameObject.SetActive(true);
				heart2.gameObject.SetActive(true);
				heart3.gameObject.SetActive(false);
				SpawnPlayer();
				break;
			case 1:
				heart1.gameObject.SetActive(true);
				heart2.gameObject.SetActive(false);
				heart3.gameObject.SetActive(false);
				SpawnPlayer();
				break;
			case 0:
				heart1.gameObject.SetActive(false);
				heart2.gameObject.SetActive(false);
				heart3.gameObject.SetActive(false);
				GameOver();
				break;
		}
	}
}
