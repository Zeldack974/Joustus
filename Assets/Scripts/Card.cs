using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Card : MonoBehaviour
{
    public int playerOwner;
    public int abstractId;
    public int handPos;
    public int state = 0;
    public Material material;
    public Canvas canvas;
    [HideInInspector]
    public Material materialInstance;

    public bool animationFinished;


    [HideInInspector]
    public CardGraphics graphics;
    public AbstractCard AbstractCard => Game.instance.playerCards[playerOwner][abstractId];

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
        Debug.Log($"start card {playerOwner} {abstractId}");
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
        if (state == States.inHand)
        {
            Vector3 newPos = Game.instance.GetPlayerCanvasTransform(playerOwner).Find("CardSlots").Find(handPos.ToString()).position;
            newPos.z = transform.position.z;
            if (!animationFinished)
            {
                transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 5);
                if ((newPos - transform.position).magnitude < 0.01)
                {
                    animationFinished = true;
                }
            }
            else
            {
                transform.position = newPos;
            }
        }
    }


    public static class States
    {
        public static int inHand = 0;
        public static int grabbed = 1;
        public static int onBoard = 2;
    }
}
