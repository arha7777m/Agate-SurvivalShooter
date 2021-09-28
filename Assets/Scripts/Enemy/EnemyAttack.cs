using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;


    void Awake ()
    {
        //cari player
        player = GameObject.FindGameObjectWithTag ("Player");

        //ambil komponen health si player
        playerHealth = player.GetComponent <PlayerHealth> ();

        //ambil komponen health enemy
        enemyHealth = GetComponent<EnemyHealth>();

        //ambil komponen animator
        anim = GetComponent <Animator> ();
    }

    void OnTriggerEnter (Collider other)
    {
        //set player dalam range
        if(other.gameObject == player && other.isTrigger == false)
        {
            playerInRange = true;
        }
    }

    //callback jika ada objek keluar dari trigger
    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange 
            && enemyHealth.currentHealth > 0)
        {
            Attack ();
        }

        //trigger animasi dead klo darah player habis
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
        }
    }

    void Attack ()
    {
        //reset timer
        timer = 0f;

        //kalo player masih ada darah, take damage
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
