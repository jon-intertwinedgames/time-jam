using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFX
{
    Running1,
    Running2,
    Running3,
    Jumping,
    NormalLanding,
    Air,
    DrawingBow,
    Shooting,
    ArrowFlying,

    FlyingArrow1,
    FlyingArrow2,
    FlyingArrow3,
    FlyingArrow4,

    ArrowHittingWall1,
    ArrowHittingWall2,
    ArrowHittingWall3,
    ArrowHittingWall4,

    ArrowHittingFlesh1,
    ArrowHittingFlesh2,
    ArrowHittingFlesh3,
    ArrowHittingFlesh4
}

public enum Music
{

}

public class AudioManager : MonoBehaviour
{
    private static AudioSource audioSource;
    private static Dictionary<SFX, AudioClip> sfxDictionary = new Dictionary<SFX, AudioClip>();
    private static Dictionary<Music, AudioClip> musicDictionary = new Dictionary<Music, AudioClip>();

    private void Start()
    {
        InitializeDatabase();
        audioSource = GetComponent<AudioSource>();
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

    public static void PlayMusic(Music music)
    {
        audioSource.clip = musicDictionary[music];
        audioSource.Play();
    }

    public static void PlayOneShotSFX(SFX sfx) //Possibly add a timer to this function
    {
        audioSource.PlayOneShot(sfxDictionary[sfx]);
    }

    public static void PlayOneShotSFX(SFX arrowHittingWall1, Vector3 position)
    {
        AudioSource audioSource3D = (new GameObject()).AddComponent<AudioSource>() as AudioSource;
        audioSource3D.transform.position = position;
        audioSource3D.clip = getSFXClip(arrowHittingWall1);
        audioSource3D.loop = false;
        audioSource3D.spatialBlend = 1;
        audioSource3D.Play();
        Destroy(audioSource3D.gameObject, 5f);
    }

    public static AudioClip getSFXClip(SFX sfx)
    {
        return sfxDictionary[sfx];
    }
}
