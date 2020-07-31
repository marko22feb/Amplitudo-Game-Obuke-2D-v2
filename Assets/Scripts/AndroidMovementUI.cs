using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AndroidMovementUI : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    Movement move;
    public float direction;
    private bool IsHoldingDown;
    public bool isVertical = false;
    public bool Jump = false;
    public bool Crouch = false;

    void Awake()
    {
        move = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();   
    }

    private void Start()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (Jump || Crouch) return;
        if (IsHoldingDown)
        {
            if (!isVertical)
                move.Move(direction);
            else move.MoveVertical(direction);
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (Jump)
        {
            move.Jump(false);
        }
        else if (Crouch)
        {
            move.Crouch();
        }
        else
        {
            IsHoldingDown = true;
            move.isUsingUItoMove = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Jump) return;
        if (Crouch) move.anim.SetBool("IsCrouching", false);
        else
        {
            LetGoOfButton();
        }
    }

    public void LetGoOfButton()
    {
        IsHoldingDown = false;
        move.isUsingUItoMove = false;
        move.Move(0f);
    }
}
