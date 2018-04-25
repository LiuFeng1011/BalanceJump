using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameLevelManager : BaseGameObject {

    public List<InGameBaseObj> objList = new List<InGameBaseObj>();
    public List<InGameBaseObj> addList = new List<InGameBaseObj>();
    public List<InGameBaseObj> delList = new List<InGameBaseObj>();

    float addStepDis = 0;

    int addCount = 0;

    public void Init()
    {

        EventManager.Register(this,
                              EventID.EVENT_TOUCH_DOWN, EventID.EVENT_TOUCH_MOVE);
        addCount = (int)(GameConst.GAME_STEP_INTERVAL * 10);

        addStepDis += GameConst.GAME_STEP_INTERVAL;
        for (int i = 0; i < 1; i++){
            InGameBaseObj obj = AddObj(InGameBaseObj.enObjType.step);

            obj.transform.position = new Vector3(0, 0, addStepDis);
            addStepDis += GameConst.GAME_STEP_INTERVAL;

            InGameStep step = (InGameStep)obj;

            step.RandScale(4, 0);
        }
        AddStepUpdate();
    }

    public void Update(){
        AddStepUpdate();

        for (int i = 0; i < objList.Count; i++)
        {
            InGameBaseObj obj = objList[i];
            obj.ObjUpdate();

            if (obj.IsDie())
            {
                delList.Add(obj);
            }
        }

        for (int i = 0; i < delList.Count; i++)
        {
            InGameBaseObj obj = delList[i];
            objList.Remove(obj);
            obj.Die();
        }
        delList.Clear();

        if (addList.Count > 0)
        {
            objList.AddRange(addList);
            addList.Clear();
        }
    }

    public InGameBaseObj AddObj(InGameBaseObj.enObjType type){

        GameObject obj = Resources.Load(InGameBaseObj.ObjResourceName[(int)type]) as GameObject;
        obj = MonoBehaviour.Instantiate(obj);

        InGameBaseObj objscript = obj.GetComponent<InGameBaseObj>();
        objList.Add(objscript);
        objscript.Init();
        return objscript;
    }

    void AddStepUpdate(){
        float roleposz = InGameManager.GetInstance().role.transform.position.z;

        while (addStepDis - roleposz < addCount){
            InGameBaseObj obj = AddObj(InGameBaseObj.enObjType.step);

            float y = 0;
            if(addStepDis > addCount){
                y = -5;
            }

            obj.transform.position = new Vector3(0,y, addStepDis);
            addStepDis += GameConst.GAME_STEP_INTERVAL;

            InGameStep step = (InGameStep)obj;

            step.RandScale(Random.Range(2f - InGameManager.GetInstance().gameScale, 5f - InGameManager.GetInstance().gameScale),
                           -3 + Random.Range(0f, 6f));
        }

    }

    public void Destroy(){
        
    }


    public override void HandleEvent(EventData resp)
    {
        switch (resp.eid)
        {
            case EventID.EVENT_TOUCH_MOVE:
                EventTouch moveeve = (EventTouch)resp;

                //Vector3 pos = GameCommon.ScreenPositionToWorld(InGameManager.GetInstance().gamecamera, moveeve.pos);

                TouchMove(moveeve.pos);
                break;
        }
    }

    public void TouchMove(Vector3 pos){
        float rate =  - (pos.x - Screen.width / 2) / 500f;
        if (rate > 1) rate = 1;
        if (rate < -1) rate = -1;

        float rotate = rate * 45;

        InGameRole role = InGameManager.GetInstance().role;
        for (int i = 0; i < objList.Count; i++)
        {
            InGameBaseObj obj = objList[i];
            if (obj == null || obj.IsDie()) continue;

            float disrate = Mathf.Abs(role.transform.position.z - obj.transform.position.z) / (GameConst.GAME_STEP_INTERVAL * 3);

            disrate =  1 - Mathf.Min(disrate, 1f);

            obj.transform.rotation = Quaternion.Euler(new Vector3(0,0,rotate * disrate));

        }

    }

}
