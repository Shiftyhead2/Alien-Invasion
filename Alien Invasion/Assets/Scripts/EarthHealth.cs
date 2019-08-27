using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthHealth : MonoBehaviour
{
    public float Health;
    public Slider HealthSlider;

    private void Start()
    {
        HealthSlider.maxValue = Health;
    }


    // Update is called once per frame
    void Update()
    {
        HealthSlider.value = Health;
        if(Health <= 0f)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
