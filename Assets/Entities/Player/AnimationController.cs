using UnityEditor.Animations;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField, Tooltip("Компонент направления")]
    private DirectionComponent directionComponent;

    [SerializeField, Tooltip("Анимации")]
    private Animator animator;

    [SerializeField, Tooltip("Текущий спрайт")]
    private SpriteRenderer sprite;

    public void OnEnable()
    {
        directionComponent.DirectionChanged += OnDirectionChanged;
    }

    public void OnDisable()
    {
        directionComponent.DirectionChanged -= OnDirectionChanged;
    }


    // Update is called once per frame
    private void OnDirectionChanged(Vector2 direction)
    {
        bool isMoving = direction != Vector2.zero;
        animator.SetBool("isMoving", isMoving);

        if (direction.x < 0)
        {
            sprite.flipX = true;
        }
        else if (direction.x > 0)
        {
            sprite.flipX = false;
        }
    }
}
