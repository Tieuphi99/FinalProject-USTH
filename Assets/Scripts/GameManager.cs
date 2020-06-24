using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerController player;

    public List<GoombaController> goombas;

    public List<GameObject> goombaGameObjects;
    // public GameObject playerPrefab;
    // public GameObject goombaPrefab;
    // public Transform movableObjectsParent;
    // public GameObject mainCamera;
    //
    // public GameObject player;
    // private GameObject _goomba;
    
    // private void Awake()
    // {
        // player = Instantiate(playerPrefab, movableObjectsParent);
        // _goomba = Instantiate(goombaPrefab, movableObjectsParent);
        
        // _goomba.GetComponent<GoombaController>().player = player;
        // mainCamera.GetComponent<FollowPlayer>().player = player;
    // }

    private void Update()
    {
        if (player.isDead)
        {
            foreach (GoombaController goomba in goombas)
            {
                goomba.speed = 0;
                goomba.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }

        foreach (GameObject goomba in goombaGameObjects)
        {
            if (goomba.transform.position.x - player.transform.position.x < 12)
            {
                goomba.SetActive(true);
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
