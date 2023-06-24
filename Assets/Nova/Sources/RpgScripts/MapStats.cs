using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStats : MonoBehaviour
{
    public int id
    {
        get
        {
            var _id = Nova.Statics.mapCtrl.GetComponent<MapCtrl>().currentId;
            return _id;
        }
    }

    private void Awake()
    {
    }
}
