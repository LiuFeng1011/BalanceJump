using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseLayerManager : InGameUIBaseLayer {

	// Use this for initialization
	void Start () {

        GameObject continueBtn = transform.Find("continueBtn").gameObject;
        GameUIEventListener.Get(continueBtn).onClick = ContinueBtn;

        GameObject homeBtn = transform.Find("homeBtn").gameObject;
        GameUIEventListener.Get(homeBtn).onClick = HomeBtn;

        GameObject replayBtn = transform.Find("replayBtn").gameObject;
        GameUIEventListener.Get(replayBtn).onClick = ReplayBtn;
	}
	
    public void ContinueBtn(GameObject obj)
    {
        InGameManager.GetInstance().Resume();
    }

    public void HomeBtn(GameObject obj)
    {

        (new EventChangeScene(GameSceneManager.SceneTag.Menu)).Send();
    }

    public void ReplayBtn(GameObject obj)
    {

        (new EventChangeScene(GameSceneManager.SceneTag.Game)).Send();
    }
}
