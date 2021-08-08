using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController instance;
    // Start is called before the first frame update
    public int currentHealth;
    public int maxHealth;
    public float damageInvinceLength = 1f;
    private float invinceCount;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentHealth = maxHealth;
        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(invinceCount > 0)
        {
            invinceCount -= Time.deltaTime;

            if(invinceCount <= 0)
            {
                PlayerController.instance.bodySr.color = new Color(PlayerController.instance.bodySr.color.r, PlayerController.instance.bodySr.color.g, PlayerController.instance.bodySr.color.b, 1f);
            }
        }
    }

    public void DamagePlayer()
    {

        if(invinceCount  <= 0)
        {
            currentHealth--;

            invinceCount = damageInvinceLength;

            PlayerController.instance.bodySr.color = new Color(PlayerController.instance.bodySr.color.r, PlayerController.instance.bodySr.color.g, PlayerController.instance.bodySr.color.b, 0.5f);

            if (currentHealth <= 0)
            {
                PlayerController.instance.gameObject.SetActive(false);

                UIController.instance.deathScreen.SetActive(true);
            }

            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
        }
    }

    public void MakeInvincible(float length)
    {
        invinceCount = length;
        PlayerController.instance.bodySr.color = new Color(PlayerController.instance.bodySr.color.r, PlayerController.instance.bodySr.color.g, PlayerController.instance.bodySr.color.b, 0.5f);
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

}
