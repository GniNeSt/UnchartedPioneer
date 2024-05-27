using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefineEnums
{
    public enum CharDirection
    {
        DOWN        = 0,
        UP,
        LEFT,
        RIGHT
    }

    public enum CharActionState
    {
        Idle        = 0,
        Walk,
        Run,
        Attack,
        AttackWalk,

        Die         = 50
    }
}
