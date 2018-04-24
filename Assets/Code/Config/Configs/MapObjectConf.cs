using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MapObjectConf
{
    public int objid;    /*  道具id    */
    public string name; /*  道具名 */
    public string prefabName;   /*  预制体 */
    public int price;/* 价格  */
}


public class ConfMapObjectManager{
    public List<MapObjectConf> datas {get;private set;}
    public Dictionary<int, MapObjectConf> dic = new Dictionary<int, MapObjectConf>();

	public void Load(){

		if(datas != null) datas.Clear();

        datas = ConfigManager.Load<MapObjectConf>();
        dic.Clear();

        for (int i = 0; i < datas.Count; i++)
        {
            MapObjectConf obj = datas[i];
            dic.Add(obj.objid, obj);
        }
	}

}
