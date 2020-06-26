using UnityEngine;

namespace SystemScripts
{
    public class DeathFromBelow : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player"))
            {
                Destroy(other.gameObject);
            }
            else
            {
                other.gameObject.GetComponent<PlayerController>().isDead = true;
            }
        }
    }
}
