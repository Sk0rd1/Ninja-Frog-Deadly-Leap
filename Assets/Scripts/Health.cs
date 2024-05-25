using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int health;
    private int maxHearths;
    private Sprite[] sprites = new Sprite[3];

    private bool isDodgeTime = false;
    private float dodgeTime = 0.3f;

    [SerializeField]
    private SpriteRenderer[] hearts = new SpriteRenderer[5];

    void Start()
    {
        sprites[0] = Resources.Load<Sprite>("Hearts/HealthUI_0");
        sprites[1] = Resources.Load<Sprite>("Hearts/HealthUI_1");
        sprites[2] = Resources.Load<Sprite>("Hearts/HealthUI_2");

        //тут повинно взяти хп з сейва
        maxHearths = 3;
        health = 2 * maxHearths;

        HealthEventSystem.instance.OnDamage += HandleDamage;
        HealthEventSystem.instance.OnHeal += HandleHeal;

        EnableHearts();
        DrawSprites();
    }

    private void HandleDamage(int damageAmount)
    {
        OnDamage(damageAmount);
    }

    private void HandleHeal(int healAmount)
    {
        OnHeal(healAmount);
    }

    public void OnDamage(int damageAmount)
    {
        if (!isDodgeTime)
        {
            StartCoroutine(DodgeTime());
            health -= damageAmount;
            DrawSprites();
            if (health <= 0)
                Death();
        }
    }

    private IEnumerator DodgeTime()
    {
        isDodgeTime = true;
        yield return new WaitForSeconds(dodgeTime);
        isDodgeTime = false;
    }

    public void OnHeal(int healAmount)
    {
        health += healAmount;
        Debug.Log(health);
    }

    private void EnableHearts()
    {
        for(int i = 0; i < 5; i++)
        {
            if(i < maxHearths)
            {
                hearts[i].gameObject.SetActive(true);
            }
            else
            {
                hearts[i].gameObject.SetActive(false);
            }
        }
    }

    private void DrawSprites()
    {
        int currentHealth = health;

        for (int i = 0; i < 5; i++)
        {
            if (currentHealth > (1 + i * 2))
                hearts[i].sprite = sprites[0];
            else if (health > (i * 2))
                hearts[i].sprite = sprites[1];
            else
                hearts[i].sprite = sprites[2];
        }
    }

    private void Death()
    {
        Debug.Log("Death");
    }
}
