using System;
using UnityEngine;
using UnityEngine.UI;

public class BlinkEffect : MonoBehaviour
{
    [Serializable]
    private enum ImageType
    {
        SpriteRenderer,
        Image
    }

    [Tooltip("Tipo de imagen que se va a usar")] [SerializeField]
    private ImageType imageType;

    [SerializeField] private SpriteRenderer spRend;
    [SerializeField] private Image image;

    [SerializeField] private float minAlpha;
    [SerializeField] private float maxAlpha;

    [Tooltip("Tiempo entre parpadeo (seg)")] [SerializeField]
    private float repeatRate;    
    [Tooltip("Alpha que se cambia entre parpadeo")] [SerializeField]
    private float alphaAmount;
    

    private Color currentColor;

    private void Start()
    {
        if (minAlpha > maxAlpha)
        {
            (minAlpha, maxAlpha) = (maxAlpha, minAlpha);
        }

        switch (imageType)
        {
            case ImageType.SpriteRenderer:
                currentColor = spRend.color;
                currentColor.a = minAlpha + 0.001f;
                spRend.color = currentColor;
                InvokeRepeating(nameof(SpriteRendererBlink), 0, repeatRate);
                break;
            case ImageType.Image:
                currentColor = image.color;
                currentColor.a = minAlpha + 0.001f;
                image.color = currentColor;
                InvokeRepeating(nameof(ImageBlink), 0, repeatRate);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SpriteRendererBlink()
    {
        // Cambio de dirección
        if (currentColor.a >= maxAlpha || currentColor.a <= minAlpha)
        {
            alphaAmount = -alphaAmount;
        }
        
        currentColor.a += alphaAmount;
        spRend.color = currentColor;
    }
    
    private void ImageBlink()
    {
        // Cambio de dirección
        if (currentColor.a >= maxAlpha || currentColor.a <= minAlpha)
        {
            alphaAmount = -alphaAmount;
        }

        currentColor.a += alphaAmount;
        image.color = currentColor;
    }
}