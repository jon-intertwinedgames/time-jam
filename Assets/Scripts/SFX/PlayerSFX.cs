using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    System.Random ran = new System.Random();

    private void SFX_Play_Running1()
    {
        AudioManager.PlayOneShotSFX(SFX.Running1);
    }

    private void SFX_Play_Running2()
    {
        AudioManager.PlayOneShotSFX(SFX.Running2);
    }

    private void SFX_Play_Running3()
    {
        AudioManager.PlayOneShotSFX(SFX.Running3);
    }
    /// <summary>
    /// RANDOM SFX
    /// </summary>
    public void SFX_RandomPlay_Running()
    {
        int i = ran.Next(0, 3);

        switch (i)
        {
            case 0:
                SFX_Play_Running1();
                break;
            case 1:
                SFX_Play_Running2();
                break;
            case 2:
                SFX_Play_Running3();
                break;
            default:
                SFX_Play_Running3();
                break;
        }

    }

    public void SFX_Jump()
    {
        AudioManager.PlayOneShotSFX(SFX.Jumping);
    }
}
