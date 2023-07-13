using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCoin;

    private void Start()
    {
        Time.timeScale = 1f;
        textCoin.text = LevelManager.Instance.coin.ToString();
    }



    public void GameStart()
    {
        LevelManager.Instance.chickCountToCoin = 0;
        LevelManager.Instance.LoadScene("GameScene");
    }

    private bool PriceCheck(int price)
    {
        return LevelManager.Instance.coin >= price;
    }


    public void MarketBuyClown(int price)
    {
        if (PriceCheck(price))
        {
            LevelManager.Instance.clownChick += 1;
            LevelManager.Instance.coin -= price;
            
        }

    }


    public void MarketBuyHat(int price)
    {
        if (PriceCheck(price))
        {
            LevelManager.Instance.hatChick += 1;
            LevelManager.Instance.coin -= price;
        }
    }





}


