using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
   public void LoadWarrior(int player)
   {
      GameManager.instance.LoadWarrior(player);
   }
}
