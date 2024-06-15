using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefineEnums
{

    #region[Manager]
    public enum SceneType
    {
        HomeScene,
        IngameScene
    }
    public enum IngameState
    {
        Ready,
        Start,
        Play,
        PlayWait,
        End,
        Result
    }

    public enum IngameResourceFolderName
    {
        Characters = 0,
        Effects,
        Objects,

        count
    }
    public enum IngamePrefabName
    {
        PlayerObj,
        SlimeObj,

        Explosion,

        BulletObj
    }

    public enum UIWndName
    {
        TitleMessageBox,
        InfoMessageBox,
        MiniStatusBox,
        TimeBox,
        KillLogBox,
        VirtualPadBox,
        CountingPanel,
        CountInfoBox,
        ResultWindow,

        max1,

        StageButton     =1000,

        max2
    }

    public enum ClearType
    {
        KillCount,
        Survive,

        end
    }
    #endregion[Manager]

    #region[Object]
    public enum MonsterKindName
    {
        SlimeObj,
        WeakBatObj,

        ModifyAlienObj,



        Count
    }
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
        Hit,
        Die         = 50
    }
    public enum MonsterRank
    {
        Normal          =1,
        Rare,
        Elite,
        Unique,
        Boss
    }
    #endregion[Object]

    #region[UI]
    public enum MessageType
    {
        Standard            = 0,    //���� close
        Timer,                      //�ڵ� close
        Scroll,                      //Scroll Ÿ���� lerp ��õ
        Blink           = Scroll,
        Fade
    }
    #endregion[UI]
}
