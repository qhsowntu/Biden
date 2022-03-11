using Biden.Func;
using Biden.Model;
using Biden.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Biden.View
{
    /// <summary>
    /// FuncWindow1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FuncWindow3_ClipList : BaseWindow
    {
        private static FuncWindow3_ClipList instance = null;
        private ViewModelFunc3 _viewModel;
        private FuncWindow3_ClipList()
        {
            InitializeComponent();
            _viewModel = new ViewModelFunc3();
            this.DataContext = _viewModel;
        }

        public static FuncWindow3_ClipList getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FuncWindow3_ClipList();
                    //바인딩 문제로 창 위치 임시방편 처리
                    FuncWindow3_ClipList.getInstance.setPos();
                }
                return instance;
            }
        }

        public void setPos()
        {
            System.Drawing.Point p = Macro.getMousePosAndColor();
            this.Top = p.Y;
            this.Left = p.X;
        }
        /*
        public override void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(this+"@");
            //Macro.getInstance.PasteModeOn = false;
            //Macro.destroy();
            string windowName = this + "";
            if (windowName.Contains("MainWindow"))
            {
                deleteAllWindow();
                //this.Close();
                return;
            }
            else
            {
                this.Hide();
            }
        }*/



    }
}
