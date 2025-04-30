using System;

public interface IInputService
{
    public event Action<float> OnChangeHorizontalInput;
    public event Action OnTapUpButton;
}
