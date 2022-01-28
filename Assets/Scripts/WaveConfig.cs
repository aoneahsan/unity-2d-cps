using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField]
    List<GameObject> enemiesPrefabs;

    [SerializeField]
    Transform pathPrefab;

    [SerializeField]
    float moveSpeed = 5f;

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public Transform GetStartingWavePoint()
    {
        return pathPrefab.GetChild(0);
    }

    public List<Transform> GetWavePoints()
    {
        List<Transform> wavePoints = new List<Transform>();
        foreach (Transform child in pathPrefab)
        {
            wavePoints.Add (child);
        }
        return wavePoints;
    }

    public int GetEnemyCount()
    {
        return enemiesPrefabs.Count;
    }

    public GameObject GetEnemyPrefab(int index)
    {
        if (index < enemiesPrefabs.Count)
        {
            return enemiesPrefabs[index];
        }
        else
        {
            return enemiesPrefabs[0];
        }
    }
}
