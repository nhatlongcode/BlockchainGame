using System.Numerics;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    public CharacterInfoUI infoUI;
    public Text priceText;
    public Text sendText;
    public Text coinText;
    public InventoryCard inventoryCardPrefab;
    public Transform invetoryHolder;
    public Transform sellPanel;
    public Transform sendPanel;
    private BigInteger sellingId;
    private string account;

    private void Awake() {
        account = PlayerPrefs.GetString("Account","");
        if (account == "") account = "0x9e26135fEAE071Aba5Af54ADB8e9Bc334B2E11d1";
        LoadOwnedCharacter();
        LoadCoinData();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadOwnedCharacter();
        }
    }
    public async void LoadCoinData()
    {
        BigInteger amount = await MyToken.BalanceOf(account)/1000000000000000000;
        coinText.text = amount.ToString();
    }
    public async void LoadOwnedCharacter()
    {
        var IDs = await MyToken.TokensOwned(account);
        foreach (Transform child in invetoryHolder.transform)
        {
            Destroy(child.gameObject);
        }
        foreach(var id in IDs)
        {
            if (!(id.ToString() == "1766900985905872605661971217099209419799069147884130072262361666378224417" ||
                id.ToString() == "26961592203541870915946560710015769672447776188642332113791347020577")) AddInventoryCard(id);
        }
    }

    public async void UnsellItem(BigInteger id)
    {
        await MyToken.CancelSale(id);
    }
    
    public async void SendProcess()
    {
        await MyToken.TransferToken(account, sendText.text, sellingId);
    }

    public async void SellProcess()
    {
        BigInteger price = BigInteger.Parse(priceText.text);
        string hash = await MyToken.SetSalePrice(sellingId, price*1000000000000000000);
    }

    public async void AddInventoryCard(BigInteger id)
    {
        BigInteger price = await MyToken.GetSellPrice(id); 
        var go = Instantiate(inventoryCardPrefab, invetoryHolder);
        go.id = id;
        if (price != 0) 
        {
            go.unsellButtonPressedEvent += UnsellItem;
            go.infoButtonPressedEvent += InfoButtonPressed;
            go.SetUnsellState();
        }
        else 
        {
            go.sellButtonPressedEvent += SellItem;
            go.sendButtonPressedEvent += SendItem;
            go.infoButtonPressedEvent += InfoButtonPressed;
        }
        go.ShowCharacter(id);
    }

    public void SellItem(BigInteger id)
    {
        //MyToken.SetSalePrice(id, )
        sellingId = id;
        ShowSellPanel();
    }

    public void SendItem(BigInteger id)
    {
        sellingId = id;
        ShowSendPanel();
    }


    public void ShowSellPanel()
    {
        sellPanel.gameObject.SetActive(true);
    }

    public void ShowSendPanel()
    {
        sendPanel.gameObject.SetActive(true);
    }


    public void BackButtonPressed()
    {
        SceneManager.LoadScene("Start");
    }

    public void CancelSellProcess()
    {
        sellPanel.gameObject.SetActive(false);  
    }

    public void CancelSendProcess()
    {
        sendPanel.gameObject.SetActive(false);
    }

    public void SendButtonPressed()
    {
        SendProcess();
    }

    public void InfoButtonPressed(BigInteger id)
    {
        infoUI.ShowCharacterStat(new PlayerStat(id));
        infoUI.SetActive(true);
    }

    public void SellButtonPressed()
    {
        BigInteger price = BigInteger.Parse(priceText.text);
        if (price < 1) return;
        SellProcess();
    }


    private void OnApplicationQuit() 
    {
        PlayerPrefs.DeleteKey("Account");
    }
}
