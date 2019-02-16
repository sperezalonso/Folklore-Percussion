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
    private AudioSource rotatingSpeaker;    // The synth sound looping at the beginning
    //private AudioClip rotatingSpeakerClip;
    
    [SerializeField] private AudioClip startNoiseClip;
    [SerializeField] AudioClip rotatingVoiceClip;
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
	    rotatingSpeaker = GameObject.FindGameObjectWithTag("StartNoise").GetComponent<AudioSource>();
	    
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
	            EnterFreeMode();
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
        UpdateSpeaker("Voice");
        
        // Play all tracks of the song
        foreach (GameObject speaker in speakers)
        {
            speaker.GetComponent<AudioSource>().Play();
        }
        songPlaying = true;
    }

    void EnterFreeMode()
    {
        currentState = GameState.FreeMode;
        songPlaying = false;
        UpdateSpeaker("Noise");
    }

    // Function used to change the clips of the rotating speaker between start sounds and voice
    void UpdateSpeaker(string clip)
    {
        switch (clip)
        {
            case "Voice":
                rotatingSpeaker.loop = false;
                rotatingSpeaker.clip = rotatingVoiceClip;
                break;
            
            case "Noise":
                rotatingSpeaker.loop = true;
                rotatingSpeaker.clip = startNoiseClip;
                break;
        }
        rotatingSpeaker.Play();
    }

}
