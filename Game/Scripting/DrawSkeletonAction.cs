﻿using System.Numerics;
using CSE210_Final.Game.Casting;
using CSE210_Final.Game.Services;

namespace CSE210_Final.Game.Scripting;

public class DrawSkeletonAction : Action
{
    private List<Skeleton> _skeletons;
    private Skeleton _boss;
    private string[] _idle = new string[6];
    private string[] _run = new string[6];
    private string[] _death = new string[6];
    private IVideoService _videoService;

    public DrawSkeletonAction(IServiceFactory serviceFactory)
    {
        _videoService = serviceFactory.GetVideoService();
    }
    
    public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
    {
        _skeletons = scene.GetAllActors<Skeleton>("skeleton");
        _boss = scene.GetFirstActor<Skeleton>("boss");
        Camera camera = scene.GetFirstActor<Camera>("camera");

        _idle[0] = "Assets/Images/Skeleton/Idle/Skeleton_Idle_0.png";
        _idle[1] = "Assets/Images/Skeleton/Idle/Skeleton_Idle_1.png";
        _idle[2] = "Assets/Images/Skeleton/Idle/Skeleton_Idle_2.png";
        _idle[3] = "Assets/Images/Skeleton/Idle/Skeleton_Idle_3.png";
        _idle[4] = "Assets/Images/Skeleton/Idle/Skeleton_Idle_4.png";
        _idle[5] = "Assets/Images/Skeleton/Idle/Skeleton_Idle_5.png";

        _run[0] = "Assets/Images/Skeleton/Run/Skeleton_Run_0.png";
        _run[1] = "Assets/Images/Skeleton/Run/Skeleton_Run_1.png";
        _run[2] = "Assets/Images/Skeleton/Run/Skeleton_Run_2.png";
        _run[3] = "Assets/Images/Skeleton/Run/Skeleton_Run_3.png";
        _run[4] = "Assets/Images/Skeleton/Run/Skeleton_Run_4.png";
        _run[5] = "Assets/Images/Skeleton/Run/Skeleton_Run_5.png";

        _death[0] = "Assets/Images/Skeleton/Death/Skeleton_Death_0.png";
        _death[1] = "Assets/Images/Skeleton/Death/Skeleton_Death_1.png";
        _death[2] = "Assets/Images/Skeleton/Death/Skeleton_Death_2.png";
        _death[3] = "Assets/Images/Skeleton/Death/Skeleton_Death_3.png";
        _death[4] = "Assets/Images/Skeleton/Death/Skeleton_Death_4.png";
        _death[5] = "Assets/Images/Skeleton/Death/Skeleton_Death_4.png";
        
        

        foreach (Skeleton skeleton in _skeletons)
        {
            if(skeleton.GetEnabled())
            {
                skeleton.SetDeathFirstFrame(true);
                skeleton.UpdateImage();
                if(skeleton.aggro)
                    skeleton.GetImage().Animate(_run, 0.5f, 60);
                else
                    skeleton.GetImage().Animate(_idle, 0.5f, 60);
            }
            
            if(!skeleton.GetEnabled())
            {
                if (skeleton.GetDeathFirstFrame())
                {
                    skeleton.SetDeathFirstFrame(false);
                    //skeleton.UpdateImage();
                    Console.WriteLine("Skeleton DEAD!!");
                    skeleton.GetImage().Animate(_death, 1f, 60, false);
                    skeleton.GetImage().ResetFrame();
                }
                skeleton.Tint(skeleton.LerpColor(skeleton.GetTint(), Color.Clear(), 0.75f));
            }
            
            if (skeleton.GetTint() != Color.White() && skeleton.GetEnabled())
            {
                skeleton.Tint(skeleton.LerpColor(skeleton.GetTint(), Color.White(), 0.15f));
            }
            
            _videoService.Draw(skeleton.GetImage(), camera, skeleton.IsMovingRight());
        }
        
        if(_boss != null)
        {
            _boss.UpdateImage();
            _boss.GetImage().Animate(_run, 0.5f, 60);
            _videoService.Draw(_boss.GetImage(), camera);
        }
    }
}