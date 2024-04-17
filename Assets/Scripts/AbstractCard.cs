using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class AbstractCard
{
    public Type type;
    public Arrow[] arrows;
    public int playerOwner;
    public int id;
    public bool abilityActive;
    public bool destroyed;
    public Card card;

    public AbstractCard(Type type, int playerOwner, int id)
    {
        this.type = type;
        arrows = type.arrows.ToList().ConvertAll(s => new Arrow(s)).ToArray();
        if (playerOwner == 1)
        {
            Arrow temp = arrows[1];
            arrows[1] = arrows[2];
            arrows[2] = temp;

        }
        this.playerOwner = playerOwner;
        this.id = id;
    }

    public Card CreateCard(int handPos)
    {
        if (card != null) return card;
        GameObject newGameObject = (GameObject)Object.Instantiate(Game.instance.cardObject, Game.instance.GetPlayerCanvasTransform(playerOwner).Find("Deck").position + new Vector3(0, 0, 1), Quaternion.identity);
        card = newGameObject.GetComponent<Card>();
        card.playerOwner = playerOwner;
        card.abstractId = id;
        card.handPos = handPos;
        return card;
    }

    public bool IsCardInDeck()
    {
        return !destroyed && card == null;
    }


    public class Arrow
    {
        public string type;
        public static Dictionary<string, int> Powers
        {
            get
            {
                Dictionary<string, int> powers = new();
                powers.Add("none", 0);
                powers.Add("simple", 1);
                powers.Add("bomb", 1);
                powers.Add("double", 2);
                return powers;
            }
        }

        public int Power
        {
            get => Powers[type];
        }

        public Arrow(string type)
        {
            this.type = type;
        }
    }

    public class Type
    {
        public string name;
        public string sprite;
        public string ability;
        public string switchDir;
        public string[] arrows = new string[4] { "none", "none", "none", "none" };


        public Type(bool register = false) //string name, string sprite, string ability, string switchDir, string[] arrows
        {
            //this.name = name;
            //this.sprite = sprite;
            //this.ability = ability;
            //this.switchDir = switchDir;
            //this.arrows = arrows;
            if (register) All.Add(this);
        }

        public override string ToString()
        {
            return $"{name};{sprite}:{ability}-{switchDir};{string.Join("-", arrows)}";
        }

        public static List<Type> All = new List<Type>();

        public static Type testPropellerRat = new(true) { name = "Propeller Rat (test)", sprite = "card_0", arrows = new string[4] { "simple", "none", "none", "none" } };
        public static Type testCard = new(true) { name = "Amongus", sprite = "card_115", ability = "Switch", switchDir = "double", arrows = new string[4] { "none", "simple", "double", "bomb" } };
    }
}
