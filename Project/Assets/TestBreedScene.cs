using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TestBreedScene : MonoBehaviour
{
    public BreedCard breedCardPrefab;
    public GameObject characterCardHolder;
    public CharacterVisual char1Selected;
    public CharacterVisual char2Selected;
    public Transform breedPanel;
    public CharacterVisual breedCharacter;
    public Text resultText;
    

    private List<CharIDButton> buttons;
    //[SerializeField]
    private string account;

    private void Awake() {
        //webLogin.OnLoggedIn.AddListener(OnLoggedIn);
        //OnLoggedIn();
        account = PlayerPrefs.GetString("Account");
        LoadOwnedCharacter();
        DeselectChar1();
        DeselectChar2();
    }

    public void AddButtonData(BigInteger id)
    {
        var breedCard = Instantiate(breedCardPrefab, characterCardHolder.transform);
        breedCard.AssignCharacter(id);
        breedCard.callWhenPressed += SelectChar;
        //newButton.CallWhenPressedEvent.AddListener(SelectChar);
    }

    public void OnLoggedIn()
    {
        
        //accountText.text = account;
        Debug.Log(account);
        
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
            AddButtonData(id);
        }
    }

    public void SelectChar(BigInteger id)
    {
        if ((char1Selected.playerStat != null && char1Selected.playerStat.Id == id) || (char2Selected.playerStat != null && char2Selected.playerStat.Id == id)) return;
        if (char1Selected.playerStat == null)
        {
            char1Selected.gameObject.SetActive(true);
            char1Selected.ShowCharacter(new PlayerStat(id));
            Debug.Log(char1Selected.playerStat.Id);
        }
        else if (char2Selected.playerStat == null)
        {
            char2Selected.gameObject.SetActive(true);
            char2Selected.ShowCharacter(new PlayerStat(id));
            Debug.Log(char2Selected.playerStat.Id);
        }
    }

    public void DeselectChar1()
    {
        char1Selected.gameObject.SetActive(false);
        char1Selected.ClearCharacter();
    }

    public void DeselectChar2()
    {
        char2Selected.gameObject.SetActive(false);
        char2Selected.ClearCharacter();
    }

    public async void OnBreedButtonPressed()
    {
        Debug.Log("button pressed");
        //ShowBreedPanel();
        if (char1Selected.playerStat == null || char2Selected.playerStat == null) return;
        string transaction = await MyToken.Breed(char1Selected.playerStat.Id, char2Selected.playerStat.Id);
        CheckBreedResult(transaction);
        //Debug.Log("parent1: " + char1Selected.playerStat.Id);
        //Debug.Log("parent2: " + char1Selected.playerStat.Id);
        //is (await MyToken.IsTransactionConfirmed(transaction))
        //if (result == true) ShowBreedPanel();
        // resultText.text = result;
    }

    public async void CheckBreedResult(string hash)
    {
        bool result = await MyToken.IsTransactionConfirmed(hash);
        //if (result == true) ShowBreedPanel();
        resultText.text = result.ToString();
    }


    public void ShowBreedPanel()
    {
        Debug.Log("show panel");
        breedPanel.gameObject.SetActive(true);
        breedCharacter.ShowCharacter(new PlayerStat(new BigInteger(PlayerStat.Breed(char1Selected.playerStat.Id.ToByteArray(), char2Selected.playerStat.Id.ToByteArray()))));
    }

    public void HideBreedPanel()
    {
        breedPanel.gameObject.SetActive(false);
        UpdateCharacterList();
        //update list character
    }

    public void UpdateCharacterList()
    {
        foreach (Transform child in characterCardHolder.transform)
        {
            Destroy(child.gameObject);
        }
        LoadOwnedCharacter();
    }

}
