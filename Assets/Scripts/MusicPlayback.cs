using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayback : MonoBehaviour {

    private Light floorLight;
    private GameObject[] speakers;
    private GameObject[] spotlights;

	// Use this for initialization
	void Start () {
        speakers = GameObject.FindGameObjectsWithTag("Speaker");
        spotlights = GameObject.FindGameObjectsWithTag("Spotlight");
        floorLight = GameObject.FindGameObjectWithTag("Main Light").GetComponent<Light>();

        floorLight.intensity = 0f;
        foreach (GameObject s in spotlights) s.GetComponent<Light>().intensity = 0f;

        for (var i = 0; i < speakers.Length; i++) Debug.Log(i + " " + speakers[i].name);
    }
	
	// Update is called once per frame
	void Update () {
        TurnLightsOn();
        SpeakerController();
	}

    private void TurnLightsOn()
    {
        // Slowly turn light intensity on
        floorLight.intensity = Mathf.Lerp(floorLight.intensity, 2.8f, Time.deltaTime / 7);

        foreach (GameObject s in spotlights)
        {
            Light sLight = s.GetComponent<Light>();

            switch (s.name)
            {
                case "Spotlight Strong":
                    sLight.intensity = Mathf.Lerp(sLight.intensity, 3f, Time.deltaTime / 9);
                    break;
                case "Spotlight Soft":
                    sLight.intensity = Mathf.Lerp(sLight.intensity, 2.2f, Time.deltaTime / 8);
                    break;
            }
        }
    }

    private void SpeakerController()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) speakers[0].GetComponent<AudioSource>().Play();
        if (Input.GetKeyDown(KeyCode.Alpha2)) speakers[1].GetComponent<AudioSource>().Play();
        if (Input.GetKeyDown(KeyCode.Alpha3)) speakers[2].GetComponent<AudioSource>().Play();
        if (Input.GetKeyDown(KeyCode.Alpha4)) speakers[3].GetComponent<AudioSource>().Play();
    }
}

