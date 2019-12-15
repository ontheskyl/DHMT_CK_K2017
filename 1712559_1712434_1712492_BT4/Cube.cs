using System;
using SharpGL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Assets;

namespace Lab04
{
    class Cube : Object
    {
        private Vertex mid;// biến mid: dịch tâm theo tọa độ mid(x,y,z)
        public Cube()
        {
            length = 2.0f;
            color = Color.White; //màu nền mặt phẳng
            mid = new Vertex(tX - 0 - length / 2, tY - 0 - length / 2, tZ - 0 - length / 2);
            solid = false; //kiểm tra xem có đang thao tác trên hình này không, nếu có thì tô viền cam, ngược lại màu đen
            type = 0;
            isTexture = false; // Kiểm tra xem có dán texture không
            texture = new Texture();
            // Tạo list có 8 điểm của Cube
            listVertex = new List<Vertex> { new Vertex(), new Vertex(), new Vertex(), new Vertex(), new Vertex(), new Vertex(), new Vertex(), new Vertex() };
            angelX = angelY = angelZ = 0; //Góc xoay của Cube
            tX = tY = tZ = 0;  // Toạ độ của Cube
            sX = sY = sZ = 1;  // Size của Cube

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

            gl.Vertex(listVertex[1].x, listVertex[1].y, listVertex[1].z);
            gl.Vertex(listVertex[2].x, listVertex[2].y, listVertex[2].z);


            gl.Vertex(listVertex[2].x, listVertex[2].y, listVertex[2].z);
            gl.Vertex(listVertex[3].x, listVertex[3].y, listVertex[3].z);


            gl.Vertex(listVertex[0].x, listVertex[0].y, listVertex[0].z);
            gl.Vertex(listVertex[3].x, listVertex[3].y, listVertex[3].z);


            gl.Vertex(listVertex[1].x, listVertex[1].y, listVertex[1].z);
            gl.Vertex(listVertex[4].x, listVertex[4].y, listVertex[4].z);


            gl.Vertex(listVertex[5].x, listVertex[5].y, listVertex[5].z);
            gl.Vertex(listVertex[4].x, listVertex[4].y, listVertex[4].z);


            gl.Vertex(listVertex[5].x, listVertex[5].y, listVertex[5].z);
            gl.Vertex(listVertex[0].x, listVertex[0].y, listVertex[0].z);


            gl.Vertex(listVertex[5].x, listVertex[5].y, listVertex[5].z);
            gl.Vertex(listVertex[6].x, listVertex[6].y, listVertex[6].z);


            gl.Vertex(listVertex[3].x, listVertex[3].y, listVertex[3].z);
            gl.Vertex(listVertex[6].x, listVertex[6].y, listVertex[6].z);


            gl.Vertex(listVertex[4].x, listVertex[4].y, listVertex[4].z);
            gl.Vertex(listVertex[7].x, listVertex[7].y, listVertex[7].z);


            gl.Vertex(listVertex[6].x, listVertex[6].y, listVertex[6].z);
            gl.Vertex(listVertex[7].x, listVertex[7].y, listVertex[7].z);



            gl.Vertex(listVertex[2].x, listVertex[2].y, listVertex[2].z);
            gl.Vertex(listVertex[7].x, listVertex[7].y, listVertex[7].z);
            gl.End();

        }

        private void Save()
        {
            mid.x = tX - 0 - length / 2;
            mid.y = tY - 0 - length / 2;
            mid.z = tZ - 0 - length / 2;

            listVertex[0].x = 0 + mid.x;
            listVertex[0].y = 0 + mid.y;
            listVertex[0].z = 0 + mid.z;

            listVertex[1].x = 0 + mid.x;
            listVertex[1].y = length + mid.y;
            listVertex[1].z = 0 + mid.z;

            listVertex[2].x = length + mid.x;
            listVertex[2].y = length + mid.y;
            listVertex[2].z = 0 + mid.z;

            listVertex[3].x = length + mid.x;
            listVertex[3].y = 0 + mid.y;
            listVertex[3].z = 0 + mid.z;

            listVertex[4].x = 0 + mid.x;
            listVertex[4].y = length + mid.y;
            listVertex[4].z = length + mid.z;

            listVertex[5].x = 0 + mid.x;
            listVertex[5].y = 0 + mid.y;
            listVertex[5].z = length + mid.z;

            listVertex[6].x = length + mid.x;
            listVertex[6].y = 0 + mid.y;
            listVertex[6].z = length + mid.z;

            listVertex[7].x = length + mid.x;
            listVertex[7].y = length + mid.y;
            listVertex[7].z = length + mid.z;
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
            //Viền khung
            VienKhung(gl);
            gl.PopMatrix();
            gl.Flush();// Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian
        }

