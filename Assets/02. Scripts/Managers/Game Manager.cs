using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int gold = 0;

    void Start()
    {
        InitGold();
    }

    private void InitGold()
    {
        // TODO: if(첫 육성이라면)
        gold = 100;
    }


}
