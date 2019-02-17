using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    private enum GameState
    {
        Intro,
        FreeMode,
        Jam
    }

    private GameState currentState;
    private GameObject[] speakers;
    private GameObject[] spotlights;
    private bool songPlaying; // Flag used to determine if the song is currently playing or already finished
    private bool menuOpen; // Flag used to toggle open or close menu
    private bool lightsOn;
    private float timer;

    private AudioSource songTrack; // Save one track of the song to check if the song is playing or not
    private AudioSource rotatingSpeaker; // This speaker plays different audio clips depending on the game state

    [SerializeField] private AudioMixer masterMixer; // Access the mixer to adjust volume levels on the menu
    [SerializeField] private AudioClip startNoiseClip; // Synth sound playing during intro and free mode
    [SerializeField] private AudioClip rotatingVoiceClip; // One of the voice tracks of the song
    [SerializeField] private GameObject[] uiElements; // Elements of the menu


    void Start()
    {
        currentState = GameState.Intro;
        songPlaying = false;
        menuOpen = false;
        speakers = GameObject.FindGameObjectsWithTag("Speaker");
        spotlights = GameObject.FindGameObjectsWithTag("Spotlight");
        rotatingSpeaker = GameObject.FindGameObjectWithTag("StartNoise").GetComponent<AudioSource>();
        timer = Time.deltaTime;
        SetLightsToZero();

        // Save one track of the song to check if the song is already finished playing
        // Since all tracks have the same length, this can be any clip
        songTrack = speakers[0].GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!lightsOn) TurnLightsOn();

        // If player chose to play the song
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

    // Function called by Start Song button
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

    // Functions called by the volume sliders in the menu, used to adjust volume levels
    public void SetMusicVolume(float musicVol)
    {
        masterMixer.SetFloat("MasterVol", musicVol);
    }

    public void SetMarimbaVolume(float marimbaVol)
    {
        masterMixer.SetFloat("MarimbaVol", marimbaVol);
    }

    // Play all tracks of the song
    void PlaySong()
    {
        UpdateSpeaker("Voice");
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

    // Function used to change the clips of the rotating speaker between start sounds and voice, and play the clip
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

    void SetLightsToZero()
    {
        // First set intensity of all lights to 0 on start
        foreach (GameObject spotlight in spotlights)
        {
            spotlight.GetComponent<Light>().intensity = 0f;
        }

        lightsOn = false;
    }

    void TurnLightsOn()
    {
        float newIntensity;
        float currentIntensity;

        // Slowly turn lights on
        for (int i = 0; i < spotlights.Length; i++)
        {
            currentIntensity = spotlights[i].GetComponent<Light>().intensity;
            switch (spotlights[i].name)
            {
                case ("Spotlight Strong"):
                    newIntensity = 3f;
                    break;
                case ("Spotlight Soft"):
                    newIntensity = 2.2f;
                    break;
                case ("Center Light"):
                    newIntensity = 2.5f;
                    break;
                case ("Floor Light"):
                    newIntensity = 1.5f;
                    break;
                case ("Speaker Light"):
                    newIntensity = 3f;
                    break;
                default:
                    newIntensity = 1f;
                    break;
            }

            spotlights[i].GetComponent<Light>().intensity =
                Mathf.Lerp(currentIntensity, newIntensity, Time.deltaTime * 0.09f);
            timer += Time.deltaTime;
        }
        // Stop running the function after a minute
        if (timer * 0.1f >= 60f) lightsOn = true;
    }
}