﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LuaInterface;
using System;
using UnityEngine.Networking;

//click Lua/Build lua bundle
public class TestABLoader : MonoBehaviour 
{
    int bundleCount = int.MaxValue;
    string tips = null;

    IEnumerator CoLoadBundle(string name, string path)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(path))
        {
            if (www == null)
            {
                Debugger.LogError(name + " bundle not exists");
                yield break;
            }

            yield return www.SendWebRequest();

            if (www.error != null)
            {
                Debugger.LogError(string.Format("Read {0} failed: {1}", path, www.error));
                yield break;
            }

            --bundleCount;
            AssetBundle ab = (www.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
            LuaFileUtils.Instance.AddSearchBundle(name, ab);
            www.Dispose();
        }                     
    }

    IEnumerator LoadFinished()
    {
        while (bundleCount > 0)
        {
            yield return null;
        }

        OnBundleLoad();
    }

    public IEnumerator LoadBundles()
    {
        string streamingPath = Application.streamingAssetsPath.Replace('\\', '/');

#if UNITY_5 || UNITY_2017 || UNITY_2018 || UNITY_2019
#if UNITY_ANDROID && !UNITY_EDITOR
        string main = streamingPath + "/" + LuaConst.osDir + "/" + LuaConst.osDir;
#else
        string main = "file:///" + streamingPath + "/" + LuaConst.osDir + "/" + LuaConst.osDir;
#endif
        UnityWebRequest www = new UnityWebRequest(main);
        yield return www.SendWebRequest();

        AssetBundle ab = (www.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
        AssetBundleManifest manifest = (AssetBundleManifest)ab.LoadAsset("AssetBundleManifest");
        List<string> list = new List<string>(manifest.GetAllAssetBundles());        
#else
        //此处应该配表获取
        List<string> list = new List<string>() { "lua.unity3d", "lua_cjson.unity3d", "lua_system.unity3d", "lua_unityengine.unity3d", "lua_protobuf.unity3d", "lua_misc.unity3d", "lua_socket.unity3d", "lua_system_reflection.unity3d" };
#endif
        bundleCount = list.Count;

        for (int i = 0; i < list.Count; i++)
        {
            string str = list[i];

#if UNITY_ANDROID && !UNITY_EDITOR
            string path = streamingPath + "/" + LuaConst.osDir + "/" + str;
#else
            string path = "file:///" + streamingPath + "/" + LuaConst.osDir + "/" + str;
#endif
            string name = Path.GetFileNameWithoutExtension(str);
            StartCoroutine(CoLoadBundle(name, path));            
        }

        yield return StartCoroutine(LoadFinished());
    }

    void Awake()
    {
        Application.logMessageReceived += ShowTips;
        LuaFileUtils file = new LuaFileUtils();
        file.beZip = true;
#if UNITY_ANDROID && UNITY_EDITOR
        if (IntPtr.Size == 8)
        {
            throw new Exception("can't run this in unity5.x process for 64 bits, switch to pc platform, or run it in android mobile");
        }
#endif
        StartCoroutine(LoadBundles());
    }

    void ShowTips(string msg, string stackTrace, LogType type)
    {
        tips += msg;
        tips += "\r\n";
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 150, 400, 300), tips);
    }

    void OnApplicationQuit()
    {
        Application.logMessageReceived -= ShowTips;
    }

    void OnBundleLoad()
    {                
        LuaState state = new LuaState();
        state.Start();
        state.DoString("print('hello tolua#:'..tostring(Vector3.zero))");
        state.DoFile("Main.lua");
        LuaFunction func = state.GetFunction("Main");
        func.Call();
        func.Dispose();
        state.Dispose();
        state = null;
	}	
}
