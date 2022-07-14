using CSE210_Final.Game.Casting;
using CSE210_Final.Game.Services;

namespace CSE210_Final.Game.Scripting
{
   class EndSceneLoader
   {

      public EndSceneLoader()
      {
      }

      private Label EndScreneLine1()
      {
         Label Line1 = new Label();
         Line1.Display("Victory!");
         Line1.MoveTo(300, 200);
         Line1.Align(Label.Center);
         return Line1;
      }

      private Label EndScreneLine2()
      {
         Label Line2 = new Label();
         Line2.Display("Press 'enter' to restart!");
         Line2.MoveTo(300, 250);
         Line2.Align(Label.Center);
         return Line2;
      }

      public void EndScreen(Scene scene, Skeleton boss)
      {
         if(boss != null)
         {
            if(boss.GetHealth() <= 0)
            {
               Label endLine1 = EndScreneLine1();
               Label endLine2 = EndScreneLine2();
               scene.AddActor("label", endLine1);
               scene.AddActor("label", endLine2);
            }
         }
      }

   }
}