using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject Inventory;
    public Player player;

    void Start()
    {
        if (Instance == null) Instance = this;
    }
}
