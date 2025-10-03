using UnityEngine;

public class TrainingManager : MonoBehaviour
{
    private Mon_Tomato mon_Tomato;

    public void SetMonster(Mon_Tomato _mon_Tomato)
    {
        mon_Tomato = _mon_Tomato;
    }

    public void OnTraining(int trainingType)
    {
        
        mon_Tomato.SetStatus(trainingType);
    }
}
