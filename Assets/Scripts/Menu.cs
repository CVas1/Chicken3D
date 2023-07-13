using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCoin;
    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private GameObject marketUI;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject levelUI;



    private void Start()
    {
        Time.timeScale = 1f;
        textCoin.text = LevelManager.Instance.coin.ToString();
        textLevel.text = "Level: " + LevelManager.Instance.level.ToString();
    }



    public void GameStart()
    {
        LevelManager.Instance.soundButtonClick.Play();
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
            LevelManager.Instance.soundButtonClick.Play();

            LevelManager.Instance.clownChick += 1;
            LevelManager.Instance.coin -= price;
            textCoin.text = LevelManager.Instance.coin.ToString();
        }
        else
        {
            LevelManager.Instance.soundBadButtonClick.Play();
        }

    }


    public void MarketBuyHat(int price)
    {
        if (PriceCheck(price))
        {
            LevelManager.Instance.soundButtonClick.Play();

            LevelManager.Instance.hatChick += 1;
            LevelManager.Instance.coin -= price;
            textCoin.text = LevelManager.Instance.coin.ToString();
        }
        else
        {
            LevelManager.Instance.soundBadButtonClick.Play();
        }
    }

    public void UIMarket()
    {
        LevelManager.Instance.soundButtonClick.Play();

        menuUI.SetActive(false);
        marketUI.SetActive(true);
        levelUI.SetActive(false);
    }
    public void UIMenu()
    {
        LevelManager.Instance.soundButtonClick.Play();

        menuUI.SetActive(true);
        marketUI.SetActive(false);
        levelUI.SetActive(true);

    }


}


