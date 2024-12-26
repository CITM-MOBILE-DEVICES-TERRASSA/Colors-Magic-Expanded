using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GameState (Abstract Base Class)
public abstract class GameState
{
    public abstract void ChangeState(GameManager manager);
}
