using System.Collections.Generic;
using EnemyScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SystemScripts
{
    public class GameManager : MonoBehaviour
    {
        public PlayerController player;
        public GameStatusController gameStatusController;
        public List<GoombaController> goombaControllers;
        public List<GameObject> goombaGameObjects;
        public List<CoinBrickController> coinBrickControllers;
        public GameObject invisibleBrick;
        public GameObject invisiblePowerUp;

        public int score;
        public string playerName = "MARIO";
        public string level = "1-1";
        public float time = 400;
        public int collectedCoin;
        public float finalTime;

        // public GameObject playerPrefab;
        // public GameObject goombaPrefab;
        // public Transform movableObjectsParent;
        // public GameObject mainCamera;
        //
        // public GameObject player;
        // private GameObject _goomba;

        private void Awake()
        {
            // player = Instantiate(playerPrefab, movableObjectsParent);
            // _goomba = Instantiate(goombaPrefab, movableObjectsParent);
            //
            // _goomba.GetComponent<GoombaController>().player = player;
            // mainCamera.GetComponent<FollowPlayer>().player = player;

            gameStatusController.SetLevel(level);
            gameStatusController.SetTime(time);
            gameStatusController.SetName(playerName);
        }

        private void Update()
        {
            StopGoombaMovingWhenPlayerDie();
            SetActiveGoombaWhenSeePlayer();
            UpdateScoreCoinBrick();
            UpdateWhenKillGoomba();
            UpdateUi();
            Debug.Log(player.isStopTime);
            if (player.isStopTime)
            {
                finalTime = time;
                player.isStopTime = false;
            }
        }

        private void StopGoombaMovingWhenPlayerDie()
        {
            if (!player.isDead) return;
            for (var i = 0; i < goombaControllers.Count; i++)
            {
                if (goombaControllers[i] == null)
                {
                    goombaControllers.Remove(goombaControllers[i]);
                }
                else
                {
                    goombaControllers[i].speed = 0;
                    goombaControllers[i].gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    goombaControllers[i].gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                }
            }
        }

        private void SetActiveGoombaWhenSeePlayer()
        {
            for (var i = 0; i < goombaGameObjects.Count; i++)
            {
                if (goombaGameObjects[i] != null)
                {
                    if (goombaGameObjects[i].transform.position.x - player.transform.position.x < 12)
                    {
                        goombaGameObjects[i].SetActive(true);
                    }
                }
                else
                {
                    goombaGameObjects.Remove(goombaGameObjects[i]);
                }
            }
        }

        private void UpdateWhenKillGoomba()
        {
            for (var i = 0; i < goombaControllers.Count; i++)
            {
                if (!goombaControllers[i].isTouchByPlayer) continue;
                score += 100;
                goombaControllers.Remove(goombaControllers[i]);
                goombaGameObjects.Remove(goombaGameObjects[i]);
            }
        }

        private void UpdateScoreCoinBrick()
        {
            for (var i = 0; i < coinBrickControllers.Count; i++)
            {
                if (!coinBrickControllers[i].isTouchByPlayer) continue;
                score += 200;
                collectedCoin += 1;
                coinBrickControllers.Remove(coinBrickControllers[i]);
            }
        }

        private void UpdateUi()
        {
            gameStatusController.SetCoin(collectedCoin);
            gameStatusController.SetScore(score.ToString());
            if (!player.isDead && !player.isWalkingToCastle && !player.isInCastle)
            {
                gameStatusController.SetTime(time -= Time.deltaTime * 2);
            }
            else if (player.isInCastle)
            {
                gameStatusController.SetTime(time -= Time.deltaTime * 60);
                if (time < 0)
                {
                    time = 0;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("BigPlayer"))
            {
                invisibleBrick.SetActive(true);
                invisibleBrick.GetComponent<BoxCollider2D>().isTrigger = false;
                invisiblePowerUp.SetActive(true);
                invisiblePowerUp.GetComponent<BoxCollider2D>().isTrigger = false;
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}