using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDBar : MonoBehaviour
{
    protected float barReductionRate, offSet;

    protected int startingValue;

    [SerializeField] private RectTransform bar_trans = null;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        barReductionRate = bar_trans.rect.width / startingValue;
        offSet = startingValue * barReductionRate;
    }

    public void UpdateBar(float currentValue)
    {
        Vector2 newHealthPos = bar_trans.localPosition;
        newHealthPos.x = currentValue * barReductionRate - offSet;
        bar_trans.localPosition = newHealthPos;
    }
}
