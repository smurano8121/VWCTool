using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class WebcamLive : MonoBehaviour {

	public int Width = 1920;
	public int Height = 1080;
	public int FPS = 30;
	public WebCamTexture webcamTexture;
	public Texture2D texture;

	void Start()
	{
		WebCamDevice[] devices = WebCamTexture.devices;
		// display all cameras
		for (var i = 0; i < devices.Length; i++)
		{
			Debug.Log(devices[i].name);
		}

		webcamTexture = new WebCamTexture(devices[0].name, Width, Height, FPS);
		GetComponent<Renderer>().material.mainTexture = webcamTexture;
		webcamTexture.Play();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.KeypadEnter)){
			webcamTexture.Stop();
			SceneManager.LoadScene("MainCamera");
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

	public void SetNumLock(bool bState) {
		if (GetNumLock() != bState) {
			keybd_event(VK_NUMLOCK, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYDOWN, 0);
			keybd_event(VK_NUMLOCK, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
		}
	}
}
