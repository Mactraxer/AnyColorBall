using System;
using UnityEngine;

class KeyboardInputService : MonoBehaviour, IInputService
{
    public event Action<float> OnChangeHorizontalInput;
    public event Action OnTapDownLeftButton;
    public event Action OnTapUpLeftButton;
    public event Action OnTapDownRightButton;
    public event Action OnTapUpRightButton;
    public event Action OnTapUpButton;

    private void Update()
    {
        OnChangeHorizontalInput?.Invoke(Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnTapUpButton?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnTapDownLeftButton?.Invoke();
        } 
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            OnTapUpLeftButton?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnTapDownRightButton?.Invoke();
        } 
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            OnTapUpRightButton?.Invoke();
        }
    }
}
