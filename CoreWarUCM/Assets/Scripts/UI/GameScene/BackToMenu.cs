using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour
{
    public void Back()
    {
        GameManager.Instance.BackToMenu();
    }
}
