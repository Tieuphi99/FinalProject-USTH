using UnityEngine;

public class BrickController : MonoBehaviour
{
    public bool isTouchByPlayer;
    private Animator _brickAnim;
    private static readonly int TouchB = Animator.StringToHash("Touch_b");
    private static readonly int TouchT = Animator.StringToHash("Touch_t");

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

        if (other.gameObject.CompareTag("BigPlayer"))
        {
            _brickAnim.SetTrigger(TouchT);
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