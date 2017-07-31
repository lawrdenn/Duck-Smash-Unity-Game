using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settings : MonoBehaviour 
{
	public AudioSource music1;
	public AudioSource sound1;

	public GameObject soundOnBtn;
	public GameObject soundOffBtn;

	public GameObject musicOnBtn;
	public GameObject musicOffBtn;

	public int sound;
	public int music;

	void Start () 
	{
		sound = PlayerPrefs.GetInt ("sound");
		music = PlayerPrefs.GetInt ("music");

		if (sound == 0) 
		{
			soundOff ();
		}
		if (music == 0) 
		{
			musicOff ();
		}

		if (sound == 1) 
		{
			soundOn ();
		}
		if (sound == 2) 
		{
			soundOff ();
		}

		if (music == 1) 
		{
			musicOn ();
		}
		if (music == 2) 
		{
			musicOff ();
		}
	}
	

	void Update () 
	{
		
	}

	public void soundOn ()
	{
		soundOffBtn.SetActive (true);
		soundOnBtn.SetActive (false);

		sound = 1;
		PlayerPrefs.SetInt ("sound", 1);

		sound1.volume = 0;
	}
	public void soundOff ()
	{
		soundOnBtn.SetActive (true);
		soundOffBtn.SetActive (false);

		sound = 2;
		PlayerPrefs.SetInt ("sound", 2);

		sound1.volume = 0.45f;
	}

	public void musicOn ()
	{
		musicOffBtn.SetActive (true);
		musicOnBtn.SetActive (false);

		music = 1;
		PlayerPrefs.SetInt ("music", 1);

		music1.volume = 0;
	}
	public void musicOff ()
	{
		musicOnBtn.SetActive (true);
		musicOffBtn.SetActive (false);

		music = 2;
		PlayerPrefs.SetInt ("music", 2);

		music1.volume = 1;
	}
		
}
