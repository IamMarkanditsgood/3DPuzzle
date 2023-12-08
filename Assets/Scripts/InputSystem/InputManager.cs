using System;
using UnityEngine;

namespace InputSystem
{
    public class InputManager
    {
        public event Action OnButtonPressed;
        public event Action OnButtonUp;
        
        public void CheckPressedKey()
        {
            if (Input.GetMouseButton(0))
            {
                OnButtonPressed?.Invoke();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                OnButtonUp?.Invoke();
            }
        }
    }
}