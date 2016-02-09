﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{

    public GameObject topWall, bottomWall, leftWall, rightWall;
    public GameObject topPortal, botPortal, leftPortal, rightPortal;
    public float spawnLocationBuffer = 100f;
    public float spawnTimeBuffer = .05f;
    public float xSpawnBuffer;
    public float ySpawnBuffer;
    private GameObject dataObject, timeObject;
    private LocalMultiplayerGameData localData;
    private Timer timer;
    public float spawnTime = 14.0f;
    private Dictionary<int, List<GameObject>> portalListSectioned;
    public GameObject centerOfScreen;
    public int speedOfObject = 10;
    public int portalSpawnBuffer = 10;
    public SpriteRenderer colorChanger;
    public Color white = new Color(255, 255, 255);
    public Color red = new Color(255, 0, 0);
    public Color yellow = new Color(255, 255, 0);
    public Color blue = new Color(0, 0, 255);
    public Color gray = new Color(0.5f, 0.5f, 0.5f, 1f);

    /// <summary>
    /// Local data connects to the multiplayer data
    /// using that data the creation of the inital play space is made
    /// load all of the scene and portal contents
    /// </summary>
    void Start()
    {
        dataObject = GameObject.Find("LocalMultiplayerGameData");
        localData = dataObject.GetComponent<LocalMultiplayerGameData>();
        timeObject = GameObject.Find("Timer");
        timer = timeObject.GetComponent<Timer>();
        portalListSectioned = new Dictionary<int, List<GameObject>>();

        PlayerSetup();
        sceneSetup();
    }

    // Update is called once per frame
    void Update()
    {
        
        //check for end of session, round, and game
        if (timer.getTime() <= 0)
        {
            if (localData.currentRound == 3 && localData.currentPlayer == localData.lastPlayer)
            {
                Debug.Log("end game");
                SceneManager.LoadScene("EndGameScreen");
            }
            else
            {
                localData.nextPlayer();
                SceneManager.LoadScene("PlayerTurnScreen");
                Destroy(this.gameObject);
            }
        }

        if(localData.playerData[localData.currentPlayer].color == "White" && timer.getTime() < spawnTime && timer.getTime() > spawnTime - spawnTimeBuffer)
        {
            
            if(localData.playerData[localData.currentPlayer].player2PortalContents.Count != 0)
            {
                
                Vector2 location = new Vector2();
                location   = GameObject.FindGameObjectWithTag("Player2").transform.position;
                location += location;
                GameObject node = Instantiate(localData.playerData[localData.currentPlayer].player2PortalContents[0], transform.position + (transform.forward * 2), Quaternion.identity) as GameObject;
                colorChanger = node.GetComponent<SpriteRenderer>();
                colorChanger.color = red;
                localData.playerData[localData.currentPlayer].currentSceneObjects.Add(localData.playerData[localData.currentPlayer].player2PortalContents[0]);
                localData.playerData[localData.currentPlayer].player2PortalContents.RemoveAt(0);
                
            }
            spawnTime--;
        }
    }
    //FUNNELS STILL LIVE
    //DONT TOUCH UNLESS ADDING MORE PLAYERS
    //code sorts the scene and controls the colors of the portals
    //the attachment of the portals to specific players
    //and changes it based on the number players in the game
    void PlayerSetup()
    {

        ///Change portal tags for collision detection in order to put the objects in the correct buckets
        if (localData.playerData[localData.currentPlayer].color == "White")
        {
            if (localData.numberOfPlayers == 2)
            {
                topPortal.tag = "Player2";
                colorChanger = topPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = red;
                Destroy(rightPortal.GetComponent<Collider2D>());
                Destroy(rightPortal.GetComponent<SpriteRenderer>());
                Destroy(botPortal.GetComponent<Collider2D>());
                Destroy(botPortal.GetComponent<SpriteRenderer>());
                Destroy(leftPortal.GetComponent<Collider2D>());
                Destroy(leftPortal.GetComponent<SpriteRenderer>());
            }
            if (localData.numberOfPlayers == 3)
            {
                topPortal.tag = "Player2";
                colorChanger = topPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = red;
                rightPortal.tag = "Player3";
                colorChanger = rightPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = yellow;
                Destroy(botPortal.GetComponent<Collider2D>());
                Destroy(botPortal.GetComponent<SpriteRenderer>());
                Destroy(leftPortal.GetComponent<Collider2D>());
                Destroy(leftPortal.GetComponent<SpriteRenderer>());
            }
            if (localData.numberOfPlayers == 4)
            {
                topPortal.tag = "Player2";
                colorChanger = topPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = red;
                rightPortal.tag = "Player3";
                colorChanger = rightPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = yellow;
                botPortal.tag = "Player4";
                colorChanger = botPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = blue;
                Destroy(leftPortal.GetComponent<Collider2D>());
                Destroy(leftPortal.GetComponent<SpriteRenderer>());

            }
            if (localData.numberOfPlayers == 5)
            {
                topPortal.tag = "Player2";
                colorChanger = topPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = red;
                rightPortal.tag = "Player3";
                colorChanger = rightPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = yellow;
                botPortal.tag = "Player4";
                colorChanger = botPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = blue;
                leftPortal.tag = "Player5";
                colorChanger = leftPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = gray;
            }
        }
            /*
            topPortal.tag = "Player1";
            rightPortal.tag = "Player3";
            botPortal.tag = "Player4";
            leftPortal.tag = "Player5";
            */
        else if (localData.playerData[localData.currentPlayer].color == "Red")
        {
            if (localData.numberOfPlayers == 2)
            {
                topPortal.tag = "Player1";
                colorChanger = topPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = white;
                Destroy(rightPortal.GetComponent<Collider2D>());
                Destroy(rightPortal.GetComponent<SpriteRenderer>());
                Destroy(botPortal.GetComponent<Collider2D>());
                Destroy(botPortal.GetComponent<SpriteRenderer>());
                Destroy(leftPortal.GetComponent<Collider2D>());
                Destroy(leftPortal.GetComponent<SpriteRenderer>());
            }
            if (localData.numberOfPlayers == 3)
            {
                topPortal.tag = "Player1";
                colorChanger = topPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = white;
                rightPortal.tag = "Player3";
                colorChanger = rightPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = yellow;
                Destroy(botPortal.GetComponent<Collider2D>());
                Destroy(botPortal.GetComponent<SpriteRenderer>());
                Destroy(leftPortal.GetComponent<Collider2D>());
                Destroy(leftPortal.GetComponent<SpriteRenderer>());
            }
            if (localData.numberOfPlayers == 4)
            {
                topPortal.tag = "Player1";
                colorChanger = topPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = white;
                rightPortal.tag = "Player3";
                colorChanger = rightPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = yellow;
                botPortal.tag = "Player4";
                colorChanger = botPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = blue;
                Destroy(leftPortal.GetComponent<Collider2D>());
                Destroy(leftPortal.GetComponent<SpriteRenderer>());

            }
            if (localData.numberOfPlayers == 5)
            {
                topPortal.tag = "Player1";
                colorChanger = topPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = white;
                rightPortal.tag = "Player3";
                colorChanger = rightPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = yellow;
                botPortal.tag = "Player4";
                colorChanger = botPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = blue;
                leftPortal.tag = "Player5";
                colorChanger = leftPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = gray;
            }
        }
            /*
            topPortal.tag = "Player2";
            rightPortal.tag = "Player1";
            botPortal.tag = "Player4";
            leftPortal.tag = "Player5";
            */
        else if (localData.playerData[localData.currentPlayer].color == "Yellow")
        {
            if (localData.numberOfPlayers == 3)
            {
                topPortal.tag = "Player2";
                colorChanger = topPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = red;
                rightPortal.tag = "Player1";
                colorChanger = rightPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = white;
                Destroy(botPortal.GetComponent<Collider2D>());
                Destroy(botPortal.GetComponent<SpriteRenderer>());
                Destroy(leftPortal.GetComponent<Collider2D>());
                Destroy(leftPortal.GetComponent<SpriteRenderer>());
            }
            if (localData.numberOfPlayers == 4)
            {
                topPortal.tag = "Player2";
                colorChanger = topPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = red;
                rightPortal.tag = "Player1";
                colorChanger = rightPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = white;
                botPortal.tag = "Player4";
                colorChanger = botPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = blue;
                Destroy(leftPortal.GetComponent<Collider2D>());
                Destroy(leftPortal.GetComponent<SpriteRenderer>());

            }
            if (localData.numberOfPlayers == 5)
            {
                topPortal.tag = "Player2";
                colorChanger = topPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = red;
                rightPortal.tag = "Player1";
                colorChanger = rightPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = white;
                botPortal.tag = "Player4";
                colorChanger = botPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = blue;
                leftPortal.tag = "Player5";
                colorChanger = leftPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = gray;
            }
        }
            /*
            topPortal.tag = "Player2";
            rightPortal.tag = "Player3";
            botPortal.tag = "Player1";
            leftPortal.tag = "Player5";
            */
        else if (localData.playerData[localData.currentPlayer].color == "Blue")
        {
            if (localData.numberOfPlayers == 4)
            {
                topPortal.tag = "Player2";
                colorChanger = topPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = red;
                rightPortal.tag = "Player3";
                colorChanger = rightPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = yellow;
                botPortal.tag = "Player1";
                colorChanger = botPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = white;
                Destroy(leftPortal.GetComponent<Collider2D>());
                Destroy(leftPortal.GetComponent<SpriteRenderer>());

            }
            if (localData.numberOfPlayers == 5)
            {
                topPortal.tag = "Player2";
                colorChanger = topPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = red;
                rightPortal.tag = "Player3";
                colorChanger = rightPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = yellow;
                botPortal.tag = "Player1";
                colorChanger = botPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = white;
                leftPortal.tag = "Player5";
                colorChanger = leftPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = gray;
            }
        }
            /*
            topPortal.tag = "Player2";
            rightPortal.tag = "Player3";
            botPortal.tag = "Player4";
            leftPortal.tag = "Player1";
            */
        else if (localData.playerData[localData.currentPlayer].color == "Gray")
        {
            if (localData.numberOfPlayers == 5)
            {
                topPortal.tag = "Player2";
                colorChanger = topPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = red;
                rightPortal.tag = "Player3";
                colorChanger = rightPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = yellow;
                botPortal.tag = "Player4";
                colorChanger = botPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = blue;
                leftPortal.tag = "Player1";
                colorChanger = leftPortal.GetComponent<SpriteRenderer>();
                colorChanger.color = white;
            }
        }
    }


    //place object back on to the scene
    //color them the corresponding color (if white player is up his leftovers are HIS color)
    //clear the list to start from zero
    void sceneSetup()
    {
        Vector2 location = new Vector2(Random.Range(leftWall.transform.position.x + spawnLocationBuffer, rightWall.transform.position.x - spawnLocationBuffer),
                                           Random.Range(topWall.transform.position.y - spawnLocationBuffer, bottomWall.transform.position.y + spawnLocationBuffer));
        if (localData.playerData[localData.currentPlayer].color == "White")
        {
            for (int i = 0; i < localData.playerData[localData.currentPlayer].currentSceneObjects.Count; i++)
            {
                GameObject node = Instantiate(localData.playerData[localData.currentPlayer].currentSceneObjects[i], location, Quaternion.identity) as GameObject;
                colorChanger = node.GetComponent<SpriteRenderer>();
                colorChanger.color = white;
            }
        }
        else if (localData.playerData[localData.currentPlayer].color == "Red")
        {
            for (int i = 0; i < localData.playerData[localData.currentPlayer].currentSceneObjects.Count; i++)
            {
                GameObject node = Instantiate(localData.playerData[localData.currentPlayer].currentSceneObjects[i], location, Quaternion.identity) as GameObject;
                colorChanger = node.GetComponent<SpriteRenderer>();
                colorChanger.color = red;
            }
        }
        else if (localData.playerData[localData.currentPlayer].color == "Yellow")
        {
            for (int i = 0; i < localData.playerData[localData.currentPlayer].currentSceneObjects.Count; i++)
            {
                GameObject node = Instantiate(localData.playerData[localData.currentPlayer].currentSceneObjects[i], location, Quaternion.identity) as GameObject;
                colorChanger = node.GetComponent<SpriteRenderer>();
                colorChanger.color = yellow;
            }
        }
        else if (localData.playerData[localData.currentPlayer].color == "Blue")
        {
            for (int i = 0; i < localData.playerData[localData.currentPlayer].currentSceneObjects.Count; i++)
            {
                GameObject node = Instantiate(localData.playerData[localData.currentPlayer].currentSceneObjects[i], location, Quaternion.identity) as GameObject;
                colorChanger = node.GetComponent<SpriteRenderer>();
                colorChanger.color = blue;
            }
        }
        else if (localData.playerData[localData.currentPlayer].color == "Gray")
        {
            for (int i = 0; i < localData.playerData[localData.currentPlayer].currentSceneObjects.Count; i++)
            {
                GameObject node = Instantiate(localData.playerData[localData.currentPlayer].currentSceneObjects[i], location, Quaternion.identity) as GameObject;
                colorChanger = node.GetComponent<SpriteRenderer>();
                colorChanger.color = gray;
            }
        }
        localData.playerData[localData.currentPlayer].currentSceneObjects.Clear();
    }
}
