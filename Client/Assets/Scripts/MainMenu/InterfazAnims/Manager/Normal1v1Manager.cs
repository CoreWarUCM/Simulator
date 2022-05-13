using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Normal1v1Manager : MonoBehaviour
{
    [Tooltip("Tiempo que dura la animación de movimiento de los botones")] [SerializeField]
    private float transitionTime = 2.0f;

    [Tooltip("Atributos para el botón de volver")] [SerializeField]
    private BackElement backButton;

    [Tooltip("Atributos para el botón de cargar guerreros")] [SerializeField]
    private ButtonElement loadButton;

    [Tooltip("Atributos para el botón del editor")] [SerializeField]
    private ButtonElement editButton;

    [Tooltip("Boton de jugar")] [SerializeField]
    private Button playButton;
    
    // Para saber cómo deben comportarse los callbacks
    private bool _normalInit;

    // Posición inicial del botón volver
    private Vector3 _backInitPos;

    // Posición inicial del botón cargar
    private Vector3 _loadInitPos;

    // Posición inicial del botón editor
    private Vector3 _editInitPos;


    [SerializeField]
    private List<VirusState> states;

//------------------------------------------------------------//
    [System.Serializable]
    private class BackElement
    {
        [Tooltip("Referencia al botón principal del menú")]
        public Button button;

        [Tooltip("GameObjects para desactivar")]
        public GameObject [] objsToDisable;
        
        [Tooltip("GameObjects para activar")]
        public GameObject [] objsToEnable;

        [Tooltip("Referencia al componente del movimiento")]
        public ButtonMovement mov;

        [Tooltip("Referencia al componente RectTransform")]
        public RectTransform rectPos;
        public void Init(UnityAction action)
        {
            button.onClick.AddListener(action);
        }
    }

    [System.Serializable]
    private class ButtonElement
    {
        // Referencia al botón principal del menú
        public Button button;

        [Tooltip("Referencia al panel para activar")]
        public GameObject panelToEnable;

        [Tooltip("Referencia al otro panel para desactivar")]
        public GameObject panelToDisable;

        [Tooltip("Referencia al componente del movimiento")]
        public ButtonMovement mov;

        [Tooltip("Referencia al componente RectTransform")]
        public RectTransform rectPos;
        
        public void Init(UnityAction action)
        {
            button.onClick.AddListener(action);
        }
    }
//------------------------------------------------------------//

    private void Start()
    {
        // Se guardan las posiciones iniciales
        _backInitPos = backButton.rectPos.anchoredPosition;
        _loadInitPos = loadButton.rectPos.anchoredPosition;
        _editInitPos = editButton.rectPos.anchoredPosition;

        // Se inicializan los callbacks
        backButton.Init(BackBehave);
        loadButton.Init(LoadButtonBehave);
        editButton.Init(EditButtonBehave);
    }

    public void UpdateVirusList()
    {
        int players = GameManager.Instance.GetVirusListCount();
        if (players == 2 && !playButton.interactable)
        {
            playButton.interactable = true;
        }
    }

//------------------------------------------------------------//
    private void BackBehave()
    {
        // Se desactivan los elementos necesarios
        foreach (var obj in backButton.objsToDisable)
        {
            obj.SetActive(false);
        }

        ResetPositions();
        // Se activan los elementos necesarios
        foreach (var obj in backButton.objsToEnable)
        {
            obj.SetActive(true);
        }
        
        _normalInit = false;
    }
//------------------------------------------------------------//
    /// <summary>
    /// Comportamiento del botón de cargar guerreros
    /// </summary>
    private void LoadButtonBehave()
    {
        // Si no se ha inicializado entonces se hará la animación de movimiento
        // y luego se muestra el panel correspondiente
        if (!_normalInit)
        {
            backButton.mov.Init(transitionTime);
            loadButton.mov.Init(transitionTime);
            editButton.mov.Init(transitionTime);

            Invoke(nameof(ShowLoadPanel), transitionTime + 0.5f);
            _normalInit = true;
        }
        else
        {
            loadButton.panelToEnable.SetActive(true);
            loadButton.panelToDisable.SetActive(false);
        }
    }

    /// <summary>
    /// Comportamiento del botón del editor
    /// </summary>
    private void EditButtonBehave()
    {
        // Si no se ha inicializado entonces se hará la animación de movimiento
        // y luego se muestra el panel correspondiente
        if (!_normalInit)
        {
            backButton.mov.Init(transitionTime);
            loadButton.mov.Init(transitionTime);
            editButton.mov.Init(transitionTime);

            Invoke(nameof(ShowEditPanel), transitionTime);
            _normalInit = true;
        }
        else
        {
            editButton.panelToEnable.SetActive(true);
            editButton.panelToDisable.SetActive(false);
        }
    }
//------------------------------------------------------------//
    
    /// <summary>
    /// Resetea las posiciones de los botones
    /// </summary>
    private void ResetPositions()
    {
        backButton.rectPos.anchoredPosition = _backInitPos;
        loadButton.rectPos.anchoredPosition = _loadInitPos;
        editButton.rectPos.anchoredPosition = _editInitPos;
    }

    /// <summary>
    /// Muestra el panel de cargar 
    /// </summary>
    private void ShowLoadPanel()
    {
        loadButton.panelToEnable.SetActive(true);
    }
 
    /// <summary>
    /// Muestra el panel de editor
    /// </summary>
    private void ShowEditPanel()
    {
        editButton.panelToEnable.SetActive(true);
    }



    public void LoadVirus(int player)
    {
        Load.LoadVirus(player, states[player],UpdateVirusList);
    }
}