using UnityEngine;

public class Player : MonoBehaviour
{
    public static int gold;

    void Start()
    {
        // TODO: if(첫 육성이라면)
        gold = 100;
    }
    public int GetGold()
    {
        return gold;
    }

    public void SetGold(int setGold)
    {
        gold += setGold;
    }



}
