using UnityEngine;
using Simulator;

/// <summary>
/// Class that makes the setup of the memory
/// </summary>
public class MemoryGroup : MonoBehaviour
{
    // MemoryGroupShader in scene, manages the communication with the shader
    [SerializeField] private MemoryGroupShader groupShader;
    
    [SerializeField]
    private uint cellAmountSet = 8000;

    /// <summary>
    /// Setup of the memory
    /// Checks if is correctly initialized.
    /// Setups the MemoryGroupShader and calls the UIManager and BattleSimulator to pass
    /// the information of the memory 
    /// </summary>
    public void Start()
    {
        if (cellAmountSet < 100)
        {
            Debug.LogWarning("Bad Layout Setup, check for null cell || length <= 0 || size <= 100");
            Destroy(gameObject);
            return;
        }
        
        groupShader.Init(cellAmountSet);

        UIManager ui = GameManager.Instance.GetUIManager();

        BattleSimulator bs = GetComponent<BattleSimulator>();
        bs.Subscribe(Simulator.MessageType.BlockModify,
            (BaseMessage bm) =>
            {
                SetColor(((BlockModifyMessage) bm).modifiedLcoation, (bm.virus == 1 ? ui.virus1Color : ui.virus2Color));
            });

        bs.Subscribe(Simulator.MessageType.BlockExecuted,
            (BaseMessage bm) =>
            {
                SetColor(((BlockExecutedMessage) bm).modifiedLcoation,
                    (bm.virus == 1 ? ui.virus1ExecuteColor : ui.virus2ExecuteColor));
            });
    }

    public void SetColor(int index, Color color)
    {
        groupShader.SetColor(index, color);
    }
}