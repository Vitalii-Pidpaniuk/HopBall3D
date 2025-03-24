using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource musicChannel;
    public AudioSource backgroundMusic;
    public AudioSource sfxChannel;

    public AudioClip ballJump;
    public AudioClip flowerCatch;
    public AudioClip fall;
    public AudioClip buttonPress;

    public AudioClip musicTrack;
    
    public bool sfxOn;
    public bool musicOn;

    
    [SerializeField] private Image sfxButton;
    [SerializeField] private Image musicButton;
    [SerializeField] private Sprite sfxOffSprite;
    [SerializeField] private Sprite sfxOnSprite;
    [SerializeField] private Sprite musicOffSprite;
    [SerializeField] private Sprite musicOnSprite;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        sfxOn = DataManager.LoadSFXSetting();
        musicOn = DataManager.LoadMusicSetting();
        
        musicChannel.Play();
        sfxChannel.Play();
        
        if(musicOn)
        {
            musicChannel.UnPause();
        }
        else
        {
            musicChannel.Pause();
        }
        if(sfxOn)
        {
            sfxChannel.UnPause();
        }
        else
        {
            musicChannel.Pause();
        }
    }

    public void PlayTrack()
    {
        musicChannel.UnPause();
    }
    
    public void PauseTrack()
    {
        musicChannel.Pause();
    }

    public void BallJumpSfx()
    {
        sfxChannel.PlayOneShot(ballJump);
    }
    
    public void FlowerCatchSfx()
    {
        sfxChannel.PlayOneShot(flowerCatch);
    }
    
    public void FallSfx()
    {
        sfxChannel.PlayOneShot(fall);
    }
    
    public void ButtonPressSfx()
    {
        sfxChannel.PlayOneShot(buttonPress);
    }
    
    public void ChangeSfxState()
    {
        if (sfxOn)
        {
            sfxChannel.Pause();
            sfxButton.sprite = sfxOffSprite;
        }
        else
        {
            sfxChannel.UnPause();
            sfxButton.sprite = sfxOnSprite;
        }
        
        sfxOn = !sfxOn;
        DataManager.SaveSFXSetting(sfxOn);
    }
    
    public void ChangeMusicState()
    {
        if (musicOn)
        {
            musicChannel.Pause();
            musicButton.sprite = musicOffSprite;
        }
        else
        {
            musicChannel.UnPause();
            musicButton.sprite = musicOnSprite;        
        }

        musicOn = !musicOn;
        DataManager.SaveMusicSetting(musicOn);
    }
}
