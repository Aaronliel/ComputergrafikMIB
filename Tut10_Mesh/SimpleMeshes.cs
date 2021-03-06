﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fusee.Engine.Core;
using Fusee.Math.Core;
using Fusee.Serialization;

namespace Fusee.Tutorial.Core
{
    public static class SimpleMeshes 
    {
        public static Mesh CreateCuboid(float3 size)
        {
            return new Mesh
            {
                Vertices = new[]
                {
                    // left, bottom, front vertex
                    new float3(-0.5f*size.x, -0.5f*size.y, -0.5f*size.z), // 0  - belongs to left
                    new float3(-0.5f*size.x, -0.5f*size.y, -0.5f*size.z), // 1  - belongs to bottom
                    new float3(-0.5f*size.x, -0.5f*size.y, -0.5f*size.z), // 2  - belongs to front

                    // left, bottom, back vertex
                    new float3(-0.5f*size.x, -0.5f*size.y,  0.5f*size.z),  // 3  - belongs to left
                    new float3(-0.5f*size.x, -0.5f*size.y,  0.5f*size.z),  // 4  - belongs to bottom
                    new float3(-0.5f*size.x, -0.5f*size.y,  0.5f*size.z),  // 5  - belongs to back

                    // left, up, front vertex
                    new float3(-0.5f*size.x,  0.5f*size.y, -0.5f*size.z),  // 6  - belongs to left
                    new float3(-0.5f*size.x,  0.5f*size.y, -0.5f*size.z),  // 7  - belongs to up
                    new float3(-0.5f*size.x,  0.5f*size.y, -0.5f*size.z),  // 8  - belongs to front

                    // left, up, back vertex
                    new float3(-0.5f*size.x,  0.5f*size.y,  0.5f*size.z),  // 9  - belongs to left
                    new float3(-0.5f*size.x,  0.5f*size.y,  0.5f*size.z),  // 10 - belongs to up
                    new float3(-0.5f*size.x,  0.5f*size.y,  0.5f*size.z),  // 11 - belongs to back

                    // right, bottom, front vertex
                    new float3( 0.5f*size.x, -0.5f*size.y, -0.5f*size.z), // 12 - belongs to right
                    new float3( 0.5f*size.x, -0.5f*size.y, -0.5f*size.z), // 13 - belongs to bottom
                    new float3( 0.5f*size.x, -0.5f*size.y, -0.5f*size.z), // 14 - belongs to front

                    // right, bottom, back vertex
                    new float3( 0.5f*size.x, -0.5f*size.y,  0.5f*size.z),  // 15 - belongs to right
                    new float3( 0.5f*size.x, -0.5f*size.y,  0.5f*size.z),  // 16 - belongs to bottom
                    new float3( 0.5f*size.x, -0.5f*size.y,  0.5f*size.z),  // 17 - belongs to back

                    // right, up, front vertex
                    new float3( 0.5f*size.x,  0.5f*size.y, -0.5f*size.z),  // 18 - belongs to right
                    new float3( 0.5f*size.x,  0.5f*size.y, -0.5f*size.z),  // 19 - belongs to up
                    new float3( 0.5f*size.x,  0.5f*size.y, -0.5f*size.z),  // 20 - belongs to front

                    // right, up, back vertex
                    new float3( 0.5f*size.x,  0.5f*size.y,  0.5f*size.z),  // 21 - belongs to right
                    new float3( 0.5f*size.x,  0.5f*size.y,  0.5f*size.z),  // 22 - belongs to up
                    new float3( 0.5f*size.x,  0.5f*size.y,  0.5f*size.z),  // 23 - belongs to back

                },
                Normals = new[]
                {
                    // left, bottom, front vertex
                    new float3(-1,  0,  0), // 0  - belongs to left
                    new float3( 0, -1,  0), // 1  - belongs to bottom
                    new float3( 0,  0, -1), // 2  - belongs to front

                    // left, bottom, back vertex
                    new float3(-1,  0,  0),  // 3  - belongs to left
                    new float3( 0, -1,  0),  // 4  - belongs to bottom
                    new float3( 0,  0,  1),  // 5  - belongs to back

                    // left, up, front vertex
                    new float3(-1,  0,  0),  // 6  - belongs to left
                    new float3( 0,  1,  0),  // 7  - belongs to up
                    new float3( 0,  0, -1),  // 8  - belongs to front

                    // left, up, back vertex
                    new float3(-1,  0,  0),  // 9  - belongs to left
                    new float3( 0,  1,  0),  // 10 - belongs to up
                    new float3( 0,  0,  1),  // 11 - belongs to back

                    // right, bottom, front vertex
                    new float3( 1,  0,  0), // 12 - belongs to right
                    new float3( 0, -1,  0), // 13 - belongs to bottom
                    new float3( 0,  0, -1), // 14 - belongs to front

                    // right, bottom, back vertex
                    new float3( 1,  0,  0),  // 15 - belongs to right
                    new float3( 0, -1,  0),  // 16 - belongs to bottom
                    new float3( 0,  0,  1),  // 17 - belongs to back

                    // right, up, front vertex
                    new float3( 1,  0,  0),  // 18 - belongs to right
                    new float3( 0,  1,  0),  // 19 - belongs to up
                    new float3( 0,  0, -1),  // 20 - belongs to front

                    // right, up, back vertex
                    new float3( 1,  0,  0),  // 21 - belongs to right
                    new float3( 0,  1,  0),  // 22 - belongs to up
                    new float3( 0,  0,  1),  // 23 - belongs to back
                },
                Triangles = new ushort[]
                {
                    0,  6,  3,     3,  6,  9,  // left
                    2, 14, 20,     2, 20,  8,  // front
                    12, 15, 18,    15, 21, 18, // right
                    5, 11, 17,    17, 11, 23,  // back
                    7, 22, 10,     7, 19, 22,  // top
                    1,  4, 16,     1, 16, 13,  // bottom 
                },
                UVs = new float2[]
                {
                    // left, bottom, front vertex
                    new float2( 1,  0), // 0  - belongs to left
                    new float2( 1,  0), // 1  - belongs to bottom
                    new float2( 0,  0), // 2  - belongs to front

                    // left, bottom, back vertex
                    new float2( 0,  0),  // 3  - belongs to left
                    new float2( 1,  1),  // 4  - belongs to bottom
                    new float2( 1,  0),  // 5  - belongs to back

                    // left, up, front vertex
                    new float2( 1,  1),  // 6  - belongs to left
                    new float2( 0,  0),  // 7  - belongs to up
                    new float2( 0,  1),  // 8  - belongs to front

                    // left, up, back vertex
                    new float2( 0,  1),  // 9  - belongs to left
                    new float2( 0,  1),  // 10 - belongs to up
                    new float2( 1,  1),  // 11 - belongs to back

                    // right, bottom, front vertex
                    new float2( 0,  0), // 12 - belongs to right
                    new float2( 0,  0), // 13 - belongs to bottom
                    new float2( 1,  0), // 14 - belongs to front

                    // right, bottom, back vertex
                    new float2( 1,  0),  // 15 - belongs to right
                    new float2( 0,  1),  // 16 - belongs to bottom
                    new float2( 0,  0),  // 17 - belongs to back

                    // right, up, front vertex
                    new float2( 0,  1),  // 18 - belongs to right
                    new float2( 1,  0),  // 19 - belongs to up
                    new float2( 1,  1),  // 20 - belongs to front

                    // right, up, back vertex
                    new float2( 1,  1),  // 21 - belongs to right
                    new float2( 1,  1),  // 22 - belongs to up
                    new float2( 0,  1),  // 23 - belongs to back                    
                },
                BoundingBox = new AABBf(-0.5f * size, 0.5f*size)
            };
        }

