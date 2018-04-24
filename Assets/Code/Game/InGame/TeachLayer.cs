using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UIEventListener.Get(transform.Find("pause").gameObject).onClick = TouchCB;
        InGameManager.GetInstance().ChangeState(enGameState.pause);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TouchCB(GameObject go){
        InGameManager.GetInstance().ChangeState(enGameState.playing);
        Destroy(gameObject);
    }
}
