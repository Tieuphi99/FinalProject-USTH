﻿using UnityEngine;

namespace SystemScripts
{
    public class FollowPlayer : MonoBehaviour
    {
        public GameObject player;
        private float _furthestPlayerPosition;
        private float _currentPlayerPosition;

        private void Start()
        {
            if (player != null)
            {
                _currentPlayerPosition = player.transform.position.x;
            }
        }

        // Camera follows player
        void LateUpdate()
        {
            if (player != null)
            {
                _currentPlayerPosition = player.transform.position.x;
                if (_currentPlayerPosition >= _furthestPlayerPosition)
                {
                    _furthestPlayerPosition = _currentPlayerPosition;
                }

                if (_currentPlayerPosition > 4.5f &&
                    _currentPlayerPosition >= _furthestPlayerPosition)
                {
                    transform.position = new Vector3(player.transform.position.x, 5, -10);
                }
            }
        }
    }
}