using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    public static SoundSystem instance;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }

    public Dictionary<string, AudioClip> sfxClips = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> bgmClips = new Dictionary<string, AudioClip>();

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public Sound[] bgm;
    public Sound[] sfx;

    #region Singleton
    private void Awake()
    {
        if(instance != null)
        {
            return;
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    Coroutine sfxAsync;

    private void Start()
    {
        for (int i = 0; i < sfx.Length; i++) sfxClips.Add(sfx[i].name, sfx[i].clip);
        for (int i = 0; i < bgm.Length; i++) bgmClips.Add(bgm[i].name, bgm[i].clip);

        PlayMusic("ambiance_1");
    }

    public void AddSfxSound(string name, AudioClip clip)
    {
        sfxClips.Add(name, clip);
    }

    /// <summary>
    /// Applicable in Inspector button
    /// </summary>
    /// <param name="name"></param>
    public void PlaySfx(string name)
    {
        if (!sfxClips.ContainsKey(name))
        {
            string l = "List of Sounds: ";

            foreach (string s in sfxClips.Keys)
                l += s + ", ";

            Debug.LogErrorFormat(gameObject, "SFX Sound named {0} doesn't exist. {1}", name, l);

            return;
        }

        sfxSource.PlayOneShot(sfxClips[name]);
    }

    /// <summary>
    /// For hard code, you can add more audiosources and another audio clips
    /// </summary>
    /// <param name="name"></param>
    /// <param name="clip"></param>
    /// <param name="sources"></param>
    public void PlaySfx(string name, AudioClip clip, AudioSource sources)
    {
        sources.PlayOneShot(sfxClips[name]);
    }

    public IEnumerator PlaySfxAsync(string name = "default", bool state = true)
    {
        if (name == "default" && state == true)
        {
            Debug.Log("Sfx name not set");
            yield break;
        }

        while (!sfxSource.isPlaying)
        {
            if (sfxSource == null || state == false)
            {
                sfxSource.Stop();
                yield break;
            }
            PlaySfx(name);
            Debug.Log("playing sound");
            yield return null;
        }
    }

    public void PlayMusic(string name)
    {
        musicSource.Stop();

        musicSource.clip = bgmClips[name];
        musicSource.Play();
    }
}
