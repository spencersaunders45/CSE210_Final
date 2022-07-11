using CSE210_Final.Game.Casting;
using System.Numerics;

namespace CSE210_Final.Game.Scripting
{
   class SkeletonHandler : Action
   {

      public SkeletonHandler()
      {
         
      }

      public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
      {
         List<Skeleton> allSkeletons = scene.GetAllActors<Skeleton>("skeleton");
      }

   }
}