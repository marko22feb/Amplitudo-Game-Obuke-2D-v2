using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatComponent : MonoBehaviour
{
    public Slider HPSlider;
    private Animator anim;
    private Movement move;

    public float currentHealth;
    public float minimumHealth;
    public float maximumHealth;

    public float minimumDamage;
    public float maximumDamage;

    public float damageMultiplayer;
    public float defenseMultiplayer;

    private bool CanBeDamaged = true;

    void Awake()
    {
        HPSlider = GameObject.Find("HPSlider").GetComponent<Slider>();
        anim = GetComponent<Animator>();
        move = GetComponent<Movement>();
        UpdateSlider();
    }

    void UpdateSlider()
    {
        HPSlider.minValue = minimumHealth;
        HPSlider.maxValue = maximumHealth;
        HPSlider.value = currentHealth;
    }

    public void ModifyHealthBy(float value, float valueMultiplayer)
    {
        if (value < 0)
        {
            if (!CanBeDamaged) return;
            value = value * defenseMultiplayer;
            StartCoroutine(iFrames(2));
            anim.SetTrigger("GotHurt");
            float currentJumpHeight = move.JumpHeight;
            move.JumpHeight = move.JumpHeight * 1.4f;
            move.Jump(true);
            move.JumpHeight = currentJumpHeight;
        }

        currentHealth = currentHealth + value * valueMultiplayer;

        if (currentHealth < minimumHealth) OnDeath();
        if (currentHealth > maximumHealth) currentHealth = maximumHealth;

        UpdateSlider();
    }

    void OnDeath()
    {

    }

    IEnumerator iFrames (float timeAmount)
    {
        CanBeDamaged = false;
        Physics2D.IgnoreLayerCollision(14, 15, true);
        yield return new WaitForSeconds(timeAmount);
        CanBeDamaged = true;
        Physics2D.IgnoreLayerCollision(14, 15, false);

        StopCoroutine(iFrames(1f));
    }
}
