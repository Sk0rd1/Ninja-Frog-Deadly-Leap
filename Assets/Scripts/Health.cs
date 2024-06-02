using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private int health;
    private int coin;
    private int maxHearts;
    private Sprite[] sprites = new Sprite[3];

    private bool isDodgeTime = false;
    private float dodgeTime = 0.3f;

    [SerializeField]
    private Image[] hearts = new Image[5];
    [SerializeField]
    private SpriteRenderer srPlayer;
    [SerializeField]
    private AudioSource coinSound;
    [SerializeField]
    private AudioSource heartSound;
    [SerializeField]
    private AudioSource damageSound;

    private Material hitBlind;
    private Material defaultMaterial;

    void Start()
    {
        sprites[0] = Resources.Load<Sprite>("Hearts/HealthUI_0");
        sprites[1] = Resources.Load<Sprite>("Hearts/HealthUI_1");
        sprites[2] = Resources.Load<Sprite>("Hearts/HealthUI_2");

        defaultMaterial = srPlayer.material;
        hitBlind = Resources.Load<Material>("Materials\\HitBlind");

        SetHealth();

        HealthEventSystem.instance.OnDamage += HandleDamage;
        HealthEventSystem.instance.OnHeal += HandleHeal;
        HealthEventSystem.instance.OnCoin += HandleCoin;

        EnableHearts();
        DrawSprites();
    }

    public void SetHealth()
    {
        maxHearts = 2 + Save.GetLvlHearts();
        health = 2 * maxHearts;
        coin = 0;
    }

    private void OnEnable()
    {
        isDodgeTime = false;
        SetHealth();
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

    private void HandleCoin(int coinAmount)
    {
        OnCoin(coinAmount);
    }

    public void OnDamage(int damageAmount)
    {
        if (!isDodgeTime)
        {
            StartCoroutine(DodgeTime());
            health -= damageAmount;

            damageSound.Play();

            DrawSprites();
            if (health <= 0)
                Death();

            if (health > 0)
                StartCoroutine(HitBlind());

        }
    }

    private IEnumerator HitBlind()
    {

        srPlayer.material = hitBlind;
        yield return new WaitForSeconds(0.2f);
        srPlayer.material = defaultMaterial;
    }

    private IEnumerator DodgeTime()
    {
        isDodgeTime = true;
        yield return new WaitForSeconds(dodgeTime);
        isDodgeTime = false;
    }

    public void OnHeal(int healAmount)
    {
        if (health < maxHearts)
        {
            health += healAmount;
            heartSound.Play();
            DrawSprites();
        }
    }

    public void OnCoin(int coinAmount)
    {
        coin += coinAmount;
        coinSound.Play();
    }

    private void EnableHearts()
    {
        for(int i = 0; i < 5; i++)
        {
            if(i < maxHearts)
            {
                hearts[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                hearts[i].GetComponent<Image>().enabled = false;
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
        StopCoroutine(HitBlind());
        GameObject.Find("HUD").GetComponent<HUDEngine>().EndGame(coin);
    }
}
