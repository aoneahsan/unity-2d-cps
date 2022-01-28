using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    EnemySpawner enemySpawner;

    WaveConfig waveConfig;

    List<Transform> wavePoints;

    int currentWaveIndex = 0;

    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        waveConfig = enemySpawner.GetWaveConfig();
        wavePoints = waveConfig.GetWavePoints();
        transform.position = wavePoints[currentWaveIndex].position;
    }

    void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (currentWaveIndex < wavePoints.Count)
        {
            Vector3 targetPosition = wavePoints[currentWaveIndex].position;
            float deltaDistance = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position =
                Vector2
                    .MoveTowards(transform.position,
                    targetPosition,
                    deltaDistance);
            if (transform.position == targetPosition)
            {
                currentWaveIndex++;
            }
        }
        else
        {
            Destroy (gameObject);
        }
    }
}
