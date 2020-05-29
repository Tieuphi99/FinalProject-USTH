using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 410f;
    public float jumpForce = 795f;
    [Range(0, 1)] public float smoothTime = 0.6f;

    private bool _isOnGround = true;
    private Vector3 _velocity = Vector3.zero;

    private Animator _playerAnim;
    private Rigidbody2D _playerRb;

    private GoombaController _goombaController;
    public GameObject goomba;

    private static readonly int IdleB = Animator.StringToHash("Idle_b");
    private static readonly int WalkB = Animator.StringToHash("Walk_b");
    private static readonly int RunB = Animator.StringToHash("Run_b");
    private static readonly int SpeedF = Animator.StringToHash("Speed_f");
    private static readonly int JumpTrig = Animator.StringToHash("Jump_trig");

    void Awake()
    {
        _playerAnim = GetComponent<Animator>();
        _playerRb = GetComponent<Rigidbody2D>();
        _goombaController = goomba.GetComponent<GoombaController>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        _playerAnim.SetFloat(SpeedF, Mathf.Abs(_playerRb.velocity.x));
    }

    void MovePlayer()
    {
        Vector3 localScale = transform.localScale;

        // Move
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector2 playerVelocity = _playerRb.velocity;
        Vector3 targetVelocity = new Vector2(horizontalInput * speed * Time.fixedDeltaTime, playerVelocity.y);
        _playerRb.velocity = Vector3.SmoothDamp(playerVelocity, targetVelocity, ref _velocity, smoothTime);

        // Jump
        if (Input.GetKey(KeyCode.Space) && _isOnGround)
        {
            _isOnGround = false;
            _playerAnim.SetTrigger(JumpTrig);
            _playerRb.AddForce(new Vector2(0f, jumpForce));
            _playerAnim.SetBool(IdleB, false);
            _playerAnim.SetBool(WalkB, false);
            _playerAnim.SetBool(RunB, false);
        }

        // Flip sprite, facing RIGHT
        if (Input.GetKey(KeyCode.RightArrow) && _isOnGround)
        {
            if (localScale.x < 0)
            {
                localScale.x *= -1f;
            }
        }

        // Flip sprite, facing LEFT
        if (Input.GetKey(KeyCode.LeftArrow) && _isOnGround)
        {
            if (localScale.x > 0)
            {
                localScale.x *= -1f;
            }
        }

        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isOnGround = true;
            _playerAnim.SetBool(IdleB, true);
            _playerAnim.SetBool(WalkB, true);
            _playerAnim.SetBool(RunB, true);
            Debug.Log("Player touch the ground!");
        }
        
        if (other.gameObject.CompareTag("EnemyHead"))
        {
            // _playerRb.AddForce(new Vector2(0f, jumpForce / 8f));
            Debug.Log("Player touch the head!");
            _goombaController.Die();
        }
    
        else if (other.gameObject.CompareTag("EnemyBody"))
        {
            // _playerRb.AddForce(new Vector2(0f, jumpForce / 8f));
            Debug.Log("Player touch the body!");
        }
        
    }
}