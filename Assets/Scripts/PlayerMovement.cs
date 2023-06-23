using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    // ----Editor
    [SerializeField] // 移动速度
    private float speed;
    [SerializeField] // 动画状态机
    private Animator animator;
    [SerializeField] // Sprite renderer
    private SpriteRenderer spriteRenderer;
    [SerializeField] // 动画中使用的精灵
    private Sprite[] animatedSprites;
    [SerializeField] // 精灵id
    private int spriteId;
    [SerializeField] // 碰撞体
    private BoxCollider2D collider;

    // ----地图类
    // 当前地图
    [SerializeField]
    private GameObject tilemapsGameObject;
    // 场景中所有的地图碰撞箱
    private TilemapCollider2D[] tilemap_cols;
    // 当前地图中所有的事件点
    private GameObject[] events;

    // ----移动相关的全局变量
    // 移动任务
    private List<Vector3> moveActions = new List<Vector3>();
    // 移动已持续时间
    private float moveTime = 0f;
    // 是否接收到移动信号
    private bool is_moving = true;
    // 接收到的移动信号值
    private Vector2 moveValue = new Vector2(0, 0);
    // 移动方向
    private Vector2Int moveDirection = new Vector2Int(0, 0);
    // 应该停下来的位置
    private Vector3 stopMovingPosition = new Vector3();
    // 上一个应该停下来的位置
    private Vector3 lastStopMovingPosition = new Vector3();

    // ----Public methods
    public void SetMovePos(Vector3 pos)
    {
        moveActions.Add(pos);
    }

    // ----Private methods
    private Vector3 GetStopMovingPos(Vector3 current_pos, Vector2Int moveDirection)
    {
        float new_pos_x, new_pos_y;

        /*// 横向
        new_pos_x = Mathf.Floor(current_pos.x + moveDirection.x) + 0.5f;

        // 纵向
        new_pos_y = Mathf.Floor(current_pos.y + moveDirection.y) + 0.5f;*/
        //横向
        if (moveDirection.x > 0)
        {
            new_pos_x = Mathf.Floor(current_pos.x + moveDirection.x);
        }
        else if (moveDirection.x < 0)
        {
            new_pos_x = Mathf.Ceil(current_pos.x + moveDirection.x);
        }
        else
        {
            new_pos_x = Mathf.Round(current_pos.x);
        }
        //纵向
        if (moveDirection.y > 0)
        {
            new_pos_y = Mathf.Floor(current_pos.y + moveDirection.y);
        }
        else if (moveDirection.y < 0)
        {
            new_pos_y = Mathf.Ceil(current_pos.y + moveDirection.y);
        }
        else
        {
            new_pos_y = Mathf.Round(current_pos.y);
        }

        var new_pos = new Vector3(new_pos_x, new_pos_y, current_pos.z);
        return new_pos;
    }
    private Vector3 MoveTowards()
    {

        float time = 1f / speed;
        moveTime += Time.deltaTime;
        // 设new_x=a(x-time)^2+stop_pos,其中a=(last_pos-stop_pos)/time^2
        var new_pos_x = -moveDirection.x / Mathf.Pow(time, 2f) * Mathf.Pow(moveTime - time, 2f) + stopMovingPosition.x + moveDirection.x;
        var new_pos_y = -moveDirection.y / Mathf.Pow(time, 2f) * Mathf.Pow(moveTime - time, 2f) + stopMovingPosition.y + moveDirection.y;
        var new_pos = new Vector3(new_pos_x, new_pos_y, transform.position.z);

        if (moveTime >= time)
        {
            moveTime = 0;
            return stopMovingPosition;
        }
        else
        {
            return new_pos;
        }
    }
    private bool IsHaveCol(Vector3 pos)
    {
        // 寻找对应点位有没有地图碰撞体
        foreach (var tilemap_col in tilemap_cols)
        {
            var tilemap = tilemap_col.gameObject.GetComponent<Tilemap>();
            var new_x = Mathf.RoundToInt(pos.x);
            var new_y = Mathf.RoundToInt(pos.y);
            var new_z = Mathf.RoundToInt(pos.z);
            var col_type = tilemap.GetColliderType(new Vector3Int(new_x, new_y, new_z));
            if (col_type != Tile.ColliderType.None)
            {
                return true;
            }
        }

        // 寻找对应点位有没有NPC
        foreach (var eventObject in events)
        {
            if (pos == eventObject.transform.position)
            {
                return true;
            }
        }
        return false;
    }
    private void Awake()
    {
        stopMovingPosition = transform.position;
        tilemap_cols = tilemapsGameObject.GetComponentsInChildren<TilemapCollider2D>();
        events = GameObject.FindGameObjectsWithTag("Event");
        tilemapsGameObject = GameObject.FindGameObjectsWithTag("Map")[0];
    }
    private void Update()
    {
        // 设置动画
        spriteRenderer.sprite = animatedSprites[spriteId];

        // 检测前方是否存在碰撞区域
        if (IsHaveCol(stopMovingPosition))
        {
            stopMovingPosition = lastStopMovingPosition;
            transform.position = lastStopMovingPosition;
            return;
        }

        // 设置碰撞触发区域
        //if (transform.position == stopMovingPosition)
        if (is_moving)
        {
            var new_x = moveDirection.x * 0.3f;
            var new_y = moveDirection.y * 0.3f;
            var new_offset = new Vector2(new_x, new_y);
            collider.offset = new_offset;
        }
        else
        {
            collider.offset = new Vector2(0f, 0f);
        }

        // 实现移动
        if (transform.position != stopMovingPosition)
        {
            if (moveActions.Count == 0)
            {
                // 玩家控制
                transform.position = Vector3.MoveTowards(transform.position, stopMovingPosition, speed * Time.deltaTime);
            }
            else
            {
                // 指定了移动的点（非玩家控制）
                transform.position = Vector3.MoveTowards(transform.position, moveActions[0], speed * Time.deltaTime);
            }
        }
        else if (is_moving)
        {
            lastStopMovingPosition = transform.position;
            if (moveActions.Count > 0)
            {
                moveActions.RemoveAt(0);
            }
            //Debug.Log("Updated lastpos to " + lastStopMovingPosition.ToString());
        }

        // 更新移动目标
        if (is_moving)
        {
            var new_moving_pos = GetStopMovingPos(transform.position, moveDirection);
            if (IsHaveCol(new_moving_pos))
            {
                return;
            }
            if (stopMovingPosition != new_moving_pos)
            {
                lastStopMovingPosition = stopMovingPosition;
                stopMovingPosition = new_moving_pos;
                moveTime = 0;
            }
        }
    }
    public void Move(InputAction.CallbackContext ctx)
    {
        moveValue = ctx.ReadValue<Vector2>(); //input system的callback
        //var v = new Vector2(0f, 0f); //速度

        // 检测移动方向
        moveDirection.x = moveValue.x > 0 ? 1 : (moveValue.x < 0 ? -1 : 0);
        moveDirection.y = moveValue.y > 0 ? 1 : (moveValue.y < 0 ? -1 : 0);

        // 设置初始移动目标点
        if (!is_moving)
        {
            var current_pos = transform.position;
            lastStopMovingPosition = current_pos;
        }

        // 检测是否停止移动
        is_moving = !(moveValue == new Vector2(0, 0));
        if (is_moving)
        {
            moveTime = 0;
        }
        /*
        if (is_moving)
        {
            v.x = moveDirection.x * speed;
            v.y = moveDirection.y * speed;
            //activeMoveDirection = moveDirection;
        }
        else
        {
            var current_pos = transform.position;
            //stopMovingPosition = GetStopMovingPos(current_pos, activeMoveDirection);
            //Debug.Log(stopMovingPosition);
        }
        */

        // 设置动画状态
        animator.SetInteger("DirectionX", moveDirection.x);
        animator.SetInteger("DirectionY", moveDirection.y);

        // 设置移动速度
        //gameObject.GetComponent<Rigidbody2D>().velocity = v;
        //Debug.Log(stopMovingPosition.ToString() + lastStopMovingPosition.ToString());
    }
}
