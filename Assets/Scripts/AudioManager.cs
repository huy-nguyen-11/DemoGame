using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

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
            return;
        }
    }

    public void PlayMusic(string name)
    {
        Sound _sound = Array.Find(musicSounds, x => x.name == name);

        if (_sound == null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            musicSource.clip = _sound.clip;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(string name)
    {
        Sound _sound = Array.Find(sfxSounds, x => x.name == name);

        if (_sound == null)
        {
            Debug.Log("sound sfx not found ");
        }
        else
        {
            sfxSource.PlayOneShot(_sound.clip);
        }
    }

    //private IEnumerator WaittingForSFX()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    PlaySFX("PlayMode");
    //}

    //public void PlaySFX2()
    //{
    //    StartCoroutine(WaittingForSFX());
    //}

    //public void PlaySFX3()
    //{
    //    StartCoroutine(WaittingForSFX2());
    //}

    //private IEnumerator WaittingForSFX2()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    PlaySFX("AddCoin");
    //}

    //public void ToggleMusic(bool on)
    //{
    //    musicSource.volume = on ? 1 : 0;
    //    SaveAudioSettings();
    //}

    //public void ToggleSFX(bool on)
    //{
    //    sfxSource.volume = on ? 1 : 0;
    //    SaveAudioSettings();
    //}

    //public void SaveAudioSettings()
    //{
    //    PlayerPrefs.SetInt("MusicOn", musicSource.volume > 0 ? 1 : 0);
    //    PlayerPrefs.SetInt("SFXOn", sfxSource.volume > 0 ? 1 : 0);
    //    PlayerPrefs.Save();
    //}

    //public void LoadAudioSettings()
    //{
    //    ToggleMusic(PlayerPrefs.GetInt("MusicOn", 1) == 1);
    //    ToggleSFX(PlayerPrefs.GetInt("SFXOn", 1) == 1);
    //}

    //public int GetMusicSetting()
    //{
    //    return PlayerPrefs.GetInt("MusicOn", 1);
    //}

    //public int GetSFXSetting()
    //{
    //    return PlayerPrefs.GetInt("SFXOn", 1);
    //}


    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }
}
