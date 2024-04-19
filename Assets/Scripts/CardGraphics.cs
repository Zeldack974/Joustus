using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.U2D;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CardGraphics
{
    public Card card;
    public bool isBomb;
    public CardGraphics(Card card) {
        this.card = card;
    }

    public void ApplyColor()
    {
        if (card.playerOwner == 0)
        {
            card.materialInstance.SetFloat("_Red", 0f);
        }
    }

    public void UpdateImage()
    {
        //card.transform.Find("Image").GameObject().GetComponent<SpriteRenderer>().sprite = SpritesReferences.instance.CardsAtlas.GetSprite(card.AbstractCard.type.sprite);
        //Debug.Log(card.AbstractCard.type.sprite);
        card.transform.Find("Image").GameObject().GetComponent<SpriteRenderer>().sprite = SpritesReferences.GetCardSprite(card.AbstractCard.type.sprite);
        if (card.playerOwner == 1)
        {
            Vector3 scale = card.transform.Find("Image").localScale;
            scale.x *= -1;
            card.transform.Find("Image").localScale = scale;
            Debug.Log("change x");
        }
    }

    public void UpdateAbility()
    {
        if (card.AbstractCard.type.ability != null)
        {
            card.transform.Find("AbilityBackground").GameObject().SetActive(true);

            GameObject abilityObj = card.transform.Find("AbilityBackground").Find("Ability").GameObject();
            abilityObj.SetActive(true);
            SpriteRenderer renderer = abilityObj.GetComponent<SpriteRenderer>();
            SpriteAtlas atlas = card.AbstractCard.abilityActive ? SpritesReferences.instance.AbilitiesAtlas : SpritesReferences.instance.AbilitiesDisabledAtlas;
            switch (card.AbstractCard.type.ability.ToLower())
            {
                case "cascade":
                    renderer.sprite = atlas.GetSprite((card.AbstractCard.abilityActive ? "abilities_" : "disabled_abilities_") + "0"); 
                    break;
                case "grave":
                    renderer.sprite = atlas.GetSprite((card.AbstractCard.abilityActive ? "abilities_" : "disabled_abilities_") + "1");
                    break;
                case "slam":
                    renderer.sprite = atlas.GetSprite((card.AbstractCard.abilityActive ? "abilities_" : "disabled_abilities_") + "2");
                    break;
                case "switch":
                    switch (card.AbstractCard.type.switchDir)
                    {
                        case "double":
                            renderer.sprite = atlas.GetSprite((card.AbstractCard.abilityActive ? "abilities_" : "disabled_abilities_") + "3");
                            break;
                        case "horizontal":
                            renderer.sprite = atlas.GetSprite((card.AbstractCard.abilityActive ? "abilities_" : "disabled_abilities_") + "4");
                            break;
                        case "vertical":
                            renderer.sprite = atlas.GetSprite((card.AbstractCard.abilityActive ? "abilities_" : "disabled_abilities_") + "5");
                            break;
                    }
                    break;
            }

        }
        else
        {
            card.transform.Find("AbilityBackground").GameObject().SetActive(false);
            card.transform.Find("AbilityBackground").Find("Ability").GameObject().SetActive(false);
        }
    }

    public void UpdateArrows()
    {
        string[] dirs = new string[4] { "Up", "Left", "Right", "Down" };
        for (int i = 0; i < 4; i++)
        {
            GameObject obj = card.transform.Find("Arrows").Find(dirs[i]).GameObject();
            SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();

            obj.SetActive(true);
            switch (card.AbstractCard.arrows[i].type)
            {
                case "none":
                    obj.SetActive(false);
                    break;
                case "simple":
                    renderer.sprite = SpritesReferences.GetSprite("arrow_simple");
                    break;
                case "double":
                    renderer.sprite = SpritesReferences.GetSprite("arrow-double");
                    break;
                case "bomb":
                    renderer.sprite = SpritesReferences.GetSprite("arrow_bomb");
                    isBomb = true;
                    break;

            }
            //renderer.sprite = 
        }
    }
}