       public static ShaderEffect MakeShaderEffect(float3 diffuseColor, float3 specularColor, float shininess)
        {
            MaterialComponent temp = new MaterialComponent
            {
                Diffuse = new MatChannelContainer
                {
                    Color = diffuseColor
                },
                Specular = new SpecularChannelContainer
                {
                    Color = specularColor,
                    Shininess = shininess
                }
            };

            return ShaderCodeBuilder.MakeShaderEffectFromMatComp(temp);
        }

        public static Mesh createCylinderFlat(float r, float h, int segments){
            float deltaAngle = 2*M.Pi/segments;
            float top = h * (0.5f);
            float bottom = -h; 

            ushort[] tris = new ushort[segments*3];
            float3[] verts = new float3[segments+1];
            float3[] norms = new float3[segments+1];
            verts[segments] = float3.Zero;
            norms[segments] = float3.UnitY;
            verts[0] = new float3(r,top,0);
            norms[0] = float3.UnitY;
            

            for (int i = 1; i < segments; i++){
                verts[i] = new float3(r*M.Cos(i*deltaAngle),top,r*M.Sin(i*deltaAngle));
                norms[i] = float3.UnitY;
                tris[3*i-1] = (ushort) segments;
                tris[3*i-2] = (ushort) i;
                tris[3*i-3] = (ushort) (i-1); 
            }
            tris[3*segments-1] = (ushort)segments;
            tris[3*segments-2] = (ushort)0;
            tris[3*segments-3] = (ushort) (segments-1);

            
            return new Mesh{
                Vertices = verts,
                Triangles = tris,
                Normals = norms

            };
        }

