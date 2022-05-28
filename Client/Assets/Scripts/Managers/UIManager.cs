using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Results results;
    [SerializeField] private VirusUI virusA;
    [SerializeField] private VirusUI virusB;
    [SerializeField] private GameObject chooseWinner;
    [SerializeField] private GameObject virusInterface;
    
    public Color virus1Color;
    public Color author1Color;
    public Color virus1ExecuteColor;
    public Color virus2Color;
    public Color author2Color;
    public Color virus2ExecuteColor;

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


    private void Awake()
    {
        virusA.authorName.color = author1Color;
        virusA.virusName.color = virus1Color;

        virusB.authorName.color = author2Color;
        virusB.virusName.color = virus2Color;
    }

    public void ShowResults(int winner)
    {
        results.go.SetActive(true);
        virusInterface.SetActive(false);
        chooseWinner.SetActive(false);
        
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

    public void SetVirusA(string name, string author)
    {
        virusA.authorName.text = author;
        virusA.virusName.text = name;
    }
    
    public void SetVirusB(string name, string author)
    {
        virusB.authorName.text = author;
        virusB.virusName.text = name;
    }
}