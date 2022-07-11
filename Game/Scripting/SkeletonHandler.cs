using CSE210_Final.Game.Casting;
using System.Numerics;

namespace CSE210_Final.Game.Scripting
{
   class SkeletonHandler
   {
      int skeletonCounter;
      List <int[]> spawnLocations = new List<int[]>();

      public SkeletonHandler()
      {
         skeletonCounter = 0;
         AddSpawnLocations();
      }

      private void AddSkeleton()
      {
         skeletonCounter++;
      }

      private void DecreaseSkeleton()
      {
         skeletonCounter--;
      }

      public int GetSkeletonCounter()
      {
         return skeletonCounter;
      }

      public List<int[]> GetSpawnLocation()
      { 
         return spawnLocations;
      }

      private void AddSpawnLocations()
      {
         spawnLocations.Add(new int[] {0,50});
         spawnLocations.Add(new int[] {20,50});
         spawnLocations.Add(new int[] {40,50});
         spawnLocations.Add(new int[] {60,50});
         spawnLocations.Add(new int[] {80,50});
         spawnLocations.Add(new int[] {100,50});
         spawnLocations.Add(new int[] {120,50});
         spawnLocations.Add(new int[] {140,50});
      }

      private void SpawnSkeleton(float x, float y , Vector2 size, Color color, int health)
      {
         Skeleton skeleton = new Skeleton(x, y, size, color, health);
         AddSkeleton();
      }

   }
}