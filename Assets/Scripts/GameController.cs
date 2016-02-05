using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	//pulbic variables
	public GameObject[] enimies;
	public GameObject[] PowerUps;
	public GameObject Boss;
	public GameObject player;
	public Text scoreText;
	public Text gameOver;
	public Text finalScore;
	public Text restartText;
	public Text startText;
	public Image[] LifeImage;

	//private variables
	private static int playerLife;
	private float timediff;
	private int bossLife;
	private int spawnCount;
	private int enemyCount;
	private int playerScore;
	private int displayImg;
	private bool bossActive;
	private bool isGameOver;

	// Use this for initialization
	void Start () {

		player.gameObject.SetActive (true);

		scoreText.text = "Score : 0";
		displayImg = LifeImage.Length - 1;
		playerLife = 3;
		bossLife = 10;
		spawnCount = 30;
		enemyCount = 0;
		startText.enabled = false;
		gameOver.enabled = false;
		finalScore.enabled = false;
		restartText.enabled = false;
		bossActive = false;
		isGameOver = false;

		Boss.gameObject.SetActive (true);

		StartCoroutine (SpawnEnimies ());
		StartCoroutine (SpawnPowerUps ());

	}

	void Update(){
	
		if (isGameOver) {
			if(Input.GetKeyDown(KeyCode.Space)){
				SceneManager.LoadScene("Main");
			}
			if(Input.GetKeyDown(KeyCode.R)){
				SceneManager.LoadScene("Level1");
			}

			player.gameObject.SetActive (false);
			Boss.gameObject.SetActive (false);

			GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawn");
			foreach (GameObject target in spawns) {
				GameObject.Destroy(target);
			}

			GameObject[] asteroid = GameObject.FindGameObjectsWithTag("Asteroid");
			foreach (GameObject target in asteroid) {
				GameObject.Destroy(target);
			}

			GameObject[] spawnbullets = GameObject.FindGameObjectsWithTag("SpawnBullet");
			foreach (GameObject target in spawnbullets) {
				GameObject.Destroy(target);
			}
		}

		CheckGameOver ();

	}

	IEnumerator SpawnPowerUps(){
		yield return new WaitForSeconds (20F);

		while (!isGameOver) {
				Vector2 spawnPosition = new Vector2 (450f, Random.Range (-180f, 180f));
				Instantiate (PowerUps [Random.Range (0, PowerUps.Length)], spawnPosition, Quaternion.identity);
				yield return new WaitForSeconds (40F);
		}

	}

	IEnumerator SpawnEnimies(){

		PlayerCollision.canTakeDamage = true;

		Instantiate (player, player.transform.position, player.transform.rotation);

		yield return new WaitForSeconds (1F);

		while(true){

			if (EnemyCount () >= spawnCount) {
				GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawn");
				foreach (GameObject target in spawns) {
					GameObject.Destroy(target);
				}
				GameObject[] spawnbullets = GameObject.FindGameObjectsWithTag("SpawnBullet");
				foreach (GameObject target in spawnbullets) {
					GameObject.Destroy(target);
				}
				GameObject[] asteroid = GameObject.FindGameObjectsWithTag("Asteroid");
				foreach (GameObject target in asteroid) {
					GameObject.Destroy(target);
				}
			}

			if (EnemyCount() < spawnCount && !bossActive) {

					GameObject Enemy = enimies [Random.Range (0, enimies.Length)];
					Vector2 spawnPosition = new Vector2 (450f, Random.Range (-180f, 180f));

					Instantiate (Enemy, spawnPosition, Quaternion.identity);

					yield return new WaitForSeconds (1F);

			} else {
				if (!bossActive && !isGameOver) {
					bossActive = true;
					yield return new WaitForSeconds (2F);
					Instantiate (Boss, new Vector2 (320f, Random.Range (-180f, 180f)), Quaternion.identity);
				}
			}	
		
			yield return new WaitForSeconds (1F);
		}
	}


	public void AddScore(int addScore){
		enemyCount++;
		playerScore += addScore;
		ShowScore ();
		//Debug.Log (playerScore);
	}

	public void MinusScore(int score){
		playerScore -= score;
		ShowScore ();
	}

	private void ShowScore(){
		scoreText.text = "Score : " + playerScore;
	}

	private int EnemyCount(){
		return enemyCount;
	}

	private void CheckGameOver(){

		//Debug.Log ("GC " +bossLife);

		if (playerLife <= 0) {
			PrintGUI ();
			gameOver.text = "Game Over";
			Boss.gameObject.SetActive (false);
			isGameOver = true;
		} 

		if(GetBossLife () <= 0){
			PrintGUI ();
			gameOver.text = "You Won";
			player.gameObject.SetActive (false);
			isGameOver = true;
		}
			

	}

	public void PrintGUI(){

		for(int i = displayImg; i >= 0; i --) {
			LifeImage [i].enabled = false;

		}
		scoreText.enabled = false;

		gameOver.enabled = true;
		finalScore.enabled = true;
		restartText.enabled = true;
		startText.enabled = true;
		finalScore.text = "Final Score : " + playerScore;
		restartText.text = "Press R to Restart";
		startText.text = "Press Space for Splash Screen";
		isGameOver = true;
	}

	public void DecreaseLife (){
		playerLife--;
		LifeImage [displayImg].enabled = false;
		displayImg--;

		MinusScore (10);

		if(playerLife > 0)
			StartCoroutine (InstantiatePlayer ());
	}

	public void IncreaseLife (){
		displayImg++;
		LifeImage [displayImg].enabled = true;
		playerLife++;
	}
		
	IEnumerator InstantiatePlayer(){
		yield return new WaitForSeconds (1F);
		Instantiate (player, player.transform.position, player.transform.rotation);
	}

	public void BossLife(int damage){

		bossLife = bossLife - damage;
	}

	public int GetBossLife(){
		return (bossLife);
	}

	public static int CheckPlayerLife(){
		return playerLife;
	}
}