using System.Numerics;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;

public class InventoryCard : MonoBehaviour
{
    public Button sellButton;
    public CharacterVisual visual;
    public BigInteger id;
    public UnityAction<BigInteger> sellButtonPressedEvent;
    public UnityAction<BigInteger> infoButtonPressedEvent;

    public void ShowCharacter(BigInteger id)
    {
        this.id = id;
        visual.ShowCharacter(new PlayerStat(id));
    }
    public void SellButtonPressed()
    {
        sellButtonPressedEvent?.Invoke(id);
    }

}
