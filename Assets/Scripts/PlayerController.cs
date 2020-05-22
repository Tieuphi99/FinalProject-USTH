using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 10;
    private Animator playerAnim;
    private Rigidbody2D playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
        ChangeAnim();
    }

    void MovePlayer()
    {
        Vector3 localScale = transform.localScale;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // transform.Translate(-speed * Time.deltaTime * Vector3.right);
            playerRb.AddForce(-speed * Time.deltaTime * Vector3.right, ForceMode2D.Impulse);
            if (localScale.x > 0f)
            {
                localScale.x *= -1f;
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            // transform.Translate(speed * Time.deltaTime * Vector3.right);
            playerRb.AddForce(speed * Time.deltaTime * Vector3.right, ForceMode2D.Impulse);
            if (localScale.x < 0f)
            {
                localScale.x *= -1f;
            }
        }
    }

    void ChangeAnim()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            playerAnim.SetBool("isIdling", false);
            playerAnim.SetBool("isWalking", true);
            if (playerAnim.GetInteger("Speed_i") > 5)
            {
                playerAnim.SetBool("isWalking", false);
                playerAnim.SetBool("isRunning", true);
            }
            else
            {
                playerAnim.SetBool("isWalking", true);
                playerAnim.SetBool("isRunning", false);
            }
        }
        else
        {
            playerAnim.SetBool("isIdling", true);
            playerAnim.SetBool("isWalking", false);
        }
    }
}