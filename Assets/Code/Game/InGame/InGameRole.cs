using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameRole : InGameBaseObj {

    public float validTouchDistance; //200  

    float updateTime = 0f, updateInterval = 0.03f, gravity = 0.008f, jumpActionTime = 0f,maxJumpActionTime = 1f;

    float baseScaleX = 0.5f;

    public int combo = 0,scores = 0;
    Vector3 moveForce,cameraBasePos;

    float targetz =0 ;

    public AnimationCurve jumpAC;

    private void Awake()
    {
        baseScaleX = transform.localScale.x;
    }

    public override void Init()
    {
        base.Init();

        validTouchDistance = 200;

        moveForce = Vector3.zero;

        cameraBasePos = InGameManager.GetInstance().gamecamera.transform.position;
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	public void RoleUpdate () {
        if (!gameObject.activeSelf) return;

        updateTime += Time.deltaTime;

        while (updateTime > updateInterval)
        {
            updateTime -= updateInterval;

            moveForce.z = 0;
            moveForce.y -= gravity;
            transform.position += moveForce;

            transform.position += new Vector3(0, 0, (targetz - transform.position.z) * 0.1f);
        }

        if(jumpActionTime >0 ){
            jumpActionTime -= Time.deltaTime;
            float rate = jumpAC.Evaluate(1f - jumpActionTime / maxJumpActionTime);
            transform.localScale = new Vector3(baseScaleX * rate,transform.localScale.y, baseScaleX * rate);
        }

        Vector3 camerapos = InGameManager.GetInstance().gamecamera.transform.position;
        camerapos.z = cameraBasePos.z + transform.position.z;

        InGameManager.GetInstance().gamecamera.transform.position = camerapos;

        if(transform.position.y < -6){
            Die();
        }
	}

    public void AddScore(bool iscombo){
        int addscore = 1;
        if(iscombo){
            combo++;
            addscore += combo;
        }else {
            combo = 0;
        }

        scores += addscore;
        InGameManager.GetInstance().inGameUIManager.AddScores(transform.position,addscore,scores,iscombo);
    }

    public void Jump(Vector3 vec){
        jumpActionTime = maxJumpActionTime;
        targetz += GameConst.GAME_STEP_INTERVAL;
        moveForce = vec * 0.2f;
        transform.forward = vec;
    }

    public void Die(){
        AudioManager.Instance.Play("sound/die");
        // game over
        InGameManager.GetInstance().GameOver();
        combo = 0;
        gameObject.SetActive(false);
        //create efffect
        GameObject effect = Resources.Load("Prefabs/Effect/RoleDieEffect") as GameObject;
        effect = Instantiate(effect);
        effect.transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        InGameStep step = other.gameObject.transform.parent.gameObject.GetComponent<InGameStep>();
        if(step == null){
            if(other.gameObject.name == "Wave"){
                Die();
            }else{
                Debug.Log("OnTriggerEnter Other");
            }
            return;
        }
        if(step.IsDie()){
            return;
        }
        step.SetDie();
        Vector3 vec = step.vecObj.position - step.transform.position;
        vec.z = 0;
        Jump(vec);


        AddScore(Mathf.Abs(step.transform.position.x - transform.position.x) < 0.5f);


        GameObject effect = Resources.Load("Prefabs/Effect/RoleHitEffect") as GameObject;
        effect = Instantiate(effect);
        effect.transform.position = transform.position;
    }

    public void Revive(){

    }

    private void OnDestroy()
    {
        EventManager.Remove(this);
    }
}
