using JetBrains.Annotations;
using System;
using UnityEngine;

public class AnimationEventReporter : MonoBehaviour
{
    public event Action AttackStarted;
    public event Action AttackHappening;
    public event Action AttackEnded;

    [UsedImplicitly]
    public void OnAttackStarted()
    {
        AttackStarted?.Invoke();
    }

    [UsedImplicitly]
    public void OnAttack()
    {
        AttackHappening?.Invoke();
    }

    [UsedImplicitly]
    public void OnAttackEnded()
    {
        AttackEnded?.Invoke();
    }
}
