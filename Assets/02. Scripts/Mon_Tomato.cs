using UnityEngine;

public class Mon_Tomato : MonoBehaviour
{
    [SerializeField] private GameObject faceObj;
    [SerializeField] private GameObject ArmsObj;

    public int hp = 10;
    public int atk = 2;
    public int def = 2;
    public int tec = 2;
    public int spd = 2;

    private int lv = 0;

    private void OnEnable()
    {
        GameManager.Instance.SetMonster(this);
        GameManager.Instance.InitMonster();
    }

    public void SetGrowUp()
    {
        if (lv == 0)
            faceObj.SetActive(true);
        if (lv == 1)
            ArmsObj.SetActive(true);
        lv++;
    }

    public void SetTraining(int trainingType)
    {

    }

    public void SetSkill()
    {

    }



}
