using System;
using System.Numerics;

namespace CSE210_Final.Game.Casting
{
   class Skeleton : Actor
   {
      bool _disabled;

      public Skeleton(float x, float y , Vector2 size, Color color, int health)
      {
         Tint(color);
         MoveTo(x, y);
         SizeTo(size);
         _health = health;
         _disabled = false;
      }

      private void CheckDeath()
      {
         if (_health == 0)
         {
            _disabled = true;
         }
      }
   }
}