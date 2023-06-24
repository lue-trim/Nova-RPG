using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;

public class EventTrigger : MonoBehaviour
{
    /*
    地图上的事件触发器
    */
    [SerializeField]
    private string novaLabel;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        /*}
        private void OnTriggerEnter2D(Collider2D collision)
        {*/
        if (collision.gameObject.CompareTag("Player"))
        {
            var emptyLoader = GameObject.FindGameObjectsWithTag("EmptyLoader")[0];
            var gameState = Nova.Utils.FindNovaController().GameState;
            //Destroy(emptyLoader.transform.GetChild(0).gameObject);
            gameState.SignalFence(true);

            //Debug.Log(Time.realtimeSinceStartup.ToString() + ": 芝士幻月。");
            var lua_string = "jump_to(\"" + novaLabel + "\")";
            //Nova.LuaRuntime.Instance.DoString(lua_string);
            Nova.LuaRuntime.Instance.DoString("__Nova.coroutineHelper:StopInterrupt()");
            Nova.LuaRuntime.Instance.DoString("input_on()");
        }
        else
        {
            Debug.Log(Time.realtimeSinceStartup.ToString() + ": event triggered but not player");
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
