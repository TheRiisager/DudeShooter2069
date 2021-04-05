using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
   void Kill();

   void TakeDamage(float damage);
}
