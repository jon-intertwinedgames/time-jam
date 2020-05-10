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
    ArrowFlying
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
    }

    public static void PlayMusic(Music music)
    {
        audioSource.clip = musicDictionary[music];
        audioSource.Play();
    }

    public static void PlayOneShot(SFX sfx) //Possibly add a timer to this function
    {
        audioSource.PlayOneShot(sfxDictionary[sfx]);
    }
}
