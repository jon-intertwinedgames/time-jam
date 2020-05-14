using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioSource managerAudioSource;
    private static Dictionary<SFX, AudioClip> sfxDictionary = new Dictionary<SFX, AudioClip>();
    private static Dictionary<Music, AudioClip> musicDictionary = new Dictionary<Music, AudioClip>();

    private void Start()
    {
        InitializeDatabase();
        managerAudioSource = GetComponent<AudioSource>();
    }

    private void InitializeDatabase()
    {
        sfxDictionary.Add(SFX.Running1, Resources.Load<AudioClip>("SFX/Running/Running1"));
        sfxDictionary.Add(SFX.Running2, Resources.Load<AudioClip>("SFX/Running/Running2"));
        sfxDictionary.Add(SFX.Running3, Resources.Load<AudioClip>("SFX/Running/Running3"));
        sfxDictionary.Add(SFX.Jumping, Resources.Load<AudioClip>("SFX/Jumping/Jump 3"));

        sfxDictionary.Add(SFX.Shooting, Resources.Load<AudioClip>("SFX/Arrow Being Shot/Shooting Arrow 3"));
        sfxDictionary.Add(SFX.NormalLanding, Resources.Load<AudioClip>("SFX/Normal Landing/Landing 4"));

        sfxDictionary.Add(SFX.FlyingArrow1, Resources.Load<AudioClip>("SFX/Arrow Flying Through the Air/Flying Arrow 1"));
        sfxDictionary.Add(SFX.FlyingArrow2, Resources.Load<AudioClip>("SFX/Arrow Flying Through the Air/Flying Arrow 2"));
        sfxDictionary.Add(SFX.FlyingArrow3, Resources.Load<AudioClip>("SFX/Arrow Flying Through the Air/Flying Arrow 3"));
        sfxDictionary.Add(SFX.FlyingArrow4, Resources.Load<AudioClip>("SFX/Arrow Flying Through the Air/Flying Arrow 4"));

        sfxDictionary.Add(SFX.ArrowHittingWall1, Resources.Load<AudioClip>("SFX/Arrow impact - Wall-Ground/Arrow Hitting Wall 1"));
        sfxDictionary.Add(SFX.ArrowHittingWall2, Resources.Load<AudioClip>("SFX/Arrow impact - Wall-Ground/Arrow Hitting Wall 2"));
        sfxDictionary.Add(SFX.ArrowHittingWall3, Resources.Load<AudioClip>("SFX/Arrow impact - Wall-Ground/Arrow Hitting Wall 3"));
        sfxDictionary.Add(SFX.ArrowHittingWall4, Resources.Load<AudioClip>("SFX/Arrow impact - Wall-Ground/Arrow Hitting Wall 4"));

        sfxDictionary.Add(SFX.ArrowHittingFlesh1, Resources.Load<AudioClip>("SFX/Arrow Hitting Flesh/Arrow Hitting Flesh 1"));
        sfxDictionary.Add(SFX.ArrowHittingFlesh2, Resources.Load<AudioClip>("SFX/Arrow Hitting Flesh/Arrow Hitting Flesh 2"));
        sfxDictionary.Add(SFX.ArrowHittingFlesh3, Resources.Load<AudioClip>("SFX/Arrow Hitting Flesh/Arrow Hitting Flesh 3"));
        sfxDictionary.Add(SFX.ArrowHittingFlesh4, Resources.Load<AudioClip>("SFX/Arrow Hitting Flesh/Arrow Hitting Flesh 4"));
    }

    /// <summary>
    /// Plays music using the Audio Manager's Audio Source.
    /// </summary>
    /// <param name="music"></param>
    public static void PlayMusic(Music music)
    {
        PlayMusic(managerAudioSource, music);
    }

    public static void PlayMusic(AudioSource audioSource, Music music)
    {
        audioSource.clip = musicDictionary[music];
        audioSource.Play();
    }

    /// <summary>
    /// Selects a random sound effect to play one-shot using the Audio Manager's Audio Source.
    /// </summary>
    /// <param name="sfxs"></param>
    public static void PlayRandomSFXOneShot(params SFX[] sfxs)
    {
        PlayRandomSFXOneShot(managerAudioSource, sfxs);
    }

    public static void PlayRandomSFXOneShot(AudioSource audioSource, params SFX[] sfxs)
    {
        int randomIndex = UnityEngine.Random.Range(0, sfxs.Length);

        PlayOneShotSFX(audioSource, sfxs[randomIndex]);
    }

    /// <summary>
    /// Plays a sound effect using the Audio Manager's Audio Source.
    /// </summary>
    /// <param name="sfx"></param>
    public static void PlayOneShotSFX(SFX sfx) //Possibly add a timer to this function
    {
        PlayOneShotSFX(managerAudioSource, sfx);
    }

    public static void PlayOneShotSFX(AudioSource audioSource, SFX sfx) //Possibly add a timer to this function
    {
        audioSource.PlayOneShot(sfxDictionary[sfx]);
    }

    //Look later
    public static void PlayNewSFX(SFX sfx, Vector3 position, float secondsToSelfDestruct = 5)
    {
        AudioSource audioSource3D = (new GameObject()).AddComponent<AudioSource>() as AudioSource;
        audioSource3D.transform.position = position;
        audioSource3D.clip = GetSFXClip(sfx);
        audioSource3D.loop = false;
        audioSource3D.spatialBlend = 1;
        audioSource3D.Play();
        Destroy(audioSource3D.gameObject, secondsToSelfDestruct);
    }

    /// <summary>
    /// Stops playing the audio clip in the Audio Manager's Audio Source.
    /// </summary>
    public static void StopPlaying()
    {
        StopPlaying(managerAudioSource);
    }

    public static void StopPlaying(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    public static AudioClip GetSFXClip(SFX sfx)
    {
        return sfxDictionary[sfx];
    }
}
