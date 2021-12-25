using UnityEngine;
using UnityEngine.Events;
using System.Numerics;
public class ShopCard : MonoBehaviour 
{
    public BigInteger id;
    public UnityAction<BigInteger> callWhenPressed;
    public void ShowCard(BigInteger id)
    {

    }

    public async void BuyCharacter()
    {
        string hash = await MyToken.Buy(id);
    }
}