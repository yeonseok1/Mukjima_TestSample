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
        // TODO: 턴 시작 시점이나 종료 전에 랜덤 이벤트
        // 시작 이벤트 종료 이후에 시뮬레이션 UI 버튼 등의 기본 상호작용 활성화
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
        // 훈련 중이라면
        if (isTraining)
            return;

        int cost = trainingCost[trainingLevel[trainingType] - 1];
        // 돈이 없다면
        if (GameManager.Instance.gold < cost)
        {
            // TODO: UI를 띄우고 훈련 취소
            Debug.Log($"돈이 부족함...필요금액:{cost}");
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
        // TODO: 코루틴 적용과 훈련중&종료 팝업UI

        // 트레이닝 성공 대성공 판정 -> 대성공시 스탯 증가 1.5배로
        // 조정이 없다면 확률은 10%
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
                trainingStatus = "체력";
                mon.hp += val;
                break;
            case 1:
                temp = mon.atk;
                trainingStatus = "공격";
                mon.atk += val;
                break;
            case 2:
                temp = mon.def;
                trainingStatus = "방어";
                mon.def += val;
                break;
            case 3:
                temp = mon.tec;
                trainingStatus = "솜씨";
                mon.tec += val;
                break;
            case 4:
                temp = mon.spd;
                trainingStatus = "속도";
                mon.spd += val;
                break;
        }

        // 트레이닝 횟수 & 레벨
        trainingCount[trainingType]++;
        if ((trainingCount[trainingType] % 3 == 0) && trainingLevel[trainingType] <= 5)
            trainingLevel[trainingType]++;

        uiManager.OnTrainingUI(isPerfect, val, temp, trainingStatus);

        yield return new WaitUntil(() => endTurnFlag);

        isTraining = false;
        EndTurn();
    }

}
