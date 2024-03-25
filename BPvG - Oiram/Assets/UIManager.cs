using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } 

    [SerializeField] private TextMeshProUGUI coinText; 

    private int coinCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep UI persistent across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoin()
    {
        coinCount++;
        UpdateCoinDisplay();
    }

    private void UpdateCoinDisplay()
    {
        coinText.text = "Coins: " + coinCount;
    }
}
