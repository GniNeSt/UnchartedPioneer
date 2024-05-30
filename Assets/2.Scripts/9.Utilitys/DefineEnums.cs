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
    public enum ExplosionType   //���� ����Ʈ ����
    {
        NonBreake        =0,
        Breake,
        Monster,
        Unique
    }
    public enum CharDirection   //ĳ���� �̵� ���� ->> �ִϸ��̼�
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
        Standard            = 0,    //���� close
        Timer,                      //�ڵ� close
        Scroll                      //Scroll Ÿ���� lerp ��õ
    }
    #endregion[UI]
}
