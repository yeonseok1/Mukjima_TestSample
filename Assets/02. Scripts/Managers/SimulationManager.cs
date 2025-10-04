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
        // TODO: 턴 시작 시점이나 종료 전에 랜덤 이벤트
        // 시작 이벤트 종료 이후에 시뮬레이션 UI 버튼 등의 기본 상호작용 활성화
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
        // 돈이 없다면
        if (GameManager.Instance.gold < trainingCost[trainingLevel[trainingType] - 1])
        {
            // TODO: UI를 띄우고 훈련 취소
            return;
        }

        // 훈련 중이라면
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
        // TODO: 코루틴 적용과 훈련중&종료 팝업UI


        // TODO: 트레이닝 성공 대성공 판정
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

        // 훈련중 연출
        Debug.Log("훈련 중...");
        
        yield return new WaitForSeconds(1.5f);

        // 성공 대성공 연출
        Debug.Log($"훈련 성공!! 능력치가 {val + trainingLevel[trainingType]} 올랐습니다");

        GameManager.Instance.SetUIMonsterStatus();
        yield return new WaitForSeconds(0.5f);


        // 훈련 성공 팝업에 훈련 완료 버튼 활성화
        uiManager.OnTrainingUI();


        yield return new WaitUntil(() => isEndTurn);
        isTraining = false;

        EndTurn();
    }

}
