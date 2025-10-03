using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private TextMeshProUGUI hp_Text;
    [SerializeField] private TextMeshProUGUI atk_Text;
    [SerializeField] private TextMeshProUGUI def_Text;
    [SerializeField] private TextMeshProUGUI tec_Text;
    [SerializeField] private TextMeshProUGUI spd_Text;
    [SerializeField] private TextMeshProUGUI gold_Text;
    [SerializeField] private TextMeshProUGUI turn_Text;

    [SerializeField] private GameObject ui_Simulation;
    [SerializeField] private GameObject ui_Training;

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
        gameManager.OnTraining(trainingType);
    }
}
