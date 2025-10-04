using System.Collections;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    public int simTurn;
    public int[] trainingLevel = { 1, 1, 1, 1, 1 };
    public int[] trainingCount = { 0, 0, 0, 0, 0 };
    public int[] trainingCost = { 30, 50, 70, 90, 110 };

    public bool isEndTurn = false;

    private bool isTraining = false;

    private void StartTurn()
    {
        // TODO: �� ���� �����̳� ���� ���� ���� �̺�Ʈ
        // ���� �̺�Ʈ ���� ���Ŀ� �ùķ��̼� UI ��ư ���� �⺻ ��ȣ�ۿ� Ȱ��ȭ
    }

    private void EndTurn()
    {
        GameManager.Instance.SetUIMonsterStatus();
        simTurn++;
        isEndTurn = false;
        //StartTurn();
    }

    public void OnTraining(int trainingType, Mon_Tomato mon)
    {
        // ���� ���ٸ�
        if (GameManager.Instance.gold < trainingCost[trainingLevel[trainingType] - 1])
        {
            // TODO: UI�� ���� �Ʒ� ���
            return;
        }

        // �Ʒ� ���̶��
        if (isTraining)
            return;

        StartCoroutine(TrainingRoutine(trainingType, mon));
    }

    public void OnWorking()
    {
        GameManager.Instance.gold += 50;
        uiManager.SetTextGold(GameManager.Instance.gold);
        EndTurn();
    }

    private IEnumerator TrainingRoutine(int trainingType, Mon_Tomato mon)
    {
        isTraining = true;
        // TODO: �ڷ�ƾ ����� �Ʒ���&���� �˾�UI


        // TODO: Ʈ���̴� ���� �뼺�� ����
        int rNum = UnityEngine.Random.Range(0, 10);

        int val = 1;
        int temp;

        switch (trainingType)
        {
            case 0:
                temp = mon.hp;
                mon.hp += val + trainingLevel[trainingType];
                break;
            case 1:
                temp = mon.atk;
                mon.atk += val + trainingLevel[trainingType];
                break;
            case 2:
                temp = mon.def;
                mon.def += val + trainingLevel[trainingType];
                break;
            case 3:
                temp = mon.tec;
                mon.tec += val + trainingLevel[trainingType];
                break;
            case 4:
                temp = mon.spd;
                mon.spd += val + trainingLevel[trainingType];
                break;
        }

        trainingCount[trainingType]++;
        if ((trainingCount[trainingType] % 3 == 0) && trainingLevel[trainingType] <= 5)
            trainingLevel[trainingType]++;

        // �Ʒ��� ����
        Debug.Log("�Ʒ� ��...");
        
        yield return new WaitForSeconds(1.5f);

        // ���� �뼺�� ����
        Debug.Log($"�Ʒ� ����!! �ɷ�ġ�� {val + trainingLevel[trainingType]} �ö����ϴ�");

        GameManager.Instance.SetUIMonsterStatus();
        yield return new WaitForSeconds(0.5f);


        // �Ʒ� ���� �˾��� �Ʒ� �Ϸ� ��ư Ȱ��ȭ
        uiManager.OnTrainingUI();


        yield return new WaitUntil(() => isEndTurn);
        isTraining = false;

        EndTurn();
    }

}
