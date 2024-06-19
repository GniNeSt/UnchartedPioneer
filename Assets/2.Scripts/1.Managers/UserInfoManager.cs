using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class UserInfoManager : MonoBehaviour
{
    static UserInfoManager _uniqueInstance;

    public float _bgmVolume{get; set;}
    public bool _bgmMute { get; set; }
    public float _sfxVolume { get; set; }
    public bool _sfxMute { get; set; }

    public static UserInfoManager _instance
    {
        get { return _uniqueInstance; }
    }
    private void Awake()
    {
        _uniqueInstance = this;
        DontDestroyOnLoad(gameObject);
        string path = Application.dataPath + "/userinfo.dat";
        InitSetData(path);
    }

    public void InitSetData(string readFile)
    {
        try
        {
            FileStream fs = new FileStream(readFile, FileMode.Open);
        }
        catch(FileNotFoundException ex)
        {
            _bgmVolume = _sfxVolume = 0.5f;
            _bgmMute = _sfxMute = false;
        }
    }
}
