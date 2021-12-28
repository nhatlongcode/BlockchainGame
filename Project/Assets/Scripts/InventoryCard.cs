using System.Numerics;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;

public class InventoryCard : MonoBehaviour
{
    public Button sellButton;
    public Button unsellButton;
    public CharacterVisual visual;
    public BigInteger id;
    public UnityAction<BigInteger> sellButtonPressedEvent;
    public UnityAction<BigInteger> unsellButtonPressedEvent;
    public UnityAction<BigInteger> infoButtonPressedEvent;

    public void ShowCharacter(BigInteger id)
    {
        this.id = id;
        visual.ShowCharacter(new PlayerStat(id));
    }

    public void SetUnsellState()
    {
        unsellButton.gameObject.SetActive(true);
        sellButton.gameObject.SetActive(false);
    }
    public void SellButtonPressed()
    {
        sellButtonPressedEvent?.Invoke(id);
        sellButton.gameObject.SetActive(false);
        unsellButton.gameObject.SetActive(true);
    }

    public void UnsellButtonPressed()
    {
        unsellButtonPressedEvent?.Invoke(id);
        sellButton.gameObject.SetActive(true);
        unsellButton.gameObject.SetActive(false);
    }

    public void SetSellingState(bool isSelling)
    {
        sellButton.gameObject.SetActive(!isSelling);
        unsellButton.gameObject.SetActive(isSelling);
    }

}
