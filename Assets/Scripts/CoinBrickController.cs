using UnityEngine;

public class CoinBrickController : MonoBehaviour
{
    public bool isTouchByPlayer;
    private Animator _coinBrickAnim;
    private static readonly int TouchB = Animator.StringToHash("Touch_b");

    private void Awake()
    {
        _coinBrickAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        TouchByPlayer();
    }

    void TouchByPlayer()
    {
        if (isTouchByPlayer)
        {
            _coinBrickAnim.SetBool(TouchB, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTouchByPlayer = true;
        }
    }
}