        private void DrawRaw(OpenGL gl)
        {
            gl.Begin(OpenGL.GL_QUADS);
            //Vẽ mặt phẳng
            gl.Vertex(0 + mid.x, 0 + mid.y, 0 + mid.z);
            gl.Vertex(0 + mid.x, length + mid.y, 0 + mid.z);
            gl.Vertex(length + mid.x, length + mid.y, 0 + mid.z); 
            gl.Vertex(length + mid.x, 0 + mid.y, 0 + mid.z);


            gl.Vertex(0 + mid.x, 0 + mid.y, 0 + mid.z); 
            gl.Vertex(0 + mid.x, length + mid.y, 0 + mid.z); 
            gl.Vertex(0 + mid.x, length + mid.y, length + mid.z);
            gl.Vertex(0 + mid.x, 0 + mid.y, length + mid.z);


            gl.Vertex(0 + mid.x, 0 + mid.y, 0 + mid.z);
            gl.Vertex(0 + mid.x, 0 + mid.y, length + mid.z);
            gl.Vertex(length + mid.x, 0 + mid.y, length + mid.z);
            gl.Vertex(length + mid.x, 0 + mid.y, 0 + mid.z);

            gl.Vertex(0 + mid.x, 0 + mid.y, length + mid.z);
            gl.Vertex(0 + mid.x, length + mid.y, length + mid.z);
            gl.Vertex(length + mid.x, length + mid.y, length + mid.z);
            gl.Vertex(length + mid.x, 0 + mid.y, length + mid.z);


            gl.Vertex(0 + mid.x, length + mid.y, 0 + mid.z);
            gl.Vertex(0 + mid.x, length + mid.y, length + mid.z);
            gl.Vertex(length + mid.x, length + mid.y, length + mid.z);
            gl.Vertex(length + mid.x, length + mid.y, 0 + mid.z);


            gl.Vertex(length + mid.x, length + mid.y, 0 + mid.z);
            gl.Vertex(length + mid.x, 0 + mid.y, 0 + mid.z);
            gl.Vertex(length + mid.x, 0 + mid.y, length + mid.z);
            gl.Vertex(length + mid.x, length + mid.y, length + mid.z);

            gl.End();
        }

        private void DrawTexture(OpenGL gl)
        {
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            //Bind the texture.
            texture.Bind(gl);
            gl.Color(1f, 1f, 1f, 0);
            gl.Begin(OpenGL.GL_QUADS);
            //Vẽ mặt phẳng
            //Right face
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(0 + mid.x, 0 + mid.y, 0 + mid.z);
            gl.TexCoord(1.0f, 0.0f); gl.Vertex(0 + mid.x, length + mid.y, 0 + mid.z);
            gl.TexCoord(0.0f, 0.0f); gl.Vertex(length + mid.x, length + mid.y, 0 + mid.z); 
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(length + mid.x, 0 + mid.y, 0 + mid.z); 

            // Behind face
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(0 + mid.x, 0 + mid.y, 0 + mid.z); 
            gl.TexCoord(0.0f, 0.0f); gl.Vertex(0 + mid.x, length + mid.y, 0 + mid.z); 
            gl.TexCoord(1.0f, 0.0f); gl.Vertex(0 + mid.x, length + mid.y, length + mid.z);
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(0 + mid.x, 0 + mid.y, length + mid.z);

            //Down face
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(0 + mid.x, 0 + mid.y, 0 + mid.z); 
            gl.TexCoord(0.0f, 0.0f); gl.Vertex(0 + mid.x, 0 + mid.y, length + mid.z);
            gl.TexCoord(1.0f, 0.0f); gl.Vertex(length + mid.x, 0 + mid.y, length + mid.z);
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(length + mid.x, 0 + mid.y, 0 + mid.z);

            //Left face
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(0 + mid.x, 0 + mid.y, length + mid.z);
            gl.TexCoord(0.0f, 0.0f); gl.Vertex(0 + mid.x, length + mid.y, length + mid.z);
            gl.TexCoord(1.0f, 0.0f); gl.Vertex(length + mid.x, length + mid.y, length + mid.z);
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(length + mid.x, 0 + mid.y, length + mid.z);
     
            // Up face
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(0 + mid.x, length + mid.y, 0 + mid.z);
            gl.TexCoord(0.0f, 0.0f); gl.Vertex(0 + mid.x, length + mid.y, length + mid.z);
            gl.TexCoord(1.0f, 0.0f); gl.Vertex(length + mid.x, length + mid.y, length + mid.z);
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(length + mid.x, length + mid.y, 0 + mid.z);

            //Front face
            gl.TexCoord(1.0f, 0.0f); gl.Vertex(length + mid.x, length + mid.y, 0 + mid.z);
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(length + mid.x, 0 + mid.y, 0 + mid.z);
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(length + mid.x, 0 + mid.y, length + mid.z);
            gl.TexCoord(0.0f, 0.0f); gl.Vertex(length + mid.x, length + mid.y, length + mid.z);

            gl.End();
            gl.Disable(OpenGL.GL_TEXTURE_2D);
        }




        ~Cube()
        {
        }
    }
}
