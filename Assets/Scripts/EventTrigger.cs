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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var lua_string = "jump_to " + novaLabel;
        Nova.LuaRuntime.Instance.DoString(lua_string);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
