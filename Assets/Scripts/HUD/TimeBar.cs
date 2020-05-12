using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBar : HUDBar
{
    private TimeManipulation timeManip_script;

    protected override void Start()
    {
        timeManip_script = GetComponent<TimeManipulation>();
        startingValue = timeManip_script.StartingTimeValue;

        base.Start();

        UpdateBar(startingValue);
        timeManip_script.UpdateTimeBar += delegate { UpdateBar(timeManip_script.CurrentTimeValue); };
    }
}
