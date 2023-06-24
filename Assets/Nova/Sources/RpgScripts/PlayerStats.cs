using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nova
{
    public class PlayerStats : MonoBehaviour, IRestorable
    {
        /*
        记录玩家状态
        */
        #region 存储结构
        [Serializable]
        public class OwnableObject
        {
            /*
            物品类 
            */
            public string name;
            public string display_name;
            public string type;
            public string discription;
            public int amount;
            public bool show;

            public OwnableObject(string name, string display_name, int amount)
            {
                this.name = name;
                this.display_name = display_name;
                this.amount = amount;
            }
        }

        [Serializable]
        public class Objects
        {
            /* 
            物品栏
            */
            private List<OwnableObject> objects;

            private int FindItem(string name, string type = "")
            {
                foreach (var ownableObject in objects)
                {
                    if (type != "")
                    {
                        if (ownableObject.type != type)
                        {
                            continue;
                        }
                    }
                    if (ownableObject.name == name)
                    {
                        return objects.IndexOf(ownableObject);
                    }
                }
                throw new NullReferenceException("数据库中未定义此物品");
            }

            // 增减物品
            public void GiveItem(string name, int amount, string type = "")
            {
                objects[FindItem(name, type)].amount += amount;
            }

            public void GiveItem(int id, int amount)
            {
                objects[id].amount += amount;
            }

            // 直接设置物品数量
            public void SetItem(string name, int amount, string type = "")
            {
                objects[FindItem(name, type)].amount = amount;
            }

            public void SetItem(int id, int amount)
            {
                objects[id].amount = amount;
            }

            // 设置物品是否显示
            public void ShowItem(string name, bool value, string type = "")
            {
                objects[FindItem(name, type)].show = value;
            }

            public void ShowItem(int id, bool value)
            {
                objects[id].show = value;
            }
        }
        #endregion

        public List<OwnableObject> objects;
        public Vector3 position;

        private void Awake()
        {
            Statics.player = gameObject;
        }

        #region 存档相关
        public string luaGlobalName = "PlayerStats";
        public string restorableName => luaGlobalName;
        [Serializable]
        private class PlayerStatsRestoreData : IRestoreData
        {
            public readonly Vector3 position;
            public readonly List<OwnableObject> objects;

            public PlayerStatsRestoreData(PlayerStats playerStats)
            {
                position = playerStats.position;
                objects = playerStats.objects;
            }
        }

        // 保存到存档里
        public IRestoreData GetRestoreData()
        {
            return new PlayerStatsRestoreData(this);
        }

        // 从存档中读取
        public void Restore(IRestoreData restoreData)
        {
            var data = restoreData as PlayerStatsRestoreData;
            position = data.position;
            objects = data.objects;
        }
        #endregion
    }
}
