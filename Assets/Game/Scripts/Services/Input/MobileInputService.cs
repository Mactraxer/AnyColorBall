using System;
using UnityEngine;

public class MobileInputService : MonoBehaviour, IInputService
{
    public event Action OnTapDownLeftButton;
    public event Action OnTapUpLeftButton;
    public event Action OnTapDownRightButton;
    public event Action OnTapUpRightButton;
    public event Action OnTapUpButton;
}
