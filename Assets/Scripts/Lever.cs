using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Door door;
    public Sprite S_active;
    public Sprite S_inactive;
    private SpriteRenderer sr;
    public bool Active = false;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateSprite();
    }

    public void TriggerLever()
    {
        Active = !Active;

        if (door != null)
        {
            door.Locked = !door.Locked;
        }

        UpdateSprite();
    }

    public void UpdateSprite()
    {
        if (Active)
        {
            sr.sprite = S_active;
        }
        else
        {
            sr.sprite = S_inactive;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerInputManager pim = collision.GetComponent<PlayerInputManager>();
            pim.door = door;
            pim.IsNearLever = true;
            pim.lever = this;
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerInputManager pim = collision.GetComponent<PlayerInputManager>();
            pim.door = null;
            pim.IsNearLever = false;
            pim.lever = null;
               
        }
    }
}
