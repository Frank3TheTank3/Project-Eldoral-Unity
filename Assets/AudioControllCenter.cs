using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioControllCenter : MonoBehaviour
{
    public AudioClip[] sounds;
    public AudioClip[] tracks;
    public int audioIndex = 5;
    public int soundAudioIndex = 0;
    public AudioSource audioController;
    public AudioSource soundsAudioController;
    public Slider volumeSlider;
    public Slider soundVolumeSlider;
    private void Awake()
    {
        audioController = gameObject.GetComponent<AudioSource>();
        soundsAudioController = FindObjectOfType<AudioSounds>().GetComponent<AudioSource>();
        soundsAudioController.volume = 0.5f;
    }
    void Start()
    {

        audioController.clip = tracks[audioIndex];
        audioController.Play();
        volumeSlider.onValueChanged.AddListener(delegate { changeVolume(volumeSlider.value); });
        soundVolumeSlider.onValueChanged.AddListener(delegate { changeSoundVolume(soundVolumeSlider.value); });
    }



    public void nextTrack()
    {
        if (audioIndex >= 0 && audioIndex < tracks.Length - 1)
        {
            audioController.Stop();
            audioIndex++;
            audioController.clip = tracks[audioIndex];
            audioController.Play();
        }
        else
        {
            audioIndex = 0;
            nextTrack();
        }
    }

    public void setTrack(int _trackNumber)
    {
        if (_trackNumber < tracks.Length)
        {
            audioController.Stop();
            audioIndex = _trackNumber;
            audioController.clip = tracks[_trackNumber];
            audioController.Play();
        }
    }

    public void setSound(int _soundNumber)
    {
        if (_soundNumber < sounds.Length)
        {
            soundAudioIndex = _soundNumber;
            soundsAudioController.clip = sounds[_soundNumber];
            soundsAudioController.Play();
        }
    }
    void changeVolume(float sliderValue)
    {
        audioController.volume = sliderValue;
        audioController.clip = tracks[audioIndex];
        audioController.Play();
    }
    void changeSoundVolume(float sliderValue)
    {
        soundsAudioController.volume = sliderValue;
        soundsAudioController.clip = sounds[0];
        soundsAudioController.Play();
    }
}
