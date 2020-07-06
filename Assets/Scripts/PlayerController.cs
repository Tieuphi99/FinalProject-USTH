using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 410f;
    public float slideDownSpeed = 410f;
    public float jumpForce = 795f;
    private float _flagPos;
    [Range(0, 1)] public float smoothTime = 0.6f;
    public bool isDead;
    private bool _isOnGround;
    private bool _isEatable;
    private bool _isFinish;
    private bool _isNotHugPole;
    private bool _isWalkingToCastle;
    private Vector3 _velocity;

    public GameObject playerSprite;
    public GameObject bigPlayer;
    public GameObject bigPlayerCollider;
    public GameObject smallPlayer;
    public GameObject smallPlayerCollider;

    private Animator _playerAnim;
    private Rigidbody2D _playerRb;
    public BoxCollider2D playerCol;
    public EdgeCollider2D playerEdgeCol;

    private static readonly int IdleB = Animator.StringToHash("Idle_b");
    private static readonly int WalkB = Animator.StringToHash("Walk_b");
    private static readonly int RunB = Animator.StringToHash("Run_b");
    private static readonly int SpeedF = Animator.StringToHash("Speed_f");
    private static readonly int JumpTrig = Animator.StringToHash("Jump_trig");
    private static readonly int DieB = Animator.StringToHash("Die_b");
    private static readonly int BigB = Animator.StringToHash("Big_b");
    private static readonly int HugB = Animator.StringToHash("Hug_b");

    void Awake()
    {
        _velocity = Vector3.zero;
        _playerAnim = GetComponent<Animator>();
        _playerRb = GetComponent<Rigidbody2D>();
        isDead = false;
        _isFinish = false;
        _isOnGround = true;
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            Die();
        }
        else if (!isDead && !_isFinish)
        {
            MovePlayer();
            GetPlayerSpeed();
        }

        if (_isFinish)
        {
            if (transform.position.y > 1.5f)
            {
                _playerAnim.SetBool(HugB, true);
                _playerAnim.SetFloat(SpeedF, 0);
                transform.Translate(slideDownSpeed * Time.deltaTime * Vector3.down);
            }
            else
            {
                if (transform.position.x < _flagPos + 0.8f)
                {
                    _playerAnim.SetBool(HugB, false);
                    transform.localScale = new Vector3(-1, 1, 1);
                    transform.position = new Vector3(_flagPos + 0.8f, transform.position.y);
                }

                _playerRb.isKinematic = false;
                StartCoroutine(HugPole());
                if (_isWalkingToCastle && _isNotHugPole)
                {
                    transform.localScale = Vector3.one;
                    _playerAnim.SetFloat(SpeedF, 3f);
                    transform.Translate(slideDownSpeed * Time.deltaTime * Vector3.right);
                }
            }
        }
    }

    void MovePlayer()
    {
        Vector3 localScale = transform.localScale;
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector2 playerVelocity = _playerRb.velocity;
        Vector3 targetVelocity = new Vector2(horizontalInput * speed * Time.fixedDeltaTime, playerVelocity.y);
        _playerRb.velocity = Vector3.SmoothDamp(playerVelocity, targetVelocity, ref _velocity, smoothTime);

        if (Input.GetKeyDown(KeyCode.Space) && _isOnGround)
        {
            _isOnGround = false;
            _playerAnim.SetTrigger(JumpTrig);
            _playerRb.AddForce(new Vector2(0f, jumpForce));
            _playerAnim.SetBool(IdleB, false);
            _playerAnim.SetBool(WalkB, false);
            _playerAnim.SetBool(RunB, false);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (localScale.x < 0)
            {
                localScale.x *= -1f;
            }
        }

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
        playerCol.enabled = false;
        _playerRb.velocity = Vector2.zero;
    }

    void GetPlayerSpeed()
    {
        _playerAnim.SetFloat(SpeedF, Mathf.Abs(_playerRb.velocity.x));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Pipe") ||
            other.gameObject.CompareTag("Brick") ||
            other.gameObject.CompareTag("Stone"))
        {
            _isOnGround = true;
            _playerAnim.SetBool(IdleB, true);
            _playerAnim.SetBool(WalkB, true);
            _playerAnim.SetBool(RunB, true);
        }

        if (other.gameObject.CompareTag("PowerBrick"))
        {
            StartCoroutine(SetBoolEatable());
        }

        if (other.gameObject.CompareTag("Pole"))
        {
            _flagPos = other.gameObject.transform.position.x;
            _isFinish = true;
            _playerRb.velocity = Vector2.zero;
            _playerRb.isKinematic = true;
            _isWalkingToCastle = true;
        }

        if (other.gameObject.CompareTag("Castle"))
        {
            _isWalkingToCastle = false;
            playerSprite.SetActive(false);
        }

        if (!other.gameObject.CompareTag("BigMushroom") || !_isEatable) return;
        tag = "BigPlayer";
        _playerAnim.SetBool(BigB, true);
        bigPlayer.SetActive(true);
        bigPlayerCollider.SetActive(true);
        smallPlayer.SetActive(false);
        smallPlayerCollider.SetActive(false);
    }

    IEnumerator SetBoolEatable()
    {
        yield return new WaitForSeconds(0.5f);
        _isEatable = true;
    }

    IEnumerator HugPole()
    {
        yield return new WaitForSeconds(1.2f);
        _isNotHugPole = true;
    }
}