using UnityEngine;

namespace DefaultNamespace
{
    public class DPMLogger
    {
        private string _class;
        private string _color;

        public DPMLogger(string @class, string color)
        {
            _class = @class;
            _color = color;
        }

        public void Log(string message)
        {
            Debug.Log($"<color={_color}>{_class}</color> => {message}");
        }
        
        public void Warn(string message)
        {
            Debug.LogWarning($"<color={_color}>{_class}</color> => {message}");
        }
        
        public void Error(string message)
        {
            Debug.LogError($"<color={_color}>{_class}</color> => {message}");
        }
    }
}