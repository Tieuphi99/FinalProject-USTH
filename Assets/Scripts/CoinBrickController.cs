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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTouchByPlayer = true;
            _coinBrickAnim.SetBool(TouchB, isTouchByPlayer);
        }
    }
}