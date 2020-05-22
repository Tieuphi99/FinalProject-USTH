using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 10;
    private Animator playerAnim;
    private Rigidbody2D playerRb;
    public int maxSpeed = 7;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeAnim();
    }

    void MovePlayer()
    {
        Vector3 localScale = transform.localScale;
        playerRb.velocity = Vector2.ClampMagnitude(playerRb.velocity, maxSpeed);
        // float horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.RightArrow))
        {
            // transform.Translate(speed * horizontalInput * Time.deltaTime * Vector3.right);
            playerRb.AddForce(speed * Time.deltaTime * Vector3.right, ForceMode2D.Impulse);
            if (localScale.x < 0)
            {
                localScale.x *= -1f;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // transform.Translate(speed * horizontalInput * Time.deltaTime * Vector3.right);
            playerRb.AddForce(-speed * Time.deltaTime * Vector3.right, ForceMode2D.Impulse);
            if (localScale.x > 0)
            {
                localScale.x *= -1f;
            }
        }
        
        transform.localScale = localScale;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.Translate(speed * Time.deltaTime * Vector3.up);
        }
    }

    void ChangeAnim()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            playerAnim.SetBool("isIdling", false);
            playerAnim.SetBool("isWalking", true);
        }
        else
        {
            playerAnim.SetBool("isIdling", true);
            playerAnim.SetBool("isWalking", false);
        }

        if (Mathf.Abs(playerRb.velocity.x) > 3.5f)
        {
            playerAnim.SetBool("isWalking", false);
            playerAnim.SetBool("isRunning", true);
        }
        else
        {
            playerAnim.SetBool("isWalking", true);
            playerAnim.SetBool("isRunning", false);
        }

        if (playerRb.velocity.x == 0)
        {
            playerAnim.SetBool("isIdling", true);
            playerAnim.SetBool("isWalking", false);
        }
    }
}