using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    static MenuManager instance;
    public static MenuManager GetInstance() { return instance; }

    public OutGameUIBaseLayer menuLayer;
    public ShopLayerManager shopLayerManager;

    GameObject soundOpenBtn, soundCloseBtn;
    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {

        //PlayerPrefs.SetInt(GameConst.USERDATANAME_COMBO_COUNT,42435);

        AudioManager.Instance.PlayBG("sound/bgm");

        Transform menu = GameObject.Find("UI Root").transform.Find("Menu");

        UILabel bestScores = menu.Find("BestScores").GetComponent<UILabel>();

        int selmodel = PlayerPrefs.GetInt(GameConst.USERDATANAME_MODEL, 0);
        int bestscores = PlayerPrefs.GetInt(GameConst.USERDATANAME_MODEL_MAXSCORES + selmodel);
        bestScores.text = "" + bestscores;

        GameObject startBtn = menu.Find("StartGame").gameObject;
        GameUIEventListener.Get(startBtn).onClick = StartCB;

        GameObject shopBtn = menu.Find("Shop").gameObject;
        GameUIEventListener.Get(shopBtn).onClick = ShopCB;

        menuLayer = menu.GetComponent<OutGameUIBaseLayer>();
        menuLayer.Init();
        menuLayer.gameObject.SetActive(true);

        Transform shop = GameObject.Find("UI Root").transform.Find("Shop");
        shopLayerManager = shop.GetComponent<ShopLayerManager>();
        shopLayerManager.Init();
        shopLayerManager.gameObject.SetActive(false);

        GameObject storyBtn = menu.Find("Story").gameObject;
        GameUIEventListener.Get(storyBtn).onClick = StoryCB;

        GameObject infinityBtn = menu.Find("Infinity").gameObject;
        GameUIEventListener.Get(infinityBtn).onClick = InfinityCB;


        GameObject leaderBoardBtn = menu.Find("Anchor").Find("LeaderBoard").gameObject;
        GameUIEventListener.Get(leaderBoardBtn).onClick = ShowLB;


        GameObject noADBtn = menu.Find("Anchor").Find("NoAD").gameObject;
        GameUIEventListener.Get(noADBtn).onClick = NoADCB;

        int noad = PlayerPrefs.GetInt("noad", 0);

        if (noad == 1)
        {
            noADBtn.SetActive(false);
        }

        GameObject starBtn = menu.Find("Anchor").Find("Star").gameObject;
        GameUIEventListener.Get(starBtn).onClick = StarCB;


        soundOpenBtn = menu.Find("Anchor").Find("Soundopen").gameObject;
        GameUIEventListener.Get(soundOpenBtn).onClick = SoundOpen;

        soundCloseBtn = menu.Find("Anchor").Find("Soundclose").gameObject;
        GameUIEventListener.Get(soundCloseBtn).onClick = SoundClose;


        if (AudioManager.Instance.MusicSize > 0)
        {
            SoundOpen(null);
        }
        else
        {
            SoundClose(null);
        }

	}


	// Update is called once per frame
	void Update () {
        shopLayerManager.ActionUpdate();
        menuLayer.ActionUpdate();
	}
    void SoundOpen(GameObject go)
    {
        AudioManager.Instance.MusicSize = 100;
        AudioManager.Instance.AudioSize = 100;
        soundOpenBtn.SetActive(false);
        soundCloseBtn.SetActive(true);
    }

    void SoundClose(GameObject go)
    {
        AudioManager.Instance.MusicSize = 0;
        AudioManager.Instance.AudioSize = 0;
        soundOpenBtn.SetActive(true);
        soundCloseBtn.SetActive(false);
    }
    void StoryCB(GameObject go){
        UserDataManager.selLevel = ConfigManager.storyLevelManager.GetDataList()[0];
    }

    void InfinityCB(GameObject go)
    {
        UserDataManager.selLevel = null;
    }

    void StartCB(GameObject go)
    {
        (new EventChangeScene(GameSceneManager.SceneTag.Game)).Send();
    }

    void ShopCB(GameObject go)
    {
        shopLayerManager.Show();
        menuLayer.Hide();
    }

    public void ShopBack(){
        shopLayerManager.Hide();
        menuLayer.Show();
    }

    void ShowLB(GameObject go){
        GameCenterManager.GetInstance().Showlb();
    }

    void NoADCB(GameObject go){
        PurchaseManager.GetInstance().DoIapPurchase(DoIapCB);
    }

    void DoIapCB(bool b,string s){
        
    }

    void StarCB(GameObject go){
        #if UNITY_IPHONE || UNITY_EDITOR
        const string APP_ID = "1369028254";
        var url = string.Format(
            "itms-apps://ax.itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?type=Purple+Software&id={0}",
            APP_ID);
        Application.OpenURL(url);
        #endif
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
