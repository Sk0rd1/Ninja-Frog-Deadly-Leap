using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    private float jumpSpeed = 250f;
    [SerializeField]
    private float secondJumpSpeed = 275f;
    [SerializeField]
    private float jumpBufferLength = 0.5f;

    private bool isStartGame = false;
    private bool isMoveRight = true;
    private int stageOfJump = 0;
    private Vector2 horizontalDirection = Vector2.zero;
    private Vector2 verticalDirection = Vector2.zero;
    private float jumpBufferCount = 0;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isStartGame)
        {
            CheckMove();

            CheckFall();

            rb.velocity = horizontalDirection + new Vector2(0, rb.velocity.y);
        }
    }

    public void PressJump()
    {
        if (isStartGame)
            jumpBufferCount = jumpBufferLength;

        isStartGame = true;
        animator.SetBool("isGameStart", true);
    }

    private void CheckFall()
    {
        animator.SetBool("isFall", rb.velocity.y < 0f);
    }

    private void CheckMove()
    {
        if (isMoveRight)
        {
            horizontalDirection = new Vector2(1f, 0) * moveSpeed;
        }
        else
        {
            horizontalDirection = new Vector2(-1f, 0) * moveSpeed;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            //jumpBufferCount = jumpBufferLength;
        }
        else
        {
            jumpBufferCount -= Time.deltaTime;
        }


        if (jumpBufferCount > 0)
        {
            Jump(1);
            jumpBufferCount = 0;
        }
    }

    private void Jump(float force)
    {
        if (stageOfJump < 2)
        {
            if(stageOfJump == 1)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                //rb.angularVelocity
            }

            if (stageOfJump == 0)
            {
                rb.AddForce(new Vector2(0, force * jumpSpeed));
            }

            if(stageOfJump == 1)
            {
                rb.AddForce(new Vector2(0, force * secondJumpSpeed));
            }

            stageOfJump++;
            animator.SetInteger("jumpStage", stageOfJump);

            /*if (stageOfJump == 2)
                StartCoroutine(SecondJump());*/
        }
    }

    private IEnumerator SecondJump()
    {
        yield return new WaitForSeconds(0.8f);
        animator.SetInteger("jumpStage", 0);
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            sr.flipX = !sr.flipX;
            isMoveRight = !isMoveRight;
            Debug.Log("Wall");
        }

        if (collision.CompareTag("Floor"))
        {
            StartCoroutine(FloorDetect());
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            sr.flipX = !sr.flipX;
            isMoveRight = !isMoveRight;
        }

        if (collision.gameObject.CompareTag("Floor"))
        {
            StartCoroutine(FloorDetect());
        }
    }

    private IEnumerator FloorDetect()
    {
        animator.SetInteger("jumpStage", 0);
        stageOfJump = 0;
        yield return null;
    }
    
    public void StopPlayer()
    {
        isStartGame = false;
        animator.SetBool("isGameStart", false);

        sr.flipX = false;
        isMoveRight = true;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
    }
}
