using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Results results;
    [SerializeField] private VirusUI virusA;
    [SerializeField] private VirusUI virusB;

    [System.Serializable]
    private class VirusUI
    {
        public TextMeshProUGUI virusName;
        public TextMeshProUGUI authorName;
        public Image virusAvatar;
        public ResizeAvatar resize;
    }

    [System.Serializable]
    private class Results
    {
        public GameObject go;
        public TextMeshProUGUI winnerVirusText;
        public TextMeshProUGUI winnerAuthorText;
        public Image winnerAvatar;
        public ResizeAvatar resize;
    }

    public void ShowResults(int winner)
    {
        results.go.SetActive(true);

        switch (winner)
        {
            // IZQUIERDA
            case 0:
                results.winnerVirusText.text = virusA.virusName.text;
                results.winnerAuthorText.text = virusA.authorName.text;
                results.winnerAvatar.sprite = virusA.virusAvatar.sprite;
                results.resize.avatarSprite = virusA.resize.avatarSprite;
                break;
            // DERECHA 
            case 1:
                results.winnerVirusText.text = virusB.virusName.text;
                results.winnerAuthorText.text = virusB.authorName.text;
                results.winnerAvatar.sprite = virusB.virusAvatar.sprite;
                results.resize.avatarSprite = virusB.resize.avatarSprite;
                break;
            // EMPATE
            case 2:
                results.winnerVirusText.text = "";
                results.winnerAuthorText.text = "";
                results.winnerAvatar.sprite = null;
                results.resize.avatarSprite = null;
                break;
        }
    }
}