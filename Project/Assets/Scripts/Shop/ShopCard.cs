using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Numerics;
public class ShopCard : MonoBehaviour 
{
    public BigInteger id;
    public Text priceText;
    public CharacterVisual visual;
    public UnityAction<BigInteger> buyButtonPressed;
    public UnityAction<BigInteger> infButtonPressed;
    public void ShowCard(BigInteger id, BigInteger price)
    {
        this.id = id;
        visual.ShowCharacter(new PlayerStat(id));
        priceText.text = price.ToString();
    }

    public void BuyCharacter()
    {
        //string hash = await MyToken.Buy(id);
        buyButtonPressed?.Invoke(id);
    }

    public void InfoButtonPressed()
    {
        infButtonPressed?.Invoke(id);
    }
}