using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL.SceneGraph.Assets;

namespace Lab04
{
    class Pyramid : Object
    {
        public Vertex _center;
        public Pyramid() //độ dày, màu nền, tâm object, chiều dài cạnh
        {
            _center = new Vertex(0, 0, 0); //tâm (mặc định (0,0,0))
            length = 2.0f; //độ dài cạnh
            height = 5.0f;
            color = Color.White; //màu nền mặt phẳng
            solid = false; //check xem có đang thao tác trên hình này không
            type = 1;
            isTexture = false;
            texture = new Texture();

            listVertex = new List<Vertex> { new Vertex(), new Vertex(), new Vertex(), new Vertex(), new Vertex(), new Vertex(), new Vertex(), new Vertex() };
            angelX = angelY = angelZ = 0;
            tX = tY = tZ = 0;
            sX = sY = sZ = 1;
        }
        private void Save()
        {
            // Áp dụng định lý đường cao tam giác vuông và đường tròn để tìm khoảng cách từ tâm (center) đến mặt phẳng đáy
            // d = d(O,mp đáy)
            Vertex V1, V2, V3, V4, V5; // 5 dinh cua hinh
            listVertex = new List<Vertex>();
            double d = (2 * height * height - length * length) / (4 * height);

            //dinh chop
            V1 = new Vertex(_center.x, _center.y + height - d, _center.z);
            // 4 dinh duoi mat day
            V2 = new Vertex(_center.x + length / 2, _center.y - d, _center.z + length / 2);
            V3 = new Vertex(_center.x + length / 2, _center.y - d, _center.z - length / 2);
            V4 = new Vertex(_center.x - length / 2, _center.y - d, _center.z - length / 2);
            V5 = new Vertex(_center.x - length / 2, _center.y - d, _center.z + length / 2);
            //luu lai cac diem
            listVertex.Add(V1);
            listVertex.Add(V2);
            listVertex.Add(V3);
            listVertex.Add(V4);
            listVertex.Add(V5);
        }

        private void VienKhung(OpenGL gl)
        {
            if (solid) //nếu đang thao tác trên hình
            {
                //viền cam đậm
                gl.Color(236 / 255.0, 135 / 255.0, 14 / 255.0);
                //tăng kích cỡ viền
                gl.LineWidth((float)3);
            }
            else // nếu không thao tác
            {
                //viền đen nhạt
                gl.Color(0 / 255.0, 0 / 255.0, 0 / 255.0);
                //tăng kích cỡ viền
                gl.LineWidth((float)1);
            }

            gl.Begin(OpenGL.GL_LINES);
            //Vẽ các cạnh
            gl.Vertex(listVertex[0].x, listVertex[0].y, listVertex[0].z);
            gl.Vertex(listVertex[1].x, listVertex[1].y, listVertex[1].z);

            gl.Vertex(listVertex[0].x, listVertex[0].y, listVertex[0].z);
            gl.Vertex(listVertex[2].x, listVertex[2].y, listVertex[2].z);

            gl.Vertex(listVertex[0].x, listVertex[0].y, listVertex[0].z);
            gl.Vertex(listVertex[3].x, listVertex[3].y, listVertex[3].z);

            gl.Vertex(listVertex[0].x, listVertex[0].y, listVertex[0].z);
            gl.Vertex(listVertex[4].x, listVertex[4].y, listVertex[4].z);

            gl.Vertex(listVertex[1].x, listVertex[1].y, listVertex[1].z);
            gl.Vertex(listVertex[2].x, listVertex[2].y, listVertex[2].z);

            gl.Vertex(listVertex[2].x, listVertex[2].y, listVertex[2].z);
            gl.Vertex(listVertex[3].x, listVertex[3].y, listVertex[3].z);

            gl.Vertex(listVertex[3].x, listVertex[3].y, listVertex[3].z);
            gl.Vertex(listVertex[4].x, listVertex[4].y, listVertex[4].z);

            gl.Vertex(listVertex[4].x, listVertex[4].y, listVertex[4].z);
            gl.Vertex(listVertex[1].x, listVertex[1].y, listVertex[1].z);

            gl.End();
        }

        public override void Draw(OpenGLControl glControl)
        {
            OpenGL gl = glControl.OpenGL;
            Save();
            gl.PushMatrix();
            gl.Rotate((float)angelX, (float)angelY, (float)angelZ);
            gl.Translate(tX, tY, tZ);
            gl.Scale(sX, sY, sZ);

            gl.Color(color.R / 255.0, color.G / 255.0, color.B / 255.0, 0);
            //Vẽ khối hoặc vẽ và dán texture
            if (isTexture)
                DrawTexture(gl);
            else
                DrawRaw(gl);
            VienKhung(gl);
            gl.PopMatrix();
            gl.Flush();
        }

