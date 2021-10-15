using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class qaControl : MonoBehaviour
{
    [HideInInspector]
    public waveSpawner ws;

    public GameObject meleeEnemy;
    public GameObject rangedEnemy;
    public GameObject summonerEnemy;

    public GameObject QApanel;

    private void Start()
    {
        QApanel.SetActive(false);
        ws = GameObject.FindObjectOfType<waveSpawner>();  //only one object has wave spawner script attached to it
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.P))
        {
            //puase the game
            Time.timeScale = 0;
        }
        if(Input.GetKeyDown(KeyCode.U))
        {
            //unpause the game
            Time.timeScale = 1;
        }

        if(Input.GetKeyDown(KeyCode.Backslash))
        {
            PlayerScript player = FindObjectOfType<PlayerScript>();
            player.TakeDamage(-5);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Restart(); //restarts the game
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            KillEnemys();    //kills all enemys in the current scene
        }

                            //controls the QA panel
        if(Input.GetKeyDown(KeyCode.F1))
        {
            Time.timeScale = 0;
            QApanel.SetActive(true);
        }
        else if(Input.GetKeyUp(KeyCode.F1))
        {
            Time.timeScale = 1;
            QApanel.SetActive(false);
        }

        //Spawing the Enemies
        #region SpawnWaves
        ///Summary
        ///
        /// Replaces the next wave spawn with 2 melee enemies
        /// 
        ///End Summary

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            int currentWaveIndex = ws.GetCurrentWaveIndex();

            Enemy[] enemyToSpawn = new Enemy[1];
            enemyToSpawn[0] = meleeEnemy.GetComponent<MeleeEnemy>();

            if ((currentWaveIndex + 1) < ws.waves.Length)
            {
                ws.waves[currentWaveIndex + 1].enemies = enemyToSpawn;
                ws.waves[currentWaveIndex + 1].count = 2;
                ws.waves[currentWaveIndex + 1].timeBetweenSpawns = 1;
            }
            
        }

        ///Summary
        ///
        /// Replaces the next wave spawn with 2 summoner enemies
        /// 
        ///End Summary

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            int currentWaveIndex = ws.GetCurrentWaveIndex();

            Enemy[] enemyToSpawn = new Enemy[1];
            enemyToSpawn[0] = summonerEnemy.GetComponent<SummonerEnemy>();

            if ((currentWaveIndex + 1) < ws.waves.Length)
            {
                ws.waves[currentWaveIndex + 1].enemies = enemyToSpawn;
                ws.waves[currentWaveIndex + 1].count = 2;
                ws.waves[currentWaveIndex + 1].timeBetweenSpawns = 1;
            }

        }

        ///Summary
        ///
        /// Replaces the next wave spawn with 2 ranged enemies
        /// 
        ///End Summary
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            int currentWaveIndex = ws.GetCurrentWaveIndex();

            Enemy[] enemyToSpawn = new Enemy[1];
            enemyToSpawn[0] = rangedEnemy.GetComponent<RangedEnemy>();

            if ((currentWaveIndex + 1) < ws.waves.Length)
            {
                ws.waves[currentWaveIndex + 1].enemies = enemyToSpawn;
                ws.waves[currentWaveIndex + 1].count = 2;
                ws.waves[currentWaveIndex + 1].timeBetweenSpawns = 1;
            }

        }

        ///Summary
        ///
        /// Replaces the next wave spawn with 2 summoner enemies
        /// 
        ///End Summary

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            int currentWaveIndex = ws.GetCurrentWaveIndex();

            Enemy[] enemyToSpawn = new Enemy[1];
            enemyToSpawn[0] = summonerEnemy.GetComponent<SummonerEnemy>();

            if ((currentWaveIndex + 1) < ws.waves.Length)
            {
                ws.waves[currentWaveIndex + 1].enemies = enemyToSpawn;
                ws.waves[currentWaveIndex + 1].count = 2;
                ws.waves[currentWaveIndex + 1].timeBetweenSpawns = 1;
            }

        }

        #endregion SpawnWaves 
    }

    public void KillEnemys()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().Death();
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //reloads the current level
    }
}
