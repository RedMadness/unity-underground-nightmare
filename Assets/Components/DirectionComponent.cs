using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DirectionComponent", menuName = "Components/DirectionComponent")]
public class DirectionComponent : ScriptableObject
{
    [SerializeField] private Vector2 _direction;

    public event Action<Vector2> DirectionChanged;

    public Vector2 Direction
    {
        get => _direction;
        set
        {
            if (_direction != value)
            {
                _direction = value;
                DirectionChanged?.Invoke(_direction);
            }
        }
    }
}
