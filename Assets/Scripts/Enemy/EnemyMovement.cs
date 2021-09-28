using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;

    private void Awake ()
    {
        //cari player dengan tag "Player"
        player = GameObject.FindGameObjectWithTag ("Player").transform;

        //ambil komponen player
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }

    void Update ()
    {
        //pindahin posisi player
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination (player.position);
        }
        else //hentikan moving
        {
            nav.enabled = false;
        }
    }
}
