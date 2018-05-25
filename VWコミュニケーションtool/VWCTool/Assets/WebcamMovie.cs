using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.IO;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Net;

public class WebcamMovie : MonoBehaviour {
    public int Width = 1920;
    public int Height = 1080;
    public int FPS = 30;
    public Texture2D texture;
    [SerializeField]
    public VideoPlayer videoPlayer;
    public VideoPlayer videoPlayer1;
    public int status;
    // Use this for initialization
    void Start()
    {
        status = 0;
        videoPlayer = gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
        GetComponent<Renderer>().material.mainTexture = videoPlayer.texture;
        videoPlayer1 = gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
        GetComponent<Renderer>().material.mainTexture = videoPlayer1.texture;
    }

    //画像のフレームレート・解像度を決める
    byte[] LoadBytes(string path)
    {
        FileStream fs = new FileStream(path, FileMode.Open);
        BinaryReader bin = new BinaryReader(fs);
        byte[] result = bin.ReadBytes((int)bin.BaseStream.Length);
        bin.Close();
        return result;
    }

	// Update is called once per frame
	void Update () {
        // Debug.Log(status);
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (status == 0)
            {
                videoPlayer.url = "file:///C://疑似窓/VirtualWindowAssets//video_01.mp4";
               // videoPlayer.Play();
               // videoPlayer.isLooping = true;
                status = 1;
            }
            else
            {
                videoPlayer.url = "file:///C://疑似窓/VirtualWindowAssets//video_01.mp4";
                videoPlayer.Play();
                videoPlayer1.isLooping = true;
                status = 0;
            }
            /*if (status == 0)
            {
                videoPlayer1.url = "file:///C://疑似窓/VirtualWindowAssets//video_01.mp4";
                //videoPlayer.url = "file:///C://疑似窓/VirtualWindowAssets//video_01.mp4";
                videoPlayer.Play();
                //videoPlayer1.Stop();
                videoPlayer.isLooping = true;
                status = 1;
            }
            else
            {
                videoPlayer.url = "file:///C://疑似窓/VirtualWindowAssets//video_01.mp4";
                videoPlayer1.url = "file:///C://疑似窓/VirtualWindowAssets//video_01.mp4";
                videoPlayer1.Play();
                videoPlayer.Stop();
                videoPlayer1.isLooping = true;
                status = 0;
            }*/
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (status == 0)
            {
                videoPlayer.url = "file:///C://疑似窓/VirtualWindowAssets//video_02.mp4";
               // videoPlayer.Play();
               // videoPlayer.isLooping = true;
                status = 1;
            }
            else
            {
                videoPlayer.url = "file:///C://疑似窓/VirtualWindowAssets//video_02.mp4";
                videoPlayer.Play();
                videoPlayer1.isLooping = true;
                status = 0;
            }
            /*if (status == 0)
            {
                videoPlayer1.url = "file:///C://疑似窓/VirtualWindowAssets//video_02.mp4";
                videoPlayer.url = "file:///C://疑似窓/VirtualWindowAssets//video_02.mp4";
                videoPlayer.Play();
                videoPlayer1.Stop();
                videoPlayer.isLooping = true;
                status = 1;
            }
            else
            {
                videoPlayer.url = "file:///C://疑似窓/VirtualWindowAssets//video_02.mp4";
                videoPlayer1.url = "file:///C://疑似窓/VirtualWindowAssets//video_02.mp4";
                videoPlayer1.Play();
                videoPlayer.Stop();
                videoPlayer1.isLooping = true;
                status = 0;
            }*/
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene("MainLive");
        }
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
    private static extern short GetKeyState(int keyCode);


    [DllImport("user32.dll")]
    private static extern int GetKeyboardState(byte[] lpKeyState);


    [DllImport("user32.dll", EntryPoint = "keybd_event")]
    private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);


    private const byte VK_NUMLOCK = 0x90; private const uint KEYEVENTF_EXTENDEDKEY = 1; private const int KEYEVENTF_KEYUP = 0x2; private const int KEYEVENTF_KEYDOWN = 0x0;

    public bool GetNumLock()
    {
        return (((ushort)GetKeyState(0x90)) & 0xffff) != 0;
    }

    public void SetNumLock(bool bState)
    {
        if (GetNumLock() != bState)
        {
            keybd_event(VK_NUMLOCK, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYDOWN, 0);
            keybd_event(VK_NUMLOCK, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
        }
    }
}
