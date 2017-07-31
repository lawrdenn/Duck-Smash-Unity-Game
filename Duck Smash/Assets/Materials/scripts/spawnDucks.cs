using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class spawnDucks : MonoBehaviour 
{
	public GameObject duck;
	public GameObject duck2;

	public Text score;

	public float duckTimer;

	public int score_i;

	public GameObject blade;

	public float globalDuckSpeed;

	public AudioSource music;

	public AudioSource deadDuck;

	public GameObject bladeButton;
	public GameObject clearBladesButton;

	public GameObject home;
	public GameObject game;

	int howToPlay_i;

	void Start () 
	{
		PlayGamesPlatform.Activate ();
		
		Social.localUser.Authenticate((bool success) => {
			//
		});

		score_i = PlayerPrefs.GetInt ("score");

		globalDuckSpeed = 2f;

		barrier = GameObject.Find ("barrier");

		StartCoroutine (spawnDuck ());
		//Instantiate (duck, new Vector3 (Random.Range(-2.5f, 2.5f), 4.14f, -10), Quaternion.identity);

		homeMenu ();

		howToPlay_i = PlayerPrefs.GetInt ("htp");
	}
	

	void Update () 
	{
		score.text = "" + score_i;

		if (Input.GetKeyDown (KeyCode.Delete)) 
		{
			PlayerPrefs.DeleteAll ();
		}
	}

	public int randomDuck;

	public GameObject Ducks;

	public IEnumerator spawnDuck ()
	{
		randomDuck = Random.Range (1, 11);
		
		yield return new WaitForSeconds (duckTimer);

		if (randomDuck != 10) 
		{
			GameObject duckClone = Instantiate (duck, new Vector3 (Random.Range (-2.5f, 2.5f), 4.14f, -10), Quaternion.identity);
			duckClone.transform.parent = Ducks.transform;
			duckClone.name = "duckClone";
		}
		if (randomDuck == 10) 
		{
			GameObject duckClone = Instantiate (duck2, new Vector3 (Random.Range (-2.5f, 2.5f), 4.14f, -10), Quaternion.identity);
			duckClone.transform.parent = Ducks.transform;
			duckClone.name = "duckClone2";
		}
		StartCoroutine (spawnDuck ());
	}

	public void spawnBlade()
	{
		if (blades.transform.childCount < bladeAmount) 
		{
			GameObject bladeClone = Instantiate (blade, new Vector3 (Random.Range (-1.5f, 1.5f), 4.5f, Random.Range (-1.5f, -6.5f)), Quaternion.identity);
			bladeClone.name = "Blade";
			bladeClone.transform.parent = blades.transform;
		}
	}

	public GameObject blades;

	public void clearBlades()
	{
		foreach (Transform child in blades.transform)
		{
			Destroy (child.gameObject);
		}
	}

	public GameObject gameOverPanel;

	public Text gameover_score;
	public Text gameover_highScore;

	public GameObject newHighScoreText;

	private GameObject barrier;

	int bladeAmount;

	public GameObject GameOver;

	public void gameOver ()
	{
		GameOver.GetComponent<GameOverAnim> ().animationPlay ();
		
		game.SetActive (false);
	
		blade.SetActive (false);

		gameOverPanel.SetActive (true);

		gameover_score.text = "" + score_i;

		if (score_i > PlayerPrefs.GetInt ("high_score")) 
		{
			gameover_highScore.text = "" + score_i;
			PlayerPrefs.SetInt ("high_score", score_i);
			newHighScoreText.SetActive (true);

			if (score_i >= 100) 
			{
				PlayerPrefs.SetInt ("blades_", 1);
			}
			if (score_i >= 150) 
			{
				PlayerPrefs.SetInt ("blades_", 2);
			}

		} else 
		{
			gameover_highScore.text = "" + PlayerPrefs.GetInt ("high_score");

			newHighScoreText.SetActive (false);

			Social.ReportScore(score_i, "CgkIub7Cht8KEAIQAA", (bool success) => {
				
			});
		}

		barrier.SetActive (false);

		music.Stop ();

		int show_ad = Random.Range (1, 5);

		if (show_ad == 3) 
		{
			GameObject.Find ("Ad Managment").GetComponent<ad_script> ().ShowAd ();
		}

		increaseBlade = false;

		GameObject.Find ("Ad Managment").GetComponent<ad_script> ().AdGameOver ();
			
	}

	public GameObject howToPlay;

	public bool increaseBlade;

	public void play ()
	{
		if (increaseBlade == true) 
		{
			blade.transform.localScale = new Vector3 (0.85f, 0.01f, 0.85f);
		} else 
		{
			blade.transform.localScale = new Vector3 (0.7f, 0.01f, 0.7f);
		}
		
		foreach (Transform child in Ducks.transform) {
			Destroy (child.gameObject);
		}

		game.SetActive (true);

		gameOverPanel.SetActive (false);

		score_i = 0;

		globalDuckSpeed = 3f;

		barrier.SetActive (true);

		blade.transform.position = new Vector3 (0, 4.5f, -4.5f);

		blade.SetActive (true);

		home.SetActive (false);

		score.gameObject.SetActive (true);

		/*if (PlayerPrefs.GetInt ("blades_") == 1) 
		{
			bladeButton.SetActive (true);
			clearBladesButton.SetActive (true);
			bladeAmount = 1;
		}
		if (PlayerPrefs.GetInt ("blades_") == 2) 
		{
			bladeButton.SetActive (true);
			clearBladesButton.SetActive (true);
			bladeAmount = 2;
		}	*/

		clearBlades ();

		if (howToPlay_i == 0) 
		{
			howToPlay.SetActive (true);
			duckTimer = 1;
			howToPlay_i = 1;
			PlayerPrefs.SetInt ("htp", 1);
			barrier.SetActive (false);
			blade.SetActive (false);
			score.gameObject.SetActive (false);
		} else 
		{
			howToPlay.SetActive (false);
			duckTimer = 1;

			//Instantiate (duck, new Vector3 (Random.Range(-2.5f, 2.5f), 4.14f, -10), Quaternion.identity);
			music.Play ();
		}
	}

	public void homeMenu ()
	{
		home.SetActive (true);
		gameOverPanel.SetActive (false);

		game.SetActive (false);

		blade.SetActive (false);
		barrier.SetActive (false);

	}

	public bool adsOn;

	public void Ads(bool On)
	{
		adsOn = On;
	}
	public void rateApp()
	{
		Application.OpenURL ("https://play.google.com/store/apps/details?id=com.ldappsstudios.ducksplosion&hl=en_GB");
	}
	public void showHighscores()
	{
		Social.localUser.Authenticate((bool success) => {

		});
		Social.ShowLeaderboardUI();
	}
}
