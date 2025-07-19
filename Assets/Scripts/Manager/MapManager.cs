using System.Collections.Generic;
using UnityEngine;

public enum TowerType
{
    A,
    B,
    C,
    D,
}

public class MapManager : MonoBehaviour
{
    private static MapManager instance;
    public static MapManager Instance { get { return instance; } }

    [SerializeField] List<GameObject> towerPrefabs;

    private void Awake()
    {
        instance = this;
    }

    public GameObject GetTowerObj(TowerType type)
    {
        return Instantiate(towerPrefabs[(int)type]);
    }

    public int GetTowerCost(TowerType type)
    {
        return towerPrefabs[(int)type].GetComponent<TowerBase>().cost;
    }
}
