﻿using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;

    void Awake ()
    {
        //ambil komponen"
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
    }

    void Update ()
    {
        //kalo sinking, object pindah kebawah
        if (isSinking)
        {
            transform.Translate (sinkSpeed * Time.deltaTime * -Vector3.up);
        }
    }

    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if (isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death ();
        }
    }

    void Death ()
    {
        isDead = true;

        BuffPlayer();
        capsuleCollider.isTrigger = true;

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }

    public void StartSinking ()
    {
        //disable komponen navmesh
        GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = false;

        //ubah rb ke kinematic
        GetComponent<Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }

    //Menambah kuat player, baik dari attack, speed, atau health
    void BuffPlayer()
    {
        float random = Random.value;
        if (random > 0.75) //rate 25%
        {
            Debug.Log("Attack player meningkat sebesar 20 dan recoil nya mengecil");
            PlayerShooting.Instance.IncreaseShooting(20, 0.01f);
        }
        if (random > 0.9) //rate 10%
        {
            PlayerHealth.Instance.AddHealth(5);
        }
        if(random > 0.95) //rate 5%
        {
            Debug.Log("Kecepatan player bertambah sebanyak 1");
            PlayerMovement.Instance.AddSpeed(1);
        }
    }
}
