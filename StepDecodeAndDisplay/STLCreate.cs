using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace StepDecodeAndDisplay
{
    public class STLCreate
    {
        //符号常量
        public const int PlaneSurface = 1;
        public const int CylinderSurface = 2;
        //数据成员
        public ArrayList STL_menber;
        public BinaryWriter STL_make;
        public ArrayList AdvancedFaceIndex;
        public int CircleDivision;//圆的剖分精度
        //成员方法
        public STLCreate()
        {
            CircleDivision = 60;
         }
        public void CreateData()//生成三角面，存放在内存中
        {
            STL_menber = new ArrayList();
            AdvancedFaceIndex = new ArrayList();
            LookForAdvancedFace();
            if (AdvancedFaceIndex.Count > 0)
            {
                for (int i= 0; i < AdvancedFaceIndex.Count;++i)//依次迭代每一个高级面
                {
                    int type = WhichKindOfSurface((int)AdvancedFaceIndex[i]);//判断该面是何种表面
                    switch (type)
                    {
                        case PlaneSurface: CreatePlaneMesh((int)AdvancedFaceIndex[i]); break;
                        case CylinderSurface: CreateCylinderMesh((int)AdvancedFaceIndex[i]);break;
                        default: break;
                    }
                }
            }
        }
        public void CreateFile()//将内存中的三角网格数据写到文件中，文件的路径作为参数
        {
            if (File.Exists("C:\\inetpub\\wwwroot\\models.stl"))
            {
                File.Delete("C:\\inetpub\\wwwroot\\models.stl");
            }

            FileStream myStream = new FileStream("C:\\inetpub\\wwwroot\\models.stl", FileMode.OpenOrCreate,FileAccess.Write);
            BinaryWriter Writer = new BinaryWriter(myStream);

            for (int i = 1; i <= 80; ++i)
            {
                string FileName = "models.stl";
                if (i <= 8)
                {
                    byte temp = (byte)FileName[i - 1];
                    Writer.Write(temp);
                }
                else
                {
                    byte temp = (byte)' ';
                    Writer.Write(temp);
                }
            }
            Writer.Write((uint)STL_menber.Count);
            for (int i=0;i<STL_menber.Count;++i)
            {
                STLTriangle temp = (STLTriangle)STL_menber[i];

                Writer.Write((float)temp.Dir.x_dir);
                Writer.Write((float)temp.Dir.y_dir);
                Writer.Write((float)temp.Dir.z_dir);

                Writer.Write((float)temp.Vertex1.x);
                Writer.Write((float)temp.Vertex1.y);
                Writer.Write((float)temp.Vertex1.z);

                Writer.Write((float)temp.Vertex2.x);
                Writer.Write((float)temp.Vertex2.y);
                Writer.Write((float)temp.Vertex2.z);

                Writer.Write((float)temp.Vertex3.x);
                Writer.Write((float)temp.Vertex3.y);
                Writer.Write((float)temp.Vertex3.z);

                Writer.Write((byte)' ');
                Writer.Write((byte)' ');

            }

            Writer.Close();
            myStream.Close();


        }
        public int WhichKindOfSurface(int index)//判断该面是何种的类型，从而调用不同的函数生成网格
        {
            int ID=Form1.MainStorage.LookForLastChildByIndex(index);
            TableItem temp = (TableItem)(Form1.MainStorage.TableNode[Form1.MainStorage.LookForNode(ID)]);
            switch (temp.TypeFlag)
            {
                case 9: return CylinderSurface; break;
                case 10: return PlaneSurface; break;
                default:return 0;
            }
        }
        public void CreatePlaneMesh(int index)//创建平面网格
        {
            //取出对应的STEP元素的索引
            int StepIdOfBound = Form1.MainStorage.LookForFirstChildByIndex(index);
            int StepIdOfLoop = Form1.MainStorage.LookForFirstChild(StepIdOfBound);
            int StepIdOfOriented = Form1.MainStorage.LookForFirstChild(StepIdOfLoop);
            int StepIdOfCurve = Form1.MainStorage.LookForFirstChild(StepIdOfOriented);
            int StepIdOfCircle = Form1.MainStorage.LookForLastChild(StepIdOfCurve);
            int IndexOfCircle = Form1.MainStorage.LookForNode(StepIdOfCircle);
            TableItem temp = (TableItem)(Form1.MainStorage.TableNode[IndexOfCircle]);
            float radius =(float)((CIRCLE)(temp.node)).radius;//读出圆的半径
            int StepIdOfCircleAXIS = Form1.MainStorage.LookForFirstChild(StepIdOfCircle);
            int StepIdOfCirclePoint = Form1.MainStorage.LookForFirstChild(StepIdOfCircleAXIS);//读出圆心
            int StepIdOfCircleDirection2 = Form1.MainStorage.LookForLastChild(StepIdOfCircleAXIS);//读出平面的方向2
            ArrayList child_temp = Form1.MainStorage.LookForChild(StepIdOfCircleAXIS);
            int StepIdOfCircleDirection1 = (int)(child_temp[1]);//读出平面方向1
            //从STEP文件编号中中实例化出信息
            TableItem TEMP_CART = (TableItem)(Form1.MainStorage.TableNode[Form1.MainStorage.LookForNode(StepIdOfCirclePoint)]);
            CARTESIAN_POINT circle_center_point = (CARTESIAN_POINT)(TEMP_CART.node);
            TableItem TEMP_DIR1 = (TableItem)(Form1.MainStorage.TableNode[Form1.MainStorage.LookForNode(StepIdOfCircleDirection1)]);
            DIRECTION circle_plane_dir1 = (DIRECTION)(TEMP_DIR1.node);
            TableItem TEMP_DIR2 = (TableItem)(Form1.MainStorage.TableNode[Form1.MainStorage.LookForNode(StepIdOfCircleDirection2)]);
            DIRECTION circle_plane_dir2 = (DIRECTION)(TEMP_DIR2.node);
            //条件判断，如果符合一定的特殊情况，那么就开始执行三角剖分
            STLPoint point1;
            STLPoint point2;
            STLPoint point3;
            STLDirection dir_temp;
            STLTriangle temp_tri;
            if ((circle_plane_dir1.x_dir == 0) && (circle_plane_dir1.y_dir == 0) && (circle_plane_dir1.z_dir > 0))
            {
                //建立方向
                dir_temp = new STLDirection();
                dir_temp.x_dir = 0;
                dir_temp.y_dir = 0;
                dir_temp.z_dir = 1;
                
                for (int i = 0; i < CircleDivision; ++i)
                {
                    //建立第一个点
                    point1 = new STLPoint();
                    point1.x = (float)circle_center_point.x_coord;
                    point1.y = (float)circle_center_point.y_coord;
                    point1.z = (float)circle_center_point.z_coord;
                    //建立第二个点
                    point2 = new STLPoint();
                    point2.x = (float)circle_center_point.x_coord+(float)(radius*Math.Cos(2*Math.PI*(i/(float)CircleDivision)));
                    point2.y = (float)circle_center_point.y_coord+(float)(radius*Math.Sin(2*Math.PI*(i/(float)CircleDivision)));
                    point2.z = (float)circle_center_point.z_coord;
                    //建立第三个点
                    point3 = new STLPoint();
                    point3.x = (float)circle_center_point.x_coord + (float)(radius*Math.Cos(2 * Math.PI * ((i+1) / (float)CircleDivision)));
                    point3.y = (float)circle_center_point.y_coord + (float)(radius*Math.Sin(2 * Math.PI * ((i+1) / (float)CircleDivision)));
                    point3.z = (float)circle_center_point.z_coord;
                    //将点和方向插入到三角形结构体之中
                    temp_tri = new STLTriangle();
                    temp_tri.Dir = dir_temp;
                    temp_tri.Vertex1 = point1;
                    temp_tri.Vertex2 = point2;
                    temp_tri.Vertex3 = point3;
                    STL_menber.Add(temp_tri);
                }

            }
            if ((circle_plane_dir1.x_dir == 0) && (circle_plane_dir1.y_dir == 0) && (circle_plane_dir1.z_dir < 0))
            {
                //建立方向
                dir_temp = new STLDirection();
                dir_temp.x_dir = 0;
                dir_temp.y_dir = 0;
                dir_temp.z_dir = -1;

                for (int i = 0; i < CircleDivision; ++i)
                {
                    //建立第一个点
                    point1 = new STLPoint();
                    point1.x = (float)circle_center_point.x_coord;
                    point1.y = (float)circle_center_point.y_coord;
                    point1.z = (float)circle_center_point.z_coord;
                    //建立第二个点
                    point2 = new STLPoint();
                    point2.x = (float)circle_center_point.x_coord + (float)(radius * Math.Cos(2 * Math.PI * (i / (float)CircleDivision)));
                    point2.y = (float)circle_center_point.y_coord + (float)(radius * Math.Sin(2 * Math.PI * (i / (float)CircleDivision)));
                    point2.z = (float)circle_center_point.z_coord;
                    //建立第三个点
                    point3 = new STLPoint();
                    point3.x = (float)circle_center_point.x_coord + (float)(radius * Math.Cos(2 * Math.PI * ((i + 1) / (float)CircleDivision)));
                    point3.y = (float)circle_center_point.y_coord + (float)(radius * Math.Sin(2 * Math.PI * ((i + 1) / (float)CircleDivision)));
                    point3.z = (float)circle_center_point.z_coord;
                    //将点和方向插入到三角形结构体之中
                    temp_tri = new STLTriangle();
                    temp_tri.Dir = dir_temp;
                    temp_tri.Vertex1 = point1;
                    temp_tri.Vertex2 = point2;
                    temp_tri.Vertex3 = point3;
                    STL_menber.Add(temp_tri);
                }

            }
            if ((circle_plane_dir1.x_dir == 0) && (circle_plane_dir1.y_dir != 0) && (circle_plane_dir1.z_dir == 0))
            {
                //
            }
            if ((circle_plane_dir1.x_dir != 0) && (circle_plane_dir1.y_dir == 0) && (circle_plane_dir1.z_dir == 0))
            {
                //
            }
        }
        public void CreateCylinderMesh(int index)//创建圆柱面网格
        {
            //取出对应元素的索引信息
            ArrayList StepIdOfBound = Form1.MainStorage.LookForChildByIndex(index);

            int StepIdOfLoop1 = Form1.MainStorage.LookForFirstChild((int)StepIdOfBound[0]);
            int StepIdOfOriented1 = Form1.MainStorage.LookForFirstChild(StepIdOfLoop1);
            int StepIdOfCurve1 = Form1.MainStorage.LookForFirstChild(StepIdOfOriented1);
            int StepIdOfCircle1 = Form1.MainStorage.LookForLastChild(StepIdOfCurve1);
            int IndexOfCircle1 = Form1.MainStorage.LookForNode(StepIdOfCircle1);
            int StepIdOfCircleAXIS1 = Form1.MainStorage.LookForFirstChild(StepIdOfCircle1);
            int StepIdOfCirclePoint1 = Form1.MainStorage.LookForFirstChild(StepIdOfCircleAXIS1);//读出圆心1
            TableItem temp1 = (TableItem)Form1.MainStorage.TableNode[Form1.MainStorage.LookForNode(StepIdOfCirclePoint1)];
            float Z1_coord = (float)((CARTESIAN_POINT)temp1.node).z_coord;

            int StepIdOfLoop2 = Form1.MainStorage.LookForFirstChild((int)StepIdOfBound[1]);
            int StepIdOfOriented2 = Form1.MainStorage.LookForFirstChild(StepIdOfLoop2);
            int StepIdOfCurve2 = Form1.MainStorage.LookForFirstChild(StepIdOfOriented2);
            int StepIdOfCircle2 = Form1.MainStorage.LookForLastChild(StepIdOfCurve2);
            int IndexOfCircle2 = Form1.MainStorage.LookForNode(StepIdOfCircle2);
            int StepIdOfCircleAXIS2 = Form1.MainStorage.LookForFirstChild(StepIdOfCircle2);
            int StepIdOfCirclePoint2 = Form1.MainStorage.LookForFirstChild(StepIdOfCircleAXIS2);//读出圆心2
            TableItem temp2 = (TableItem)Form1.MainStorage.TableNode[Form1.MainStorage.LookForNode(StepIdOfCirclePoint2)];
            float Z2_coord = (float)((CARTESIAN_POINT)temp2.node).z_coord;

            float height = (float)Math.Abs(Z1_coord - Z2_coord);//求出圆柱的高度
            int StepIdOfAxis3 = Form1.MainStorage.LookForFirstChild((int)StepIdOfBound[2]);
            ArrayList all_child = Form1.MainStorage.LookForChild(StepIdOfAxis3);
            int StepIdOfCircleCenter = (int)all_child[0];//找到底面中心索引
            int StepIdOfCylinderDirection = (int)all_child[1];//找到方向索引
            TableItem Cylinder = (TableItem)Form1.MainStorage.TableNode[Form1.MainStorage.LookForNode((int)StepIdOfBound[2])];
            CYLINDRICAL_SURFACE Cylinder_obj = (CYLINDRICAL_SURFACE)Cylinder.node;
            float radius = (float)Cylinder_obj.radius;
            //读出底面的
            //从STEP文件编号中中实例化出信息
            TableItem TEMP_CYL = (TableItem)(Form1.MainStorage.TableNode[Form1.MainStorage.LookForNode(StepIdOfCircleCenter)]);
            CARTESIAN_POINT Cylinder_center = (CARTESIAN_POINT)TEMP_CYL.node;
            TableItem TEMP_CYL_DIR = (TableItem)(Form1.MainStorage.TableNode[Form1.MainStorage.LookForNode(StepIdOfCylinderDirection)]);
            DIRECTION Cylinder_dir = (DIRECTION)TEMP_CYL_DIR.node;

            STLDirection dir_temp;
            STLDirection dir_temp2;
            STLPoint point1;
            STLPoint point2;
            STLPoint point3;
            STLTriangle temp_tri1;
            STLPoint point4;
            STLTriangle temp_tri2;
            //条件判断，如果符合一定的特殊情况，那么就开始执行三角剖分
            if ((Cylinder_dir.x_dir == 0) && (Cylinder_dir.y_dir == 0) && (Cylinder_dir.z_dir > 0))
            {
                for (int i = 0; i < CircleDivision; ++i)
                {
                    //建立方向
                    dir_temp = new STLDirection();
                    dir_temp.x_dir = (float)Cylinder_center.x_coord + radius*(float)Math.Cos(0.5*( 2*Math.PI* (i / (float)CircleDivision) + 2*Math.PI * ((i + 1) / (float)CircleDivision)));
                    dir_temp.y_dir = (float)Cylinder_center.y_coord + radius*(float)Math.Sin(0.5 * (2*Math.PI* (i / (float)CircleDivision) + 2*Math.PI * ((i + 1) / (float)CircleDivision)));
                    dir_temp.z_dir = (float)Cylinder_center.z_coord;

                    //以下部分建立第一个三角形
                    //建立第一个点
                    point1 = new STLPoint();
                    point1.x = (float)Cylinder_center.x_coord + radius * (float)Math.Cos(2*Math.PI * (i / (float)CircleDivision));
                    point1.y = (float)Cylinder_center.y_coord + radius * (float)Math.Sin(2 * Math.PI * (i / (float)CircleDivision));
                    point1.z = (float)Cylinder_center.z_coord;
                    //建立第二个点
                    point2 = new STLPoint();
                    point2.x = (float)Cylinder_center.x_coord + radius * (float)Math.Cos(2 * Math.PI * ((i+1) / (float)CircleDivision));
                    point2.y = (float)Cylinder_center.y_coord + radius * (float)Math.Sin(2 * Math.PI * ((i+1) / (float)CircleDivision));
                    point2.z = (float)Cylinder_center.z_coord;
                    //建立第三个点
                    point3 = new STLPoint();
                    point3.x = (float)Cylinder_center.x_coord + radius * (float)Math.Cos(2 * Math.PI * (i / (float)CircleDivision));
                    point3.y = (float)Cylinder_center.y_coord + radius * (float)Math.Sin(2 * Math.PI * (i / (float)CircleDivision));
                    point3.z = (float)Cylinder_center.z_coord + height;

                    //以下部分建立第二个三角形
                    //前面有两个点可以共用，因此只用建立起一个点
                    point4 = new STLPoint();
                    point4.x = (float)Cylinder_center.x_coord + radius*(float)Math.Cos(2*Math.PI * ((i + 1) / (float)CircleDivision));
                    point4.y = (float)Cylinder_center.y_coord + radius*(float)Math.Sin(2*Math.PI * ((i + 1) / (float)CircleDivision));
                    point4.z = (float)Cylinder_center.z_coord + height;

                    //将点和方向插入到三角形结构体之中

                    temp_tri1 = new STLTriangle();
                    temp_tri1.Dir = dir_temp;
                    temp_tri1.Vertex1 = point1;
                    temp_tri1.Vertex2 = point2;
                    temp_tri1.Vertex3 = point3;
                    STL_menber.Add(temp_tri1);


                    temp_tri2 = new STLTriangle();
                    temp_tri2.Dir = dir_temp;
                    temp_tri2.Vertex1 = point3;
                    temp_tri2.Vertex2 = point4;
                    temp_tri2.Vertex3 = point2;
                    STL_menber.Add(temp_tri2);

                }

            }

            if ((Cylinder_dir.x_dir == 0) && (Cylinder_dir.y_dir > 0) && (Cylinder_dir.z_dir == 0))
            //条件判断，如果符合一定的特殊情况，那么就开始执行三角剖分
            {
            //
            }
            if ((Cylinder_dir.x_dir > 0) && (Cylinder_dir.y_dir == 0) && (Cylinder_dir.z_dir == 0))
            {
                //
            }
        }
        public void LookForAdvancedFace()
        {
            for (int i = 0; i < Form1.MainStorage.TableNode.Count; ++i)
            {
                TableItem temp=(TableItem)(Form1.MainStorage.TableNode[i]);
                if ( temp.TypeFlag== 13)//判断是否是高级面
                {
                    AdvancedFaceIndex.Add(i);
                }
            }
        }
    }
}