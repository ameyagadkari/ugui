using UnityEngine;

namespace UGUI
{
    public class MainManager : MonoBehaviour
    {
        public static MainManager Instance
        {
            get
            {
                return _instance ?? (_instance = FindObjectOfType<MainManager>());
            }
        }
        private static MainManager _instance;

        public int CubeNumber
        {
            get
            {
                _cubeNumber++;
                return _cubeNumber;
            }
        }

        private int _cubeNumber;
        public int SphereNumber
        {
            get
            {
                _sphereNumber++;
                return _sphereNumber;
            }
        }

        private int _sphereNumber;
        public int CylinderNumber
        {
            get
            {
                _cylinderNumber++;
                return _cylinderNumber;
            }
        }

        private int _cylinderNumber;
        public int CapsuleNumber
        {
            get
            {
                _capsuleNumber++;
                return _capsuleNumber;
            }
        }

        private int _capsuleNumber;
    }

}

