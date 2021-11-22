using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side { Left, Center, Right }
public class PlayerController : MonoBehaviour
{
    public float xMove;
    public float moveSpeed;
    public float jumpForce;
    public float rollDuration;

    [HideInInspector] public Side side = Side.Center;
    private float newXPos = 0f;
    private float x = 0f;
    private float y = 0f;
    private float colHeight = 0f;
    private float colCenterY = 0f;
    private bool inJump = false;
    private bool inRoll = false;
    private CharacterController m_character;
    private Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        m_character = GetComponent<CharacterController>();
        m_animator = GetComponentInChildren<Animator>();
        colHeight = m_character.height;
        colCenterY = m_character.center.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (SwipeInput.swipedLeft && !inRoll)
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

        if (SwipeInput.swipedRight && !inRoll)
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

        Vector3 moveVector = new Vector3(x - transform.position.x, y * Time.deltaTime, 0);
        x = Mathf.Lerp(x, newXPos, Time.deltaTime * moveSpeed);
        m_character.Move(moveVector);
        Jump();
        Roll();
    }

    public void Jump()
    {
        if (m_character.isGrounded)
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
            y -= jumpForce * 2 * Time.deltaTime;
        }
    }

    internal float rollCounter;
    public void Roll()
    {
        rollCounter -= Time.deltaTime;
        if(rollCounter <= 0f)
        {
            rollCounter = 0f;
            m_character.center = new Vector3(0, colCenterY, 0);
            m_character.height = colHeight;
            inRoll = false;
        }
        if (SwipeInput.swipedDown)
        {
            m_animator.Play("Slide");
            rollCounter = rollDuration;
            y -= 20f;
            m_character.center = new Vector3(0, -0.5f, 0);
            m_character.height = (colHeight / 4f);
            inRoll = true;
            inJump = false;
        }
    }
}
