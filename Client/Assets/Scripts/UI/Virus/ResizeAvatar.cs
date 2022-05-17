using UnityEngine;
using UnityEngine.UI;

public class ResizeAvatar : MonoBehaviour
{
    [Tooltip("Referencia al spirte que puede llevar el avatar")]
    public Sprite avatarSprite;

    [Tooltip("Referencia al Rect Transform del GameObject")] [SerializeField]
    private RectTransform avatarRectTr;

    [Tooltip("Referencia al componente Image del GameObject")] [SerializeField]
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        UpdateAvatar();
    }

    public void UpdateAvatar()
    {
        if (!avatarSprite) return;

        var resetColor = new Color(1, 1, 1, 1);
        image.color = resetColor;

        // Dimensiones del sprite - pixeles
        var spriteW = avatarSprite.rect.width;
        var spriteH = avatarSprite.rect.height;

        // Dimensiones del rect transform - pixeles
        var rect = avatarRectTr.rect;
        var avatarW = rect.width;
        var avatarH = rect.height;

        // k = (w1 * h1) / (w2 * h2)
        var k = (spriteW * avatarH) / (spriteH * avatarW);
        var newSize = new Vector2
        {
            x = (spriteW <= spriteH ? k * avatarW : avatarW) - avatarW,
            y = (spriteH <= spriteW ? k * avatarH : avatarH) - avatarH
        };

        avatarRectTr.sizeDelta = newSize;
    }
}