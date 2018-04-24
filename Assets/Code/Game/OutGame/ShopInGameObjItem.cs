using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInGameObjItem : MonoBehaviour {

    UISprite sprite,bg;
    UIButton btn;
    public MapObjectConf conf;
    GameObject priceObj;

    NofullAction nofullAction;

    public void Init(MapObjectConf conf,int islock){
        this.conf = conf;

        sprite = transform.GetComponent<UISprite>();
        sprite.spriteName = conf.prefabName;

        bg = transform.Find("bg").GetComponent<UISprite>();

        btn = transform.GetComponent<UIButton>();
        btn.normalSprite = conf.prefabName;

        priceObj = transform.Find("price").gameObject;

        nofullAction = priceObj.transform.Find("ComboIcon").GetComponent<NofullAction>();

        if(conf.price != -1 && islock == 0){
            UILabel priceLabel = nofullAction.transform.Find("CountLabel").GetComponent<UILabel>();
            priceLabel.text = conf.price + "";
        }else{
            priceObj.SetActive(false);
        }

        GameUIEventListener.Get(gameObject).onClick = Sel;
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Nofull(){
        nofullAction.StartAction();
    }

    public void Sel(GameObject go){
        MenuManager.GetInstance().shopLayerManager.SelItem(this);
    }

    public void Unlock(){
        priceObj.SetActive(false);
    }

    public void SetColor(Color forward,Color back){
        sprite.color = forward;
        btn.defaultColor = forward;
        bg.color = back;
    }
}
