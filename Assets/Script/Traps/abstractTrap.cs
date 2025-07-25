using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class abstractTrap: MonoBehaviour
{
    protected Vector3 fixedPosition;
    protected Vector3 desticationPosition;

    public abstract void Move();
    public abstract void Restart();
}
