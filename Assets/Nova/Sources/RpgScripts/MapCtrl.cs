using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCtrl : MonoBehaviour
{
    public GameObject[] maps;

    private int id;
    public int currentId
    {
        get
        {
            return id;
        }
    }

    public void SetMap(int id)
    {
        if (transform.childCount == 0)
        {
            return;
        }
        var new_map = Instantiate(maps[id]);
        new_map.transform.SetParent(transform);
    }
    
}

namespace Nova
{
    public class MapCtrl
    {
        #region 存档相关
        public string luaGlobalName = "MapCtrl";
        public string restorableName => luaGlobalName;
        [Serializable]
        private class MapStatsRestoreData : IRestoreData
        {
            public readonly Vector3 position;

            public MapStatsRestoreData(MapCtrl mapCtrl)
            {
                //position = playerStats.position;
            }
        }

        // 保存到存档里
        public IRestoreData GetRestoreData()
        {
            return new MapStatsRestoreData(this);
        }

        // 从存档中读取
        public void Restore(IRestoreData restoreData)
        {
            var data = restoreData as MapStatsRestoreData;
        }
        #endregion
    }
}
