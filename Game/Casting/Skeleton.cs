using System;
using System.Numerics;

namespace CSE210_Final.Game.Casting
{
   class Skeleton : Actor
   {

      public Skeleton(Vector2 pos, Vector2 size, Color color)
      {
         Tint(color);
         MoveTo(pos);
         SizeTo(size);
      }
   }
}