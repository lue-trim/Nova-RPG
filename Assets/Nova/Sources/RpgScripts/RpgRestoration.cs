using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nova
{

    public class RpgRestoration : MonoBehaviour, IRestorable
    {
        // Nova存档用
        private GameState gameState;
        public string luaGlobalName;
        public string restorableName => luaGlobalName;
        void Awake()
        {
            // 增加Nova存档信息
            var controller = Utils.FindNovaController();
            gameState = controller.GameState;
            gameState.AddRestorable(this);
        }

        void OnDestroy()
        {
            gameState.RemoveRestorable(this);
        }
        void Update()
        {

        }

        #region 存档相关
        private class MapRestoreData : IRestoreData
        {

        }

        public IRestoreData GetRestoreData()
        {
            return new MapRestoreData();
        }

        public void Restore(IRestoreData data)
        {

        }
    }
    #endregion
}
