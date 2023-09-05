using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();

        if (bc == null)
            return Define.WorldObject.Unknow;

        return bc.WorldObjectType;
    }

    public void Despawn(GameObject go)
    {
        
    }
}
