using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float HealthPoints;
    public Slider HealthBar;

    private void Start()
    {
        HealthBar.maxValue = HealthPoints;
        

    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.value = HealthPoints;
        if(HealthPoints <= 0f)
        {
            Die();
        }
        
    }

    public void TakeDamage(float damage)
    {
        HealthPoints -= damage;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
