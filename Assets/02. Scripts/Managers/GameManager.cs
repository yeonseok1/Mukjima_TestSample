using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private SimulationManager simulationManager;

    private Mon_Tomato mon;

    public int gold;

    void Start()
    {

        // TODO: ∞ÒµÂ ¡§∫∏ √ ±‚»≠
        gold = 100;
        uiManager.SetTextGold(gold);
    }

    public int GetGold()
    {
        return gold;
    }

    public void SetGold(int setGold)
    {
        gold += setGold;
    }


    #region ¿∞º∫ Ω√πƒ∑π¿Ãº«
    public void SetMonster(Mon_Tomato _mon)
    {
        mon = _mon;
        SetUIMonsterStatus();
    }

    public void InitMonster()
    {
        SetUIMonsterStatus();
    }

    public void SetUIMonsterStatus()
    {
        uiManager.SetTextStatus(mon.hp, mon.atk, mon.def, mon.tec, mon.spd);
    }

    public void SetTraining(int trainingType)
    {
        simulationManager.OnTraining(trainingType, mon);
    }

    public void SetWorking()
    {
        simulationManager.OnWorking();
    }

    #endregion


    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        //throw new System.NotImplementedException();
    }
}
