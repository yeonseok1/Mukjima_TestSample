using System.Collections;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    private int simTurn;
    private int[] trainingLevel = { 1, 1, 1, 1, 1 };
    private int[] trainingCount = { 0, 0, 0, 0, 0 };
    private int[] trainingCost = { 30, 50, 70, 90, 110 };

    private bool endTurnFlag = false;

    private bool isTraining = false;

    void Start()
    {
        simTurn = 1;
        uiManager.SetTextTurn(simTurn);
    }

    private void StartTurn()
    {
        // TODO: �� ���� �����̳� ���� ���� ���� �̺�Ʈ
        // ���� �̺�Ʈ ���� ���Ŀ� �ùķ��̼� UI ��ư ���� �⺻ ��ȣ�ۿ� Ȱ��ȭ
    }

    private void EndTurn()
    {
        GameManager.Instance.SetUIMonsterStatus();
        simTurn++;
        uiManager.SetTextTurn(simTurn);
        endTurnFlag = false;
        //StartTurn();
    }

    public void EndTurnFlagChange()
    {
        endTurnFlag = !endTurnFlag;
    }

    public void OnTraining(int trainingType, Mon_Tomato mon)
    {
        // �Ʒ� ���̶��
        if (isTraining)
            return;

        int cost = trainingCost[trainingLevel[trainingType] - 1];
        // ���� ���ٸ�
        if (GameManager.Instance.gold < cost)
        {
            // TODO: UI�� ���� �Ʒ� ���
            Debug.Log($"���� ������...�ʿ�ݾ�:{cost}");
            return;
        }
        GameManager.Instance.SetGold(-cost);

        StartCoroutine(TrainingRoutine(trainingType, mon));
    }

    public void OnWorking()
    {
        GameManager.Instance.SetGold(50);
        EndTurn();
    }

    private IEnumerator TrainingRoutine(int trainingType, Mon_Tomato mon)
    {
        isTraining = true;
        // TODO: �ڷ�ƾ ����� �Ʒ���&���� �˾�UI

        // Ʈ���̴� ���� �뼺�� ���� -> �뼺���� ���� ���� 1.5���
        // ������ ���ٸ� Ȯ���� 10%
        bool isPerfect = UnityEngine.Random.Range(0, 100) < 10;

        int val = 1 + trainingLevel[trainingType];
        if (isPerfect)
            val += val >> 1;
        int temp = 0;
        string trainingStatus = "";

        switch (trainingType)
        {
            case 0:
                temp = mon.hp;
                trainingStatus = "ü��";
                mon.hp += val;
                break;
            case 1:
                temp = mon.atk;
                trainingStatus = "����";
                mon.atk += val;
                break;
            case 2:
                temp = mon.def;
                trainingStatus = "���";
                mon.def += val;
                break;
            case 3:
                temp = mon.tec;
                trainingStatus = "�ؾ�";
                mon.tec += val;
                break;
            case 4:
                temp = mon.spd;
                trainingStatus = "�ӵ�";
                mon.spd += val;
                break;
        }

        // Ʈ���̴� Ƚ�� & ����
        trainingCount[trainingType]++;
        if ((trainingCount[trainingType] % 3 == 0) && trainingLevel[trainingType] <= 5)
            trainingLevel[trainingType]++;

        uiManager.OnTrainingUI(isPerfect, val, temp, trainingStatus);

        yield return new WaitUntil(() => endTurnFlag);

        isTraining = false;
        EndTurn();
    }

}
