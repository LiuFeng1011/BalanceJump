using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLayerManager : OutGameUIBaseLayer {

    public Color normalColor, selColor,unLockColor, lockColor;

    UIScrollView itemList;
    public GamePadScoresLabel combolabel;
    List<ShopInGameObjItem> objItemList = new List<ShopInGameObjItem> ();

    public override void Init()
    {
        base.Init();

        GameObject startBtn = transform.Find("StartGame").gameObject;
        GameUIEventListener.Get(startBtn).onClick = StartCB;

        GameObject backBtn = transform.Find("Back").gameObject;
        GameUIEventListener.Get(backBtn).onClick = BackCB;

        GameObject watchBtn = transform.Find("WatchAD").gameObject;
        GameUIEventListener.Get(watchBtn).onClick = WatchAD;

        UILabel watchADCountLabel = watchBtn.transform.Find("Label").GetComponent<UILabel>();
        watchADCountLabel.text = "+" + GameConst.WATCH_AD_REWAER_COUNT;

        itemList = transform.Find("ItemList").GetComponent<UIScrollView>();


        //combo label
        combolabel = transform.Find("TopLeft").Find("ComboIcon").Find("CountLabel").GetComponent<GamePadScoresLabel>();
        int combocount = PlayerPrefs.GetInt(GameConst.USERDATANAME_COMBO_COUNT, 0);
        combolabel.Init(combocount);
        combolabel.SetScores(combocount);

        //item list
        UIGrid grid = itemList.transform.Find("Grid").GetComponent<UIGrid>();

        List<MapObjectConf> objList = ConfigManager.confMapObjectManager.datas;

        GameObject itemObjRes = Resources.Load("Prefabs/UI/ShopInGameObjItem") as GameObject;

        int selRole = PlayerPrefs.GetInt(GameConst.USERDATANAME_SELECT_ROLE,0);

        for (int i = 0; i < objList.Count;i  ++){
            MapObjectConf conf = objList[i];
            GameObject itemObj = NGUITools.AddChild(grid.gameObject, itemObjRes);
            ShopInGameObjItem item = itemObj.GetComponent<ShopInGameObjItem>();

            int islock = PlayerPrefs.GetInt(GameConst.USERDATANAME_UNLOCK_ROLE + conf.objid,0);
            item.Init(conf,islock);

            objItemList.Add(item);
            item.SetColor((conf.price != -1 && islock == 0) ? lockColor : unLockColor,
                          selRole == conf.objid ? selColor : normalColor);

        }
        grid.Reposition();
        itemList.ResetPosition();
    }

    public void SelItem(ShopInGameObjItem objitem){
        MapObjectConf conf = objitem.conf;
        int islock = PlayerPrefs.GetInt(GameConst.USERDATANAME_UNLOCK_ROLE + conf.objid, 0);
        if(conf.price != -1 && islock == 0){
            int combocount = PlayerPrefs.GetInt(GameConst.USERDATANAME_COMBO_COUNT, 0);
            if(combocount < conf.price){
                objitem.Nofull();
                return;
            }
            combocount -= conf.price;
            PlayerPrefs.SetInt(GameConst.USERDATANAME_COMBO_COUNT,combocount);
            PlayerPrefs.SetInt(GameConst.USERDATANAME_UNLOCK_ROLE + conf.objid, 1);
            combolabel.SetScores(combocount);
            objitem.Unlock();
        }

        int selRole = PlayerPrefs.GetInt(GameConst.USERDATANAME_SELECT_ROLE, 0);
        for (int i = 0; i < objItemList.Count; i ++){
            ShopInGameObjItem item = objItemList[i];
            if(item.conf.objid == selRole){
                item.SetColor(unLockColor,normalColor);
                break;
            }
        }
        objitem.SetColor(unLockColor, selColor);
        PlayerPrefs.SetInt(GameConst.USERDATANAME_SELECT_ROLE,conf.objid);
    }

    void StartCB(GameObject go)
    {
        (new EventChangeScene(GameSceneManager.SceneTag.Game)).Send();
    }

    void BackCB(GameObject go)
    {
        MenuManager.GetInstance().ShopBack();
    }

    void WatchAD(GameObject go){
        ADManager.GetInstance().PlayReviveAD(ADCB, ADCloseCB);
    }

    public void ADCloseCB(string str)
    {
    }

    public void ADCB(string str)
    {
        int combocount = PlayerPrefs.GetInt(GameConst.USERDATANAME_COMBO_COUNT, 0);
        combocount += GameConst.WATCH_AD_REWAER_COUNT;
        PlayerPrefs.SetInt(GameConst.USERDATANAME_COMBO_COUNT, combocount);
        combolabel.SetScores(combocount);
    }
}
