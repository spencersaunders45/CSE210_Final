using System;
using System.Numerics;

namespace CSE210_Final.Game.Casting
{
   class Skeleton : Actor
   {

      public Skeleton(float x, float y , Vector2 size, Color color)
      {
         Tint(color);
         MoveTo(x, y);
         SizeTo(size);
      }
   }
}