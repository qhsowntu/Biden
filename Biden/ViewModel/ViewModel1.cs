using Biden.Model;
using Biden.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Biden.ViewModel
{
    class ViewModel1 : INotifyPropertyChanged
    {


        public Command CmdBtn01 { get; set; }
        public Command CmdFuncBtn01 { get; set; }
        public Command CmdFuncBtn02 { get; set; }
        public Command CmdFuncBtn03 { get; set; }
        public Command CmdFuncBtn04 { get; set; }
        private TempClass tempClass;
        private FuncWindow1 tempWindow;

        public ViewModel1()
        {
            CmdBtn01 = new Command(Execute_Btn01, CanExecute_Btn01);
            CmdFuncBtn01 = new Command(Execute_FuncBtn01, CanExecute_Btn01);
            CmdFuncBtn02 = new Command(Execute_FuncBtn02, CanExecute_Btn01);
            CmdFuncBtn03 = new Command(Execute_FuncBtn03, CanExecute_Btn01);
            CmdFuncBtn04 = new Command(Execute_FuncBtn04, CanExecute_Btn01);
            tempClass = new TempClass();
            tempClass.Num = 1;
            tempClass.Num2 = 7;

            //ButtonCommand = new RelayCommand(new Action<object>(ChangeBgColor));
        }

        private void Execute_Btn01(object obj)
        {
            //do Something
            tempClass.Num = tempClass.Num * 2;
            OnPropertyChanged("Num");

            Text01 = Text01 + "A";

            ColorBtn01 = new SolidColorBrush(Colors.Green);
        }
        
        private void Execute_FuncBtn01(object obj)
        {
            //do Something
            if (tempWindow == null)
            {
                tempWindow = FuncWindow1.getInstance;
            }
            tempWindow.ShowDialog();
        }
        private void Execute_FuncBtn02(object obj)
        {
            //do Something
        }
        private void Execute_FuncBtn03(object obj)
        {
            //do Something
        }
        private void Execute_FuncBtn04(object obj)
        {
            //do Something
        }


        private bool CanExecute_Btn01(object obj) { return true; }


        public int Num
        {
            get
            {
                return tempClass.Num;
            }
            set
            {
                tempClass.Num = value;
                Num2 = value * 2;
                OnPropertyChanged("Num");
            }
        }

        public int Num2
        {
            get
            {
                return tempClass.Num2;
            }
            set
            {
                tempClass.Num2 = value;
                OnPropertyChanged("Num2");
            }
        }

        /*
        public Brush ColorBtn01
        {
            get
            {
                return _selectedColor;
            }
            set
            {
                if (_selectedColor != null)
                    _selectedColor = value;

                OnPropertyChanged("ColorBtn01");
            }
        }*/


        
        private SolidColorBrush _selectedColor = new SolidColorBrush(Colors.White);
        public SolidColorBrush ColorBtn01
        {
            get { return _selectedColor; }
            set
            {
                if (value == _selectedColor)
                    return;

                _selectedColor = value;
                OnPropertyChanged("ColorBtn01");
            }
        }



        private string text01 = "*";
        public string Text01
        {
            get { return text01; }
            set
            {
                if (value == text01)
                    return;

                text01 = value;
                OnPropertyChanged("Text01");
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }



        private ICommand m_ButtonCommand;

        public ICommand ButtonCommand
        {
            get
            {
                return m_ButtonCommand;
            }
            set
            {
                m_ButtonCommand = value;
            }
        }


        public void ChangeBgColor(object obj)
        {
            /*HERE I WANT TO CHANGE GRID COLOR*/
        }









    }




}
