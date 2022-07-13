using System;
using System.Numerics;
using CSE210_Final.Game.Scripting;

namespace CSE210_Final.Game.Casting
{
   class Skeleton : Actor
   {

      private Image _image;
      private Scene _scene;
      
      public Skeleton(float x, float y , Vector2 size, Color color, Scene scene)
      {
         Tint(color);
         MoveTo(x, y);
         SizeTo(size);
         _scene = scene;
      }

      public void UpdateImage()
      {
         if (_image == null)
         {
            _image = new Image();
            _scene.AddActor("skeleton-image", _image);
         }
         
         _image.MoveTo(GetPosition());
         _image.SizeTo(GetSize());
         _image.Tint(GetTint());
      }

      public Image GetImage()
      {
         return _image;
      }
   }
}