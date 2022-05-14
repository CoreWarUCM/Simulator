using UnityEngine;

public class FadeAnim : MonoBehaviour
{
    [Tooltip("Referencia al Animator del efecto fade_in-out")] [SerializeField]
    private Animator fade;
    
    [SerializeField]
    private Loader loader;

    private int _levelToLoad;
    private static readonly int FadeOut = Animator.StringToHash("FadeOut");

    public void FadeToLevel(int levelIndex)
    {
        _levelToLoad = levelIndex;
        fade.SetTrigger(FadeOut);
    }

    public void OnFadeComplete()
    {
        Loader.LoadScene(_levelToLoad);
    }
}