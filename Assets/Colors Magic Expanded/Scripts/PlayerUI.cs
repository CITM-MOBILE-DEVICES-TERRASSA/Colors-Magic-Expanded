using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public Player player;

    public void ResetPlayerColor()
    {
        if (player != null)
        {
            player.TakeDamage();
        }
    }
}
