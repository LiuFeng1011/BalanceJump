using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameBaseObj : MSBaseObject {
    public enum enObjType
    {
        role,
        step,
        ground,
        bullet,
    }

    public static string[] ObjResourceName = {
        "Prefabs/MapObj/InGameRole",
        "Prefabs/MapObj/InGameStep",
        "Prefabs/MapObj/GroundPlane",
        "Prefabs/MapObj/InGameBullet",
    };
    bool isdie = false;

    public override void ObjUpdate(){
    }

    public virtual void SetDie(){
        isdie = true;
    }

    public virtual bool IsDie()
    {
        return isdie;
    }

    public virtual void Die(){
        Destroy(gameObject);
    }

}
