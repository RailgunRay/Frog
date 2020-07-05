using System.Linq;
using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private WaterLily lilyPrefab;

    [SerializeField]
    private Player playerPrefab;

    [SerializeField]
    private Fly flyPrefab;

    [SerializeField]
    private Mosquito mosquitoPrefab;

    [SerializeField]
    private Vector2 timeBetweenSpawnsMinMax;
#pragma warning restore 0649

    public static WaterLily[] lilies;
    public static Player player;
    private Transform[] spawnPoints;
    private float maxSpawnRadius = 1.5f;
    private Vector3 playerSpawnPos;
    IEnumerator spawnEnemiesRoutine;

    void Awake()
    {
        SpawnLilies();
        SpawnPlayer(playerSpawnPos.Flat());
    }

    void Start()
    {
        spawnEnemiesRoutine = SpawnEnemiesRoutine();
        StartCoroutine(spawnEnemiesRoutine);
    }

    void SpawnLilies()
    {
        lilies = new WaterLily[5];
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint").
                      Select(point => point.transform).ToArray();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Vector3 newInstancePosition = (Vector2)spawnPoints[i].position +
                                          Random.insideUnitCircle * maxSpawnRadius;
            var newLily = Instantiate(lilyPrefab, newInstancePosition, Quaternion.identity);
            newLily.transform.rotation = Quaternion.Euler(Vector3.forward * Random.Range(0, 360));
            if (i == 0) playerSpawnPos = newInstancePosition;
            lilies[i] = newLily;
        }
    }

    void SpawnPlayer(Vector3 position)
    {
        if (!Player.instance)
        {
            Player.instance = player = Instantiate(playerPrefab, position - Vector3.forward * 5f, Quaternion.Euler(0, 0, 90));
        }
        else
        {
            return;
        }
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        while (true)
        {
            SpawnNewEnemy();
            float waitSeconds = Mathf.Lerp(timeBetweenSpawnsMinMax.y, timeBetweenSpawnsMinMax.x,
                                           Difficulty.GetDifficultyPercent());
            yield return new WaitForSeconds(waitSeconds);
        }
    }

    private void SpawnNewEnemy()
    {
        Enemy newEnemy;
        Vector3 newSpawnSpot = PickSpawnSpot();
        newEnemy = RandomiseEnemyType();
        Instantiate(newEnemy, newSpawnSpot, Quaternion.identity);
    }

    private Enemy RandomiseEnemyType()
    {
        Enemy newEnemy;
        if (Random.value > .5f)
        {
            newEnemy = flyPrefab;
        }
        else
        {
            newEnemy = mosquitoPrefab;
        }

        return newEnemy;
    }

    void StopSpawningEnemies()
    {
        StopCoroutine(spawnEnemiesRoutine);
    }

    private Vector3 PickSpawnSpot()
    {
        Camera viewCamera = Camera.main;
        return VectorExtensionMethods.GetRandomPointOutsideOfTheViewport(viewCamera).Flat();
    }
}
