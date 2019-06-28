using System;
using System.Collections.Generic;
using System.Linq;
using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Math.Core;
using Fusee.Serialization;
using Fusee.Xene;
using static System.Math;
using static Fusee.Engine.Core.Input;
using static Fusee.Engine.Core.Time;

namespace Fusee.Tutorial.Core
{
    public class AssetsPicking : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRenderer _sceneRenderer;
        private ScenePicker _scenePicker;
        private PickResult _currentPick;
        private float3 _oldColor;
        private float _velocity;
        private float _direction;
        private TransformComponent _frontAxleTransform;
        private TransformComponent _frontRWheelTransform;
        private TransformComponent _frontLWheelTransform;
        private TransformComponent _rearAxleTransform;
        private TransformComponent _rearRWheelTransform;
        private TransformComponent _rearLWheelTransform;
        private TransformComponent _bodyTransform;
        private TransformComponent _gunTransform;
        private TransformComponent _turretTransform;

        private bool _turret = false;
        private bool _gun = false;
        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to white (100% intensity in all color channels R, G, B, A).
            RC.ClearColor = new float4(0.8f, 0.9f, 0.7f, 1);
            _scene = AssetStorage.Get<SceneContainer>("CubeCar.fus");
            _frontAxleTransform = _scene.Children.FindNodes(node => node.Name=="FrontAxle")?.FirstOrDefault()?.GetTransform();
            _frontRWheelTransform = _scene.Children.FindNodes(node => node.Name=="FrontRightWheel")?.FirstOrDefault()?.GetTransform();
            _frontLWheelTransform = _scene.Children.FindNodes(node => node.Name=="FrontLeftWheel")?.FirstOrDefault()?.GetTransform();
            _rearAxleTransform = _scene.Children.FindNodes(node => node.Name=="RearAxle")?.FirstOrDefault()?.GetTransform();
            _rearRWheelTransform = _scene.Children.FindNodes(node => node.Name=="RearRightWheel")?.FirstOrDefault()?.GetTransform();
            _rearLWheelTransform = _scene.Children.FindNodes(node => node.Name=="RearLeftWheel")?.FirstOrDefault()?.GetTransform();
            _bodyTransform = _scene.Children.FindNodes(node => node.Name == "Body")?.FirstOrDefault()?.GetTransform();
            _turretTransform = _scene.Children.FindNodes(node => node.Name == "Turret")?.FirstOrDefault()?.GetTransform();
            _gunTransform = _scene.Children.FindNodes(node => node.Name == "Gun")?.FirstOrDefault()?.GetTransform();
            _gunTransform.Rotation.z = 1.5f;
            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRenderer(_scene);
            _scenePicker = new ScenePicker(_scene);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            if (Mouse.LeftButton)
            {
                float2 pickPosClip = Mouse.Position * new float2(2.0f / Width, -2.0f / Height) + new float2(-1, 1);
                _scenePicker.View = RC.View;
                _scenePicker.Projection = RC.Projection;
                List<PickResult> pickResults = _scenePicker.Pick(pickPosClip).ToList();
                
                PickResult newPick = null;
                if (pickResults.Count > 0)
                {
                    pickResults.Sort((a, b) => Sign(a.ClipPos.z - b.ClipPos.z));
                    newPick = pickResults[0];
                }
                if (newPick?.Node != _currentPick?.Node)
                {
                    if (_currentPick != null)
                    {
                        _currentPick.Node.GetComponent<ShaderEffectComponent>().Effect.SetEffectParam("DiffuseColor",_oldColor);
                    }
                    if (newPick != null)
                    {
                        
                        _oldColor = (float3) newPick.Node.GetComponent<ShaderEffectComponent>().Effect.GetEffectParam("DiffuseColor");
                        newPick.Node.GetComponent<ShaderEffectComponent>().Effect.SetEffectParam("DiffuseColor", new float3(_oldColor.r*0.5f,_oldColor.g*0.5f,_oldColor.b*0.5f));
                        switch(newPick.Node.Name){
                            case "Turret":
                                _turret = true;
                                _gun = false;
                                break;
                            case "Gun":
                                _gun = true;
                                _turret = false;
                                break;
                            default:
                                _gun = false;
                                _turret = false;
                                break;
                        }

                    }
                    _currentPick = newPick;
                }
                

            }

            if(Keyboard.WSAxis !=0){
                _velocity  = (_velocity - Keyboard.WSAxis)%30;
                _rearAxleTransform.Rotation.z  = _velocity;
                _frontAxleTransform.Rotation.z = _velocity;
 
            } else {
                _velocity *= 0f;
                _rearAxleTransform.Rotation.z  = _velocity;
                _frontAxleTransform.Rotation.z = _velocity;

            }
            if(_gun)
                _gunTransform.Rotation.z = limit(_gunTransform.Rotation.z += Keyboard.UpDownAxis * DeltaTime, 0.5f,1.5f);
            if(_turret)
                _turretTransform.Rotation.y += Keyboard.LeftRightAxis * DeltaTime;

            _frontLWheelTransform.Rotation = new float3(M.Sin(_velocity)*limit(Keyboard.ADAxis, -0.5f,0.5f), M.Cos(_velocity)* limit(Keyboard.ADAxis, -0.5f,0.5f),0);
            _frontRWheelTransform.Rotation = new float3 (M.Sin(_velocity)*limit(Keyboard.ADAxis, -0.5f,0.5f),M.Cos(_velocity)* limit(Keyboard.ADAxis, -0.5f,0.5f),0);
            

            if(_velocity!=0)
                _bodyTransform.Rotation.y += limit(Keyboard.ADAxis, -0.5f,0.5f)*DeltaTime*Keyboard.WSAxis;
            
            _bodyTransform.Translation = new float3(-5*M.Cos(_bodyTransform.Rotation.y),-5*M.Sin(_bodyTransform.Rotation.y),0);

            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            // Setup the camera 
            RC.View = float4x4.CreateTranslation(-5*M.Cos(_bodyTransform.Rotation.y),-5*M.Sin(_bodyTransform.Rotation.y), 50) * float4x4.CreateRotationX(-M.Pi/4/* (float) Atan(20.0 / 40.0)*/);

            // Render the scene on the current render context
            _sceneRenderer.Render(RC);

            // Swap buffers: Show the contents of the backbuffer (containing the currently rendered frame) on the front buffer.
            Present();
        }


        // Is called when the window was resized
        public override void Resize()
        {
            // Set the new rendering area to the entire new windows size
            RC.Viewport(0, 0, Width, Height);

            // Create a new projection matrix generating undistorted images on the new aspect ratio.
            var aspectRatio = Width / (float)Height;

            // 0.25*PI Rad -> 45� Opening angle along the vertical direction. Horizontal opening angle is calculated based on the aspect ratio
            // Front clipping happens at 1 (Objects nearer than 1 world unit get clipped)
            // Back clipping happens at 2000 (Anything further away from the camera than 2000 world units gets clipped, polygons will be cut)
            var projection = float4x4.CreatePerspectiveFieldOfView(M.PiOver4, aspectRatio, 1, 20000);
            RC.Projection = projection;
        }

        private float limit(float input, float min,float max){
            if(input <= min)
                return min;
            if(input>= max)
                return max;
            return input;
        }
    }
}
