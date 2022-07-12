using System;
using System.Numerics;
using System.Timers;

namespace CSE210_Final.Game.Casting
{
   class Skeleton : Actor
   {
      bool _disabled;
      float _startX;
      float _startY;
      int _startHealth;
      private static System.Timers.Timer checkDeath;
      private static System.Timers.Timer respawnTimer;

      public Skeleton(float x, float y , Vector2 size, Color color, int health)
      {
         Tint(color);
         MoveTo(x, y);
         SizeTo(size);
         _health = health;
         _startHealth = health;
         _disabled = false;
         _startX = x;
         _startY = y;
         RespawnTimer();
      }

      private void CheckHealth()
      {
         if (_health == 0)
         {
            _disabled = true;
         }
      }

      public void DealDamage(int damage)
      {
         _health -= damage;
         CheckHealth();
      }

      private void RespawnTimer()
      {
         // Runs every 1 seconds
         checkDeath = new System.Timers.Timer(1000);
         checkDeath.Elapsed += StartRespawn;
         checkDeath.AutoReset = true;
         checkDeath.Enabled = true;
      }

      private void StartRespawn(Object source, ElapsedEventArgs e)
      {
         if(_health == 0)
         {
            checkDeath.Enabled = false;
            // Runs every 30 seconds
            respawnTimer = new System.Timers.Timer(30000);
            respawnTimer.Elapsed += Respawn;
            respawnTimer.AutoReset = true;
            respawnTimer.Enabled = true;
         }
      }

      private void Respawn(Object source, ElapsedEventArgs e)
      {
         MoveTo(_startX, _startY);
         _health = _startHealth;
         respawnTimer.Enabled = false;
         checkDeath.Enabled = true;
      }
   }
}