using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefineEnums;
namespace DefineUtility
{
    public struct StageInfo
    {
        public int _id;
        public string _stageName;
        public StageMapName _map;
        public MonsterKindName[] _monsters;
        public StageInfo(int id, string stageName, StageMapName map, MonsterKindName[] monsters)
        {
            _id = id;
            _stageName = stageName;
            _map = map;
            _monsters = monsters;
        }
    }
    public struct ClearConditionInfo
    {
        public ClearType _Type;

        public int _endCount;
        public float _limitTime;

        public ClearConditionInfo(ClearType type, int count, float time)
        {
            _Type = type;
            _endCount = count;
            _limitTime = time;

        }
    }
    public static class PoolUtils
    {
        public const int _homeUIOffsetIndex = 1000;
        public static Vector2 _iconSizeInPopWindow = new Vector2(52,52);
        static int[] _ingamePrefabByFolder = { 2, 1, 1 };

        public static int GetCountIngamePrefab(IngameResourceFolderName name)
        {
            return _ingamePrefabByFolder[(int)name];
        }
    }
    //추가 스크립트
    public static class RarePrice
    {
        static float[] _MonterPrice = {1,1,2,3,4,5};
        public static float GetPriceOfMonPrefab(MonsterRank rank)
        {
            return _MonterPrice[(int)rank];
        }
    }
}
