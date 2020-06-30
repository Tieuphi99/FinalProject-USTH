﻿using System.Collections;
using UnityEngine;

namespace EnemyScripts
{
    public class EnemyBody : MonoBehaviour
    {
        private PlayerController _playerController;
        private GoombaController _goombaController;
    
        public GameObject goomba;

        private void Awake()
        {
            _goombaController = goomba.GetComponent<GoombaController>();
            _playerController = _goombaController.player.GetComponent<PlayerController>();
        }

        private void Update()
        {
            if (_goombaController.isTouchByPlayer)
            {
                GetComponent<BoxCollider2D>().offset = Vector2.zero;
                GetComponent<BoxCollider2D>().size = new Vector2(1.04f, 0.32f);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Die(other.gameObject));
                _playerController.isDead = true;
            }
            else if (other.gameObject.CompareTag("BigPlayer"))
            {
                StartCoroutine(Die(other.gameObject));
                _playerController.isDead = true;
            }
        }

        IEnumerator Die(GameObject playerGameObject)
        {
            yield return new WaitForSeconds(1);
            playerGameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 4000));
            playerGameObject.GetComponent<Rigidbody2D>().gravityScale = 25;
            playerGameObject.GetComponent<PlayerController>().playerCol.enabled = false;
            playerGameObject.GetComponent<PlayerController>().playerEdgeCol.enabled = false;
        }
    }
}