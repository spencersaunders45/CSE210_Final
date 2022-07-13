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
         Actor player = scene.GetFirstActor("player");
         List<Skeleton> allSkeletons = scene.GetAllActors<Skeleton>("skeleton");
         MoveTowardsPlayer(allSkeletons, player, scene);
      }

      private void MoveTowardsPlayer(List<Skeleton> allSkeletons, Actor palyer, Scene scene)
      {
         Vector2 playerLocation = palyer.GetPosition();
         for(int i = 0; i < allSkeletons.Count; i++)
         {
            // Get the skeleton and it's current position
            Skeleton targetSkele = allSkeletons[i];
            Vector2 currentPos = allSkeletons[i].GetPosition();
            // Get desired movement direction
            Vector2 targetDir = playerLocation - currentPos;
            // Normalize the vector
            targetDir = Vector2.Normalize(targetDir);
            
            // Move the skeleton
            targetSkele.Steer(targetDir);
            targetSkele.Move();
         }
      }

   }
}