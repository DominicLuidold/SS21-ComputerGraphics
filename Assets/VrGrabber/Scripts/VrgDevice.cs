using UnityEngine;

namespace VrGrabber
{
    public class Device {
        static IDevice _instance;
        public static IDevice instance {
            get {
                if (_instance == null) {
#if !UNITY_WSA
                    _instance = new VrgOculusTouchDevice();
                    Debug.Log("not wsa");
#elif UNITY_WSA
                    _instance = new VrgWinMRMotionControllerDevice();
                    Debug.Log("wsa");
#else
#error "Not implemented."
#endif

#if UNITY_EDITOR
                    _instance = new CustomControllerEmulator();
                    Debug.Log("editor");
#endif
                }
                return _instance;
            }
        }

        public Vector3 GetLocalPosition(ControllerSide side) {
            return _instance.GetLocalPosition(side);
        }

        public Quaternion GetLocalRotation(ControllerSide side) {
            return _instance.GetLocalRotation(side);
        }

        public bool GetHold(ControllerSide side) {
            return _instance.GetHold(side);
        }

        public bool GetRelease(ControllerSide side)
        {
            return _instance.GetRelease(side);
        }

        public bool GetHover(ControllerSide side) {
            return _instance.GetHover(side);
        }

        public bool GetClick(ControllerSide side) {
            return _instance.GetClick(side);
        }

        public Vector2 GetCoord(ControllerSide side) {
            return _instance.GetCoord(side);
        }
    }
}
