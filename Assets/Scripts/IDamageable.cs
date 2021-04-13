using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void Kill();

    public void TakeDamage(float damage);
}
