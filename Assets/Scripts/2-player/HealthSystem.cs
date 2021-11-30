using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    //simple healthSystem to keep track of player hp and damage taken.
    //to restart the level
    [SerializeField] WinAndPass winSystem;
    [SerializeField] float timeBetweenFlashes=0.1f;

    
    [SerializeField]private int health;
     int maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 3;
        health = maxHealth;
    }

    //to set health to max once a new level starts, or otherwise heal if stated;
    public void addHealth(int heal = 0,bool max = false)
    {
        if (max)
        {
            health = maxHealth;
        }
        else
        { 
            health = health+heal>maxHealth ? maxHealth : health+heal;
        }
    }
    //take damage if hit an enemy
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            takeDamage(1);
            Debug.Log($"Current healh: {health}");
        }
    }

    private void takeDamage(int damage)
    {
        health -= damage;
        if (health<= 0)
        { //initiate game over if dead(level restart)
            Gameover();
            health = maxHealth;
            return;
        }
       
            StartCoroutine(damageTakenFlash());
        
    }

      IEnumerator damageTakenFlash()
    {
        var renderer = GetComponent<SpriteRenderer>();

        for (int i = 0; i < 3; i++)
        {
            renderer.enabled = false;
            yield return new WaitForSeconds(timeBetweenFlashes);
            renderer.enabled = true;
            yield return new WaitForSeconds(timeBetweenFlashes);
        }
        
    }

    private void Gameover()
    {
        //restart level
        winSystem.Gameover();
        //reset position
        transform.position = WinAndPass.positions["bottom left"];
    }
}
