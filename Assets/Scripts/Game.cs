using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    public int playersCardsAmount = 16;
    public GameObject cardObject;
    public Canvas canvas;

    public static Game instance;
    public int turn;

    public List<AbstractCard>[] playerCards = new List<AbstractCard>[2];
    public int?[,] cardSlots = new int?[2, 3];

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (!Loader.loaded)
        {
            Debug.Log("Trying to load a scene before loading assets");
            SceneManager.LoadScene(0);
            return;
        };

        InitializePlayersCards();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
        }

        for (int i = 0; i < 2; i++)
        {
            GetPlayerCanvasTransform(i).Find("CardsCount").GameObject().GetComponent<TextMeshProUGUI>().text = $"{GetPlayerDeckCount(i)} / {playersCardsAmount}";
            GetPlayerCanvasTransform(i).Find("Deck").GameObject().GetComponent<UnityEngine.UI.Image>().sprite = SpritesReferences.instance.DeckAtlas.GetSprite("deck_" + Math.Max(Math.Min(GetPlayerDeckCount(i), 16) - 1, 0));
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
        DrawCards(0);
        DrawCards(1);
    }

    public void DrawCards(int player)
    {
        for (int i = 0; i < 3; i++)
        {
            if (cardSlots[player, i] == null)
            {
                foreach (AbstractCard abstractCard in playerCards[player])
                {
                    if (abstractCard.IsCardInDeck())
                    {
                        //System.Threading.Timer timer = new System.Threading.Timer(x => {
                        //    abstractCard.CreateCard(i);
                        //    cardSlots[player, i] = abstractCard.id;
                        //}, null, 250 * , 0);
                        abstractCard.CreateCard(i);
                        cardSlots[player, i] = abstractCard.id;
                        break;
                    }
                }
            }
        }
    }

    public Transform GetPlayerCanvasTransform(int playerId)
    {
        return canvas.transform.Find(playerId == 0 ? "Left" : "Right");
    }

    public AbstractCard IdToAbstractCard(int player, int id)
    {
        return playerCards[player][id];
    }

    public int GetPlayerDeckCount(int player)
    {
        int count = 0;
        foreach (AbstractCard abstractCard in playerCards[player])
        {
            if (abstractCard.IsCardInDeck()) count++;
        }
        return count;
    }
}
