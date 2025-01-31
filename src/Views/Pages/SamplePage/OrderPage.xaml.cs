using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using pvfLoaderXinyu;
using Wpf.Ui.Abstractions.Controls;
using WPFUI_SAMPLE.ViewModels.Pages;
using static Stimulsoft.Report.StiOptions.Export;

namespace WPFUI_SAMPLE.Views.Pages.SamplePage;
/// <summary>
/// OrderPage.xaml 的交互逻辑
/// </summary>
public partial class OrderPage : INavigableView<OrderViewModel>
{
    public OrderViewModel ViewModel
    {
        get;
    }
    private Pvf Pvf
    {
        get;
        set;
    }

    private string pvfFilename;
    public OrderPage(OrderViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog fileDialog = new OpenFileDialog();
        fileDialog.Multiselect = false;
        fileDialog.Title = "请选择文件";
        fileDialog.Filter = "Script.pvf文件(*.pvf)|*.pvf";
        if (fileDialog.ShowDialog() == DialogResult.OK)
        {
            pvfFilename = fileDialog.FileName;
            await loadPvfThread();
            //pvf.getPvfFileByPath("equipment/equipment.lst", Encoding.UTF8);
        }
    }

    private async Task loadPvfThread()
    {
        Pvf = new Pvf(pvfFilename);//正常使用只需要这一个就行了，关闭窗体的时候记得dispose()
                                   //string fileContent = Pvf.getPvfFileByPath("equipment/character/common/jacket/leather/vest_crleather.equ", Encoding.UTF8);
        var fileContent = Pvf.getPvfFileByPath("equipment/equipment.lst", Encoding.UTF8);
        var itemArr = fileContent.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).Where(t => !t.StartsWith('#')).ToList();

        foreach (var item in itemArr)
        {
            var arr = item.Split("\t", StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length == 2)
            {
                string id = arr[0];
                string path = arr[1].Trim('`');
                if (id == "601590012")
                {
                    string? equipEdu = Pvf.getPvfFileByPath($"equipment/{path}", Encoding.UTF8);
                }

                // 处理id和path
                Console.WriteLine($"ID: {id}, Path: {path}");
            }
        }

        Pvf.dispose();//不用了就释放掉
    }


    private void dfsCreateNode(HeaderTreeNode tag, ref TreeNode tree, string[] a, int deep)
    {
        if (a.Length - 1 == deep)//到文件了
        {
            TreeNode item = new TreeNode();
            item.Tag = tag;
            item.Name = a[deep];
            item.Text = a[deep];
            item.ImageKey = "file";
            tree.Nodes.Add(item);
            return;
        }
        if (!tree.Nodes.ContainsKey(a[deep]))
        {
            tree.Nodes.Add(a[deep], a[deep]);
        }
        var item1 = tree.Nodes[a[deep]];
        dfsCreateNode(tag, ref item1, a, deep + 1);
    }
    private void sortNodes(ref TreeNode node)
    {
        if (node.Tag == null) //对文件夹进行排序
        {
            List<TreeNode> al = new List<TreeNode>();
            foreach (TreeNode tn in node.Nodes)
                al.Add(tn);
            al.Sort(new Comparison<TreeNode>(delegate (TreeNode tx, TreeNode ty)
            {
                if (tx.Tag == null && ty.Tag != null)
                    return -1;
                else if (tx.Tag != null && ty.Tag == null)
                    return 1;
                else
                    return string.Compare(tx.Text, ty.Text);
            }));
            node.Nodes.Clear();
            for (int i = 0; i < al.Count; i++)
            {
                TreeNode tn = al[i];
                sortNodes(ref tn);
                node.Nodes.Add(tn);
            }
        }
    }
}
