using System.IO;
using UnityEngine;

namespace UGUI
{
    public class Manager : MonoBehaviour
    {
        public string Username { get; set; }
        public enum SceneNames { Login, Main }
        public static Manager Instance
        {
            get { return _instance ?? (_instance = FindObjectOfType<Manager>()); }
        }
        private static Manager _instance;
        private StreamWriter _streamWriter;
        private string _currentDirectory;
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _currentDirectory = Directory.GetCurrentDirectory();
            if (!Directory.Exists(_currentDirectory + "\\Logging"))
            {
                Directory.CreateDirectory(_currentDirectory + "\\Logging");
            }
        }

        private void OnDestroy()
        {
            if (_streamWriter == null) return;
            if (ContentManager.Instance.CurrentlySelected != null)
            {
                Instance.WriteToFile("}");
            }
            _streamWriter.Flush();
            _streamWriter.Close();
        }

        public void WriteToFile(string data)
        {
            if (_streamWriter == null)
            {
                _streamWriter = new StreamWriter(_currentDirectory + "\\Logging\\" + Username + ".txt", true);
            }
            _streamWriter.WriteLine(data);
        }
    }
}