using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCreateObjManager : BaseGameObject {

    float addStepTime = 0, addSawTime = 0f;
    const float MAX_ADDHEIGHT = 4.0f, MIN_ADDHEIGHT = 0.8f;
    const float MAX_ADD_SAW_TIME = 3f,MAX_ADD_STEP_TIME = 7f;

    float lastAddTime = 0, addDis = 0;

    public void Init()
    {
    }

    public void Update()
    {
        //AddSawUpdate();
    }


    public void Destroy()
    {

    }
}
