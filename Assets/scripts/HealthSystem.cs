using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    public float lowHealth;
    public Image bloodImage;

    public void TakeDamage(float damage)
    {
        if(damage > 0)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Destroy(gameObject);
                ChangeBloodImageAlpha(1f);
            }

            ChangeBloodImageAlpha(0.8f);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void ChangeBloodImageAlpha(float alpha)
    {
        if (bloodImage != null)
        {
            var color = bloodImage.color;
            color.a = alpha;

            bloodImage.color = color;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentHealth);

        if (bloodImage != null)
        {
            var color = bloodImage.color;
            
            if(currentHealth <= lowHealth)
            {
                color.a = 0.15f;
            }
            else
            {
                color.a -= 0.01f;
            }


            bloodImage.color = color;
        }
    }
}
