using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duck_script : MonoBehaviour 
{
	public GameObject blood1;

	private float speed;

	void Start () 
	{
		blood1.SetActive (false);

		gameObject.isStatic = false;

		if (gameObject.name != "Duck1") 
		{
			if (gameObject.name != "Duck2") 
			{
				StartCoroutine (destroyObject2 ());
			}
		}
	}

	public BoxCollider bc;

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "Blade" || col.gameObject.name == "Blade2" || col.gameObject.name == "Blade3") 
		{
			if (gameObject.name == "duckClone2") 
			{
				GameObject.Find ("SpawnDuck").GetComponent<spawnDucks> ().deadDuck.pitch = 2f;
			}
			if (gameObject.name == "duckClone") 
			{
				GameObject.Find ("SpawnDuck").GetComponent<spawnDucks> ().deadDuck.pitch = 1.5f;
			}
			
			blood1.SetActive (true);

			StartCoroutine (destroyObject ());

			GameObject.Find ("SpawnDuck").GetComponent<spawnDucks> ().deadDuck.Play ();

			bc.enabled = false;
		}
		if (col.gameObject.name == "barrier") 
		{
			GameObject.Find ("SpawnDuck").GetComponent<spawnDucks> ().gameOver ();

			Destroy (gameObject);
		}
		if (col.gameObject.name == "barrier2") 
		{
			Destroy (gameObject);
		}
	}
		
	public IEnumerator destroyObject()
	{
		yield return new WaitForSeconds (1);
		Destroy (gameObject);
	}
	public IEnumerator destroyObject2()
	{
		yield return new WaitForSeconds (6);
		Destroy (gameObject);
	}

	public GameObject SpawnDuck;

	void Update () 
	{
		if (gameObject.name != "Duck1" || gameObject.name != "Duck2") 
		{
			if (gameObject.name == "duckClone") 
			{
				transform.position += Vector3.forward * Time.deltaTime * SpawnDuck.GetComponent<spawnDucks> ().globalDuckSpeed;
			}
			if (gameObject.name == "duckClone2") 
			{
				transform.position += Vector3.forward * Time.deltaTime * SpawnDuck.GetComponent<spawnDucks> ().globalDuckSpeed * 1.25f;
			}
		}
	}
}
