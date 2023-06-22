using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    /*
    记录玩家状态
    */
    public Statics.OwnableObject[] objects;
    public Vector2 spawnPoint;

    private void Awake()
    {
        Statics.player = gameObject;
    }
}
