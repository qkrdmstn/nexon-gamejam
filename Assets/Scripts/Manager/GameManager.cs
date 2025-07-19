using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    Main,
    CutScene0,
    Stage0, Stage1, Stage2,
    CurScene1
}

public class GameManager : MonoBehaviour
{
    public SceneType curScene;

    public int gold;
    public int curHP;
    public int maxHP;

    public GameObject playerObj;
    public CinemachineImpulseSource impulseSource;
    public event Action OnGolded; //��� ȹ������ �� �ݹ� �̺�Ʈ
    public event Action OnHPChanged; //HP ����� �� �ݹ� �̺�Ʈ
    public static GameManager instance;
    void Awake()
    {
        if (instance == null)
        { //���� ���̸�
            instance = this; //����
        }
        else if (instance != this)
        { //�̹� �����Ǿ� ������
            Destroy(this.gameObject); //���θ���� ����
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
        StartCoroutine(FindPlayerAwait());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetGold(int num)
    {
        gold += num;
        OnGolded.Invoke();
    }

    public void UseGold(int num)
    {
        gold -= num;
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
            StartCoroutine(StageOver());
    }

    private IEnumerator StageOver()
    {
        playerObj.GetComponent<Player>().Dead();
        yield return new WaitForSeconds(0.9f);
        Time.timeScale = 0.0f;
        FindObjectOfType<StageOverUI>().SetActiveUI();
    }

    public void StageClear()
    {
        Time.timeScale = 0.0f;
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
        curScene = type;
        SceneManager.LoadScene(type.ToString());
        SetUp();
    }

    private IEnumerator FindPlayerAwait()
    {
        yield return new WaitForSeconds(2);
        playerObj = GameObject.FindWithTag("Player");
    }
}
