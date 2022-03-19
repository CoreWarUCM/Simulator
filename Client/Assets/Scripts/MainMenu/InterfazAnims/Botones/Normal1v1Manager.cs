using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
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

    // Para saber cómo deben comportarse los callbacks
    private bool normalInit = false;

    // Posición inicial del botón volver
    private Vector3 backInitPos;

    // Posición inicial del botón cargar
    private Vector3 loadInitPos;

    // Posición inicial del botón historial
    private Vector3 histInitPos;

//------------------------------------------------------------//
    [System.Serializable]
    private class BackElement
    {
        [Tooltip("Referencia al botón principal del menú")]
        public Button button;

        [Tooltip("Referencia al GameObject de Normal1v1")]
        public GameObject normal1v1;

        [Tooltip("Referencia al GameObject de MainMenu")]
        public GameObject mainMenu;

        [Tooltip("Referencia al panel de cargar guerreros")]
        public GameObject loadPanel;

        [Tooltip("Referencia al panel del historial")]
        public GameObject historyPanel;

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
        backInitPos = backButton.button.gameObject.transform.position;
        loadInitPos = loadButton.button.gameObject.transform.position;
        histInitPos = historyButton.button.gameObject.transform.position;

        // Se inicializan los callbacks
        backButton.Init(BackBehave);
        loadButton.Init(LoadButtonBehave);
        historyButton.Init(HistoryButtonBehave);
    }

//------------------------------------------------------------//
    private void BackBehave()
    {
        // Se desactivan los elementos del menú
        backButton.normal1v1.SetActive(false);
        backButton.loadPanel.SetActive(false);
        backButton.historyPanel.SetActive(false);
        ResetPositions();
        // Se activa el menú principal
        backButton.mainMenu.SetActive(true);
        normalInit = false;
    }
//------------------------------------------------------------//
    /// <summary>
    /// Comportamiento del botón de cargar guerreros
    /// </summary>
    private void LoadButtonBehave()
    {
        // Si no se ha inicializado entonces se hará la animación de movimiento
        // y luego se muestra el panel correspondiente
        if (!normalInit)
        {
            backButton.mov.Init(transitionTime);
            loadButton.mov.Init(transitionTime);
            historyButton.mov.Init(transitionTime);

            Invoke(nameof(ShowLoadPanel), transitionTime + 0.5f);
            normalInit = true;
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
        if (!normalInit)
        {
            backButton.mov.Init(transitionTime);
            loadButton.mov.Init(transitionTime);
            historyButton.mov.Init(transitionTime);

            Invoke(nameof(ShowHistoryPanel), transitionTime);
            normalInit = true;
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
        backButton.button.gameObject.transform.position = backInitPos;
        loadButton.button.gameObject.transform.position = loadInitPos;
        historyButton.button.gameObject.transform.position = histInitPos;
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
}