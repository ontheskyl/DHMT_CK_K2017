using System;
using SharpGL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SharpGL.SceneGraph;

namespace Lab04
{
    class Prism : Object
    {
        private double R_bot;
        private Vertex _center;

        public Prism() //màu nền, tâm, chiều dài cạnh, check đang chọn
        {
            _center = new Vertex(0, 0, 0);
            length = 2.0f; //độ dài cạnh cua mat day tam giac
            height = 5.0f; // chieu cao cua lang tru
            color = Color.White; //màu nền mặt phẳng
            color = Color.White; //màu nền mặt phẳng

            solid = false; //check xem có đang thao tác trên hình này không
            R_bot = Math.Sqrt(3) * length / 3;
            listVertex = new List<Vertex>();

            type = 2;

            listVertex = new List<Vertex> { new Vertex(), new Vertex(), new Vertex(), new Vertex(), new Vertex(), new Vertex(), new Vertex(), new Vertex() };
            angelX = angelY = angelZ = 0;
            tX = tY = tZ = 0;
            sX = sY = sZ = 1;
        }

        private void Save()
        {
            listVertex.Clear();
            R_bot = Math.Sqrt(3) * length / 3;
            // Lưu tất cả đỉnh, hinh chay doc theo truc y
            Vertex V1, V2, V3, V4, V5, V6; //6 đỉnh
            //listVertex = new List<Vertex>();
            //3 diem dau la mot tam giac
            V1 = new Vertex(_center.x + R_bot, _center.y - height / 2, _center.z);
            V2 = new Vertex(_center.x - R_bot / 2, _center.y - height / 2, _center.z - R_bot / 2);
            V3 = new Vertex(_center.x - R_bot / 2, _center.y - height / 2, _center.z - R_bot / 2);
            //3 diem con lai cung tao nen mot mat tam giac
            V4 = new Vertex(V1.x, _center.y + height / 2, V1.z);
            V5 = new Vertex(V2.x, _center.y + height / 2, V2.z);
            V6 = new Vertex(V3.x, _center.y + height / 2, V3.z);

            listVertex.Add(V1);
            listVertex.Add(V2);
            listVertex.Add(V3);
            listVertex.Add(V4);
            listVertex.Add(V5);
            listVertex.Add(V6);
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

            gl.Vertex(listVertex[0].x, listVertex[0].y, listVertex[0].z);
            gl.Vertex(listVertex[2].x, listVertex[2].y, listVertex[2].z);

            gl.Vertex(listVertex[0].x, listVertex[0].y, listVertex[0].z); 
            gl.Vertex(listVertex[3].x, listVertex[3].y, listVertex[3].z); 

            gl.Vertex(listVertex[1].x, listVertex[1].y, listVertex[1].z);
            gl.Vertex(listVertex[4].x, listVertex[4].y, listVertex[4].z); 

            gl.Vertex(listVertex[2].x, listVertex[2].y, listVertex[2].z); 
            gl.Vertex(listVertex[5].x, listVertex[5].y, listVertex[5].z);

            gl.Vertex(listVertex[3].x, listVertex[3].y, listVertex[3].z);
            gl.Vertex(listVertex[4].x, listVertex[4].y, listVertex[4].z);

            gl.Vertex(listVertex[5].x, listVertex[5].y, listVertex[5].z);
            gl.Vertex(listVertex[3].x, listVertex[3].y, listVertex[3].z);

            gl.Vertex(listVertex[5].x, listVertex[5].y, listVertex[5].z); 
            gl.Vertex(listVertex[4].x, listVertex[4].y, listVertex[4].z);

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
            gl.Begin(OpenGL.GL_TRIANGLES);
            //Ve mat tam giac
            gl.Vertex(listVertex[0].x, listVertex[0].y, listVertex[0].z); 
            gl.Vertex(listVertex[1].x, listVertex[1].y, listVertex[1].z); 
            gl.Vertex(listVertex[2].x, listVertex[2].y, listVertex[2].z); 

            gl.Vertex(listVertex[3].x, listVertex[3].y, listVertex[3].z);
            gl.Vertex(listVertex[4].x, listVertex[4].y, listVertex[4].z);
            gl.Vertex(listVertex[5].x, listVertex[5].y, listVertex[5].z);

            gl.End();

            gl.Color(color.R / 255.0, color.G / 255.0, color.B / 255.0, 0);
            gl.Begin(OpenGL.GL_QUADS);
            //Ve mat ben
            gl.Vertex(listVertex[0].x, listVertex[0].y, listVertex[0].z);
            gl.Vertex(listVertex[1].x, listVertex[1].y, listVertex[1].z);
            gl.Vertex(listVertex[4].x, listVertex[4].y, listVertex[4].z);
            gl.Vertex(listVertex[3].x, listVertex[3].y, listVertex[3].z);

            gl.Vertex(listVertex[0].x, listVertex[0].y, listVertex[0].z);
            gl.Vertex(listVertex[2].x, listVertex[2].y, listVertex[2].z);
            gl.Vertex(listVertex[5].x, listVertex[5].y, listVertex[5].z);
            gl.Vertex(listVertex[3].x, listVertex[3].y, listVertex[3].z);

            gl.Vertex(listVertex[1].x, listVertex[1].y, listVertex[1].z);
            gl.Vertex(listVertex[2].x, listVertex[2].y, listVertex[2].z);
            gl.Vertex(listVertex[5].x, listVertex[5].y, listVertex[5].z);
            gl.Vertex(listVertex[4].x, listVertex[4].y, listVertex[4].z);

            gl.End();

            VienKhung(gl);
            gl.PopMatrix();
            gl.Flush();// Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian
        }

        ~Prism()
        {
        }
    }
}
