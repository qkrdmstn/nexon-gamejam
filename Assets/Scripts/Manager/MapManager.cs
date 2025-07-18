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

    [SerializeField] List<TowerGround> towerGrounds;
    [SerializeField] List<GameObject> towerPrefabs;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SetAllTowerGroundHighlight(false);
    }

    public void SetAllTowerGroundHighlight(bool isOn) //Ÿ�� UI �������� �巡������ �� ȣ��
    {
        if (isOn)
            towerGrounds.ForEach(tg => tg.HighlightOn());
        else
            towerGrounds.ForEach(tg => tg.HighlightOff());
    }

    public GameObject GetTower(TowerType type)
    {
        return Instantiate(towerPrefabs[(int)type]);
    }
}
