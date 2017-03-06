using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace StepDecodeAndDisplay
{
    struct STLDirection
    {
        public float x_dir;
        public float y_dir;
        public float z_dir;
    };
    struct STLPoint
    {
        public float x;
        public float y;
        public float z;
    };
    struct STLTriangle
    {
        public STLDirection Dir;
        public STLPoint Vertex1;
        public STLPoint Vertex2;
        public STLPoint Vertex3;
    };

    //信息存储结构体（FINAL）
    struct CARTESIAN_POINT//笛卡尔点
    {
        public string property;//属性
        public double x_coord; //x坐标
        public double y_coord; //y坐标
        public double z_coord; //z坐标
    };
    struct DIRECTION//方向
    {
        public string property;//属性
        public double x_dir;//x方向
        public double y_dir;//y方向
        public double z_dir;//z方向
    };
    struct VERTEX_POINT//顶点
    {
        public string property;//属性
    };
    struct CIRCLE//圆
    {
        public string property;//属性
        public double radius;//半径
    };
    struct EDGE_CURVE//边界曲线
    {
        public string property;//属性
        public bool dir;       //布尔值表示方向
    };
    struct ORIENTED_EDGE//导向边
    {
        public string property;//属性
        public string flag_1;  //一个星号，意义不明
        public string flag_2;  //一个星号，意义不明
        public bool dir;       //布尔值表示方向
    };
    struct AXIS2_PLACEMENT_3D//3D放置的轴
    {
        public string property;//属性
    };
    struct EDGE_LOOP//环形边
    {
        public string property;//属性
    };
    struct CYLINDRICAL_SURFACE//圆柱面
    {
        public string property;//属性
        public double radius;//半径
    };
    struct PLANE//平面
    {
        public string property;//属性
    };
    struct FACE_OUTER_BOUND//外侧面边界
    {
        public string property;//属性
        public bool dir;       //布尔值表示方向
    };
    struct FACE_BOUND//面边界
    {
        public string property;//属性
        public bool dir;       //布尔值表示方向
    };
    struct ADVANCED_FACE//高级面
    {
        public string property;//属性
        public bool dir;       //布尔值表示方向
    };
    struct CLOSED_SHELL//封闭壳体
    {
        public string property;//属性
    };
    struct MANIFOLD_SOLID_BREP//组合B-rep表示
    {
        public string property;//属性
    };
    struct ADVANCED_BREP_SHAPE_REPRESENTATION//高级B-rep形状表示
    {
        //空白待拓展
    };
    //表项定义
    struct TableItem//表项（FINAL）
    {
        public int STEPId;//step特征ID
        public ArrayList child;//孩子结点向量，向量里面记录的是孩子结点的STEPId
        public int TypeFlag;//结点类型标记
        public object node;//信息节点
        public string GetTypeName()
        {
            switch (TypeFlag)
            {
                case 1: return "笛卡尔点"; break;
                case 2: return "方向"; break;
                case 3: return "顶点"; break;
                case 4: return "圆"; break;
                case 5: return "边界曲线"; break;
                case 6: return "导向边"; break;
                case 7: return "轴"; break;
                case 8: return "环形边"; break;
                case 9: return "圆柱面"; break;
                case 10:return "平面"; break;
                case 11:return "外侧面边界"; break;
                case 12:return "面边界"; break;
                case 13:return "高级面"; break;
                case 14: return "封闭壳体"; break;
                case 15: return "组合B-rep表示"; break;
                case 16: return "高级B-rep形状表示"; break;
                default:return "";
            }
        }
    };
}