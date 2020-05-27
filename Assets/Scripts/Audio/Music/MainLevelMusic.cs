using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLevelMusic : MonoBehaviour
{
    [SerializeField]
    private AudioOptions introTheme, battleMusicTheme;

    private void Start()
    {
        PlayIntroMusic();
        StartCoroutine(AudioManager.CallMethodAfterMusicClip(Music.IntroBattleTheme, PlayBattleTheme));
    }

    private void PlayIntroMusic()
    {
        AudioManager.PlayMusic(introTheme.Volume, introTheme.Delay, false, Music.IntroBattleTheme);
    }

    private void PlayBattleTheme()
    {
        AudioManager.PlayMusic(battleMusicTheme.Volume, battleMusicTheme.Delay, true, Music.BattleThemeLoop);
    }
}
