using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameStep : InGameBaseObj {
    float moveMinX = 0,moveMaxX = 0, speed, downTime = 0, downMaxTime = 0.6f, dieTime = 0f, lifeTime = 0f;
    // Use this for initialization
    Vector3 baseScale,basePosition;
    public float dieActionTime = 1.0f;
    public Transform vecObj;
    Transform stepObj;

    bool isMove = false;

    public AnimationCurve ac;

    public override void Init()
    {
        base.Init();

        vecObj = transform.Find("vecObj");
        stepObj = transform.Find("Cube");

        //isMove = Random.Range(0f, 1f) < 0.3f;
        if(isMove){
            speed = Random.Range(0.5f, 1.5f);
        }
    }



    public override void ObjUpdate()
    {
        base.ObjUpdate();

        lifeTime += Time.deltaTime;

        float disrate = Mathf.Abs(InGameManager.GetInstance().role.transform.position.z - transform.position.z) / (GameConst.GAME_STEP_INTERVAL * 10);

        disrate = Mathf.Min(disrate, 1f);

        float x = transform.position.x;
        if (isMove) {
            x = transform.position.x + speed * Time.deltaTime;

            if(speed > 0 && transform.position.x > moveMaxX||
               speed < 0 && transform.position.x < moveMinX){
                speed = -speed;
            }

        }
        transform.position = new Vector3(x, -5 * disrate, transform.position.z);


    }

	
	// Update is called once per frame
	void Update () {
        if (!IsDie()) return;
        transform.position -= new Vector3(0, Time.deltaTime * 3, 0);
        dieTime += Time.deltaTime;

        float rate = Mathf.Min(dieTime / dieActionTime, 1f);
        transform.localScale = new Vector3(baseScale.x + baseScale.x * (ac.Evaluate(rate)) * 0.5f, baseScale.y + baseScale.y * ac.Evaluate(rate ), transform.localScale.z);


	}

    public void RandScale(float scale, float posx){
        transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(posx, transform.position.y, transform.position.z);
        basePosition = transform.position;
        baseScale = transform.localScale;

        float dis = Random.Range(2f, 0.5f);
        moveMinX = Mathf.Max(transform.position.x - dis,-3);
        moveMaxX = Mathf.Min(transform.position.x + dis, 3);
    }

    public override void Die(){
        //childObj.GetComponent<Renderer>().material = m_light; 
        Invoke("DestroySelf", 1.0f);
    }

    void DestroySelf(){
        base.Die();
    }

}
