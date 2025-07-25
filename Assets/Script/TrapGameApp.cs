using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class TrapGameApp : Architecture<TrapGameApp>
{
    protected override void Init()
    {
        this.RegisterModel(new PlayerModel());
    }
}
