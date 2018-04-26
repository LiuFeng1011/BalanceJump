using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachLayer : MonoBehaviour {
    float lineWidth, handTime = 0f;
    Transform hand;

	// Use this for initialization
	void Start () {
        UIEventListener.Get(transform.Find("pause").gameObject).onClick = TouchCB;
        InGameManager.GetInstance().ChangeState(enGameState.pause);

        UISprite sprite = transform.Find("Line").GetComponent<UISprite>();
        lineWidth = sprite.localSize.x;
        Debug.Log("lineWidth : " + lineWidth);

        hand = transform.Find("Hand");

	}
	
	// Update is called once per frame
	void Update () {
        handTime += Time.deltaTime * 2.5f;

        hand.transform.localPosition = new Vector3(Mathf.Sin(handTime) * lineWidth * 0.45f,
                                                   hand.transform.localPosition.y,
                                                   hand.transform.localPosition.z);
	}

    public void TouchCB(GameObject go){
        InGameManager.GetInstance().StartGame();
        Destroy(gameObject);
    }
}
