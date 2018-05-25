using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class WebcamTexture : MonoBehaviour {

    //画像の縦横比・
    public int Width = 1920;
    public int Height = 1080;

    public Texture2D texture;
    public String before_url, after_url; //パス
    public String view_url; //画像がなかった場合に映写する風景画のパス
    string[] image_url; //表示されていない画像のパスを保存する

    DateTime image_old; //擬似窓に表示されていない画像の一番古いやつ
    DateTime image_new; //擬似窓に表示されていない画像
    String old_file; //古いファイル
    int num; //何番目が一番古いかを記録

    int state; //表示されていない画像があるかを管理


    //画像の保存先のパス
    void Start()
    {
        state = 0;
        before_url = "C://Users//smura//Desktop//VWコミュニケーションtool//VWCTool//album//before//";
        after_url = "C://Users//smura//Desktop//VWコミュニケーションtool//VWCTool//album//after//";
        view_url = "C://Users//smura//Desktop//VWコミュニケーションtool//VWCTool//album//view//";
    }

    // 画像をビット列に変換
    byte[] LoadBytes(string path)
    {
        FileStream fs = new FileStream(path, FileMode.Open);
        BinaryReader bin = new BinaryReader(fs);
        byte[] result = bin.ReadBytes((int)bin.BaseStream.Length);
        bin.Close();
        return result;
    }

    //投稿された画像あるいは風景画を表示
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            state = FileLog();
            if (state == 1) { EnviromentView(); }
            else
            {

                texture = new Texture2D(0, 0);
                texture.LoadImage(LoadBytes(image_url[num]));
                GetComponent<Renderer>().material.mainTexture = texture;
                System.IO.File.SetCreationTime(image_url[num], DateTime.Now);

            }
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene("WebcamLive");
        }
    }


    //擬似窓に表示されていない画像のパスを表示する
    public int FileLog()
    {
        num = 0; //初期化

        //擬似窓に表示されていない画像のパスを取得
        image_url = System.IO.Directory.GetFiles(before_url, "*", System.IO.SearchOption.TopDirectoryOnly);

        if (image_url.Length == 0) { return 1; }

        else
        {
            image_old = File.GetCreationTime(image_url[0]);

            for (int i = 0; i < image_url.Length; i++)
            {
                Debug.Log(image_url[i]);
                image_new = File.GetCreationTime(image_url[i]);

                if (image_new < image_old)
                {
                    image_old = image_new;
                    num = i;
                }
            }

            old_file = Path.GetFileName(image_url[num]); //最も古いファイルを記憶
            return 0;

        }
    }

    //擬似窓に表示されていない画像がない場合に環境映像を映す
    public void EnviromentView()
    {

        texture = new Texture2D(0, 0);
        texture.LoadImage(LoadBytes("" + view_url + "view_1.jpg"));
        GetComponent<Renderer>().material.mainTexture = texture;
        return;
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
