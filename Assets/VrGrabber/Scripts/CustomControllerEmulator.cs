using UnityEngine;

namespace VrGrabber
{
    //class is same as VrgOculusTouchDevice but for unity editor using button A for Grab, S for pulling grabbed object and D for pushing grabbed object
    public class CustomControllerEmulator : IDevice
    {
        private OVRInput.Controller GetOVRController(ControllerSide side)
        {
            return (side == ControllerSide.Left) ?
                OVRInput.Controller.LTouch :
                OVRInput.Controller.RTouch;
        }

        public Vector3 GetLocalPosition(ControllerSide side)
        {
            return OVRInput.GetLocalControllerPosition(GetOVRController(side));
        }

        public Quaternion GetLocalRotation(ControllerSide side)
        {
            return OVRInput.GetLocalControllerRotation(GetOVRController(side));
        }

        public bool GetHold(ControllerSide side)
        {
            return Input.GetKey(KeyCode.Space);
        }

        public bool GetRelease(ControllerSide side)
        {
            return Input.GetKeyUp(KeyCode.Space);
        }

        public bool GetHover(ControllerSide side)
        {
            return Input.GetKeyUp(KeyCode.Space);
        }

        public bool GetClick(ControllerSide side)
        {
            return Input.GetKey(KeyCode.Y) || Input.GetKey(KeyCode.X);
        }

        public Vector2 GetCoord(ControllerSide side)
        {
            if(Input.GetKey(KeyCode.Y))
            {
                return new Vector2(0f, -1.0f);
            } else if(Input.GetKey(KeyCode.X))
            {
                return new Vector2(0f, 1.0f);
            }
            return new Vector2(0,0);
        }
    }
}