        public static Mesh createCylinder(float r, float h, int segments){
            float deltaAngle = 2*M.Pi/segments;
            float top = h * (0.5f);
            float bottom = -top; 

            ushort[] tris = new ushort[4*(segments*3)];
            float3[] verts = new float3[2*(2*segments+1)];
            float3[] norms = new float3[2*(2*segments+1)];
            
            
            //Top-Fläche Mittel und EndPunkt
            verts[segments] = new float3(0,top,0);
            norms[segments] = float3.UnitY;
            verts[0] = new float3(r,top,0);
            norms[0] = float3.UnitY;

            for (int i = 1; i < segments; i++){
                //Deckel
                verts[i] = new float3(r*M.Cos(i*deltaAngle),top,r*M.Sin(i*deltaAngle));
                norms[i] = float3.UnitY;
                tris[3*i-1] = (ushort) segments;
                tris[3*i-2] = (ushort) i;
                tris[3*i-3] = (ushort) (i-1); 

                //Boden
                verts[i + segments+1] = new float3(r*M.Cos(i*deltaAngle),bottom,r*M.Sin(i*deltaAngle));
                norms[i + segments+1] = new float3(0,-1,0);
                tris[3*i-3+3*segments] = (ushort) (2*segments+1);
                tris[3*i-2+3*segments] = (ushort) (segments+1 + i);
                tris[3*i-1+ 3*segments] = (ushort) (segments+1+(i-1)); 

                //Mantel-top
                verts[i+3*segments+1] = new float3(r*M.Cos(i*deltaAngle),top,r*M.Sin(i*deltaAngle));
                norms[i+3*segments+1] = new float3(M.Cos(i*deltaAngle),0,M.Sin(i*deltaAngle));
                tris[3*i - 1 + 6*segments]= (ushort) (3*segments +1+i);
                tris[3*i - 2 + 6*segments]= (ushort) (2*segments  +2+i);
                tris[3*i -3 + 6*segments]= (ushort) (2*segments +2+(i-1));

                //Mantel-bottom
                verts[i+2*segments+1] = new float3(r*M.Cos(i*deltaAngle),bottom,r*M.Sin(i*deltaAngle));
                norms[i+2*segments+1] = new float3(M.Cos(i*deltaAngle),0,M.Sin(i*deltaAngle));
                tris[3*i - 3 + 9*segments]= (ushort) (2*segments+2 +i);
                tris[3*i - 2 + 9*segments]= (ushort) (3*segments +2 +i);
                tris[3*i -1 + 9*segments]= (ushort) (3*segments +2+(i-1));
            }

            //Top
            tris[3*segments-1] = (ushort) segments;
            tris[3*segments-2] = (ushort) 0;
            tris[3*segments-3] = (ushort) (segments-1);

            //Bottom
            verts[segments+1] = new float3(0,bottom,0);
            norms[segments+1] = new float3(0,-1,0);   
            verts[2*segments+1] = new float3(r,bottom,0);
            norms[2*segments+1] = new float3(0,-1,0);

            tris[6*segments-1] = (ushort) (segments*2+1);
            tris[6*segments-2] = (ushort) (segments+1);
            tris[6*segments-3] = (ushort) (segments*2);

            //Mantel-Bottom
            verts[3*segments + 1] = new float3(r,bottom,0);
            norms[3*segments + 1] = new float3(1,0,0);

            //Mantel-Top
            verts[4*segments + 1] = new float3(r,top,0);
            norms[4*segments + 1] = new float3(1,0,0);


         
            tris[9*segments -1] = (ushort) (4*segments +1);
            tris[9*segments -2] = (ushort) (2*segments+2);
            tris[9*segments -3] = (ushort) (3*segments+1);

            tris[12*segments -1] = (ushort) (2*segments+2);
            tris[12*segments -2] = (ushort) (4*segments +1);
            tris[12*segments -3] = (ushort) (3*segments +2);

            return new Mesh{
                Vertices = verts,
                Triangles = tris,
                Normals = norms

            };
        }
        public static Mesh CreateCone(float radius, float height, int segments)
        {
            return CreateConeFrustum(radius, 0.0f, height, segments);
        }

        public static Mesh CreateConeFrustum(float radiuslower, float radiusupper, float height, int segments)
        {
            throw new NotImplementedException();
        }


        public static Mesh CreatePyramid(float baselen, float height)
        {
            throw new NotImplementedException();
        }
        public static Mesh CreateTetrahedron(float edgelen)
        {
            throw new NotImplementedException();
        }

        public static Mesh CreateTorus(float mainradius, float segradius, int segments, int slices)
        {
            throw new NotImplementedException();
        }

    }
}
