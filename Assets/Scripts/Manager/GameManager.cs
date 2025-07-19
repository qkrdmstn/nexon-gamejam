using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    Main,
    CutScene0,
    Stage0, Stage1, Stage2,
    CutScene1
}

public class GameManager : MonoBehaviour
{
    public SceneType curScene;

    public int gold;
    public int curHP;
    public int maxHP;

    public GameObject playerObj;
    public CinemachineImpulseSource impulseSource;
    [SerializeField] List<TowerShopIcon> towershops;
    public event Action OnGolded; //골드 획득했을 때 콜백 이벤트
    public event Action OnHPChanged; //HP 닳았을 때 콜백 이벤트
    public static GameManager instance;
    void Awake()
    {
        if (instance == null)
        { //생성 전이면
            instance = this; //생성
        }
        else if (instance != this)
        { //이미 생성되어 있으면
            Destroy(this.gameObject); //새로만든거 삭제
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        SetUp();
        SoundManager.Instance.PlayBGM(BGM.STAGE);
    }

    public void SetUp()
    {
        Time.timeScale = 1.0f;
        curHP = maxHP;
        gold = 50;
        playerObj = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetGold(int num)
    {
        gold += num;
        towershops.ForEach(ts => ts.CheckPurchase(gold));
        OnGolded.Invoke();
    }

    public void UseGold(int num)
    {
        gold -= num;
        towershops.ForEach(ts => ts.CheckPurchase(gold));
        OnGolded.Invoke();
    }

    public void RecoverHP(int num = 1)
    {
        curHP = Mathf.Min(maxHP, curHP + num);
        OnHPChanged.Invoke();
    }

    public void OnDamage(int damage = 1)
    {
        curHP -= damage;
        impulseSource.GenerateImpulse();
        OnHPChanged.Invoke();
        if (curHP <= 0)
            StageOver();
    }


    public void StageOver()
    {
        Time.timeScale = 0.0f;
        FindObjectOfType<StageOverUI>().SetActiveUI();
    }
    public void StageClear()
    {
        FindObjectOfType<StageClearUI>().SetActiveUI();
    }

    public void NxtScene()
    {
        int nxtSceneNum = (int)curScene + 1;
        ChangeScene((SceneType)nxtSceneNum);
    }

    public void PreveScene()
    {
        int prevSceneNum = (int)curScene - 1;
        ChangeScene((SceneType)prevSceneNum);
    }

    public void ReloadScene()
    {
        ChangeScene(curScene);
    }

    public void ChangeScene(SceneType type)
    {
        SceneManager.LoadScene(type.ToString());
        SetUp();
    }
}
