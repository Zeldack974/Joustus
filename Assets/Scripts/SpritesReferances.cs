using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpritesReferances : MonoBehaviour
{
    public SpriteAtlas CardsAtlas;
    public Sprite simpleArrow;
    public Sprite doubleArrow;
    public Sprite bombArrow;
    public Sprite bombArrowBomb;
    public SpriteAtlas AbilitiesAtlas;
    public SpriteAtlas AbilitiesDisabledAtlas;
    public SpriteAtlas DeckAtlas;


    public static SpritesReferances instance;

    private void Awake()
    {
        instance = this;
    }
}
