using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NofullAction : MonoBehaviour {

    float actionTime = 0f, maxActionTime = 1f,basex = 0f;
    public AnimationCurve ac;

	// Use this for initialization
	void Start () {
        basex = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
        if(actionTime <= 0){
            return;
        }
        actionTime = Mathf.Max(actionTime - Time.deltaTime,0f);

        float scale = 1-actionTime / maxActionTime;

        transform.position = new Vector3(basex + ac.Evaluate(scale) * 0.2f, transform.position.y, transform.position.z);
	}

    public void StartAction(){
        actionTime = maxActionTime;
    }
}
