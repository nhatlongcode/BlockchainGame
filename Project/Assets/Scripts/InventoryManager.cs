using System.Numerics;
using UnityEngine.UI;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Text priceText;
    public InventoryCard inventoryCardPrefab;
    public Transform invetoryHolder;
    public Transform sellPanel;
    private BigInteger sellingId;
    private string account;

    private void Awake() {
        account = PlayerPrefs.GetString("Account");
    }
    public void SellItem(BigInteger id)
    {
        //MyToken.SetSalePrice(id, )
        sellingId = id;
        ShowSellPanel();
    }

    public void ShowSellPanel()
    {
        sellPanel.gameObject.SetActive(true);
    }

    public void CancelSellProcess()
    {
        sellPanel.gameObject.SetActive(false);  
    }

    public void SellButtonPressed()
    {
        BigInteger price = BigInteger.Parse(priceText.text);
        if (price < 1) return;
        SellProcess();
    }

    public async void SellProcess()
    {
        BigInteger price = BigInteger.Parse(priceText.text);
        string hash = await MyToken.SetSalePrice(sellingId, price);
    }

    public void AddInventoryCard(BigInteger id)
    {
        var go = Instantiate(inventoryCardPrefab, invetoryHolder);
        go.id = id;
        go.sellButtonPressedEvent += SellItem;
    }
    private void OnApplicationQuit() 
    {
        PlayerPrefs.DeleteKey("Account");
    }
}
