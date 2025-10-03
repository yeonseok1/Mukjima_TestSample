using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private UIManager uiManager;

    private Mon_Tomato mon;

    private int gold;

    public int simTurn;

    void Start()
    {

        // TODO: ��� ���� �ʱ�ȭ
        gold = 100;
        uiManager.SetTextGold(gold);
    }

    public void SetMonster(Mon_Tomato _mon)
    {
        mon = _mon;
    }

    public void InitMonster()
    {
        SetMonsterStatus();
    }

    public void SetMonsterStatus()
    {
        uiManager.SetTextStatus(mon.hp, mon.atk, mon.def, mon.tec, mon.spd);
        uiManager.SetTextGold(gold);
    }

    public void OnTraining(int trainingType)
    {
        // TODO: Ʈ���̴� ���� �뼺�� ����
        // TODO: Ʈ���̴� ����
        int val = 1;

        switch (trainingType)
        {
            case 0:
                mon.hp += val;
                break;
            case 1:
                mon.atk += val;
                break;
            case 2:
                mon.def += val;
                break;
            case 3:
                mon.tec += val;
                break;
            case 4:
                mon.spd += val;
                break;
        }

        // TODO: �ڷ�ƾ ����� �Ʒ���&���� �˾�UI
        SetMonsterStatus();
        simTurn++;
    }

    public int GetGold()
    {
        return gold;
    }

    public void SetGold(int setGold)
    {
        gold += setGold;
    }

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        //throw new System.NotImplementedException();
    }
}
