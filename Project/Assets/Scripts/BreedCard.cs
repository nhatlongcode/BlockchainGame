using System.Numerics;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class BreedCard : MonoBehaviour
{
    [SerializeField]
    private CharacterVisual visual;
    [SerializeField]
    private Button selectButton;
    private BigInteger charID;
    public UnityAction<BigInteger> callWhenPressed;
    public void AssignCharacter(BigInteger id)
    {
        charID = id;
        PlayerStat stat = new PlayerStat(id);
        visual.ShowCharacter(stat);
    }

    public void OnButtonClick()
    {
        callWhenPressed.Invoke(charID);
    }

}