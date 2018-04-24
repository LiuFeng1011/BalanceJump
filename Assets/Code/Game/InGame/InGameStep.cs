using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameStep : InGameBaseObj {
    float baseX, moveWidth, speed, downTime = 0, downMaxTime = 0.6f;
    // Use this for initialization

    public Transform vecObj;
    Transform stepObj;

    public override void Init()
    {
        base.Init();

        vecObj = transform.Find("vecObj");
        stepObj = transform.Find("Cube");
    }

    public override void ObjUpdate()
    {
        base.ObjUpdate();

        if(transform.position.y < 0){
            transform.position += new Vector3(0, Time.deltaTime, 0);
        }

    }

	
	// Update is called once per frame
	void Update () {
        if (!IsDie()) return;
        transform.position -= new Vector3(0, Time.deltaTime * 15, 0);
	}

    public void RandScale(float scale, float posx){
        transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(posx, transform.position.y, transform.position.z);
    }

    public override void Die(){
        //childObj.GetComponent<Renderer>().material = m_light; 
        Invoke("DestroySelf", 1.0f);
    }

    void DestroySelf(){
        base.Die();
    }

}
