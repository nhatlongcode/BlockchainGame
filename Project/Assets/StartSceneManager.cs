using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    public Transform logInPanel;
    public Button logInButton;
    public WebLogin webLogin;
    public Text coinText;
    private string account;
    private void Awake() 
    {
        // if (PlayerPrefs.HasKey("Account"))
        // {
        //     account = PlayerPrefs.GetString("Account","");
        //     if (account != "")
        //     {
        //         HideLogInPanel();
        //     }
        //     else ShowLogInPanel();
        // }
        // else
        // {
        //     ShowLogInPanel();
        // }
        webLogin.OnLoggedIn.AddListener(OnLoggedIn);
        webLogin.OnLogin();
        
    }

    public void ShowLogInPanel()
    {
        
        logInPanel.gameObject.SetActive(true);
    }

    public void HideLogInPanel()
    {
        logInPanel.gameObject.SetActive(false);
    }

    public void OnLoggedIn()
    {
        HideLogInPanel();
        account = PlayerPrefs.GetString("Account","");
        SetCoinText();
    }

    public async void SetCoinText()
    {
        BigInteger balance = await MyToken.BalanceOf(account);
        coinText.text = balance.ToString();
    }

    public void LoadInventoryScene()
    {
        SceneManager.LoadScene("Inventory", LoadSceneMode.Single);
    }

    public void LoadShopScene()
    {
        SceneManager.LoadScene("Shop", LoadSceneMode.Single);
    }

    public void LoadBreedScene()
    {
        SceneManager.LoadScene("Breed", LoadSceneMode.Single);
    }

    private void OnApplicationQuit() 
    {
        PlayerPrefs.DeleteKey("Account");
    }


}
