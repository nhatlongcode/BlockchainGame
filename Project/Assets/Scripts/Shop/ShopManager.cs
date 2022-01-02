using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Numerics;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public CharacterInfoUI infoUI;
    public ShopCard shopCardPrefab;
    public Transform shopCardHolder;
    public Text coinText;
    private Dictionary<BigInteger, BigInteger> salePrices;
    private string account;
    private void Awake() 
    {
        account = PlayerPrefs.GetString("Account","");
        if (account == "") account = "0x9e26135fEAE071Aba5Af54ADB8e9Bc334B2E11d1";
        salePrices = new Dictionary<BigInteger, BigInteger>();
        LoadShopData();
        LoadCoinData();
    }

    public async void LoadCoinData()
    {
        BigInteger amount = await MyToken.BalanceOf(account)/1000000000000000000;
        coinText.text = amount.ToString();
    }

    public async void LoadShopData()
    {
        List<BigInteger> ls = await MyToken.ItemsForSale(0, 100);
        foreach (var id in ls)
        {
            string owner = await MyToken.OwnerOf(id);
            if (id != 0 && account != owner) 
            {
                BigInteger price = await MyToken.GetSellPrice(id);
                AddShopCard(id, price);
            }
        }

        // Dictionary<BigInteger, BigInteger> dict = await MyToken.GetSalePrices(0, 100);
        // Debug.Log(dict.Count);
        // foreach (var id in ls)
        // {
        //     Debug.Log(id.ToString());
        //     // if (id != 0)
        //     // {
        //     //     if (salePrices.ContainsKey(id))
        //     //     {
        //     //         AddShopCard(id, salePrices[id]);
        //     //     }
        //     // }
        // }
    }

    // public async int GetSellPrice(BigInteger id)
    // {
    //     int price = await MyToken.GetSellPrice(id);
    //     Debug.Log(price);
    //     return price;
    // }

    public async void GetSalePrices()
    {
        var result = await MyToken.GetSalePrices(0, 100);
        salePrices = result;
    }

    public void AddShopCard(BigInteger id, BigInteger price)
    {
        var go = Instantiate(shopCardPrefab, shopCardHolder);
        go.ShowCard(id, price/1000000000000000000);
        go.buyButtonPressed += BuyButtonPressed;
        go.infButtonPressed += InfoButtonPressed;
    }

    public async void GetSalePrice()
    {
        //MyToken.GetSalePrices
    }

    public async void BuyButtonPressed(BigInteger id)
    {
        BigInteger balance = await MyToken.BalanceOf(account);
        string hash = await MyToken.BuyWithBudget(id, balance);
    }

    public void UpdateBudget()
    {
        
    }

    public void BackToStartScene()
    {
        SceneManager.LoadScene("Start");
    }

    public void InfoButtonPressed(BigInteger id)
    {
        infoUI.ShowCharacterStat(new PlayerStat(id));
        infoUI.SetActive(true);
    }
}
