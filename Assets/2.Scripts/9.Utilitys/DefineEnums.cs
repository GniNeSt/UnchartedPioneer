using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefineEnums
{
    #region[Manager]
    public enum IngameState
    {
        Ready,
        Start,
        Play,
        End,
        Result
    }
    #endregion[Manager]

    #region[Object]
    public enum ExplosionType   //폭발 이펙트 종류
    {
        NonBreake        =0,
        Breake,
        Monster,
        Unique
    }
    public enum CharDirection   //캐릭터 이동 방향 ->> 애니메이션
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

    #endregion[Object]

    #region[UI]
    public enum MessageType
    {
        Standard            = 0,    //수동 close
        Timer,                      //자동 close
        Scroll                      //Scroll 타입은 lerp 추천
    }
    #endregion[UI]
}
