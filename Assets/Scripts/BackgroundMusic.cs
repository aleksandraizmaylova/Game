using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [System.Serializable]
    public class LocationMusic
    {
        public string locationName;
        public AudioClip musicClip;
        [Range(0, 1)] public float volume = 0.5f;
    }

    public LocationMusic[] locationMusics;
    public float fadeDuration = 1f;

    private AudioSource[] audioSources;
    private int currentSourceIndex = 0;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        audioSources = new AudioSource[2];
        for (int i = 0; i < 2; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            audioSources[i].loop = true;
            audioSources[i].playOnAwake = false;
        }
    }

    public void ChangeMusic(string locationName)
    {
        LocationMusic locationMusic = System.Array.Find(locationMusics, x => x.locationName == locationName);
        if (locationMusic == null)
        {
            Debug.LogWarning($"Music for location {locationName} not found!");
            return;
        }

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeMusic(locationMusic));
    }

    private IEnumerator FadeMusic(LocationMusic newMusic)
    {
        int newSourceIndex = 1 - currentSourceIndex;
        AudioSource newSource = audioSources[newSourceIndex];
        AudioSource currentSource = audioSources[currentSourceIndex];

        // Настраиваем новый источник
        newSource.clip = newMusic.musicClip;
        newSource.volume = 0f;
        newSource.Play();

        // Плавное перекрестное затухание
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / fadeDuration;
            currentSource.volume = Mathf.Lerp(newMusic.volume, 0f, progress);
            newSource.volume = Mathf.Lerp(0f, newMusic.volume, progress);
            yield return null;
        }

        // Завершаем переход
        currentSource.Stop();
        currentSourceIndex = newSourceIndex;
    }

    public void StopMusic()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        StartCoroutine(FadeOutMusic());
    }

    private IEnumerator FadeOutMusic()
    {
        AudioSource currentSource = audioSources[currentSourceIndex];
        float startVolume = currentSource.volume;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            currentSource.volume = Mathf.Lerp(startVolume, 0f, timer / fadeDuration);
            yield return null;
        }

        currentSource.Stop();
    }
}