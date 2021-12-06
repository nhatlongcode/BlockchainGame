using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TestBreedScene : MonoBehaviour
{
    public CharIDButton charIDButtonPrefab;
    public GameObject charIDButtonHolder;
    public WebLogin webLogin;
    public Button selectedButton1;
    public Button selectedButton2;
    public Text selectedText1;
    public Text selectedText2;
    public Text resultText;
    public Text accountText;
    private List<CharIDButton> buttons;
    private string account;

    private void Awake() {
        webLogin.OnLoggedIn.AddListener(OnLoggedIn);
        selectedButton1.onClick.AddListener(DeselectChar1);
        selectedButton2.onClick.AddListener(DeselectChar2);
        DeselectChar1();
        DeselectChar2();
    }

    public void AddButtonData(string id)
    {
        var newButton = Instantiate(charIDButtonPrefab, charIDButtonHolder.transform);
        newButton.IDText.text = id;
        newButton.CallWhenPressedEvent.AddListener(SelectChar);
    }

    public void OnLoggedIn()
    {
        account = PlayerPrefs.GetString("Account");
        accountText.text = account;
        Debug.Log(account);
        LoadOwnedCharacter();
    }

    public async void LoadOwnedCharacter()
    {
        var IDs = await MyToken.TokensOwned(account);
        // foreach (Transform child in charIDButtonHolder.transform)
        // {
        //     Destroy(child.gameObject);
        // }
        foreach(var id in IDs)
        {
            Debug.Log(id.ToString());
            AddButtonData(id.ToString());
        }
    }

    public void SelectChar(string id)
    {
        if (selectedText1.text == "")
        {
            selectedText1.text = id;
        }
        else if (selectedText2.text == "")
        {
            selectedText2.text = id;
        }
    }

    public void DeselectChar1()
    {
        selectedText1.text = "";
    }

    public void DeselectChar2()
    {
        selectedText2.text = "";
    }

    public async void OnBreedButtonPressed()
    {
        if (selectedText1.text == "" || selectedText2.text == "") return;
        string result = await MyToken.Breed(BigInteger.Parse(selectedText1.text), BigInteger.Parse(selectedText2.text));
        resultText.text = result;
    }
}
