using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private SimulationManager simulationManager;

    [SerializeField] private TextMeshProUGUI hp_Text;
    [SerializeField] private TextMeshProUGUI atk_Text;
    [SerializeField] private TextMeshProUGUI def_Text;
    [SerializeField] private TextMeshProUGUI tec_Text;
    [SerializeField] private TextMeshProUGUI spd_Text;
    [SerializeField] private TextMeshProUGUI gold_Text;
    [SerializeField] private TextMeshProUGUI turn_Text;

    [SerializeField] private GameObject ui_Simulation;
    [SerializeField] private GameObject ui_Training;

    [SerializeField] private GameObject ui_PopTraining;
    [SerializeField] private GameObject button_EndTraining;
    [SerializeField] private TextMeshProUGUI text_PopTraining;

    public void SetTextTurn(int simTurn)
    {
        turn_Text.text = $"{simTurn}";
    }

    public void SetTextStatus(int hp, int atk, int def, int tec, int spd)
    {
        hp_Text.text = hp.ToString();
        atk_Text.text = atk.ToString();
        def_Text.text = def.ToString();
        tec_Text.text = tec.ToString();
        spd_Text.text = spd.ToString();
    }

    public void SetTextGold(int gold)
    {
        gold_Text.text = gold.ToString();
    }

    public void OnClickTrainingUIButton()
    {
        ui_Simulation.SetActive(false);
        ui_Training.SetActive(true);
    }

    public void OnClickTrainingBackButton()
    {
        ui_Simulation.SetActive(true);
        ui_Training.SetActive(false);
    }

    public void OnClickTrainingButton(int trainingType)
    {
        GameManager.Instance.SetTraining(trainingType);
    }

    public void OnTrainingUI(bool isPerfect, int val, int temp, string trainingStatus)
    {
        StartCoroutine(UITrainingRoutine(isPerfect, val, temp, trainingStatus));
    }

    private IEnumerator UITrainingRoutine(bool isPerfect, int val, int temp, string trainingStatus)
    {
        ui_PopTraining.SetActive(true);
        text_PopTraining.text = "ÈÆ·ÃÁß...";

        yield return new WaitForSeconds(1f);

        text_PopTraining.text = isPerfect ? "ÈÆ·Ã ´ë¼º°ø!!" : "ÈÆ·Ã ¼º°ø!";

        yield return new WaitForSeconds(1f);

        GameManager.Instance.SetUIMonsterStatus();
        text_PopTraining.text += $"\n{trainingStatus} : {temp} -> {temp + val}";

        button_EndTraining.SetActive(true);
    }

    public void OnClickTrainingEnd()
    {
        button_EndTraining.SetActive(false);
        ui_PopTraining.SetActive(false);
        simulationManager.EndTurnFlagChange();
    }

    public void OnClickWorkingButton()
    {
        GameManager.Instance.SetWorking();
    }

    
}
