using UnityEngine;
using Simulator;

public class MemoryGroup : MonoBehaviour
{
    [SerializeField] private MemoryGroupShader _groupShader;
    
    private Renderer _groupShaderR;

    public static int cellAmount;
    
    [SerializeField]
    private uint cellAmountSet = 8000;

    private void Awake()
    {
        _groupShaderR = _groupShader.GetComponent<Renderer>();
    }

    public void Start()
    {
        if (cellAmountSet < 100)
        {
            Debug.LogWarning("Bad Layout Setup, check for null cell || length <= 0 || size <= 100");
            Destroy(gameObject);
            return;
        }

        MemoryGroup.cellAmount = (int)cellAmountSet;

        _groupShader.Init(cellAmountSet);

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
        _groupShader.SetColor(index, color);
    }
}