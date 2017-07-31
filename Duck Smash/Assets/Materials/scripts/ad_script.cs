using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class ad_script : MonoBehaviour 
{
	public GameObject BladeButton;
	
	public void ShowAd()
	{
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}
	}

	public void AdGameOver()
	{
		int isBladeButtonActive = Random.Range (1, 5);

		if (Advertisement.IsReady ("rewardedVideo")) {
			if (isBladeButtonActive == 3) {
				BladeButton.SetActive (true);
			} 
			if (isBladeButtonActive != 3) {
				BladeButton.SetActive (false);
			}
		} else {
			BladeButton.SetActive (false);
		}
	}
		
	public void ShowRewardedAd()
	{
		if (Advertisement.IsReady ("rewardedVideo")) {
			
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show ("rewardedVideo", options);
		}
	}

	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log ("The ad was successfully shown.");
			// reward...
			GameObject.Find ("SpawnDuck").GetComponent<spawnDucks> ().increaseBlade = true;
			BladeButton.SetActive (false);
			//
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}

	void Start () 
	{
		Advertisement.Initialize ("1436437", true);
	}
	

	void Update () 
	{
		
	}
}
