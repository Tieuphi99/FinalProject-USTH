using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public GameStatusController gameStatusController;
    public List<GoombaController> goombaControllers;
    public List<GameObject> goombaGameObjects;
    public List<CoinBrickController> coinBrickControllers;

    public int score;
    public string playerName = "MARIO";
    public string level = "1-1";
    public float time = 400;
    public int collectedCoin;

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
        UpdateUI();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void StopGoombaMovingWhenPlayerDie()
    {
        if (!player.isDead) return;
        foreach (var goomba in goombaControllers)
        {
            goomba.speed = 0;
            goomba.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            goomba.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    void SetActiveGoombaWhenSeePlayer()
    {
        foreach (var goomba in goombaGameObjects)
        {
            if (goomba.transform.position.x - player.transform.position.x < 12)
            {
                goomba.SetActive(true);
            }
        }
    }

    void UpdateWhenKillGoomba()
    {
        for (int i = 0; i < goombaControllers.Count; i++)
        {
            if (!goombaControllers[i].isTouchByPlayer) continue;
            score += 100;
            goombaControllers.Remove(goombaControllers[i]);
            goombaGameObjects.Remove(goombaGameObjects[i]);
        }
    }

    void UpdateScoreCoinBrick()
    {
        for (int i = 0; i < coinBrickControllers.Count; i++)
        {
            if (!coinBrickControllers[i].isTouchByPlayer) continue;
            score += 200;
            collectedCoin += 1;
            coinBrickControllers.Remove(coinBrickControllers[i]);
        }
    }

    void UpdateUI()
    {
        gameStatusController.SetCoin(collectedCoin);
        gameStatusController.SetScore(score.ToString());
        if (!player.isDead)
        {
            gameStatusController.SetTime(time -= Time.deltaTime * 2);
        }
    }
}