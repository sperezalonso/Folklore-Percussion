using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{

    public AudioMixer masterMixer;

    public void SetMusicVolume(float musicVol)
    {
        masterMixer.SetFloat("MasterVol", musicVol);
    }
    
    public void SetMarimbaVolume(float marimbaVol)
    {
        masterMixer.SetFloat("MarimbaVol", marimbaVol);
    }
}
