using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioSource MusicAS;
    public Slider MusicSldr;

    public static MusicManager instance;

    [Range(-80, 20)]
    public float MusicVol;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MasterVolume()
    {
        musicMixer.SetFloat("MasterVolume", MusicSldr.value);
    }

    void Start()
    {
        PlayAudio(MusicAS);
        MusicSldr.value = MusicVol;
        MusicSldr.minValue = -80;
        MusicSldr.maxValue = 20;
    }

    void Update()
    {
        MasterVolume();
    }

    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }
}