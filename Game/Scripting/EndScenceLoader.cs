using CSE210_Final.Game.Casting;
using CSE210_Final.Game.Services;

namespace CSE210_Final.Game.Scripting
{
   class EndSceneLoader
   {

      public EndSceneLoader()
      {
      }

      private Label WinLine1()
      {
         Label Line1 = new Label();
         Line1.Display("Victory!");
         Line1.MoveTo(300, 200);
         Line1.Align(Label.Center);
         return Line1;
      }

      private Label LoseLine1()
      {
         Label Line1 = new Label();
         Line1.Display("You Died");
         Line1.MoveTo(300, 200);
         Line1.Align(Label.Center);
         return Line1;
      }

      private Label Line2()
      {
         Label Line2 = new Label();
         Line2.Display("Press 'enter' to restart!");
         Line2.MoveTo(300, 250);
         Line2.Align(Label.Center);
         return Line2;
      }

      public void EndScreen(Scene scene, Skeleton boss, Actor player)
      {
         if(boss != null && player != null)
         {
            if(boss.GetHealth() <= 0 && player.GetHealth() > 0)
            {
               Label winLine1 = WinLine1();
               Label line2 = Line2();
               scene.AddActor("label", winLine1);
               scene.AddActor("label", line2);
            }
            
            if(player.GetHealth() <= 0 && boss.GetHealth() > 0)
            {
               Label loseLine1 = LoseLine1();
               Label line2 = Line2();
               scene.AddActor("label", loseLine1);
               scene.AddActor("label", line2);
            }
         }
      }

   }
}