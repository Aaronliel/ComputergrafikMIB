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
    public class FirstSteps : RenderCanvas
    {
        // Init is called on startup. 
        private SceneContainer _scene;
        private SceneRenderer _sceneRenderer;
        private float _camAngle = 0;
        private TransformComponent _cubeTransform;
        private TransformComponent _secondCubeTransform;
        private float _cubeAngle;
        private ShaderEffectComponent _thirdCubeShader;
        private TransformComponent _thirdCubeTransform;
        public override void Init()
        {
            // Set the clear color for the backbuffer to light green (intensities in R, G, B, A).
            RC.ClearColor = new float4(0,0.6f, 0.8f, 1.0f);
            _cubeTransform = new TransformComponent {Scale = new float3(1, 1, 1), Translation = new float3(0,0,0)};
            // Create a scene with a cube
            _secondCubeTransform = new TransformComponent {Scale = new float3(1,1,1),Translation = new float3(0,0,0), Rotation = new float3(0,0,0)};
            _thirdCubeTransform = new TransformComponent {Scale = new float3(1,1,1),Translation = new float3(0,0,0), Rotation = new float3(0,0,0)};; 
            // The three components: one XForm, one Shader and the Mesh
            var cubeTransform = new TransformComponent {Scale = new float3(1, 1, 1), Translation = new float3(10,20,0)};
            var cubeShader = new ShaderEffectComponent
            { 
                Effect = SimpleMeshes.MakeShaderEffect(new float3 (0, 0, 1), new float3 (1, 1, 1),  4)
            };
            var cubeMesh = SimpleMeshes.CreateCuboid(new float3(10, 10, 10));
            _thirdCubeShader = cubeShader;
            // Assemble the cube node containing the three components
            var cubeNode = new SceneNodeContainer();
            cubeNode.Components = new List<SceneComponentContainer>();
            cubeNode.Components.Add(_cubeTransform);
            cubeNode.Components.Add(cubeShader);
            cubeNode.Components.Add(cubeMesh);
            
            var secondCube = new SceneNodeContainer(); 
            secondCube.Components = new List<SceneComponentContainer>();
            secondCube.Components.Add(_secondCubeTransform);
            secondCube.Components.Add(cubeShader);
            secondCube.Components.Add(cubeMesh);

            var thirdCube = new SceneNodeContainer();
            thirdCube.Components = new List<SceneComponentContainer>();
            thirdCube.Components.Add(_thirdCubeTransform);
            thirdCube.Components.Add(_thirdCubeShader);
            thirdCube.Components.Add(cubeMesh); 
            // Create the scene containing the cube as the only object
            _scene = new SceneContainer();
            _scene.Children = new List<SceneNodeContainer>();
            _scene.Children.Add(cubeNode);
            _scene.Children.Add(secondCube);
            _scene.Children.Add(thirdCube);
            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRenderer(_scene);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);
            
            _camAngle += 90.0f*M.Pi/180.0f*DeltaTime;
            _cubeAngle += M.Pi*0.5f*DeltaTime;
            float3 _cubeColor = (HlsToRgb((M.Pi*TimeSinceStart*30)%360));
            Diagnostics.Log(_cubeColor);

            _cubeTransform.Translation = new float3(0, 2*M.Sin(2*TimeSinceStart),0);
            _cubeTransform.Scale = new float3(1,1f-(M.Sin(2*TimeSinceStart)),1);
            _cubeTransform.Rotation = new float3(0,0.25f,0);

            _secondCubeTransform.Rotation = new float3(_cubeAngle,0.0f,_cubeAngle);
            _secondCubeTransform.Translation = new float3(M.Cos(_cubeAngle)*56,0,M.Sin(_cubeAngle)*56);

            _thirdCubeTransform.Translation = new float3(M.Cos(_cubeAngle)*20,M.Sin(_cubeAngle)*20,0);
            _thirdCubeTransform.Rotation = new float3 (0,0,_cubeAngle);
            _thirdCubeShader.Effect = SimpleMeshes.MakeShaderEffect(_cubeColor, new float3 (1, 1, 1),  4);

            //Setup Camera
            RC.View = float4x4.CreateTranslation(0,0,50);


            _sceneRenderer.Render(RC);

            // Swap buffers: Show the contents of the backbuffer (containing the currently rendered frame) on the front buffer.
            Present();
        }
     
            // Convert an HLS value into an RGB value. Quelle: http://csharphelper.com/blog/2016/08/convert-between-rgb-and-hls-color-models-in-c/
        public static float3 HlsToRgb(float h)
        {
            float l = 0.5f;
            float s= 1;
            float p2;
            if (l <= 0.5) p2 = l * (1 + s);
            else p2 = l + s - l * s;

            float p1 = 2 * l - p2;
            float float_r,float_g,float_b;
            if (s == 0)
            {
                float_r = l;
                float_g = l;
                float_b = l;
            }
            else
            {
                float_r = QqhToRgb(p1, p2, h + 120);
                float_g = QqhToRgb(p1, p2, h);
                float_b = QqhToRgb(p1, p2, h - 120);
            }
            Diagnostics.Log(h);
            Diagnostics.Log(s);
            Diagnostics.Log(l);
            return new float3(float_r,float_g,float_b);
        }

        private static float QqhToRgb(float q1, float q2, float hue)
        {
            if (hue > 360) hue -= 360;
            else if (hue < 0) hue += 360;

            if (hue < 60) return q1 + (q2 - q1) * hue / 60;
            if (hue < 180) return q2;
            if (hue < 240) return q1 + (q2 - q1) * (240 - hue) / 60;
            return q1;
}
 

        // Is called when the window was resized
        public override void Resize()
        {
            // Set the new rendering area to the entire new windows size
            RC.Viewport(0, 0, Width, Height);

            // Create a new projection matrix generating undistorted images on the new aspect ratio.
            var aspectRatio = Width / (float)Height;

            // 0.25*PI Rad -> 45° Opening angle along the vertical direction. Horizontal opening angle is calculated based on the aspect ratio
            // Front clipping happens at 1 (Objects nearer than 1 world unit get clipped)
            // Back clipping happens at 2000 (Anything further away from the camera than 2000 world units gets clipped, polygons will be cut)
            var projection = float4x4.CreatePerspectiveFieldOfView(M.PiOver4, aspectRatio, 1, 20000);
            RC.Projection = projection;
        }
    }
}