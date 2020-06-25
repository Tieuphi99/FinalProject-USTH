using System;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    public bool isTouchByPlayer;
    private Animator _brickAnim;
    private static readonly int TouchB = Animator.StringToHash("Touch_b");

    private void Awake()
    {
        _brickAnim = GetComponent<Animator>();
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTouchByPlayer = true;
            _brickAnim.SetBool(TouchB, isTouchByPlayer);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTouchByPlayer = false;
            _brickAnim.SetBool(TouchB, isTouchByPlayer);
        }
    }
}