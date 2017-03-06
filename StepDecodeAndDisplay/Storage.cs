using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Text;
using System.IO;

namespace StepDecodeAndDisplay
{

    public class Storage
    {
        //符号常量定义（FINAL）
        public const int CARTESIAN_POINT_ = 1;//笛卡尔点
        public const int DIRECTION_ = 2;//方向
        public const int VERTEX_POINT_ = 3;//顶点
        public const int CIRCLE_ = 4;//圆
        public const int EDGE_CURVE_ = 5;//边界曲线
        public const int ORIENTED_EDGE_ = 6;//导向边
        public const int AXIS2_PLACEMENT_3D_ = 7;//轴
        public const int EDGE_LOOP_ = 8;//环形边
        public const int CYLINDRICAL_SURFACE_ = 9;//圆柱面
        public const int PLANE_ = 10;           //平面
        public const int FACE_OUTER_BOUND_ = 11;//外侧面边界
        public const int FACE_BOUND_ = 12;//面边界
        public const int ADVANCED_FACE_ = 13;//高级面
        public const int CLOSED_SHELL_ = 14;//封闭壳体
        public const int MANIFOLD_SOLID_BREP_ = 15;//组合B-rep表示
        public const int ADVANCED_BREP_SHAPE_REPRESENTATION_ = 16;//高级B-rep表示
        //数据成员定义
        public ArrayList TableNode;    //结构体向量，存储STEP元素几何信息和拓扑信息
        public StringBuilder str_temp; //暂存来自文件读取对象的字符串，供信息提取用到

