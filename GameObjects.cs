using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace RSMv
{
   public class GameObjects
   {
       public float Xp;
       public float Yp;
       public float Zp;
       public float Size;
       private float[] _vertices;
       private readonly uint[] _indices = {
           0, 1, 2,
           2, 3, 0
       };

       private int _vertexBufferObject;
       private int _vertexArrayObject;
       private int _elementBufferObject;
       private int _texture;

       private void UpdateVertices()
       {
           _vertices = new float[] {   
               -Size + Xp + 0.2f, -Size + Yp, Zp,
                Size + Xp, -Size + Yp, Zp, 
                Size + Xp,  Size + Yp, Zp,
               -Size + Xp + 0.2f,  Size + Yp, Zp
           };
       }

       public GameObjects(float x, float y, float z, float size)
       {
           Xp = x;
           Yp = y;
           Zp = z;
           Size = size;
           UpdateVertices();
       }

       public void Load()
       {
           _vertexArrayObject = GL.GenVertexArray();
           GL.BindVertexArray(_vertexArrayObject);

           _vertexBufferObject = GL.GenBuffer();
           GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
           GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

           _elementBufferObject = GL.GenBuffer();
           GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
           GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

           GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
           GL.EnableVertexAttribArray(0);
       }

       public void Render()
       {
           GL.BindVertexArray(_vertexArrayObject);
           GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
       }

       public void Unload()
       {
           GL.DeleteBuffer(_vertexBufferObject);
           GL.DeleteVertexArray(_vertexArrayObject);
           GL.DeleteBuffer(_elementBufferObject);
       }
       
       public void Move(float deltaX, float deltaY)
       {
           Xp += deltaX;
           Yp += deltaY;
           UpdateVertices();
           GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
           GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, _vertices.Length * sizeof(float), _vertices);
       }

       public bool IsColliding(GameObjects other)
       {
           return !(Xp + Size < other.Xp - other.Size ||
                    Xp - Size > other.Xp + other.Size ||
                    Yp + Size < other.Yp - other.Size ||
                    Yp - Size > other.Yp + other.Size);
       }
   }
}