using UnityEngine;

public class Remove : MonoBehaviour
{

    private GameObject warrior;

    public void ChangeWarrior(GameObject newW)
    {
        warrior = newW;
    }

    public void RemoveWarrior()
    {
        if (!warrior) return;
        
        GameObject.Destroy(warrior);
    }
}
