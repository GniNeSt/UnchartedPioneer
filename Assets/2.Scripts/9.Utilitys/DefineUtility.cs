using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefineEnums;
namespace DefineUtility
{
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
