using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Attributes")]
    
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GUIManager GUI;
    
    //private readonly System.Random rnd = new System.Random();
    private int maxHealth = 100;
    private AudioSource source;

    public bool isDead { get; set; } = false;
    
    public int currentHealth { get; set; }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) TakeDamage(10); //for example
    }

    private void Start()
    {
        currentHealth = maxHealth;
        GUI.SetMaxHealth(currentHealth);
        GUI.SetHealth(currentHealth);
    }
    
    internal void TakeDamage(int damage)
    {
        currentHealth -= damage;
        GUI.SetHealth(currentHealth);
        
        //if (currentHealth <= 0) Die();
    }
    
    internal void TakeHealing(int health)
    {
        if (currentHealth + health <= maxHealth) currentHealth += health;
        else currentHealth = maxHealth;
        
        GUI.SetHealth(currentHealth);
    }
    
    private void Die()
    {
        //SaveSystem.DeleteData();
        isDead = true;
        GetComponent<PolygonCollider2D>().enabled = false;
        deathScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
