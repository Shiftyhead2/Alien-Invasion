using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive;
    public Transform EnemyPrefab;
    public Transform Earth;
    public float SpawnRadious;

    public float TimeBetweenWaves = 5f;
    private float CountDown = 2f;
    public static int WaveIndex = 0;
    private int MaxWaves = 25;
    [TextArea]
    public string[] Taunts;
    private bool HasChosen = false;

    public Canvas UI;
    public Text WaveText;
    public Text EnemiesRemainingText;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        EnemiesRemainingText.text = "ENEMIES ALIVE:" + EnemiesAlive.ToString();
        if (EnemiesAlive > 0)
        {
            return;
        }
        else if (EnemiesAlive == 0)
        {
            if (!HasChosen)
            {
                WaveText.text = Taunts[Random.Range(0, Taunts.Length)];
                HasChosen = true;
            }

        }


        if (CountDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            CountDown = TimeBetweenWaves;
            WaveText.text = "WAVE " + WaveIndex.ToString();
            HasChosen = false;
            return;

        }
        CountDown -= Time.deltaTime;

    }

    IEnumerator SpawnWave()
    {
        WaveIndex++;
        for (int i = 0; i < WaveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);

        }
    }

    void SpawnEnemy()
    {
        Vector2 SpawnPos = Earth.position;
        SpawnPos += Random.insideUnitCircle.normalized * SpawnRadious;
        Instantiate(EnemyPrefab,SpawnPos,Quaternion.identity);
        EnemiesAlive++;
    }

}
