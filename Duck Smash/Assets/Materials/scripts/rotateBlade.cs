using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateBlade : MonoBehaviour 
{
	public int whatScore;

	private bool textIncrease;
	private bool textDecrease;

	public GameObject scoreText;

	void Start () 
	{
		
	}

	void Update () 
	{
		transform.Rotate (Vector3.up * 10);

		if (textIncrease) 
		{
			scoreText.transform.localScale += Vector3.up * 0.1f;
			scoreText.transform.localScale += Vector3.right * 0.1f;

			if (scoreText.transform.localScale.x > 1.75f) 
			{
				textIncrease = false;
				textDecrease = true;
			}
		}
		if (textDecrease) 
		{
			scoreText.transform.localScale += Vector3.up * -0.1f;
			scoreText.transform.localScale += Vector3.right * -0.1f;

			if (scoreText.transform.localScale.x < 1f)
			{
				textDecrease = false;
				scoreText.transform.localScale = new Vector3 (1f, 1f, 1);
			}
	    }

		if (Input.GetKeyDown (KeyCode.Delete)) 
		{
			PlayerPrefs.DeleteAll ();
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "Score Collider") {
			
			GameObject.Find ("SpawnDuck").GetComponent<spawnDucks> ().score_i += 1;

			if (GameObject.Find ("SpawnDuck").GetComponent<spawnDucks> ().globalDuckSpeed < 20 && GameObject.Find ("SpawnDuck").GetComponent<spawnDucks> ().score_i < 100) {
				GameObject.Find ("SpawnDuck").GetComponent<spawnDucks> ().globalDuckSpeed += 0.3f;
			}
			if (GameObject.Find ("SpawnDuck").GetComponent<spawnDucks> ().score_i > 100) {
				GameObject.Find ("SpawnDuck").GetComponent<spawnDucks> ().globalDuckSpeed += 0.2f;
			}
		
			Destroy (col.gameObject);

			textIncrease = true;
			textDecrease = false;
		}
		if (col.gameObject.name == "Score Collider2") 
		{
			GameObject.Find ("SpawnDuck").GetComponent<spawnDucks> ().score_i += 5;

			textIncrease = true;
			Destroy (col.gameObject);

		}

	}
}
