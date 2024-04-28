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
    public int handSlot;
    public int state = 0;
    public Material material;
    public Canvas canvas;
    [HideInInspector] public Material materialInstance;
    public int animationState = 1;
    public float scale;
    [HideInInspector] public Animator animator;

    [HideInInspector] public CardGraphics graphics;

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
        animator = GetComponent<Animator>();
        graphics = new CardGraphics(this);
        graphics.ShowDetails(false);
    }

    // Update is called once per frame
    void Update()
    {
        scale = Game.instance.canvas.transform.localScale.magnitude;
        transform.localScale = Vector3.one * scale * 175;
        if (state == States.inHand)
        {
            Vector3 newPos = Game.instance.GetPlayerCanvasTransform(playerOwner).Find("CardSlots").Find(handSlot.ToString()).position;
            newPos.z = transform.position.z;
            Debug.Log(animationState);
            if (animationState == 1)
            { 
                transform.position = transform.position + (newPos - transform.position).normalized * Mathf.Min(Time.deltaTime * scale * 200f, 1);
                if ((newPos - transform.position).magnitude < 0.05)
                {
                    animationState = 2;
                    animator.SetInteger("AnimationState", animationState);
                    Debug.Log($"card {AbstractCard.id} change state to " + animationState);
                }
            }
            else
            {
                transform.position = newPos;
            }
        }
    }

    public void FinishPlaceAnimation()
    {
        animationState = 0;
        graphics.ShowDetails(true);
    }


    public static class States
    {
        public static int inHand = 0;
        public static int grabbed = 1;
        public static int onBoard = 2;
    }
}
