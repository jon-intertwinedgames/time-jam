using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioSource managerAudioSource;
    private static Dictionary<SFX, AudioClip> sfxDictionary = new Dictionary<SFX, AudioClip>();
    private static Dictionary<Music, AudioClip> musicDictionary = new Dictionary<Music, AudioClip>();
    static bool isInit = false;

    private void Start()
    {
        InitializeDatabase();
        managerAudioSource = GetComponent<AudioSource>();
    }

    private void InitializeDatabase()
    {
        if (isInit) return;
        isInit = true;
        sfxDictionary.Add(SFX.Running1, Resources.Load<AudioClip>("SFX/Running/Running1"));
        sfxDictionary.Add(SFX.Running2, Resources.Load<AudioClip>("SFX/Running/Running2"));
        sfxDictionary.Add(SFX.Running3, Resources.Load<AudioClip>("SFX/Running/Running3"));

        sfxDictionary.Add(SFX.Jumping, Resources.Load<AudioClip>("SFX/Jumping/Jump 3"));

        sfxDictionary.Add(SFX.Shooting2, Resources.Load<AudioClip>("SFX/Arrow Being Shot/Shooting Arrow 2"));
        sfxDictionary.Add(SFX.Shooting3, Resources.Load<AudioClip>("SFX/Arrow Being Shot/Shooting Arrow 3"));

        sfxDictionary.Add(SFX.NormalLanding, Resources.Load<AudioClip>("SFX/Normal Landing/Landing 4"));

        sfxDictionary.Add(SFX.Flying1, Resources.Load<AudioClip>("SFX/Flying/Girl Flying 1"));
        sfxDictionary.Add(SFX.Flying2, Resources.Load<AudioClip>("SFX/Flying/Girl Flying 2"));
        sfxDictionary.Add(SFX.Flying3, Resources.Load<AudioClip>("SFX/Flying/Girl Flying 3"));
        sfxDictionary.Add(SFX.Flying4, Resources.Load<AudioClip>("SFX/Flying/Girl Flying 4"));

        sfxDictionary.Add(SFX.FlyingArrow1, Resources.Load<AudioClip>("SFX/Arrow Flying Through the Air/Flying Arrow 1"));
        sfxDictionary.Add(SFX.FlyingArrow2, Resources.Load<AudioClip>("SFX/Arrow Flying Through the Air/Flying Arrow 2"));
        sfxDictionary.Add(SFX.FlyingArrow3, Resources.Load<AudioClip>("SFX/Arrow Flying Through the Air/Flying Arrow 3"));
        sfxDictionary.Add(SFX.FlyingArrow4, Resources.Load<AudioClip>("SFX/Arrow Flying Through the Air/Flying Arrow 4"));

        sfxDictionary.Add(SFX.BowDrawing1, Resources.Load<AudioClip>("SFX/Bow Drawing/Bow Drawing 1"));
        sfxDictionary.Add(SFX.BowDrawing2, Resources.Load<AudioClip>("SFX/Bow Drawing/Bow Drawing 2"));

        sfxDictionary.Add(SFX.ArrowHittingWall1, Resources.Load<AudioClip>("SFX/Arrow impact - Wall-Ground/Arrow Hitting Wall 1"));
        sfxDictionary.Add(SFX.ArrowHittingWall2, Resources.Load<AudioClip>("SFX/Arrow impact - Wall-Ground/Arrow Hitting Wall 2"));
        sfxDictionary.Add(SFX.ArrowHittingWall3, Resources.Load<AudioClip>("SFX/Arrow impact - Wall-Ground/Arrow Hitting Wall 3"));
        sfxDictionary.Add(SFX.ArrowHittingWall4, Resources.Load<AudioClip>("SFX/Arrow impact - Wall-Ground/Arrow Hitting Wall 4"));

        sfxDictionary.Add(SFX.ArrowHittingFlesh1, Resources.Load<AudioClip>("SFX/Arrow Hitting Flesh/Arrow Hitting Flesh 1"));
        sfxDictionary.Add(SFX.ArrowHittingFlesh2, Resources.Load<AudioClip>("SFX/Arrow Hitting Flesh/Arrow Hitting Flesh 2"));
        sfxDictionary.Add(SFX.ArrowHittingFlesh3, Resources.Load<AudioClip>("SFX/Arrow Hitting Flesh/Arrow Hitting Flesh 3"));
        sfxDictionary.Add(SFX.ArrowHittingFlesh4, Resources.Load<AudioClip>("SFX/Arrow Hitting Flesh/Arrow Hitting Flesh 4"));

        sfxDictionary.Add(SFX.SlowingDownTime1, Resources.Load<AudioClip>("SFX/SlowingDownTime/Slowing Down Time 1"));
        sfxDictionary.Add(SFX.SlowingDownTime2, Resources.Load<AudioClip>("SFX/SlowingDownTime/Slowing Down Time 2"));
        sfxDictionary.Add(SFX.SlowingDownTime3, Resources.Load<AudioClip>("SFX/SlowingDownTime/Slowing Down Time 3"));
        sfxDictionary.Add(SFX.SlowingDownTime4, Resources.Load<AudioClip>("SFX/SlowingDownTime/Slowing Down Time 4"));

        sfxDictionary.Add(SFX.Teleporting1, Resources.Load<AudioClip>("SFX/Teleporting/Teleporting 1"));
        sfxDictionary.Add(SFX.Teleporting2, Resources.Load<AudioClip>("SFX/Teleporting/Teleporting 2"));
        sfxDictionary.Add(SFX.Teleporting3, Resources.Load<AudioClip>("SFX/Teleporting/Teleporting 3"));
        sfxDictionary.Add(SFX.Teleporting4, Resources.Load<AudioClip>("SFX/Teleporting/Teleporting 4"));
        sfxDictionary.Add(SFX.Teleporting5, Resources.Load<AudioClip>("SFX/Teleporting/Teleporting 5"));

        sfxDictionary.Add(SFX.Drifting, Resources.Load<AudioClip>("SFX/Drifter/Drifting"));

        sfxDictionary.Add(SFX.DrifterBeamShooting1, Resources.Load<AudioClip>("SFX/Drifter/Drifter Beam/Laser Blast 1"));
        sfxDictionary.Add(SFX.DrifterBeamShooting2, Resources.Load<AudioClip>("SFX/Drifter/Drifter Beam/Laser Blast 2"));
        sfxDictionary.Add(SFX.DrifterBeamShooting3, Resources.Load<AudioClip>("SFX/Drifter/Drifter Beam/Laser Blast 3"));
        sfxDictionary.Add(SFX.DrifterBeamShooting4, Resources.Load<AudioClip>("SFX/Drifter/Drifter Beam/Laser Blast 4"));

        sfxDictionary.Add(SFX.DrifterBeamHit1, Resources.Load<AudioClip>("SFX/Drifter/Drifter Beam Hit/Laser Hit 1"));
        sfxDictionary.Add(SFX.DrifterBeamHit2, Resources.Load<AudioClip>("SFX/Drifter/Drifter Beam Hit/Laser Hit 2"));
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
    /// Plays a random sound effect using the Audio Manager's Audio Source.
    /// </summary>
    /// <param name="willLoop"></param>
    /// <param name="sfxs"></param>
    public static void PlayRandomSFX(bool willLoop, params SFX[] sfxs)
    {
        PlayRandomSFX(managerAudioSource, willLoop, sfxs);
    }

    public static void PlayRandomSFX(AudioSource audioSource, bool willLoop, params SFX[] sfxs)
    {
        int randomIndex = Random.Range(0, sfxs.Length);
        SFX currentSFX = sfxs[randomIndex];

        PlaySFX(audioSource, willLoop, currentSFX);
    }

    /// <summary>
    /// Plays a sound effect using the Audio Manager's Audio Source.
    /// </summary>
    /// <param name="willLoop"></param>
    /// <param name="sfx"></param>
    public static void PlaySFX(bool willLoop, SFX sfx)
    {
        PlaySFX(managerAudioSource, willLoop, sfx);
    }

    public static void PlaySFX(AudioSource audioSource, bool willLoop, SFX sfx)
    {
        audioSource.clip = sfxDictionary[sfx];
        audioSource.loop = willLoop;
        audioSource.Play();
    }

    /// <summary>
    /// Selects a random sound effect to play one-shot using the Audio Manager's Audio Source.
    /// </summary>
    /// <param name="sfxs"></param>
    public static void PlayRandomOneShotSFX(params SFX[] sfxs)
    {
        PlayRandomOneShotSFX(managerAudioSource, sfxs);
    }

    public static void PlayRandomOneShotSFX(AudioSource audioSource, params SFX[] sfxs)
    {
        int randomIndex = Random.Range(0, sfxs.Length);

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

    /// <summary>
    /// Creates a new Audio Source
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
}
