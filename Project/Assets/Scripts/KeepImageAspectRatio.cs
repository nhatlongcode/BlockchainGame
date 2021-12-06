using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Image))]
public class KeepImageAspectRatio : MonoBehaviour
{
    public enum Dimension
    {
        WIDTH,
        HEIGHT
    }   
    public Dimension dimension;
    public float value;

    private void Awake() 
    {
        var image = GetComponent<Image>();
        var rect = GetComponent<RectTransform>();
        if (dimension == Dimension.HEIGHT)
        {
            float h = value;
            float w = (value * image.sprite.rect.width) / image.sprite.rect.height;
            rect.sizeDelta = new Vector2(w, h);
        }
        else if (dimension == Dimension.WIDTH)
        {
            float w = value;
            float h = (value * image.sprite.rect.height) / image.sprite.rect.width;
            rect.sizeDelta = new Vector2(w, h);
        }
    }
}
