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
    public class HierarchyInput : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRenderer _sceneRenderer;
        private float _camAngleY = 0;
        private float _camAngleX = 0;
        private float _upperAngle = 0;
        private float _lowerAngle = 0;
        private float _bodyAngle = 0;
        private float _greifAngle = 0; 
        private float _openAngle = 0;
        private float _camDistanceZ = 50;
        private bool open = false;
        
        private TransformComponent _baseTransform;
        private TransformComponent _bodyTransform;
        private TransformComponent _upperTransform;
        private TransformComponent _lowerTransform;

        private TransformComponent _greifBaseTransform;
        private TransformComponent _greifRightTransform;
        private TransformComponent _greifLeftTransform;



        SceneContainer CreateScene()
        {
            // Initialize transform components that need to be changed inside "RenderAFrame"
            _baseTransform = new TransformComponent
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 0, 0)
            };
            _bodyTransform = new TransformComponent
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 6, 0)
            };
            
            _upperTransform = new TransformComponent
            {
                Rotation = new float3(60*M.Pi/180, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(2, 4, 0)
            };
            
            _lowerTransform = new TransformComponent
            {
                Rotation = new float3(d2r(60), 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(-2, 4, 0)
            };

            _greifBaseTransform = new TransformComponent
            {
                Rotation = new float3(0,0,0),
                Scale = new float3(1,1,1),
                Translation = new float3(0,5.5f,-0.5f)
            };
            _greifRightTransform = new TransformComponent
            {
                Rotation = new float3(0,0,0),
                Scale = new float3(1,1,1),
                Translation = new float3(2.5f,1,1)
            };
            _greifLeftTransform = new TransformComponent
            {
                Rotation = new float3(0,0,0),
                Scale = new float3(1,1,1),
                Translation = new float3(-2.5f,1,1)
            };
            // Setup the scene graph
            return new SceneContainer
            {
                Children = new List<SceneNodeContainer>
                {
                    new SceneNodeContainer
                    {
                        Components = new List<SceneComponentContainer>
                        {
                            // TRANSFROM COMPONENT
                            _baseTransform,

                            // SHADER EFFECT COMPONENT
                            new ShaderEffectComponent
                            {
                                Effect = SimpleMeshes.MakeShaderEffect(new float3(0.7f, 0.7f, 0.7f), new float3(0.7f, 0.7f, 0.7f), 5)
                            },

                            // MESH COMPONENT
                            SimpleMeshes.CreateCuboid(new float3(10, 2, 10))
                        },
                        Children = new List<SceneNodeContainer>
                        {
                            new SceneNodeContainer
                            {
                                Components = new List<SceneComponentContainer> //Roter Drehanteil des Krans 
                                {
                                    _bodyTransform,

                                    new ShaderEffectComponent
                                    {
                                        Effect = SimpleMeshes.MakeShaderEffect(new float3(1,0.2f,0.2f),new float3(1,0.2f,0.2f),5)
                                    },
                                    
                                    SimpleMeshes.CreateCuboid(new float3(2,10,2))
                                    
                                },
                                Children = new List<SceneNodeContainer>{
                                    new SceneNodeContainer
                                    {
                                        Components = new List<SceneComponentContainer> //Pivot-Punkt zum drehen
                                        {
                                            _upperTransform
                                        },
                                        Children = new List<SceneNodeContainer>
                                        {
                                            new SceneNodeContainer
                                            {
                                                Components = new List<SceneComponentContainer>
                                                {
                                                    new TransformComponent{Translation= new float3 (0f,4f,0f),Scale = new float3(1,1,1)},
                                                    new ShaderEffectComponent
                                                    {
                                                        Effect = SimpleMeshes.MakeShaderEffect(new float3(0.2f,0.2f,1f),new float3(0.2f,0.2f,1f),5)
                                                                                                                
                                                    },
                                                    SimpleMeshes.CreateCuboid(new float3(2f,10f,2f))
                                                },
                                                Children = new List<SceneNodeContainer>{
                                                    new SceneNodeContainer
                                                    {
                                                        Components = new List<SceneComponentContainer> //Pivot-Punkt zum drehen
                                                        {
                                                            _lowerTransform
                                                        },
                                                        Children= new List<SceneNodeContainer>
                                                        {
                                                            new SceneNodeContainer
                                                            {
                                                                Components = new List<SceneComponentContainer>
                                                                {
                                                                    new TransformComponent{Translation= new float3(0,4,0), Scale = new float3(1,1,1)},
                                                                    new ShaderEffectComponent
                                                                    {
                                                                        Effect = SimpleMeshes.MakeShaderEffect(new float3(0.2f,1f,0.2f),new float3(0.2f,1f,0.2f),5)
                                                                    },
                                                                    SimpleMeshes.CreateCuboid(new float3(2,10,2))
                                                                },
                                                                Children = new List<SceneNodeContainer>
                                                                {
                                                                    new SceneNodeContainer
                                                                    {
                                                                        Components = new List<SceneComponentContainer>
                                                                        {
                                                                            _greifBaseTransform
                                                                        },
                                                                        Children = new List<SceneNodeContainer>
                                                                        {
                                                                            new SceneNodeContainer
                                                                            {
                                                                                Components = new List<SceneComponentContainer>
                                                                                {
                                                                                    new TransformComponent{Translation= new float3(0,0,0), Scale = new float3(1,1,1)},
                                                                                    new ShaderEffectComponent
                                                                                    {
                                                                                        Effect = SimpleMeshes.MakeShaderEffect(new float3(1f,1f,0.2f),new float3(1f,1f,0.2f),5)
                                                                                    },
                                                                                    SimpleMeshes.CreateCuboid(new float3(6,1,1))
                                                                                },
                                                                                Children = new List<SceneNodeContainer>
                                                                                {
                                                                                    new SceneNodeContainer
                                                                                    {
                                                                                        Components = new List<SceneComponentContainer>
                                                                                        {
                                                                                            _greifRightTransform
                                                                                        },
                                                                                        Children = new List<SceneNodeContainer>
                                                                                        {
                                                                                            new SceneNodeContainer
                                                                                            {
                                                                                                Components = new List<SceneComponentContainer>
                                                                                                {
                                                                                                    new TransformComponent{Translation= new float3(0,1,0),Scale=new float3(1,1,1)},
                                                                                                    new ShaderEffectComponent
                                                                                                    {
                                                                                                        Effect = SimpleMeshes.MakeShaderEffect(new float3(0,0,0),new float3(0,0,0),4)
                                                                                                    },
                                                                                                    SimpleMeshes.CreateCuboid(new float3(1,5,1))
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    
                                                                                    },
                                                                                    new SceneNodeContainer
                                                                                    {
                                                                                        Components = new List<SceneComponentContainer>
                                                                                        {
                                                                                            _greifLeftTransform
                                                                                        },
                                                                                        Children = new List<SceneNodeContainer>
                                                                                        {
                                                                                            new SceneNodeContainer
                                                                                            {
                                                                                                Components = new List<SceneComponentContainer>
                                                                                                {
                                                                                                    new TransformComponent{Translation= new float3(0,1,0),Scale=new float3(1,1,1)},
                                                                                                    new ShaderEffectComponent
                                                                                                    {
                                                                                                        Effect = SimpleMeshes.MakeShaderEffect(new float3(0,0,0),new float3(0,0,0),4)
                                                                                                    },
                                                                                                    SimpleMeshes.CreateCuboid(new float3(1,5,1))
                                                                                                }
                                                                                            }
                                                                                        }                                                                                            
                                                                                        
                                                                                    }

                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                
                                                }
                                            }
                                        }
                                    }
                                }
                                
                            }
                        }
                    }
                }
            };
        }

        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to white (100% intensity in all color channels R, G, B, A).
            RC.ClearColor = new float4(0.8f, 0.9f, 0.7f, 1);

            _scene = CreateScene();

            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRenderer(_scene);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);
            
            //Mouse Input
            if(Mouse.LeftButton){
                _camAngleY += -Mouse.Velocity.x * Time.DeltaTime * 0.01f;
                _camAngleX += -Mouse.Velocity.y * Time.DeltaTime * 0.005f;
            }

            //Touch Input
            _camAngleY += -Touch.GetVelocity(TouchPoints.Touchpoint_0).x * Time.DeltaTime*0.01f;
            _camAngleX += -Touch.GetVelocity(TouchPoints.Touchpoint_0).y * Time.DeltaTime*0.005f;
            //_camDistanceZ = limit(_camDistanceZ -= Touch.TwoPointDistanceVel*0.01f,1,200);
            
            //Key Inputs
            if(Keyboard.GetKey(KeyCodes.Space)){

                if(limitReached(_openAngle,d2r(40)))
                    open = false;
                if(limitReached(_openAngle,d2r(0)))
                    open = true;
            }
            if(open && !(_openAngle/d2r(40)==1)){
                    _openAngle = limit (_openAngle += Time.DeltaTime *0.8f,d2r(0),d2r(40));
            }
            
            if(!open && !limitReached(_openAngle,d2r(0))){
                    _openAngle = limit (_openAngle -= Time.DeltaTime*0.8f,d2r(0),d2r(40));
            }
            

            
            _upperAngle = limit(_upperAngle += Keyboard.WSAxis * Time.DeltaTime,d2r(0),d2r(100));
            _lowerAngle = limit(_lowerAngle += Keyboard.UpDownAxis * Time.DeltaTime,d2r(0),d2r(120));
            _bodyAngle = limit(_bodyAngle += Keyboard.ADAxis * Time.DeltaTime,d2r(0),d2r(360));
            _greifAngle = limit(_greifAngle += Keyboard.LeftRightAxis * Time.DeltaTime,d2r(-90),d2r(90));
            
            // Setup the camera 
            RC.View = float4x4.CreateTranslation(0, -15, _camDistanceZ)* float4x4.CreateRotationX(_camAngleX) * float4x4.CreateRotationY(_camAngleY);
            _bodyTransform.Rotation= new float3(0,_bodyAngle,0);
            _upperTransform.Rotation= new float3(_upperAngle,0,0);
            _lowerTransform.Rotation= new float3(_lowerAngle,0,0);
            _greifBaseTransform.Rotation = new float3(0,_greifAngle,0);
            _greifLeftTransform.Rotation = new float3(0,0,-_openAngle);
            _greifRightTransform.Rotation = new float3(0,0,_openAngle);
            // Render the scene on the current render context
            _sceneRenderer.Render(RC);

            // Swap buffers: Show the contents of the backbuffer (containing the currently rendered farame) on the front buffer.
            Present();
        }
        public float d2r(float d) => d*M.Pi/180;
        public float limit(float value,float min, float max){
            if (value<=min)
                return min;
            if(value>=max)
                return max;
            return value;
        }
        public bool limitReached(float angle, float limit) => angle == limit;
       

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