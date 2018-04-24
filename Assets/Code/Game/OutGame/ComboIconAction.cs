using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboIconAction : MonoBehaviour {

    float actionTime = 0f, maxActionTime = 1f,randTime = 0, basex = 0f, scaleTime= 0f;
    public AnimationCurve ac;

    // Use this for initialization
    void Start () {
        basex = transform.position.x;
    }

	// Update is called once per frame
	void Update () {
        //scaleTime += Time.deltaTime;
        //float actionscale = 1 + Mathf.Sin(scaleTime)* 0.1f;
        //transform.localScale = new Vector3(actionscale, actionscale, 1);

        actionTime += Time.deltaTime;

        if (actionTime > maxActionTime){ 

            if(actionTime > maxActionTime + randTime ){
                actionTime = 0;
                randTime = Random.Range(1f, 4f);
            }else{
                return;
            }
        }

        float rotationScale = actionTime / maxActionTime;

        float val = ac.Evaluate(rotationScale);
        transform.localScale = new Vector3(1 + val, 1+val, 1);


        transform.rotation = Quaternion.Euler(new Vector3(0,0,val * 45));
	}
}
