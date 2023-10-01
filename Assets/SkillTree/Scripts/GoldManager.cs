using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance;
    [SerializeField] private TextMeshProUGUI _goldText;

    private int _gold;
    public int Gold
    {
        get
        {
            return _gold;
        }
        set
        {
            _gold = value;
            _goldText.text = $"Remain Gold : {_gold}$";
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Gold += 9;
    }

    public bool CheckSpendGold(int value)
    {
        return Gold - value >= 0;
    }
}
