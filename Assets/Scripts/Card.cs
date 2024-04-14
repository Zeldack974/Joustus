using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Card : MonoBehaviour
{
    public int playerOwer;
    public int abstractId;
    public Material material;
    public Canvas canvas;
    [HideInInspector]
    public Material materialInstance;



    [HideInInspector]
    public CardGraphics graphics;
    public AbstractCard AbstractCard => Game.instance.playerCards[playerOwer][abstractId];

    void Awake()
    {
        this.materialInstance = (Material)Instantiate(this.material);
        foreach (SpriteRenderer renderer in this.gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.material = this.materialInstance;
        }
        //spriteRenderer.material = this.materialInstance;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"start card {playerOwer} {abstractId}");
        graphics = new CardGraphics(this);
        graphics.ApplyColor();
        graphics.UpdateArrows();
        graphics.UpdateImage();
        graphics.UpdateAbility();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.one * Game.instance.canvas.transform.localScale.magnitude * 175;
    }
}
