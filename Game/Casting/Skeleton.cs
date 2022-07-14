using System;
using System.Numerics;
using CSE210_Final.Game.Scripting;
using System.Timers;
using CSE210_Final.Game.Services;

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
      private Image _image;
      private Scene _scene;
      
      // When damaged
      private Vector2 _knockbackVector;
      private float _knockbackDistance;
      private string _damageSound;
      private string _deathSound;
      private ISettingsService _settingsService;
      private IAudioService _audioService;


      public Skeleton(float x, float y , Vector2 size, Color color, int health, Scene scene, IServiceFactory serviceFactory)
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
         _scene = scene;

         _settingsService = serviceFactory.GetSettingsService();
         _audioService = serviceFactory.GetAudioService();
         _damageSound = _settingsService.GetString("skeleton_damage_sound");
         _deathSound = _settingsService.GetString("skeleton_death_sound");
         
         _knockbackVector = Vector2.Zero;
         _knockbackDistance = 24;

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

   private void CheckHealth()
      {
         if (_health <= 0)
         {
            Disable();
            _audioService.PlaySound(_deathSound);
         }
         else
         {
            _audioService.PlaySound(_damageSound);
         }
      }

      public void DealDamage(int damage, Vector2 damagePos)
      {
         _health -= damage;
         
         // Set up knockback
         _knockbackVector = damagePos - GetCenter();
         _knockbackVector = Vector2.Normalize(_knockbackVector) * _knockbackDistance;
         
         Tint(Color.Red());

         CheckHealth();
      }

      public Vector2 GetKnockback()
      {
         return _knockbackVector;
      }

      public void SetKnockback(Vector2 newKnockback)
      {
         _knockbackVector = newKnockback;
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
         if(_health <= 0)
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