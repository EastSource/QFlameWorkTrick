using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class abstractTrap: MonoBehaviour
{
    //初期位置
    protected Vector3 fixedPosition;
    //移動先の位置
    protected Vector3 desticationPosition;

    public abstract void Move();
    
    //リロードされた際の処理関数
    public abstract void Restart();
}
