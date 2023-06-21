using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Animator animator;

    public void Move(InputAction.CallbackContext ctx)
    {
        var moveValue = ctx.ReadValue<Vector2>(); //input system的callback
        var v = new Vector2(0f, 0f); //速度

        // 横向
        int direction_x;
        if (moveValue.x < 0)
        {
            //往左走
            v.x = -speed;
            direction_x = -1;
        }
        else if (moveValue.x > 0)
        {
            //往右走
            v.x = speed;
            direction_x = 1;
        }
        else
        {
            //横向不动
            v.x = 0;
            direction_x = 0;
        }

        // 纵向
        int direction_y;
        if (moveValue.y < 0)
        {
            //往下走
            v.y = -speed;
            direction_y = -1;
        }
        else if (moveValue.y > 0)
        {
            //往上走
            v.y = speed;
            direction_y = 1;
        }
        else
        {
            //纵向不动
            v.y = 0;
            direction_y = 0;
        }

        // 设置动画状态
        animator.SetInteger("DirectionX", direction_x);
        animator.SetInteger("DirectionY", direction_y);

        // 设置移动速度
        gameObject.GetComponent<Rigidbody2D>().velocity = v;
    }
}
