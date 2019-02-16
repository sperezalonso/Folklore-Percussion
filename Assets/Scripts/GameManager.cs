using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private enum GameState
    {
        Intro,
        FreeMode,
        Jam
    }
    private GameState currentState;
    private GameObject[] speakers;
    private bool songPlaying;
    private AudioSource startNoise;    // The synth sound looping at the beginning
    private AudioSource songTrack;    // Save one track of the song to check if the song is playing or not
    [SerializeField] private GameObject[] uiElements;
    private bool menuOpen;        // Flag used to toggle open or close menu

    void Awake()
    {
        currentState = GameState.Intro;
    }
    
	void Start ()
	{
	    songPlaying = false;
	    menuOpen = true;
	    speakers = GameObject.FindGameObjectsWithTag("Speaker");
	    startNoise = GameObject.FindGameObjectWithTag("StartNoise").GetComponent<AudioSource>();
	    
	    // Save one track of the song to check if the song is already finished playing
	    // Since all tracks have the same length, this can be any clip
	    songTrack = speakers[0].GetComponent<AudioSource>();
	}
	
	void Update () {

        // If player chose to play along to the song
	    if (currentState == GameState.Jam)
	    {
	        // Use songPlaying flag to avoid starting the song multiple times over
	        if (!songPlaying)
	        {
	            PlaySong();
	        }
	        
	        // If the song finished playing, change GameState
	        else if (songPlaying && !songTrack.isPlaying)
	        {
	            currentState = GameState.FreeMode;
	            startNoise.Play();
	        }
	    }
	    	    
	    
	}
    
    // Function called by Start Jam button
    public void StartJam()
    {
        currentState = GameState.Jam;
    }


    // Function called by Toggle Menu button, used to open or close the menu
    public void ToggleMenu()
    {
        for (int i = 0; i < uiElements.Length; i++)
        {
            uiElements[i].SetActive(!menuOpen);   
        }
        menuOpen = !menuOpen;
    }
   
    void PlaySong()
    {
        startNoise.Stop();
        
        // Play all tracks of the song
        foreach (GameObject speaker in speakers)
        {
            speaker.GetComponent<AudioSource>().Play();
        }
        songPlaying = true;
    }
    
}
