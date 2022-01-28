using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    List<WaveConfig> waveConfigs;

    WaveConfig currentWaveConfig;

    [SerializeField]
    float delayInEnemies = 2;

    [SerializeField]
    float delayInWaves = .5f;

    [SerializeField]
    int totalWavesCount = 100;

    List<Transform> wavePoints;

    bool isLooping = true;

    int loopCount = 0;

    void Start()
    {
        StartCoroutine(StartWaves());
    }

    IEnumerator StartWaves()
    {
        do
        {
            foreach (WaveConfig wave in waveConfigs)
            {
                currentWaveConfig = wave;

                for (int i = 0; i < currentWaveConfig.GetEnemyCount(); i++)
                {
                    Instantiate(currentWaveConfig.GetEnemyPrefab(i),
                    currentWaveConfig.GetStartingWavePoint().position,
                    Quaternion.Euler(0, 0, 180), // rotating enemy on z axis by 180 degree
                    transform);

                    yield return new WaitForSecondsRealtime(delayInEnemies);
                }

                yield return new WaitForSecondsRealtime(delayInWaves);
            }

            // check loop count
            if (loopCount > totalWavesCount)
            {
                isLooping = false;
            }
            else
            {
                loopCount++;
            }
        }
        while (isLooping);
    }

    public WaveConfig GetWaveConfig()
    {
        return currentWaveConfig;
    }
}
