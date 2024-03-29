﻿using UnityEngine;
using System.Collections;

public class PositionCollisionPlayer4 : MonoBehaviour
{
    private GameObject dataObject;
    private LocalMultiplayerGameData localData;
    public GameObject confetti, happyBunny, sadBunny;

    // Use this for initialization
    void Start()
    {
        dataObject = GameObject.Find("LocalMultiplayerGameData");
        localData = dataObject.GetComponent<LocalMultiplayerGameData>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (localData.playerData[localData.getPreviousPlayer()].color == "Blue")
        {
            //make sure score increased
            if (localData.playerData[localData.getPreviousPlayer()].preTurnScore < localData.playerData[localData.getPreviousPlayer()].score)
            {
                GameObject.Find("Camera").GetComponent<SoundManager>().PlaySound(0);
                GameObject node = Instantiate(confetti, gameObject.transform.position, Quaternion.identity) as GameObject;
                GameObject happy = Instantiate(happyBunny, gameObject.transform.position, Quaternion.identity) as GameObject;
            }
            else if (localData.playerData[localData.getPreviousPlayer()].preTurnScore > localData.playerData[localData.getPreviousPlayer()].score)
            {
                GameObject.Find("Camera").GetComponent<SoundManager>().PlaySound(1);
                GameObject node = Instantiate(sadBunny, gameObject.transform.position, Quaternion.identity) as GameObject;
            }
        }
    }

}
