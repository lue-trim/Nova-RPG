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
            Debug.Log(Time.realtimeSinceStartup.ToString() + ": 芝士幻月。");
            //var lua_string = "jump_to " + novaLabel;
            //Nova.LuaRuntime.Instance.DoString(lua_string);}
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
