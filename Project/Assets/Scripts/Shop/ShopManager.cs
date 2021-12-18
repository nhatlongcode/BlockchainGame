using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Numerics;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public ShopCard shopCardPrefab;
    public Text coinText;
    private string account;
    private void Awake() 
    {
        account = PlayerPrefs.GetString("Account");
        //LoadShopData();

    }

    public async void LoadShopData()
    {
        List<BigInteger> ls = await MyToken.ItemsForSale(0, 100);
        Debug.Log(ls.Count);
        foreach (var id in ls)
        {
            Debug.Log(id.ToString());
        }
    }

    public void UpdateBudget()
    {
        
    }

}
