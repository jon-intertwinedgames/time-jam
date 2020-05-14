using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntertwinedUtility : MonoBehaviour
{
    System.Random ran = new System.Random();

    /// <summary>
    /// Excutes one random function/action from the number of args
    /// </summary>
    /// <param name="actions"></param>
    private void RandomFunctionCall(params Action[] actions)
    {
        int i = ran.Next(0, actions.Length - 1);

        actions[i]();
    }
}
