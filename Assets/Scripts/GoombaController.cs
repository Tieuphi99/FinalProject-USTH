using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GoombaController : MonoBehaviour
{
    public int speed = 3;

    private Animator _goombaAnim;

    private static readonly int DieB = Animator.StringToHash("Die_b");

    private void Awake()
    {
        _goombaAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.left);
    }

    public void Die()
    {
        _goombaAnim.SetBool(DieB, true);
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}