        private void DrawRaw(OpenGL gl)
        {
            gl.Begin(OpenGL.GL_QUADS);
            //Ve mat day
            gl.Vertex(listVertex[1].x, listVertex[1].y, listVertex[1].z);
            gl.Vertex(listVertex[2].x, listVertex[2].y, listVertex[2].z);
            gl.Vertex(listVertex[3].x, listVertex[3].y, listVertex[3].z);
            gl.Vertex(listVertex[4].x, listVertex[4].y, listVertex[4].z);

            gl.End();
            //Ve mat ben la 4 tam giac
            gl.Begin(OpenGL.GL_TRIANGLES);

            gl.Vertex(listVertex[0].x, listVertex[0].y, listVertex[0].z);
            gl.Vertex(listVertex[1].x, listVertex[1].y, listVertex[1].z);
            gl.Vertex(listVertex[2].x, listVertex[2].y, listVertex[2].z);

            gl.Vertex(listVertex[0].x, listVertex[0].y, listVertex[0].z);
            gl.Vertex(listVertex[2].x, listVertex[2].y, listVertex[2].z);
            gl.Vertex(listVertex[3].x, listVertex[3].y, listVertex[3].z);

            gl.Vertex(listVertex[0].x, listVertex[0].y, listVertex[0].z);
            gl.Vertex(listVertex[3].x, listVertex[3].y, listVertex[3].z);
            gl.Vertex(listVertex[4].x, listVertex[4].y, listVertex[4].z);

            gl.Vertex(listVertex[0].x, listVertex[0].y, listVertex[0].z);
            gl.Vertex(listVertex[4].x, listVertex[4].y, listVertex[4].z);
            gl.Vertex(listVertex[1].x, listVertex[1].y, listVertex[1].z);

            gl.End();
        }

        private void DrawTexture(OpenGL gl)
        {
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            //Bind the texture.
            texture.Bind(gl);
            gl.Color(1f, 1f, 1f, 0);
            gl.Begin(OpenGL.GL_QUADS);
            //Ve mat day
            gl.TexCoord(0.0f, 0.0f); gl.Vertex(listVertex[1].x, listVertex[1].y, listVertex[1].z);
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(listVertex[2].x, listVertex[2].y, listVertex[2].z);
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(listVertex[3].x, listVertex[3].y, listVertex[3].z);
            gl.TexCoord(1.0f, 0.0f); gl.Vertex(listVertex[4].x, listVertex[4].y, listVertex[4].z);

            //gl.End();
            //Ve mat ben la 4 tam giac
            //front face
            gl.TexCoord(0.5f, 0.0f); gl.Vertex(listVertex[0].x, listVertex[0].y, listVertex[0].z);
            gl.TexCoord(0.5f, 0.0f); gl.Vertex(listVertex[0].x, listVertex[0].y, listVertex[0].z);
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(listVertex[1].x, listVertex[1].y, listVertex[1].z);
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(listVertex[2].x, listVertex[2].y, listVertex[2].z);

            // right face
            gl.TexCoord(0.5f, 0.0f); gl.Vertex(listVertex[0].x, listVertex[0].y, listVertex[0].z);
            gl.TexCoord(0.5f, 0.0f); gl.Vertex(listVertex[0].x, listVertex[0].y, listVertex[0].z);
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(listVertex[2].x, listVertex[2].y, listVertex[2].z);
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(listVertex[3].x, listVertex[3].y, listVertex[3].z);
            //behind face
            gl.TexCoord(0.5f, 0.0f); gl.Vertex(listVertex[0].x, listVertex[0].y, listVertex[0].z);
            gl.TexCoord(0.5f, 0.0f); gl.Vertex(listVertex[0].x, listVertex[0].y, listVertex[0].z);
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(listVertex[3].x, listVertex[3].y, listVertex[3].z);
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(listVertex[4].x, listVertex[4].y, listVertex[4].z);
            //left face
            gl.TexCoord(0.5f, 0.0f); gl.Vertex(listVertex[0].x, listVertex[0].y, listVertex[0].z);
            gl.TexCoord(0.5f, 0.0f); gl.Vertex(listVertex[0].x, listVertex[0].y, listVertex[0].z);
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(listVertex[4].x, listVertex[4].y, listVertex[4].z);
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(listVertex[1].x, listVertex[1].y, listVertex[1].z);

            gl.End();
            gl.Disable(OpenGL.GL_TEXTURE_2D);
        }
    }
}
