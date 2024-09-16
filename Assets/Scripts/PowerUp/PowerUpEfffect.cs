using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpEfffect : ScriptableObject
{
    public abstract void Apply(GameObject target);
}
