using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StepDecodeAndDisplay
{
    public partial class Form1 : Form
    {
        public static Storage MainStorage = new Storage();
        public static STLCreate STLCreator = new STLCreate();
        public Form1()
        {
            InitializeComponent();
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "STEP文件(*.stp)|*.stp";
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            MainStorage.DecodeString(openFileDialog1.FileName);
            TableItem temp_node;
            for (int i = 0; i < MainStorage.TableNode.Count; ++i)
            {
                temp_node = (TableItem)MainStorage.TableNode[i];
                if (temp_node.TypeFlag == 16)
                {
 
                    TreeNode TreeRoot = new TreeNode();
                    TreeRoot.Text=temp_node.GetTypeName()+temp_node.STEPId;
                    TreeRoot.Tag =i;
                    treeView1.Nodes.Add(TreeRoot);
                    CreateTreeNode(TreeRoot);
                }
            }
            /******************************************************************************************************/
            Form1.STLCreator.CreateData();
            toolStripStatusLabel1.Text = STLCreator.STL_menber.Count.ToString();
            Form1.STLCreator.CreateFile();
            webBrowser1.Refresh();
            webBrowser1.Navigate("http://127.0.0.1/STLDisplay.html");

        }

        private bool CreateTreeNode(TreeNode parentTree)
        {
            int TableIndex = (int)parentTree.Tag;//索引到表项
            TableItem Obj_TableNode = (TableItem)(MainStorage.TableNode[TableIndex]);
            if (Obj_TableNode.child.Count != 0)//如果不存在孩子结点
            {
                for (int a = 0; a < Obj_TableNode.child.Count; ++a)//如果存在孩子结点
                {
                    int index = MainStorage.LookForNode((int)(Obj_TableNode.child[a]));
                    if (index == -1)
                    {
                        continue;
                    }
                    TableItem cache = (TableItem)(MainStorage.TableNode[index]);
                    TreeNode temp = new TreeNode(cache.GetTypeName() + cache.STEPId);
                    temp.Tag = index;
                    parentTree.Nodes.Add(temp);
                    CreateTreeNode(temp);
                }
            }
            return true;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int index = (int)treeView1.SelectedNode.Tag;
            TableItem tmp_node = (TableItem)(MainStorage.TableNode[index]);
            tableLayoutPanel1.Controls.Clear();
            Label label1 = new Label();
            label1.BorderStyle=System.Windows.Forms.BorderStyle.FixedSingle;
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label1.Text = "StepId：";
            Label label2 = new Label();
            label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label2.Text =tmp_node.STEPId.ToString();
            Label label3 = new Label();
            label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label3.Text = "类型：";
            Label label4 = new Label();
            label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label4.Text =tmp_node.GetTypeName();
            Label label5 = new Label();
            label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label5.Text = "属性字段：";
            Label label6 = new Label();
            label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            Label label7 = new Label();
            label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            Label label8 = new Label();
            label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            Label label9 = new Label();
            label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            Label label10 = new Label();
            label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            Label label11 = new Label();
            label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            Label label12 = new Label();
            label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //针对不同的节点类型进行处理
            switch (tmp_node.TypeFlag)
            {
                case 1:
                     {
                        CARTESIAN_POINT struct_tmp = (CARTESIAN_POINT)(tmp_node.node);
                        label6.Text = struct_tmp.property;
                        label7.Text = "X坐标：";
                        label8.Text = struct_tmp.x_coord.ToString();
                        label9.Text = "Y坐标：";
                        label10.Text = struct_tmp.y_coord.ToString();
                        label11.Text = "Z坐标：";
                        label12.Text = struct_tmp.z_coord.ToString();
                    }
                     ; break;
                case 2:
                     {
                        DIRECTION struct_tmp = (DIRECTION)(tmp_node.node);
                        label6.Text = struct_tmp.property;
                        label7.Text = "X投影分量：";
                        label8.Text = struct_tmp.x_dir.ToString();
                        label9.Text = "Y投影分量：";
                        label10.Text = struct_tmp.y_dir.ToString();
                        label11.Text = "Z投影分量：";
                        label12.Text = struct_tmp.z_dir.ToString();
                    }
                     ; break;
                case 3:
                     {
                        VERTEX_POINT struct_tmp = (VERTEX_POINT)(tmp_node.node);
                        label6.Text = struct_tmp.property;
                        label7.Text = "";
                        label8.Text = "";
                        label9.Text = "";
                        label10.Text ="";
                        label11.Text ="";
                        label12.Text = "";
                    }
                     ; break;
                case 4:
                     {
                        CIRCLE struct_tmp = (CIRCLE)(tmp_node.node);
                        label6.Text = struct_tmp.property;
                        label7.Text = "半径:";
                        label8.Text = struct_tmp.radius.ToString();
                        label9.Text = "";
                        label10.Text = "";
                        label11.Text = "";
                        label12.Text = "";
                    }
                     ; break;
                case 5:
                      {
                        EDGE_CURVE struct_tmp = (EDGE_CURVE)(tmp_node.node);
                        label6.Text = struct_tmp.property;
                        label7.Text = "方向：";
                        label8.Text = struct_tmp.dir.ToString();
                        label9.Text = "";
                        label10.Text = "";
                        label11.Text = "";
                        label12.Text = "";
                    }
                      ; break;
                case 6:
                    {
                        ORIENTED_EDGE struct_tmp = (ORIENTED_EDGE)(tmp_node.node);
                        label6.Text = struct_tmp.property;
                        label7.Text = "属性1：";
                        label8.Text = struct_tmp.flag_1;
                        label9.Text = "属性2：";
                        label10.Text = struct_tmp.flag_2;
                        label11.Text = "方向：";
                        label12.Text = struct_tmp.dir.ToString();
                    }
                      ; break;
                case 7:
                     {
                        AXIS2_PLACEMENT_3D struct_tmp = (AXIS2_PLACEMENT_3D)(tmp_node.node);
                        label6.Text = struct_tmp.property;
                        label7.Text = "";
                        label8.Text = "";
                        label9.Text = "";
                        label10.Text = "";
                        label11.Text = "";
                        label12.Text = "";
                    }
                      ; break;
                case 8:
                     {
                        EDGE_LOOP struct_tmp = (EDGE_LOOP)(tmp_node.node);
                        label6.Text = struct_tmp.property;
                        label7.Text = "";
                        label8.Text = "";
                        label9.Text = "";
                        label10.Text = "";
                        label11.Text = "";
                        label12.Text = "";
                    }
                     ; break;
                case 9:
                      {
                        CYLINDRICAL_SURFACE struct_tmp = (CYLINDRICAL_SURFACE)(tmp_node.node);
                        label6.Text = struct_tmp.property;
                        label7.Text = "半径：";
                        label8.Text = struct_tmp.radius.ToString();
                        label9.Text = "";
                        label10.Text = "";
                        label11.Text = "";
                        label12.Text = "";
                    }
                     ; break;
                case 10:
                      {
                        PLANE struct_tmp = (PLANE)(tmp_node.node);
                        label6.Text = struct_tmp.property;
                        label7.Text = "";
                        label8.Text = "";
                        label9.Text = "";
                        label10.Text = "";
                        label11.Text = "";
                        label12.Text = "";
                    }
                      ; break;
                case 11:
                      {
                        FACE_OUTER_BOUND struct_tmp = (FACE_OUTER_BOUND)(tmp_node.node);
                        label6.Text = struct_tmp.property;
                        label7.Text = "方向：";
                        label8.Text = struct_tmp.dir.ToString();
                        label9.Text = "";
                        label10.Text = "";
                        label11.Text = "";
                        label12.Text = "";
                    }
                      ; break;
                case 12:
                    {
                        FACE_BOUND struct_tmp = (FACE_BOUND)(tmp_node.node);
                        label6.Text = struct_tmp.property;
                        label7.Text = "方向：";
                        label8.Text = struct_tmp.dir.ToString();
                        label9.Text = "";
                        label10.Text = "";
                        label11.Text = "";
                        label12.Text = "";
                    }
                    ; break;
                case 13:
                    {
                        ADVANCED_FACE struct_tmp = (ADVANCED_FACE)(tmp_node.node);
                        label6.Text = struct_tmp.property;
                        label7.Text = "方向：";
                        label8.Text = struct_tmp.dir.ToString();
                        label9.Text = "";
                        label10.Text = "";
                        label11.Text = "";
                        label12.Text = "";
                    }
                    ; break;
                case 14:
                    {
                        CLOSED_SHELL struct_tmp = (CLOSED_SHELL)(tmp_node.node);
                        label6.Text = struct_tmp.property;
                        label7.Text = "";
                        label8.Text = "";
                        label9.Text = "";
                        label10.Text = "";
                        label11.Text = "";
                        label12.Text = "";
                    }
                    ; break;
                case 15:
                    {
                        MANIFOLD_SOLID_BREP struct_tmp = (MANIFOLD_SOLID_BREP)(tmp_node.node);
                        label6.Text = struct_tmp.property;
                        label7.Text = "";
                        label8.Text = "";
                        label9.Text = "";
                        label10.Text = "";
                        label11.Text = "";
                        label12.Text = "";
                    }
                    ; break;
                case 16:
                    {
                        ADVANCED_BREP_SHAPE_REPRESENTATION struct_tmp = (ADVANCED_BREP_SHAPE_REPRESENTATION)(tmp_node.node);
                        label6.Text = "缺省";
                        label7.Text = "";
                        label8.Text = "";
                        label9.Text = "";
                        label10.Text = "";
                        label11.Text = "";
                        label12.Text = "";
                    }
                    ; break;
            }

            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 1, 0);
            tableLayoutPanel1.Controls.Add(label3, 2, 0);
            tableLayoutPanel1.Controls.Add(label4, 3, 0);
            tableLayoutPanel1.Controls.Add(label5, 4, 0);
            tableLayoutPanel1.Controls.Add(label6, 5, 0);
            tableLayoutPanel1.Controls.Add(label7, 0, 1);
            tableLayoutPanel1.Controls.Add(label8, 1, 1);
            tableLayoutPanel1.Controls.Add(label9, 2, 1);
            tableLayoutPanel1.Controls.Add(label10, 3, 1);
            tableLayoutPanel1.Controls.Add(label11, 4, 1);
            tableLayoutPanel1.Controls.Add(label12, 5, 1);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void 退出ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
