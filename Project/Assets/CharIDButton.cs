using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class CharIDButton : MonoBehaviour
{
    public Text IDText;
    public Button IDButton;
    public UnityEvent<string> CallWhenPressedEvent;

    public void OnButtonPressed()
    {
        CallWhenPressedEvent?.Invoke(IDText.text);
    }

}