        //方法定义
        //构造函数和析构函数
        public Storage()//默认构造函数
        {
            TableNode = new ArrayList();
        }
        ~Storage()//默认析构函数
        {
            //
        }
        public void DecodeCARTESIAN_POINT()//解码笛卡尔点字符串
        {
            //插入笛卡尔点
            TableItem temp = new TableItem();
            temp.child = new ArrayList();
            StringBuilder reg = new StringBuilder();//存储所有中间字符串
            int i = 0;//循环控制变量
            //STEPId解码
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.STEPId = int.Parse(reg.ToString());
            //无孩子结点，无需解码
            //TypeFlag解码
            temp.TypeFlag = CARTESIAN_POINT_;
            //信息节点解码
            CARTESIAN_POINT struct_tmp = new CARTESIAN_POINT();//建立结构体，并将指针赋值给共用体
            reg.Clear();//清空字符串
            while (this.str_temp[i] != '\'')//跳过前面的字符，直接读取有效信息
            {
                ++i;
            }
            ++i;
            //读取字符串'XX'里的属性
            while (this.str_temp[i] != '\'')
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            struct_tmp.property = reg.ToString();
            reg.Clear();
            //读取x坐标信息
            while (!Char.IsDigit(this.str_temp[i]) && (this.str_temp[i] != '+') && (this.str_temp[i] != '-') && (this.str_temp[i] != '.'))//跳过无用的字符
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]) || (this.str_temp[i] == '+') || (this.str_temp[i] == '-') || (this.str_temp[i] == '.'))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            struct_tmp.x_coord = double.Parse(reg.ToString());
            reg.Clear();
            //读取y坐标信息
            while (!Char.IsDigit(this.str_temp[i]) && (this.str_temp[i] != '+') && (this.str_temp[i] != '-') && (this.str_temp[i] != '.'))//跳过无用的字符
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]) || (this.str_temp[i] == '+') || (this.str_temp[i] == '-') || (this.str_temp[i] == '.'))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            struct_tmp.y_coord = double.Parse(reg.ToString());
            reg.Clear();
            //读取z坐标信息
            while (!Char.IsDigit(this.str_temp[i]) && (this.str_temp[i] != '+') && (this.str_temp[i] != '-') && (this.str_temp[i] != '.'))//跳过无用的字符
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]) || (this.str_temp[i] == '+') || (this.str_temp[i] == '-') || (this.str_temp[i] == '.'))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            struct_tmp.z_coord = double.Parse(reg.ToString());
            reg.Clear();
            //将结构体插入到向量里
            temp.node = struct_tmp;
            this.TableNode.Add(temp);
        }
        public void DecodeDIRECTION()//解码方向字符串
        {
            //插入方向
            TableItem temp = new TableItem();
            temp.child = new ArrayList();
            StringBuilder reg = new StringBuilder();//存储所有中间字符串
            int i = 0;//循环控制变量
                      //STEPId解码
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.STEPId = int.Parse(reg.ToString());
            //无孩子结点，无需解码
            //TypeFlag解码
            temp.TypeFlag = DIRECTION_;
            //信息节点解码
            DIRECTION struct_tmp = new DIRECTION();//建立结构体，并将指针赋值给共用体
            reg.Clear();//清空字符串
            while (this.str_temp[i] != '\'')//跳过前面的字符，直接读取有效信息
            {
                ++i;
            }
            ++i;
            //读取字符串'XX'里的属性
            while (this.str_temp[i] != '\'')
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            struct_tmp.property = reg.ToString();
            reg.Clear();
            //读取x坐标信息
            while (!Char.IsDigit(this.str_temp[i]) && (this.str_temp[i] != '+') && (this.str_temp[i] != '-') && (this.str_temp[i] != '.'))//跳过无用的字符
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]) || (this.str_temp[i] == '+') || (this.str_temp[i] == '-') || (this.str_temp[i] == '.'))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            struct_tmp.x_dir = double.Parse(reg.ToString());
            reg.Clear();
            //读取y坐标信息
            while (!Char.IsDigit(this.str_temp[i]) && (this.str_temp[i] != '+') && (this.str_temp[i] != '-') && (this.str_temp[i] != '.'))//跳过无用的字符
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]) || (this.str_temp[i] == '+') || (this.str_temp[i] == '-') || (this.str_temp[i] == '.'))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            struct_tmp.y_dir = double.Parse(reg.ToString());
            reg.Clear();
            //读取z坐标信息
            while (!Char.IsDigit(this.str_temp[i]) && (this.str_temp[i] != '+') && (this.str_temp[i] != '-') && (this.str_temp[i] != '.'))//跳过无用的字符
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]) || (this.str_temp[i] == '+') || (this.str_temp[i] == '-') || (this.str_temp[i] == '.'))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            struct_tmp.z_dir = double.Parse(reg.ToString());
            reg.Clear();
            //将结构体插入到向量里
            temp.node = struct_tmp;
            this.TableNode.Add(temp);
        }
        public void DecodeVERTEX_POINT()//解码顶点字符串
        {
            //插入顶点类型
            TableItem temp = new TableItem();
            temp.child = new ArrayList();
            StringBuilder reg = new StringBuilder();//存储所有中间字符串
            int i = 0;//循环控制变量
            //STEPId解码
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.STEPId = int.Parse(reg.ToString());
            //TypeFlag解码
            temp.TypeFlag = VERTEX_POINT_;
            //信息节点解码
            VERTEX_POINT struct_tmp = new VERTEX_POINT();//建立结构体，并将指针赋值给共用体
            reg.Clear();//清空字符串
            while (this.str_temp[i] != '\'')//跳过前面的字符，直接读取有效信息
            {
                ++i;
            }
            ++i;
            //读取字符串'XX'里的属性
            while (this.str_temp[i] != '\'')
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            struct_tmp.property = reg.ToString();
            reg.Clear();

            //读取孩子结点信息
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.child.Add(int.Parse(reg.ToString()));
            reg.Clear();
            temp.node = struct_tmp;
            //将结构体插入到向量里
            TableNode.Add(temp);
        }
        public void DecodeCIRCLE()//解码圆字符串
        {
            //插入向量
            TableItem temp = new TableItem();
            temp.child = new ArrayList();
            StringBuilder reg = new StringBuilder();//存储所有中间字符串
            int i = 0;//循环控制变量
            //STEPId解码
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.STEPId = int.Parse(reg.ToString());
            //无孩子结点，无需解码
            //TypeFlag解码
            temp.TypeFlag = CIRCLE_;
            //信息节点解码
            CIRCLE struct_tmp = new CIRCLE();//建立结构体，并将指针赋值给共用体
            reg.Clear();//清空字符串
            while (this.str_temp[i] != '\'')//跳过前面的字符，直接读取有效信息
            {
                ++i;
            }
            ++i;
            //读取字符串'XX'里的属性
            while (this.str_temp[i] != '\'')
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            struct_tmp.property = reg.ToString();
            reg.Clear();

            //读取孩子结点信息
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.child.Add(int.Parse(reg.ToString()));
            reg.Clear();
            //读取一行属性
            while (!Char.IsDigit(this.str_temp[i]) && (this.str_temp[i] != '+') && (this.str_temp[i] != '-') && (this.str_temp[i] != '.'))//跳过无用的字符
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]) || (this.str_temp[i] == '+') || (this.str_temp[i] == '-') || (this.str_temp[i] == '.'))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            struct_tmp.radius = double.Parse(reg.ToString());
            reg.Clear();

            temp.node = struct_tmp;
            //将结构体插入到向量里
            TableNode.Add(temp);
        }
        public void DecodeEDGE_CURVE()//解码边界曲线字符串
        {
            //插入线
            TableItem temp = new TableItem();
            temp.child = new ArrayList();
            StringBuilder reg = new StringBuilder();//存储所有中间字符串
            int i = 0;//循环控制变量
            //STEPId解码
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.STEPId = int.Parse(reg.ToString());
            //无孩子结点，无需解码
            //TypeFlag解码
            temp.TypeFlag = EDGE_CURVE_;
            //信息节点解码
            EDGE_CURVE struct_tmp = new EDGE_CURVE();//建立结构体，并将指针赋值给共用体
            reg.Clear();//清空字符串
            while (this.str_temp[i] != '\'')//跳过前面的字符，直接读取有效信息
            {
                ++i;
            }
            ++i;
            //读取字符串'XX'里的属性
            while (this.str_temp[i] != '\'')
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            struct_tmp.property = reg.ToString();
            reg.Clear();

            //读取孩子结点信息
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.child.Add(int.Parse(reg.ToString()));
            reg.Clear();
            //读取孩子2结点信息
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.child.Add(int.Parse(reg.ToString()));
            reg.Clear();
            //读取孩子3结点信息
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.child.Add(int.Parse(reg.ToString()));
            reg.Clear();
            //插入方向信息
            while (this.str_temp[i] != '.')
            {
                ++i;
            }
            ++i;
            if (this.str_temp[i] == 'T')
            {
                struct_tmp.dir = true;
            }
            else
            {
                struct_tmp.dir = false;
            }
            temp.node = struct_tmp;
            //将结构体插入到向量里
            TableNode.Add(temp);
        }
        public void DecodeORIENTED_EDGE()     //解码导向边字符串
        {
            //插入方向
            TableItem temp = new TableItem();
            temp.child = new ArrayList();
            StringBuilder reg = new StringBuilder();//存储所有中间字符串
            int i = 0;//循环控制变量
                      //STEPId解码
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.STEPId = int.Parse(reg.ToString());
            //无孩子结点，无需解码
            //TypeFlag解码
            temp.TypeFlag = ORIENTED_EDGE_;
            //信息节点解码
            ORIENTED_EDGE struct_tmp = new ORIENTED_EDGE();//建立结构体，并将指针赋值给共用体
            reg.Clear();//清空字符串
            while (this.str_temp[i] != '\'')//跳过前面的字符，直接读取有效信息
            {
                ++i;
            }
            ++i;
            //读取字符串'XX'里的属性
            while (this.str_temp[i] != '\'')
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            struct_tmp.property = reg.ToString();
            reg.Clear();

            //两个星号，意义不明
            struct_tmp.flag_1 = "*";
            struct_tmp.flag_2 = "*";
            //读取孩子节点信息
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.child.Add(int.Parse(reg.ToString()));
            reg.Clear();
            //插入方向信息
            while (this.str_temp[i] != '.')
            {
                ++i;
            }
            ++i;
            if (this.str_temp[i] == 'T')
            {
                struct_tmp.dir = true;
            }
            else
            {
                struct_tmp.dir = false;
            }
            //将结构体插入到向量里
            temp.node = struct_tmp;
            this.TableNode.Add(temp);
        }
        public void DecodeAXIS2_PLACEMENT_3D()//解码3D放置轴线字符串
        {
            //插入轴
            TableItem temp = new TableItem();
            temp.child = new ArrayList();
            StringBuilder reg = new StringBuilder();//存储所有中间字符串
            int i = 0;//循环控制变量
            //STEPId解码
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.STEPId = int.Parse(reg.ToString());
            //无孩子结点，无需解码
            //TypeFlag解码
            temp.TypeFlag = AXIS2_PLACEMENT_3D_;
            //信息节点解码
            AXIS2_PLACEMENT_3D struct_tmp = new AXIS2_PLACEMENT_3D();//建立结构体，并将指针赋值给共用体
            reg.Clear();//清空字符串
            while (this.str_temp[i] != '\'')//跳过前面的字符，直接读取有效信息
            {
                ++i;
            }
            ++i;
            //读取字符串'XX'里的属性
            while (this.str_temp[i] != '\'')
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            struct_tmp.property = reg.ToString();
            reg.Clear();

            //读取孩子结点信息
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.child.Add(int.Parse(reg.ToString()));
            reg.Clear();
            //读取孩子2结点信息
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.child.Add(int.Parse(reg.ToString()));
            reg.Clear();
            //读取孩子3结点信息
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.child.Add(int.Parse(reg.ToString()));
            reg.Clear();

            temp.node = struct_tmp;
            //将结构体插入到向量里
            TableNode.Add(temp);
        }
        public void DecodeEDGE_LOOP()         //解码环形边字符串
        {
            //插入向量
            TableItem temp = new TableItem();
            temp.child = new ArrayList();
            StringBuilder reg = new StringBuilder();//存储所有中间字符串
            int i = 0;//循环控制变量
            //STEPId解码
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.STEPId = int.Parse(reg.ToString());
            //无孩子结点，无需解码
            //TypeFlag解码
            temp.TypeFlag = EDGE_LOOP_;
            //信息节点解码
            EDGE_LOOP struct_tmp = new EDGE_LOOP();//建立结构体，并将指针赋值给共用体
            reg.Clear();//清空字符串
            while (this.str_temp[i] != '\'')//跳过前面的字符，直接读取有效信息
            {
                ++i;
            }
            ++i;
            //读取字符串'XX'里的属性
            while (this.str_temp[i] != '\'')
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            struct_tmp.property = reg.ToString();
            reg.Clear();
            //读取多个子结点信息，长度不定
            while (this.str_temp[i] != '(')
            {
                ++i;
            }
            i++;
            while (this.str_temp[i] != ')')
            {
                if (!Char.IsDigit(this.str_temp[i]))
                {
                    ++i;
                }
                else if (Char.IsDigit(this.str_temp[i]) && (!Char.IsDigit(this.str_temp[i + 1])))
                {
                    reg.Append(this.str_temp[i]);
                    temp.child.Add(int.Parse(reg.ToString()));
                    reg.Clear();
                    ++i;
                }
                else
                {
                    reg.Append(this.str_temp[i]);
                    ++i;
                }
            }

            //将结构体插入到向量里
            temp.node = struct_tmp;
            TableNode.Add(temp);
        }
        public void DecodeCYLINDRICAL_SURFACE()//解码圆柱面字符串
        {
            //插入圆柱面
            TableItem temp = new TableItem();
            temp.child = new ArrayList();
            StringBuilder reg = new StringBuilder();//存储所有中间字符串
            int i = 0;//循环控制变量
            //STEPId解码
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.STEPId = int.Parse(reg.ToString());
            //无孩子结点，无需解码
            //TypeFlag解码
            temp.TypeFlag = CYLINDRICAL_SURFACE_;
            //信息节点解码
            CYLINDRICAL_SURFACE struct_tmp = new CYLINDRICAL_SURFACE();//建立结构体，并将指针赋值给共用体
            reg.Clear();//清空字符串
            while (this.str_temp[i] != '\'')//跳过前面的字符，直接读取有效信息
            {
                ++i;
            }
            ++i;
            //读取字符串'XX'里的属性
            while (this.str_temp[i] != '\'')
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            struct_tmp.property = reg.ToString();
            reg.Clear();

            //读取孩子结点信息
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.child.Add(int.Parse(reg.ToString()));
            reg.Clear();
            //读取一行属性
            while (!Char.IsDigit(this.str_temp[i]) && (this.str_temp[i] != '+') && (this.str_temp[i] != '-') && (this.str_temp[i] != '.'))//跳过无用的字符
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]) || (this.str_temp[i] == '+') || (this.str_temp[i] == '-') || (this.str_temp[i] == '.'))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            struct_tmp.radius = double.Parse(reg.ToString());
            reg.Clear();
            temp.node = struct_tmp;
            //将结构体插入到向量里
            TableNode.Add(temp);
        }
        public void DecodePLANE()         //解码平面字符串
        {
            //插入线
            TableItem temp = new TableItem();
            temp.child = new ArrayList();
            StringBuilder reg = new StringBuilder();//存储所有中间字符串
            int i = 0;//循环控制变量
            //STEPId解码
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.STEPId = int.Parse(reg.ToString());
            //TypeFlag解码
            temp.TypeFlag = PLANE_;
            //信息节点解码
            PLANE struct_tmp = new PLANE();//建立结构体，并将指针赋值给共用体
            reg.Clear();//清空字符串
            while (this.str_temp[i] != '\'')//跳过前面的字符，直接读取有效信息
            {
                ++i;
            }
            ++i;
            //读取字符串'XX'里的属性
            while (this.str_temp[i] != '\'')
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            struct_tmp.property = reg.ToString();
            reg.Clear();

            //读取孩子结点信息
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.child.Add(int.Parse(reg.ToString()));
            reg.Clear();
            temp.node = struct_tmp;
            //将结构体插入到向量里
            TableNode.Add(temp);
        }
        public void DecodeFACE_OUTER_BOUND()  //解码外侧面边界字符串
        {
            //插入线
            TableItem temp = new TableItem();
            temp.child = new ArrayList();
            StringBuilder reg = new StringBuilder();//存储所有中间字符串
            int i = 0;//循环控制变量
            //STEPId解码
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.STEPId = int.Parse(reg.ToString());
            //无孩子结点，无需解码
            //TypeFlag解码
            temp.TypeFlag = FACE_OUTER_BOUND_;
            //信息节点解码
            FACE_OUTER_BOUND struct_tmp = new FACE_OUTER_BOUND();//建立结构体，并将指针赋值给共用体
            reg.Clear();//清空字符串
            while (this.str_temp[i] != '\'')//跳过前面的字符，直接读取有效信息
            {
                ++i;
            }
            ++i;
            //读取字符串'XX'里的属性
            while (this.str_temp[i] != '\'')
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            struct_tmp.property = reg.ToString();
            reg.Clear();

            //读取孩子结点信息
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.child.Add(int.Parse(reg.ToString()));
            reg.Clear();
            //插入方向信息
            while (this.str_temp[i] != '.')
            {
                ++i;
            }
            ++i;
            if (this.str_temp[i] == 'T')
            {
                struct_tmp.dir = true;
            }
            else
            {
                struct_tmp.dir = false;
            }
            temp.node = struct_tmp;
            //将结构体插入到向量里
            TableNode.Add(temp);
        }
        public void DecodeFACE_BOUND()  //解码面边界字符串
        {
            //插入线
            TableItem temp = new TableItem();
            temp.child = new ArrayList();
            StringBuilder reg = new StringBuilder();//存储所有中间字符串
            int i = 0;//循环控制变量
            //STEPId解码
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.STEPId = int.Parse(reg.ToString());
            //无孩子结点，无需解码
            //TypeFlag解码
            temp.TypeFlag = FACE_BOUND_;
            //信息节点解码
            FACE_BOUND struct_tmp = new FACE_BOUND();//建立结构体，并将指针赋值给共用体
            reg.Clear();//清空字符串
            while (this.str_temp[i] != '\'')//跳过前面的字符，直接读取有效信息
            {
                ++i;
            }
            ++i;
            //读取字符串'XX'里的属性
            while (this.str_temp[i] != '\'')
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            struct_tmp.property = reg.ToString();
            reg.Clear();

            //读取孩子结点信息
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.child.Add(int.Parse(reg.ToString()));
            reg.Clear();
            //插入方向信息
            while (this.str_temp[i] != '.')
            {
                ++i;
            }
            ++i;
            if (this.str_temp[i] == 'T')
            {
                struct_tmp.dir = true;
            }
            else
            {
                struct_tmp.dir = false;
            }
            temp.node = struct_tmp;
            //将结构体插入到向量里
            TableNode.Add(temp);
        }
        public void DecodeADVANCED_FACE()     //解码高级面字符串
        {
            //插入线
            TableItem temp = new TableItem();
            temp.child = new ArrayList();
            StringBuilder reg = new StringBuilder();//存储所有中间字符串
            int i = 0;//循环控制变量
            //STEPId解码
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.STEPId = int.Parse(reg.ToString());
            //TypeFlag解码
            temp.TypeFlag = ADVANCED_FACE_;
            //信息节点解码
            ADVANCED_FACE struct_tmp = new ADVANCED_FACE();//建立结构体，并将指针赋值给共用体
            reg.Clear();//清空字符串
            while (this.str_temp[i] != '\'')//跳过前面的字符，直接读取有效信息
            {
                ++i;
            }
            ++i;
            //读取字符串'XX'里的属性
            while (this.str_temp[i] != '\'')
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            struct_tmp.property = reg.ToString();
            reg.Clear();

            //读取多个子结点信息，长度不定
            while (this.str_temp[i] != '(')
            {
                ++i;
            }
            i++;
            while (this.str_temp[i] != ')')
            {
                if (!Char.IsDigit(this.str_temp[i]))
                {
                    ++i;
                }
                else if (Char.IsDigit(this.str_temp[i]) && (!Char.IsDigit(this.str_temp[i + 1])))
                {
                    reg.Append(this.str_temp[i]);
                    temp.child.Add(int.Parse(reg.ToString()));
                    reg.Clear();
                    ++i;
                }
                else
                {
                    reg.Append(this.str_temp[i]);
                    ++i;
                }
            }

            //读取最后一个孩子结点信息
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.child.Add(int.Parse(reg.ToString()));
            reg.Clear();
            //插入方向信息
            while (this.str_temp[i] != '.')
            {
                ++i;
            }
            ++i;
            if (this.str_temp[i] == 'T')
            {
                struct_tmp.dir = true;
            }
            else
            {
                struct_tmp.dir = false;
            }
            temp.node = struct_tmp;
            //将结构体插入到向量里
            TableNode.Add(temp);
        }
        public void DecodeCLOSED_SHELL()   //解码封闭壳体字符串
        {
            //插入向量
            TableItem temp = new TableItem();
            temp.child = new ArrayList();
            StringBuilder reg = new StringBuilder();//存储所有中间字符串
            int i = 0;//循环控制变量
            //STEPId解码
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.STEPId = int.Parse(reg.ToString());
            //无孩子结点，无需解码
            //TypeFlag解码
            temp.TypeFlag = CLOSED_SHELL_;
            //信息节点解码
            CLOSED_SHELL struct_tmp = new CLOSED_SHELL();//建立结构体，并将指针赋值给共用体
            reg.Clear();//清空字符串
            while (this.str_temp[i] != '\'')//跳过前面的字符，直接读取有效信息
            {
                ++i;
            }
            ++i;
            //读取字符串'XX'里的属性
            while (this.str_temp[i] != '\'')
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            struct_tmp.property = reg.ToString();
            reg.Clear();
            //读取多个子结点信息，长度不定
            while (this.str_temp[i] != '(')
            {
                ++i;
            }
            i++;
            while (this.str_temp[i] != ')')
            {
                if (!Char.IsDigit(this.str_temp[i]))
                {
                    ++i;
                }
                else if (Char.IsDigit(this.str_temp[i]) && (!Char.IsDigit(this.str_temp[i + 1])))
                {
                    reg.Append(this.str_temp[i]);
                    temp.child.Add(int.Parse(reg.ToString()));
                    reg.Clear();
                    ++i;
                }
                else
                {
                    reg.Append(this.str_temp[i]);
                    ++i;
                }
            }

            //将结构体插入到向量里
            temp.node = struct_tmp;
            TableNode.Add(temp);
        }
        public void DecodeMANIFOLD_SOLID_BREP()//解码组合B-rep表示字符串
        {
            //插入组合B-Rep表示
            TableItem temp = new TableItem();
            temp.child = new ArrayList();
            StringBuilder reg = new StringBuilder();//存储所有中间字符串
            int i = 0;//循环控制变量
            //STEPId解码
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.STEPId = int.Parse(reg.ToString());
            //TypeFlag解码
            temp.TypeFlag = MANIFOLD_SOLID_BREP_;
            //信息节点解码
            MANIFOLD_SOLID_BREP struct_tmp = new MANIFOLD_SOLID_BREP();//建立结构体，并将指针赋值给共用体
            reg.Clear();//清空字符串
            while (this.str_temp[i] != '\'')//跳过前面的字符，直接读取有效信息
            {
                ++i;
            }
            ++i;
            //读取字符串'XX'里的属性
            while (this.str_temp[i] != '\'')
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            struct_tmp.property = reg.ToString();
            reg.Clear();

            //读取孩子结点信息
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.child.Add(int.Parse(reg.ToString()));
            reg.Clear();
            temp.node = struct_tmp;
            //将结构体插入到向量里
            TableNode.Add(temp);
        }
        public void DecodeADVANCED_BREP_SHAPE_REPRESENTATION()//解码高级B-rep形状表示字符串
        {
            //插入高级B-rep形状表示
            TableItem temp = new TableItem();
            temp.child = new ArrayList();
            StringBuilder reg = new StringBuilder();//存储所有中间字符串
            int i = 0;//循环控制变量
            //STEPId解码
            while (!Char.IsDigit(this.str_temp[i]))//跳过无用的字符，直接找到数字
            {
                ++i;
            }
            while (Char.IsDigit(this.str_temp[i]))
            {
                reg.Append(this.str_temp[i]);
                ++i;
            }
            temp.STEPId = int.Parse(reg.ToString());
            //无孩子结点，无需解码
            //TypeFlag解码
            temp.TypeFlag = ADVANCED_BREP_SHAPE_REPRESENTATION_;
            //信息节点解码
            ADVANCED_BREP_SHAPE_REPRESENTATION struct_tmp = new ADVANCED_BREP_SHAPE_REPRESENTATION();//建立结构体，并将指针赋值给共用体
            reg.Clear();//清空字符串
            while (this.str_temp[i] != '(')
            {
                ++i;
            }
            ++i;
            //读取多个子结点信息，长度不定
            while (this.str_temp[i] != '(')
            {
                ++i;
            }
            i++;
            while (this.str_temp[i] != ')')
            {
                if (!Char.IsDigit(this.str_temp[i]))
                {
                    ++i;
                }
                else if (Char.IsDigit(this.str_temp[i]) && (!Char.IsDigit(this.str_temp[i + 1])))
                {
                    reg.Append(this.str_temp[i]);
                    temp.child.Add(int.Parse(reg.ToString()));
                    reg.Clear();
                    ++i;
                }
                else
                {
                    reg.Append(this.str_temp[i]);
                    ++i;
                }
            }
            //将结构体插入到向量里
            temp.node = struct_tmp;
            TableNode.Add(temp);
        }
        public int IfSkipThisString()  //判断是否跳过当前字符串，并返回类型
        {
            StringBuilder obj = new StringBuilder();//该字符串用来接收STEP几何元素类型
            int i = 0;//循环变量处理字符串
            if (this.str_temp.Length == 0)
            {
                return 0;
            }
            if (this.str_temp[i] != '#')
            {
                return -1;//返回-1表示类型不对
                ++i;
            }
            else
            {
                while (this.str_temp[i] != '=')
                {
                    ++i;
                }
                ++i;
                if (this.str_temp[i] == '(')
                {
                    return -1;
                }
                while (Char.IsLetter(this.str_temp[i]) || Char.IsDigit(this.str_temp[i]) || (this.str_temp[i] == '_'))
                {
                    obj.Append(this.str_temp[i]);
                    ++i;
                }
                //if-else嵌套判定返回的类型
                if (obj.ToString() == "CARTESIAN_POINT")
                {
                    return 1;
                }
                else if (obj.ToString() == "DIRECTION")
                {
                    return 2;
                }
                else if (obj.ToString() == "VERTEX_POINT")
                {
                    return 3;
                }
                else if (obj.ToString() == "CIRCLE")
                {
                    return 4;
                }
                else if (obj.ToString() == "EDGE_CURVE")
                {
                    return 5;
                }
                else if (obj.ToString() == "ORIENTED_EDGE")
                {
                    return 6;
                }
                else if (obj.ToString() == "AXIS2_PLACEMENT_3D")
                {
                    return 7;
                }
                else if (obj.ToString() == "EDGE_LOOP")
                {
                    return 8;
                }
                else if (obj.ToString() == "CYLINDRICAL_SURFACE")
                {
                    return 9;
                }
                else if (obj.ToString() == "PLANE")
                {
                    return 10;
                }
                else if (obj.ToString() == "FACE_OUTER_BOUND")
                {
                    return 11;
                }
                else if (obj.ToString() == "FACE_BOUND")
                {
                    return 12;
                }
                else if (obj.ToString() == "ADVANCED_FACE")
                {
                    return 13;
                }
                else if (obj.ToString() == "CLOSED_SHELL")
                {
                    return 14;
                }
                else if (obj.ToString() == "MANIFOLD_SOLID_BREP")
                {
                    return 15;
                }
                else if (obj.ToString() == "ADVANCED_BREP_SHAPE_REPRESENTATION")
                {
                    return 16;
                }
                else
                {
                    return -2;
                }


            }
        }
        public void DecodeString(string FilePath)//处理字符串，提取信息，然后插入表项
        {
            //函数所用局部变量定义
            int type_f;//字符串所含信息类型标记
            int type_o; //目前的循环所要解析的目标类型
            str_temp = new StringBuilder();
            TableNode = new ArrayList();
            StreamReader Reader;
            for (type_o = 1; type_o <= 16; ++type_o)//依次迭代文件中的各个类型
            {
                Reader = new StreamReader(FilePath);
                Form1.MainStorage.str_temp.Clear();
                StringBuilder read_temp = new StringBuilder(Reader.ReadLine().ToString());
                while (read_temp.ToString() != "DATA;")
                {
                    read_temp.Clear();
                    read_temp.Append(Reader.ReadLine().ToString());
                }

                while (!Reader.EndOfStream)
                {
                    read_temp.Clear();

                    read_temp.Append(Reader.ReadLine());
                    if (read_temp.Length == 0)//跳过空行
                    {
                        continue;
                    }
                    if (read_temp.ToString() == "ENDSEC;")//终止解码
                    {
                        goto EndThisLoop;
                    }
                    while (read_temp[read_temp.Length - 1] != ';')
                    {
                        read_temp.Append(Reader.ReadLine());
                    }
                    Form1.MainStorage.str_temp.Append(read_temp.ToString().Replace(" ", ""));//将读取到的变量插入到临时变量，并消除掉里面的所有空格

                    type_f = IfSkipThisString();//判断是否跳过当前字符串，并返回类型
                    if (type_f == type_o)
                    {
                        switch (type_f)//根据不同字符串类型执行解码操作
                        {
                            case CARTESIAN_POINT_: DecodeCARTESIAN_POINT(); break;
                            case DIRECTION_: DecodeDIRECTION(); break;
                            case VERTEX_POINT_: DecodeVERTEX_POINT(); break;
                            case CIRCLE_: DecodeCIRCLE(); break;
                            case EDGE_CURVE_: DecodeEDGE_CURVE(); break;
                            case ORIENTED_EDGE_: DecodeORIENTED_EDGE(); break;
                            case AXIS2_PLACEMENT_3D_: DecodeAXIS2_PLACEMENT_3D(); break;
                            case EDGE_LOOP_: DecodeEDGE_LOOP(); break;
                            case CYLINDRICAL_SURFACE_: DecodeCYLINDRICAL_SURFACE(); break;
                            case PLANE_: DecodePLANE(); break;
                            case FACE_OUTER_BOUND_: DecodeFACE_OUTER_BOUND(); break;
                            case FACE_BOUND_: DecodeFACE_BOUND(); break;
                            case ADVANCED_FACE_: DecodeADVANCED_FACE(); break;
                            case CLOSED_SHELL_: DecodeCLOSED_SHELL(); break;
                            case MANIFOLD_SOLID_BREP_: DecodeMANIFOLD_SOLID_BREP(); break;
                            case ADVANCED_BREP_SHAPE_REPRESENTATION_: DecodeADVANCED_BREP_SHAPE_REPRESENTATION(); break;
                            default: continue;
                        }
                    }
                    Form1.MainStorage.str_temp.Clear();
                }
            EndThisLoop: Reader.Close();
            }
        }
        public int LookForNode(int StepId)//查找表项，以STEP_ID形式,返回索引号
        {
            for (int i = 0; i < this.TableNode.Count; ++i)
            {
                TableItem temp = (TableItem)(this.TableNode[i]);
                if (StepId == temp.STEPId)
                {
                    return i;
                }
                else
                {
                    continue;
                }
            }
            return -1;
        }
        public ArrayList LookForChild(int StepId)
        {
            TableItem Temp = (TableItem)(this.TableNode[LookForNode(StepId)]);
            return Temp.child;
        }
        public ArrayList LookForChildByIndex(int Index)
        {
            TableItem Temp = (TableItem)(this.TableNode[Index]);
            return Temp.child;
        }
        public int LookForFirstChild(int StepId)
        {
            TableItem Temp = (TableItem)(this.TableNode[LookForNode(StepId)]);
            return (int)Temp.child[0];
        }
        public int LookForFirstChildByIndex(int Index)
        {
            TableItem Temp = (TableItem)(this.TableNode[Index]);
            return (int)Temp.child[0];
        }
        public int LookForLastChild(int StepId)
        {
            TableItem Temp = (TableItem)(this.TableNode[LookForNode(StepId)]);
            return (int)Temp.child[Temp.child.Count-1];
        }
        public int LookForLastChildByIndex(int Index)
        {
            TableItem Temp = (TableItem)(this.TableNode[Index]);
            return (int)Temp.child[Temp.child.Count - 1];
        }
    }
}
