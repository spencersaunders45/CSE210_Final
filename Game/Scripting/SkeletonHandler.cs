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
            Vector2 skeleton = allSkeletons[i].GetPosition();
            // Moves up
            if(playerLocation.X == skeleton.X && playerLocation.Y > skeleton.Y)
            {
               float x = skeleton.X;
               float y = skeleton.Y;
               allSkeletons[i].MoveTo(x, y+1);
            }
            // Moves down
            else if(playerLocation.X == skeleton.X && playerLocation.Y < skeleton.Y)
            {
               float x = skeleton.X;
               float y = skeleton.Y;
               allSkeletons[i].MoveTo(x, y-1);
            }
            // Moves left
            else if(playerLocation.X < skeleton.X && playerLocation.Y == skeleton.Y)
            {
               float x = skeleton.X;
               float y = skeleton.Y;
               allSkeletons[i].MoveTo(x-1, y);
            }
            //Moves right
            else if(playerLocation.X > skeleton.X && playerLocation.Y == skeleton.Y)
            {
               float x = skeleton.X;
               float y = skeleton.Y;
               allSkeletons[i].MoveTo(x+1, y);
            }
            // Move up-right
            else if(playerLocation.X > skeleton.X && playerLocation.Y > skeleton.Y)
            {
               float x = skeleton.X;
               float y = skeleton.Y;
               allSkeletons[i].MoveTo(x+1, y+1);
            }
            // Moves up-left
            else if(playerLocation.X < skeleton.X && playerLocation.Y > skeleton.Y)
            {
               float x = skeleton.X;
               float y = skeleton.Y;
               allSkeletons[i].MoveTo(x-1, y+1);
            }
            // Moves down-right
            else if(playerLocation.X < skeleton.X && playerLocation.Y < skeleton.Y)
            {
               float x = skeleton.X;
               float y = skeleton.Y;
               allSkeletons[i].MoveTo(x+1, y-1);
            }
            //Moves down-left
            else if(playerLocation.X > skeleton.X && playerLocation.Y < skeleton.Y)
            {
               float x = skeleton.X;
               float y = skeleton.Y;
               allSkeletons[i].MoveTo(x-1, y-1);
            }
         }
      }

   }
}