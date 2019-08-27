using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float HealthPoints;
    public Slider HealthSlider;
    private bool DropKits;
    public Transform DropItem;

    private void Start()
    {
        HealthSlider.maxValue = HealthPoints;
        DropKits = (Random.value > 0.5f);
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthSlider.value = HealthPoints;
        if (HealthPoints <= 0f)
        {
            Die();
            
        }
        
    }

   public void TakeDamage(float damage, Transform damager)
    {
        HealthPoints -= damage;
        GetComponent<enemyAI>().Target = damager;
       
    }

    void Die()
    {
        if (DropKits)
        {
            Instantiate(DropItem,transform.position,Quaternion.identity);
        }
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
