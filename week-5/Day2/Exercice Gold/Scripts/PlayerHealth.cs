using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public Slider healthBar;
    int health;

    void Start()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    public void Damage(int amount)
    {
        health -= amount;
        healthBar.value = health;
        if (health <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Heal(int amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
        healthBar.value = health;
    }
}
