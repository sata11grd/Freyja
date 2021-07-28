using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Freyja.Demo.Demo5
{
public class DemoSceneManager : MonoBehaviour
{
    void Start() {
        byte[] hp = null;
        var input = 10;
        // byte配列に変換
        byte[] arr = System.Text.Encoding.UTF8.GetBytes(input.ToString());

        // AES暗号化サンプル処理
        AesEncryptSample(arr);

        //===================================
        int aesKeySize = 128;
        int aesBlockSize = 128;
        string aesIv = "1234567890123456";
        string aesKey = "1234567890123456";
        //===================================

        var tmp = AesEncrypt(arr, aesKeySize, aesBlockSize, aesIv, aesKey);
        var sw = new Stopwatch();
        sw.Start();
        for (var i = 0; i < 100000; ++i)
        {
            arr = AesDecrypt(tmp, aesKeySize, aesBlockSize, aesIv, aesKey);
        }
        Debug.Log(sw.Elapsed);
        Debug.Log(arr);
    }

    /// <summary>
    /// AES暗号化サンプル
    /// </summary>
    private void AesEncryptSample (byte[] arr) {
        // AES設定値
        //===================================
        int aesKeySize = 128;
        int aesBlockSize = 128;
        string aesIv = "1234567890123456";
        string aesKey = "1234567890123456";
        //===================================

        // AES暗号化
        byte[] arrEncrypted = AesEncrypt(arr, aesKeySize, aesBlockSize, aesIv, aesKey);

        // ファイル書き込み
        string path = System.IO.Path.Combine(Application.temporaryCachePath, "UserDataAES");
        System.IO.File.WriteAllBytes(path, arrEncrypted);

        // ファイル読み込み
        byte[] arrRead = System.IO.File.ReadAllBytes(path);

        // 復号化
        byte[] arrDecrypt = AesDecrypt(arrRead, aesKeySize, aesBlockSize, aesIv, aesKey);

        // byte配列を文字列に変換
        string decryptStr = System.Text.Encoding.UTF8.GetString(arrDecrypt);

        //Debug.Log("AES : " + decryptStr);
    }

    /// <summary>
    /// AES暗号化
    /// </summary>
    public byte[] AesEncrypt (byte[] byteText, int aesKeySize, int aesBlockSize, string aesIv, string aesKey) {
        // AESマネージャー取得
        var aes = GetAesManager(aesKeySize, aesBlockSize, aesIv, aesKey);
        // 暗号化
        byte[] encryptText = aes.CreateEncryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return encryptText;
    }

    /// <summary>
    /// AES復号化
    /// </summary>
    public byte[] AesDecrypt (byte[] byteText, int aesKeySize, int aesBlockSize, string aesIv, string aesKey) {
        // AESマネージャー取得
        var aes = GetAesManager(aesKeySize, aesBlockSize, aesIv, aesKey);
        // 復号化
        byte[] decryptText = aes.CreateDecryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return decryptText;
    }

    /// <summary>
    /// AesManagedを取得
    /// </summary>
    /// <param name="keySize">暗号化鍵の長さ</param>
    /// <param name="blockSize">ブロックサイズ</param>
    /// <param name="iv">初期化ベクトル(半角X文字（8bit * X = [keySize]bit))</param>
    /// <param name="key">暗号化鍵 (半X文字（8bit * X文字 = [keySize]bit）)</param>
    private AesManaged GetAesManager (int keySize, int blockSize, string iv, string key) {
        AesManaged aes = new AesManaged();
        aes.KeySize = keySize;
        aes.BlockSize = blockSize;
        aes.Mode = CipherMode.CBC;
        aes.IV = Encoding.UTF8.GetBytes(iv);
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.Padding = PaddingMode.PKCS7;
        return aes;
    }
}

}