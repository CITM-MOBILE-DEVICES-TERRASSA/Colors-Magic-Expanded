using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMELevelSelector : MonoBehaviour
{
    public List<Level> levels;

    public void SelectLevel(Level level)
    {
        Debug.Log("Selected Level: " + level.name);
    }
}
