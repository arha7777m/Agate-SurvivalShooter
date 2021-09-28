using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    float timer;

    [SerializeField] MonoBehaviour factory;
    IFactory Factory { get { return factory as IFactory; } }

    void Spawn ()
    {
        //kalo player mati, stop buat enemy
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        //buat nilai random
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);
        int spawnEnemy = Random.Range(0, 3);

        //duplikat enemy
        Factory.FactoryMethod(spawnEnemy, spawnPoints[spawnPointIndex]);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTime)
        {
            Spawn();
            spawnTime *= 0.99f;
            timer = 0;
        }
    }
}
