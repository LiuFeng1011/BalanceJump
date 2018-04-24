using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutGameUIBaseLayer : InGameUIBaseLayer {

    public override void Init(){
        base.Init();
        actionHeight = 0;
    }

    protected override void ShowAction()
    {
        if (showTime <= 0) return;
        showTime -= Time.deltaTime;
        float rate = action.Evaluate(showTime / maxActionTime);

        actionLabel.color = new Color(1, 1, 1, 1 - rate);

    }

    protected override void HideAction()
    {
        if (hideTime <= 0) return;
        hideTime -= Time.deltaTime * 2;

        if (hideTime <= 0)
        {
            gameObject.SetActive(false);
        }

        float rate = hideTime / maxActionTime;

        actionLabel.color = new Color(1, 1, 1, rate);
    }
}
