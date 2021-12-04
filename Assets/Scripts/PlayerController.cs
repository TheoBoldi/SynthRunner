using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side { Left, Center, Right }
public class PlayerController : MonoBehaviour
{
    public float xMove;
    public float moveSpeed;
    public float jumpForce;
    public float rollForce;
    public float gravityScale;

    [HideInInspector] public Side side = Side.Center;
    private float newXPos = 0f;
    private float x = 0f;
    private float y = 0f;
    private float rollDuration;
    private bool isGrounded = true;
    private bool inJump = false;
    private bool inRoll = false;
    public static bool inCharge = false;
    private Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        inCharge = false;
        m_animator = GetComponentInChildren<Animator>();
        rollDuration = m_animator.runtimeAnimatorController.animationClips[1].length;
    }

    // Update is called once per frame
    void Update()
    {
        if (SwipeInput.swipedLeft)
        {
            if(side == Side.Center)
            {
                newXPos = -xMove;
                side = Side.Left;
            }
            else if(side == Side.Right)
            {
                newXPos = 0;
                side = Side.Center;
            }
        }

        if (SwipeInput.swipedRight)
        {
            if (side == Side.Center)
            {
                newXPos = xMove;
                side = Side.Right;
            }
            else if (side == Side.Left)
            {
                newXPos = 0;
                side = Side.Center;
            }
        }

        var pos = transform.position;
        pos.y = Mathf.Clamp(transform.position.y, 0, transform.position.y);
        transform.position = pos;

        Vector3 moveVector = new Vector3(x - transform.position.x, y * Time.deltaTime * gravityScale, 0);
        x = Mathf.Lerp(x, newXPos, Time.deltaTime * moveSpeed);
        transform.Translate(moveVector);
        Jump();
        Roll();
        GroundCheck();
    }

    public void Jump()
    {
        if (isGrounded)
        {
            if (inJump)
            {
                inJump = false;
            }
            if (SwipeInput.swipedUp)
            {
                y = jumpForce;
                inJump = true;
            }
        }
        else 
        {
            y -= jumpForce * 1.25f * gravityScale * Time.deltaTime;
        }
    }

    internal float rollCounter;
    public void Roll()
    {
        rollCounter -= Time.deltaTime;
        if(rollCounter <= 0f)
        {
            rollCounter = 0f;
            inRoll = false;
        }
        if (SwipeInput.swipedDown)
        {
            m_animator.Play("Slide");
            rollCounter = rollDuration;
            if (!isGrounded)
            {
                y -= rollForce;
                inCharge = true;
            }
            inRoll = true;
            inJump = false;
        }
    }

    public void GroundCheck()
    {
        if (transform.position.y <= 0.1f)
        {
            if (!isGrounded)
            {
                y = 0;
            }

            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded)
            inCharge = false;
    }
}
