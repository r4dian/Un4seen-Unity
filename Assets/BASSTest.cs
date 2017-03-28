using System;
using System.IO;
using UnityEngine;
using Un4seen.Bass;

public class BASSTest : MonoBehaviour
{
    private int m_handle; // Music's Handle 
    private SYNCPROC m_PosSync = new SYNCPROC(LogPattern);
    private SYNCPROC m_EndSync = new SYNCPROC(LogEnd);

    // Utils for packing params / unpacking data 
    private int MakeLong (short lowPart, short highPart) 
    {
        return (int)(((ushort)lowPart) | (uint)(highPart << 16));
    }
    private static short GetHiWord(int dword) 
    {
        return (short) (dword >> 16);
    }
    private static short GetLoWord(int dword) 
    {
        return (short) dword;
    }

    // Use this for initialization
    void Start()
    {
        if (Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
        {
            // m_handle = Bass.BASS_StreamCreateFile(Path.Combine(Application.dataPath, @"Audio\80s hex drums.mp3"), 0, 0, BASSFlag.BASS_DEFAULT);
            m_handle = Bass.BASS_MusicLoad(Path.Combine(Application.streamingAssetsPath, @"Audio/Placebo-Bionic.compat.it"), 0, 0, BASSFlag.BASS_DEFAULT, 0);

            if (m_handle != 0)
            {
                /* Set some sync listeners & their callbacks
                 * http://www.un4seen.com/doc/#bass/BASS_ChannelSetSync.html
                 * http://www.bass.radio42.com/help/html/b3003de9-624a-e621-6b9c-2304b4dfe02c.htm
                 */ 
                Bass.BASS_ChannelSetSync(m_handle, BASSSync.BASS_SYNC_MUSICPOS|BASSSync.BASS_SYNC_ONETIME, MakeLong(8-1,0), m_PosSync, IntPtr.Zero);
                Bass.BASS_ChannelSetSync(m_handle, BASSSync.BASS_SYNC_MUSICPOS|BASSSync.BASS_SYNC_ONETIME, MakeLong(12-1,0), m_PosSync, IntPtr.Zero);
                Bass.BASS_ChannelSetSync(m_handle, BASSSync.BASS_SYNC_MUSICPOS|BASSSync.BASS_SYNC_ONETIME, MakeLong(16-1,0), m_PosSync, IntPtr.Zero);
                Bass.BASS_ChannelSetSync(m_handle, BASSSync.BASS_SYNC_END|BASSSync.BASS_SYNC_ONETIME, 0, m_EndSync, IntPtr.Zero);
                // Start Playing
                Bass.BASS_ChannelPlay(m_handle, true);
                Debug.Log("Should be playing");
            }
            else
            {
                Debug.Log("File error");
            }
        }
    }

	private static void LogPattern(int handle, int channel, int data, IntPtr user)
    {
        Debug.Log("Order: "+GetLoWord(data)+" Row: "+GetHiWord(data)+" reached");
    }
	private static void LogEnd(int handle, int channel, int data, IntPtr user)
    {
        Debug.Log("Song Ended");
    }

    // Update is called once per frame
	void Update () 
    {
	}

    void OnDestroy()
    {
        Bass.BASS_MusicFree(m_handle);
        Bass.BASS_Free();
    }
}
