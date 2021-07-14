using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freyja
{
    [CreateAssetMenu(fileName = "Freyja Settings", menuName = "Freyja/Freyja Settings")]
    public class FreyjaSettings : ScriptableObject
    {
        /// <summary>
        /// The singleton instance.
        /// </summary>
        private static FreyjaSettings _instance;
        
        /// <summary>
        /// It returns singleton instance.
        /// </summary>
        /// <returns></returns>
        public static FreyjaSettings Instance()
        {
            if (_instance is null)
            {
                _instance = Resources.Load("Freyja Settings") as FreyjaSettings;
            }

            if (_instance is null)
            {
                Debug.LogError("Failed to load Freyja Settings from resources.");
            }

            return _instance;
        }
        
        /// <summary>
        /// The file path which freyja data will be saved.
        /// </summary>
        [Header("Freyja Settings")]
        [SerializeField] private string freyjaDataFilePath = "C:\\Users\\sfuna\\Desktop\\.frd";
        public string FreyjaDataFilePath => freyjaDataFilePath;
        
        /// <summary>
        /// The file path which the log file will be saved.
        /// </summary>
        [Header("Frey Settings")]
        [SerializeField] private string logFilePath = "C:\\Users\\sfuna\\Desktop\\frey.log";
        public string LogFilePath => logFilePath;

        /// <summary>
        /// The key used for encryption.
        /// </summary>
        [SerializeField] private string encryptionKey = "sj2m9zs6";
        public string EncryptionKey => encryptionKey;

        /// <summary>
        /// The key used for decryption.
        /// </summary>
        [SerializeField] private string decryptionKey = "sj2m9zs6";
        public string DecryptionKey => decryptionKey;
    }
}