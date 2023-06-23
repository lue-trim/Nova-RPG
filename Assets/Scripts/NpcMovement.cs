using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NpcMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Sprite[] animatedSprites;

    private bool is_moving = true;
    private Vector2 moveValue = new Vector2(0, 0);
    private Vector2Int moveDirection = new Vector2Int(0, 0);
    private Vector2Int activeMoveDirection = new Vector2Int(0, 0);
    private Vector3 stopMovingPosition = new Vector3();

    private Vector3 GetStopMovingPos(Vector3 current_pos, Vector2Int moveDirection)
    {
        float new_pos_x, new_pos_y;

        // 横向

        new_pos_x = Mathf.Floor(current_pos.x + moveDirection.x);


        new_pos_y = Mathf.Floor(current_pos.y + moveDirection.y);
        var new_pos = new Vector3(new_pos_x, new_pos_y, current_pos.z);
        return new_pos;
    }
    private void Update()
    {
        var num = 1f / 0.1f;
        transform.position = new Vector3(Mathf.Round(transform.position.x * num) / num, Mathf.Round(transform.position.y * num) / num, Mathf.Round(transform.position.z * num) / num);
        /*
            if (!is_moving)
            {
                //transform.position = Vector3.MoveTowards(transform.position, stopMovingPosition, 0.1f);
                transform.position = stopMovingPosition;
            }
            if (transform.position == stopMovingPosition)
            {
                is_moving = true;
            }
        */
    }
    public void Move(InputAction.CallbackContext ctx)
    {
        moveValue = ctx.ReadValue<Vector2>(); //input system的callback
        var v = new Vector2(0f, 0f); //速度

        // 检测移动方向
        moveDirection.x = moveValue.x > 0 ? 1 : (moveValue.x < 0 ? -1 : 0);
        moveDirection.y = moveValue.y > 0 ? 1 : (moveValue.y < 0 ? -1 : 0);

        // 检测是否停止移动
        is_moving = !(moveValue == new Vector2(0, 0));
        if (is_moving)
        {
            v.x = moveDirection.x * speed;
            v.y = moveDirection.y * speed;
            activeMoveDirection = moveDirection;
        }
        else
        {
            var current_pos = transform.position;
            stopMovingPosition = GetStopMovingPos(current_pos, activeMoveDirection);
            //Debug.Log(stopMovingPosition);
        }

        // 设置动画状态
        animator.SetInteger("DirectionX", moveDirection.x);
        animator.SetInteger("DirectionY", moveDirection.y);

        // 设置移动速度
        gameObject.GetComponent<Rigidbody2D>().velocity = v;
    }
}
