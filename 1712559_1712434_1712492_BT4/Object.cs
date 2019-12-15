using System;
using SharpGL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SharpGL.SceneGraph.Assets;

namespace Lab04
{
    abstract class Object
    {
        public List<Vertex> listVertex; // danh sách tất cả đỉnh của object
        public Color color; // màu nền
        public double length; // chiều dài cạnh
        public double height;
        public bool solid;
        public string name = "Object ";
        public int type;
        public static int _countObjects = 0;

        public double angelX, angelY, angelZ;
        public double tX, tY, tZ;
        public double sX, sY, sZ;

        public bool isTexture;
        public Texture texture;

        public Object() { }

        public virtual void Draw(OpenGLControl gl)
        {
            gl.OpenGL.Color(color.R / 255.0, color.G / 255.0, color.B / 255.0);
        }

        public void Update()
        {
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public int GetCount() { return _countObjects; }

        ~Object()
        {
        }


    }
}
