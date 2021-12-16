using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public ShopCard shopCardPrefab;
    private void Awake() 
    {
        //string account = PlayerPrefs.GetString("Account");
        LoadShopData();

    }

    public async void LoadShopData()
    {
        List<BigInteger> ls = await MyToken.ItemsForSale(0, 100);
        foreach (var id in ls)
        {
            Debug.Log(id.ToString());
        }
    }
}
