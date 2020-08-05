using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatComponent : MonoBehaviour
{
    public Slider HPSlider;
    private Animator anim;
    private Movement move;
    private EnemyMovement Emove;
    private SpriteRenderer sr;

    private Canvas MainHUD;
    private Canvas DeathScreen;

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
        MainHUD = GameObject.Find("MainHUD").GetComponent<Canvas>();
        DeathScreen = GameObject.Find("DeathScreen").GetComponent<Canvas>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        move = GetComponent<Movement>();
        Emove = GetComponent<EnemyMovement>();
        UpdateSlider();
    }

    void UpdateSlider()
    {
        HPSlider.minValue = minimumHealth;
        HPSlider.maxValue = maximumHealth;
        HPSlider.value = currentHealth;
    }

    public void ModifyHealthBy(float value, float valueMultiplayer, bool IgnoreIframes)
    {
        if (value < 0)
        {
            if (!IgnoreIframes)
            {
                if (!CanBeDamaged) return;
            }

            value = value * defenseMultiplayer;
            if (value == 0) return;

            StartCoroutine(iFrames(2));
            anim.SetTrigger("GotHurt");
            if (Emove != null)
                Emove.IsStunned = true;

            if (move != null)
            {
                float currentJumpHeight = move.JumpHeight;
                move.JumpHeight = move.JumpHeight * 1.4f;
                move.Jump(true);
                move.JumpHeight = currentJumpHeight;
            } else
            {
                //FixLater
            }

            OnDeath();
        }

        currentHealth = currentHealth + value * valueMultiplayer;

        if (currentHealth < minimumHealth) OnDeath();
        if (currentHealth > maximumHealth) currentHealth = maximumHealth;

        if (Emove != null)
            anim.SetFloat("CurrentHP", currentHealth);

        if (move != null)
        UpdateSlider();
    }

    void OnDeath()
    {
        if (currentHealth > minimumHealth) return;

        if (move == null)
        {
            if (Emove != null)
                Emove.IsStunned = true;
            StartCoroutine(DelayDestroySelf(2f));
        }
        else
        {
            Time.timeScale = 0f;
            MainHUD.enabled = false;
            DeathScreen.enabled = true;
        }
    }

    IEnumerator DelayDestroySelf (float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(gameObject);
    }

    IEnumerator iFrames (float timeAmount)
    {
        CanBeDamaged = false;
        Physics2D.IgnoreLayerCollision(14, 15, true);
        yield return new WaitForSeconds(timeAmount);
        if (currentHealth > minimumHealth)
        {
            CanBeDamaged = true;
            Physics2D.IgnoreLayerCollision(14, 15, false);
            anim.SetTrigger("HurtOver");

            if (Emove != null)
                Emove.IsStunned = false;
        }

        StopCoroutine(iFrames(1f));
        
    }

    public IEnumerator PowerUp(float duration)
    {
        float currentDamageMultiplayer = damageMultiplayer;
        float currentDefenseMultiplayer = defenseMultiplayer;
        Color currentColor = sr.color;

        sr.color = Color.blue;
        damageMultiplayer = damageMultiplayer * 2f;
        defenseMultiplayer = 0f;
        yield return new WaitForSeconds(duration);
        damageMultiplayer = currentDamageMultiplayer;
        defenseMultiplayer = currentDefenseMultiplayer;
        sr.color = currentColor;
    }
}
