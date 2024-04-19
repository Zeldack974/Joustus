using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpritesReferences : MonoBehaviour
{
    public SpriteAtlas CardsAtlas;
    public SpriteAtlas AbilitiesAtlas;
    public SpriteAtlas AbilitiesDisabledAtlas;
    public SpriteAtlas DeckAtlas;
    public Dictionary<string, Sprite> sprites = new();

    public static Sprite GetSprite(string name)
    {
        return instance.sprites[name];
    }

    public static Sprite GetCardSprite(string name)
    {
        Sprite atlasSprite = instance.CardsAtlas.GetSprite(name);

        if (atlasSprite == null)
        {
            Debug.Log("Not using atlas sprite !");
        }

        return atlasSprite != null ? atlasSprite : GetSprite(name);
    }

    public static SpritesReferences instance;

    private void Awake()
    {
        instance = this;
    }
}
