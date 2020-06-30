using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PowerUpsController : MonoBehaviour
{
    public int speedRight;
    public int speedUp;
    public bool isMoving;
    public bool isTouchByPlayer;
    private bool _isEatable;
    private float _firstYPos;

    // Start is called before the first frame update
    void Awake()
    {
        _firstYPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouchByPlayer)
        {
            if (transform.position.y < _firstYPos + 1)
            {
                transform.Translate(speedUp * Time.deltaTime * Vector2.up);
            }
            else if (CompareTag("BigMushroom") || CompareTag("1UpMushroom"))
            {
                isMoving = true;
                GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }

        if (!isMoving || !CompareTag("BigMushroom")) return;
        isTouchByPlayer = false;
        transform.Translate(speedRight * Time.deltaTime * Vector2.right);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTouchByPlayer = true;
            StartCoroutine(SetBoolEatable());
        }
        else if (other.gameObject.CompareTag("BigPlayer"))
        {
            isTouchByPlayer = true;
            StartCoroutine(SetBoolEatable());
        }

        if (other.gameObject.CompareTag("Stone") || other.gameObject.CompareTag("Pipe"))
        {
            speedRight = -speedRight;
        }

        if (other.gameObject.CompareTag("Player") && _isEatable)
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("BigPlayer") && _isEatable)
        {
            Destroy(gameObject);
        }

        if (!other.gameObject.CompareTag("Enemy") || CompareTag("FireFlower") || CompareTag("UltimateStar")) return;
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy") || CompareTag("FireFlower")) return;
        GetComponent<BoxCollider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().isKinematic = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && _isEatable)
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("BigPlayer") && _isEatable)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator SetBoolEatable()
    {
        yield return new WaitForSeconds(1);
        _isEatable = true;
    }
}