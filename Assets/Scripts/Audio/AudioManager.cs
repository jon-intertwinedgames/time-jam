using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    private static AudioSource managerAudioSource;
    public static AudioSource ManagerAudioSource { get => managerAudioSource; }

    private static Dictionary<SFX, AudioClip> sfxDictionary = new Dictionary<SFX, AudioClip>();
    private static Dictionary<Music, AudioClip> musicDictionary = new Dictionary<Music, AudioClip>();

    public static Dictionary<SFX, AudioClip> SfxDictionary { get => sfxDictionary; }
    public static Dictionary<Music, AudioClip> MusicDictionary = new Dictionary<Music, AudioClip>();

    static bool isInit = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeDatabase();
        managerAudioSource = GetComponent<AudioSource>();
    }

    private void InitializeDatabase()
    {
        if (isInit) return;
        isInit = true;

        InitializeMusic();
        InitializeSFX();
    }

    private void InitializeMusic()
    {
        musicDictionary.Add(Music.IntroBattleTheme, Resources.Load<AudioClip>("Music/Intro"));
        musicDictionary.Add(Music.BattleThemeLoop, Resources.Load<AudioClip>("Music/Battle Theme Loop"));
    }

    private void InitializeSFX()
    {
        sfxDictionary.Add(SFX.Running1, Resources.Load<AudioClip>("SFX/Running/Running1"));
        sfxDictionary.Add(SFX.Running2, Resources.Load<AudioClip>("SFX/Running/Running2"));
        sfxDictionary.Add(SFX.Running3, Resources.Load<AudioClip>("SFX/Running/Running3"));

        sfxDictionary.Add(SFX.Jumping, Resources.Load<AudioClip>("SFX/Jumping/New Jump 2"));

        sfxDictionary.Add(SFX.Shooting2, Resources.Load<AudioClip>("SFX/Arrow Being Shot/Shooting Arrow 2"));
        sfxDictionary.Add(SFX.Shooting3, Resources.Load<AudioClip>("SFX/Arrow Being Shot/Shooting Arrow 3"));

        sfxDictionary.Add(SFX.NormalLanding, Resources.Load<AudioClip>("SFX/Normal Landing/Landing 4"));

        sfxDictionary.Add(SFX.Flying1, Resources.Load<AudioClip>("SFX/Flying/Girl Flying 1"));
        sfxDictionary.Add(SFX.Flying2, Resources.Load<AudioClip>("SFX/Flying/Girl Flying 2"));
        sfxDictionary.Add(SFX.Flying3, Resources.Load<AudioClip>("SFX/Flying/Girl Flying 3"));
        sfxDictionary.Add(SFX.Flying4, Resources.Load<AudioClip>("SFX/Flying/Girl Flying 4"));

        sfxDictionary.Add(SFX.FlyingArrow, Resources.Load<AudioClip>("SFX/Arrow Flying Through the Air/Arrow Flying"));

        sfxDictionary.Add(SFX.DrawingBow, Resources.Load<AudioClip>("SFX/Bow Drawing/Drawing Bow"));

        sfxDictionary.Add(SFX.ArrowHittingWall1, Resources.Load<AudioClip>("SFX/Arrow impact - Wall-Ground/Arrow Hitting Wall 1"));
        sfxDictionary.Add(SFX.ArrowHittingWall2, Resources.Load<AudioClip>("SFX/Arrow impact - Wall-Ground/Arrow Hitting Wall 2"));
        sfxDictionary.Add(SFX.ArrowHittingWall3, Resources.Load<AudioClip>("SFX/Arrow impact - Wall-Ground/Arrow Hitting Wall 3"));
        sfxDictionary.Add(SFX.ArrowHittingWall4, Resources.Load<AudioClip>("SFX/Arrow impact - Wall-Ground/Arrow Hitting Wall 4"));

        sfxDictionary.Add(SFX.ArrowHittingFlesh1, Resources.Load<AudioClip>("SFX/Arrow Hitting Flesh/Arrow Hitting Flesh 1"));
        sfxDictionary.Add(SFX.ArrowHittingFlesh2, Resources.Load<AudioClip>("SFX/Arrow Hitting Flesh/Arrow Hitting Flesh 2"));
        sfxDictionary.Add(SFX.ArrowHittingFlesh3, Resources.Load<AudioClip>("SFX/Arrow Hitting Flesh/Arrow Hitting Flesh 3"));
        sfxDictionary.Add(SFX.ArrowHittingFlesh4, Resources.Load<AudioClip>("SFX/Arrow Hitting Flesh/Arrow Hitting Flesh 4"));

        sfxDictionary.Add(SFX.SlowingDownTime, Resources.Load<AudioClip>("SFX/Slowing Down Time/Slowing Down Time"));

        sfxDictionary.Add(SFX.Teleporting1, Resources.Load<AudioClip>("SFX/Teleporting/Teleporting 1"));
        sfxDictionary.Add(SFX.Teleporting2, Resources.Load<AudioClip>("SFX/Teleporting/Teleporting 2"));
        sfxDictionary.Add(SFX.Teleporting3, Resources.Load<AudioClip>("SFX/Teleporting/Teleporting 3"));
        sfxDictionary.Add(SFX.Teleporting4, Resources.Load<AudioClip>("SFX/Teleporting/Teleporting 4"));
        sfxDictionary.Add(SFX.Teleporting5, Resources.Load<AudioClip>("SFX/Teleporting/Teleporting 5"));

        sfxDictionary.Add(SFX.Drifting, Resources.Load<AudioClip>("SFX/Drifter/Drifting"));

        sfxDictionary.Add(SFX.DrifterBeamShooting1, Resources.Load<AudioClip>("SFX/Drifter/Drifter Beam/Drifter Beam Shooting 1"));
        sfxDictionary.Add(SFX.DrifterBeamShooting2, Resources.Load<AudioClip>("SFX/Drifter/Drifter Beam/Drifter Beam Shooting 2"));
        sfxDictionary.Add(SFX.DrifterBeamShooting3, Resources.Load<AudioClip>("SFX/Drifter/Drifter Beam/Drifter Beam Shooting 3"));

        sfxDictionary.Add(SFX.DrifterBeamHit1, Resources.Load<AudioClip>("SFX/Drifter/Drifter Beam Hit/Laser Hit 1"));
        sfxDictionary.Add(SFX.DrifterBeamHit2, Resources.Load<AudioClip>("SFX/Drifter/Drifter Beam Hit/Laser Hit 2"));

        sfxDictionary.Add(SFX.DrifterDeath, Resources.Load<AudioClip>("SFX/Drifter/Drifter Death"));
    }

    /// <summary>
    /// Plays music using the Audio Manager's Audio Source.
    /// </summary>
    /// <param name="volume">The volume level.</param>
    /// <param name="delay">Seconds of delay.</param>
    /// <param name="music">The music to play.</param>
    public static Coroutine PlayMusic(float volume, float delay, bool willLoop, Music music)
    {
        return PlayMusic(managerAudioSource, volume, delay, willLoop, music);
    }

    /// <summary>
    /// Plays music using the given Audio Source.
    /// </summary>
    /// <param name="audioSource">The Audio Source this sfx will play from.</param>
    /// <param name="volume">The volume level.</param>
    /// <param name="delay">Seconds of delay.</param>
    /// <param name="music">The music to play.</param>
    public static Coroutine PlayMusic(AudioSource audioSource, float volume, float delay, bool willLoop, Music music)
    {
        Action audioAction = delegate
        {
            audioSource.clip = musicDictionary[music];
            audioSource.volume = volume;
            audioSource.loop = willLoop;
            audioSource.Play();
        };

        if(delay == 0)
        {
            audioAction();
            return null;
        }

        return instance.StartCoroutine(PlayNow(delay, audioAction));
    }

    /// <summary>
    /// Plays a random sound effect using the Audio Manager's Audio Source.
    /// </summary>
    /// <param name="volume">The volume level.</param>
    /// <param name="delay">Seconds of delay.</param>
    /// <param name="willLoop">Value for whether or not the sound effect will loop.</param>
    /// <param name="sfxs">Sound effects that may be played.</param>
    public static Coroutine PlayRandomSFX(float volume, float delay, bool willLoop, params SFX[] sfxs)
    {
        return PlayRandomSFX(managerAudioSource, volume, delay, willLoop, sfxs);
    }

    /// <summary>
    /// Plays a random sound effect using the given Audio Source.
    /// </summary>
    /// <param name="audioSource">The Audio Source this sfx will play from.</param>
    /// <param name="volume">The volume level.</param>
    /// <param name="delay">Seconds of delay.</param>
    /// <param name="willLoop">Value for whether or not the sound effect will loop.</param>
    /// <param name="sfxs">Sound effects that may be played.</param>
    public static Coroutine PlayRandomSFX(AudioSource audioSource, float volume, float delay, bool willLoop, params SFX[] sfxs)
    {
        int randomIndex = UnityEngine.Random.Range(0, sfxs.Length);
        SFX currentSFX = sfxs[randomIndex];

        return PlaySFX(audioSource, volume, delay, willLoop, currentSFX);
    }

    /// <summary>
    /// Plays a sound effect using the Audio Manager's Audio Source.
    /// </summary>
    /// <param name="volume">The volume level.</param>
    /// <param name="delay">Seconds of delay.</param>
    /// <param name="willLoop">Value for whether or not the sound effect will loop.</param>
    /// <param name="sfx">The sound effect that will be played.</param>
    public static Coroutine PlaySFX(float volume, float delay, bool willLoop, SFX sfx)
    {
        return PlaySFX(managerAudioSource, volume, delay, willLoop, sfx);
    }

    /// <summary>
    /// Plays a sound effect using the given Audio Source.
    /// </summary>
    /// <param name="audioSource">The Audio Source this sfx will play from.</param>
    /// <param name="volume">The volume level.</param>
    /// <param name="delay">Seconds of delay.</param>
    /// <param name="willLoop">Value for whether or not the sound effect will loop.</param>
    /// <param name="sfx">The sound effect that will be played.</param>
    public static Coroutine PlaySFX(AudioSource audioSource, float volume, float delay, bool willLoop, SFX sfx)
    {
        Action audioAction = delegate
        {
            audioSource.clip = sfxDictionary[sfx];
            audioSource.loop = willLoop;
            audioSource.volume = volume;
            audioSource.Play();
        };

        if(delay == 0)
        {
            audioAction();
            return null;
        }

        return instance.StartCoroutine(PlayNow(delay, audioAction));
    }

    /// <summary>
    /// Selects a random sound effect to play one-shot using the Audio Manager's Audio Source.
    /// </summary>
    /// <param name="volume">The volume level.</param>
    /// <param name="delay">Seconds of delay.</param>
    /// <param name="sfxs">Sound effects that may be played.</param>
    public static Coroutine PlayRandomOneShotSFX(float volume, float delay, params SFX[] sfxs)
    {
        return PlayRandomOneShotSFX(managerAudioSource, volume, delay, sfxs);
    }

    /// <summary>
    /// Selects a random sound effect to play one-shot using the given Audio Source.
    /// </summary>
    /// <param name="audioSource">The Audio Source this sfx will play from.</param>
    /// <param name="volume">The volume level.</param>
    /// <param name="delay">Seconds of delay.</param>
    /// <param name="sfxs">Sound effects that may be played.</param>
    public static Coroutine PlayRandomOneShotSFX(AudioSource audioSource, float volume, float delay, params SFX[] sfxs)
    {
        int randomIndex = UnityEngine.Random.Range(0, sfxs.Length);

        return PlayOneShotSFX(audioSource, volume, delay, sfxs[randomIndex]);
    }

    /// <summary>
    /// Plays a sound effect using the Audio Manager's Audio Source.
    /// </summary>
    /// <param name="volume">The volume level.</param>
    /// <param name="delay">Seconds of delay.</param>
    /// <param name="sfx">The sound effect that will be played.</param>
    public static Coroutine PlayOneShotSFX(float volume, float delay, SFX sfx)
    {
        return PlayOneShotSFX(managerAudioSource, volume, delay, sfx);
    }

    /// <summary>
    /// Plays a sound effect using the Audio Manager's Audio Source.
    /// </summary>
    /// <param name="audioSource">The Audio Source this sfx will play from.</param>
    /// <param name="volume">The volume level.</param>
    /// <param name="delay">Seconds of delay.</param>
    /// <param name="sfx">The sound effect that will be played.</param>
    public static Coroutine PlayOneShotSFX(AudioSource audioSource, float volume, float delay, SFX sfx)
    {
        Action audioAction = delegate
        {
            audioSource.volume = volume;
            audioSource.PlayOneShot(sfxDictionary[sfx]);
        };

        if (delay == 0)
        {
            audioAction();
            return null;
        }

        return instance.StartCoroutine(PlayNow(delay, audioAction));
    }

    /// <summary>
    /// Stops playing the audio clip in the Audio Manager's Audio Source.
    /// </summary>
    public static void StopPlaying()
    {
        StopPlaying(managerAudioSource, null);
    }

    /// <summary>
    /// Stops playing the audio clip in the given Audio Source.
    /// </summary>
    /// <param name="audioSource">The Audio Source this sfx will play from.</param>
    public static void StopPlaying(AudioSource audioSource)
    {
        StopPlaying(audioSource, null);
    }

    /// <summary>
    /// Stops playing the audio clip in the Audio Manager's Audio Source and stops the given coroutine.
    /// </summary>
    /// <param name="sfxCoroutine">Coroutine to stop.</param>
    public static void StopPlaying(Coroutine sfxCoroutine)
    {
        StopPlaying(managerAudioSource, sfxCoroutine);
    }

    /// <summary>
    /// Stops playing the audio clip in the given Audio Source and stops the given coroutine.
    /// </summary>
    /// <param name="audioSource">The Audio Source this sfx will play from.</param>
    /// <param name="sfxCoroutine">Coroutine to stop.</param>
    public static void StopPlaying(AudioSource audioSource, Coroutine sfxCoroutine)
    {
        audioSource.Stop();

        if (sfxCoroutine != null)
        {
            instance.StopCoroutine(sfxCoroutine);
        }
    }

    /// <summary>
    /// Creates a new Audio Source.
    /// </summary>
    /// <param name="parent">(Optional) The parent transform for the newly created Audio Source.</param>
    public static AudioSource CreateAudioSource(Transform parent = null)
    {
        AudioSource newAudioSource = new GameObject().AddComponent<AudioSource>();
        newAudioSource.spatialBlend = 1;
        newAudioSource.transform.SetParent(parent);
        newAudioSource.transform.localPosition = Vector2.zero;
        newAudioSource.name = "New Audio Source GameObject";
        return newAudioSource;
    }

    public static IEnumerator CallMethodAfterMusicClip(Music music, Action actionMethod)
    {
        yield return new WaitForSecondsRealtime(musicDictionary[music].length);
        actionMethod();
    }

    private static IEnumerator PlayNow(float delay, System.Action PlayMethod)
    {
        yield return new WaitForSeconds(delay);
        PlayMethod();
    }
}