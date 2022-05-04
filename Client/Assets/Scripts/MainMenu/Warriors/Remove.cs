using UnityEngine;

public class Remove : MonoBehaviour
{

    private VirusState virus;
    
    public void ChangeVirus(VirusState newW)
    {
        virus = newW;
    }

    public void RemoveVirus()
    {
        if (!virus) return;

        virus.Reset();
        virus = null;
    }
}
