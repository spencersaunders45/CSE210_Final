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
            Skeleton currentSkeleton = allSkeletons[i];
            Vector2 currentPos = allSkeletons[i].GetPosition();
            // Get desired movement direction
            Vector2 targetDir = playerLocation - currentPos;
            // Normalize the vector
            targetDir = Vector2.Normalize(targetDir);
            
            // Avoid overlapping with other skeletons
            Vector2 avoidDir = Vector2.Zero; // Default the avoiding direction to nothing, so that it does nothing if nothing needs to happen.
            foreach (Skeleton skeleton in allSkeletons)
            {
               // Don't check with self
               if(skeleton != currentSkeleton && skeleton.GetEnabled())
               {
                  // Get the position of the other skeleton
                  Vector2 otherSkeletonPosition = skeleton.GetPosition();
            
                  // Check distance
                  if (Vector2.Distance(otherSkeletonPosition, currentPos) < 14f)
                  {
                     // Set avoid dir
                     avoidDir = otherSkeletonPosition - currentPos; // With vectors, you can do targetPos - currentPos,
                                                                    // which will return a direction towards the target.

                     avoidDir = Vector2.Normalize(avoidDir);        // Normalizing it sets the values of x and y to be
                                                                    // between 0 and 1. This allows us to have more control
                                                                    // over the exact values we want.
                  }
               }
            }
            
            currentSkeleton.SetKnockback(Vector2.Lerp(currentSkeleton.GetKnockback(), Vector2.Zero, 0.25f));
            
            // Move the skeleton
            currentSkeleton.Steer((targetDir - avoidDir) - currentSkeleton.GetKnockback());
            currentSkeleton.Move();
         }
      }

   }
}