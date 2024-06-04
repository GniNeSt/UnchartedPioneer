using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefineEnums;
namespace DefineUtility
{
    public static class PoolUtils
    {
        static int[] _ingamePrefabByFolder = { 2, 1, 1 };

        public static int GetCountIngamePrefab(IngameResourceFolderName name)
        {
            return _ingamePrefabByFolder[(int)name];
        }
    }
}
