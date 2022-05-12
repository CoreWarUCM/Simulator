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

    [Tooltip("Atributos para el botón del historial")] [SerializeField]
    private ButtonElement historyButton;

    [Tooltip("Boton de jugar")] [SerializeField]
    private Button playButton;
    
    // Para saber cómo deben comportarse los callbacks
    private bool _normalInit;

    // Posición inicial del botón volver
    private Vector3 _backInitPos;

    // Posición inicial del botón cargar
    private Vector3 _loadInitPos;

    // Posición inicial del botón historial
    private Vector3 _histInitPos;


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

        public void Init(UnityAction action)
        {
            button.onClick.AddListener(action);
        }
    }
//------------------------------------------------------------//

    private void Start()
    {
        // Se guardan las posiciones iniciales
        _backInitPos = backButton.button.gameObject.transform.position;
        _loadInitPos = loadButton.button.gameObject.transform.position;
        _histInitPos = historyButton.button.gameObject.transform.position;

        // Se inicializan los callbacks
        backButton.Init(BackBehave);
        loadButton.Init(LoadButtonBehave);
        historyButton.Init(HistoryButtonBehave);
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
            historyButton.mov.Init(transitionTime);

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
    /// Comportamiento del botón del historial
    /// </summary>
    private void HistoryButtonBehave()
    {
        // Si no se ha inicializado entonces se hará la animación de movimiento
        // y luego se muestra el panel correspondiente
        if (!_normalInit)
        {
            backButton.mov.Init(transitionTime);
            loadButton.mov.Init(transitionTime);
            historyButton.mov.Init(transitionTime);

            Invoke(nameof(ShowHistoryPanel), transitionTime);
            _normalInit = true;
        }
        else
        {
            historyButton.panelToEnable.SetActive(true);
            historyButton.panelToDisable.SetActive(false);
        }
    }
//------------------------------------------------------------//
    
    /// <summary>
    /// Resetea las posiciones de los botones
    /// </summary>
    private void ResetPositions()
    {
        backButton.button.gameObject.transform.position = _backInitPos;
        loadButton.button.gameObject.transform.position = _loadInitPos;
        historyButton.button.gameObject.transform.position = _histInitPos;
    }

    /// <summary>
    /// Muestra el panel de cargar 
    /// </summary>
    private void ShowLoadPanel()
    {
        loadButton.panelToEnable.SetActive(true);
    }
 
    /// <summary>
    /// Muestra el panel de historial
    /// </summary>
    private void ShowHistoryPanel()
    {
        historyButton.panelToEnable.SetActive(true);
    }



    public void LoadVirus(int player)
    {
        Load.LoadVirus(player, states[player],UpdateVirusList);
    }
}