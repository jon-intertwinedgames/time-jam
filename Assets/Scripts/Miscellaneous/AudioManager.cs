using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFX
{
    Running,
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
