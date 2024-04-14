using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    public int playersCardsAmount = 16;
    public GameObject cardObject;
    public Canvas canvas;

    public static Game instance;

    public List<AbstractCard>[] playerCards = new List<AbstractCard>[2];

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Debug.Log("start game");
        instance = this;
        InitializePlayersCards();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject newGameObject = (GameObject)Instantiate(cardObject, Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 1), Quaternion.identity);
            newGameObject.GetComponent<Card>().playerOwer = Random.Range(0, 1 + 1);
            newGameObject.GetComponent<Card>().abstractId = Random.Range(0, playerCards[newGameObject.GetComponent<Card>().playerOwer].Count);
            //newGameObject.transform.SetParent(canvas.transform, false);
        }
    }

    public void InitializePlayersCards()
    {
        playerCards[0] = new List<AbstractCard>();
        playerCards[1] = new List<AbstractCard>();

        List<AbstractCard.Type> usedCard = new List<AbstractCard.Type>();

        for (int i = 0; i < playersCardsAmount; i++)
        {
            AbstractCard.Type type = AbstractCard.Type.All[Random.Range(0, AbstractCard.Type.All.Count)];
            playerCards[0].Add(new AbstractCard(type, 0, i));
            playerCards[1].Add(new AbstractCard(type, 1, i));
            //Debug.Log(type);
        }

        playerCards[0].Shuffle();
        playerCards[1].Shuffle();
    }

}
