using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freyja
{
    [CreateAssetMenu(fileName = "Freyja Settings", menuName = "Freyja/Freyja Settings")]
    public class FreyjaSettings : ScriptableObject
    {
        private static FreyjaSettings _instance;
        
        public static FreyjaSettings Instance()
        {
            if (_instance is null)
            {
                _instance = Resources.Load("Freyja Settings") as FreyjaSettings;
            }

            return _instance;
        }
        
        [Header("Freyja Settings")]
        [SerializeField] private string freyjaDataFilePath = "C:\\Users\\sfuna\\Desktop\\.frd";
        public string FreyjaDataFilePath => freyjaDataFilePath;
        
        [Header("Frey Settings")]
        [SerializeField] private string logFilePath = "C:\\Users\\sfuna\\Desktop\\frey.log";
        public string LogFilePath => logFilePath;

        [SerializeField] private string encryptionKey = "sj2m9zs6";
        public string EncryptionKey => encryptionKey;

        [SerializeField] private string decryptionKey = "sj2m9zs6";
        public string DecryptionKey => decryptionKey;
    }
}