using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using ManagedBass;

public class BASSTest : MonoBehaviour
{
    private int m_handle;
    private SyncProcedure m_PosSync = new SyncProcedure(LogPattern);
    private SyncProcedure m_EndSync = new SyncProcedure(LogEnd);

    // Utils for packing params / unpacking data 
    private int MakeLong(short lowPart, short highPart)
    {
        return (int)(((ushort)lowPart) | (uint)(highPart << 16));
    }
    private static short GetHiWord(int dword)
    {
        return (short)(dword >> 16);
    }
    private static short GetLoWord(int dword)
    {
        return (short)dword;
    }


    // Start is called before the first frame update
    void Start()
    {
        Bass.Init();
        m_handle = Bass.MusicLoad(Path.Combine(Application.streamingAssetsPath, @"Audio/Placebo-Bionic.compat.it"), 0, 0, BassFlags.Default, 0);

        if (m_handle != 0)
        {
            /* Set some sync listeners & their callbacks
             * http://www.un4seen.com/doc/#bass/BASS_ChannelSetSync.html
             * http://www.bass.radio42.com/help/html/b3003de9-624a-e621-6b9c-2304b4dfe02c.htm
             */
            Bass.ChannelSetSync(m_handle, SyncFlags.Position | SyncFlags.Onetime, MakeLong(8 - 1, 0), m_PosSync, IntPtr.Zero);
            Bass.ChannelSetSync(m_handle, SyncFlags.Position | SyncFlags.Onetime, MakeLong(12 - 1, 0), m_PosSync, IntPtr.Zero);
            Bass.ChannelSetSync(m_handle, SyncFlags.Position | SyncFlags.Onetime, MakeLong(16 - 1, 0), m_PosSync, IntPtr.Zero);
            Bass.ChannelSetSync(m_handle, SyncFlags.End | SyncFlags.Onetime, 0, m_EndSync, IntPtr.Zero);

            // Start Playing
            Bass.ChannelPlay(m_handle, true);
            Debug.Log("Should be playing");
        }
        else
        {
            Debug.Log("File error");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private static void LogPattern(int handle, int channel, int data, IntPtr user)
    {
        Debug.Log("Order: " + GetLoWord(data) + " Row: " + GetHiWord(data) + " reached");
    }
    private static void LogEnd(int handle, int channel, int data, IntPtr user)
    {
        Debug.Log("Song Ended");
    }

    void OnDestroy()
    {
        Bass.MusicFree(m_handle);
        Bass.Free();
    }
}
