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

    public bool isDead;
    private bool _isOnGround = true;
    private Vector3 _velocity = Vector3.zero;

    private Animator _playerAnim;
    private Rigidbody2D _playerRb;
    private BoxCollider2D _playerCol;

    private static readonly int IdleB = Animator.StringToHash("Idle_b");
    private static readonly int WalkB = Animator.StringToHash("Walk_b");
    private static readonly int RunB = Animator.StringToHash("Run_b");
    private static readonly int SpeedF = Animator.StringToHash("Speed_f");
    private static readonly int JumpTrig = Animator.StringToHash("Jump_trig");
    private static readonly int DieB = Animator.StringToHash("Die_b");

    void Awake()
    {
        _playerAnim = GetComponent<Animator>();
        _playerRb = GetComponent<Rigidbody2D>();
        _playerCol = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            Die();
        }
        else
        {
            MovePlayer();
            GetPlayerSpeed();
        }
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
        if (Input.GetKeyDown(KeyCode.Space) && _isOnGround)
        {
            _isOnGround = false;
            _playerAnim.SetTrigger(JumpTrig);
            _playerRb.AddForce(new Vector2(0f, jumpForce));
            _playerAnim.SetBool(IdleB, false);
            _playerAnim.SetBool(WalkB, false);
            _playerAnim.SetBool(RunB, false);
        }

        // Flip sprite, facing RIGHT
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (localScale.x < 0)
            {
                localScale.x *= -1f;
            }
        }

        // Flip sprite, facing LEFT
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (localScale.x > 0)
            {
                localScale.x *= -1f;
            }
        }

        transform.localScale = localScale;
    }

    void Die()
    {
        _playerAnim.SetBool(DieB, isDead);
        _playerCol.enabled = false;
        _playerRb.velocity = Vector2.zero;
    }

    void GetPlayerSpeed()
    {
        _playerAnim.SetFloat(SpeedF, Mathf.Abs(_playerRb.velocity.x));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Pipe") )
        {
            _isOnGround = true;
            _playerAnim.SetBool(IdleB, true);
            _playerAnim.SetBool(WalkB, true);
            _playerAnim.SetBool(RunB, true);
        }
    }
}