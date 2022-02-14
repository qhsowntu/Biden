using Biden.Func;
using Biden.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Biden.ViewModel
{
    class ViewModelFunc1 : INotifyPropertyChanged
    {

        public Command CmdFuncBtn01 { get; set; }
        public Command CmdFuncBtn02 { get; set; }
        public Command CmdFuncBtn03 { get; set; }
        public Command CmdFuncBtn04 { get; set; }
        public Command CmdFuncBtn01_Add { get; set; }
        public Command CmdFuncBtn01_AddOK { get; set; }
        public Command CmdFuncBtn01_AddCancel { get; set; }
        private bool spinner;
        private int count;
        private Thread myThread;
        private FuncWindow1_Add tempWindow;
        private RuleClass rule;
        private List<RuleClass> ruleList;
        private ObservableCollection<string> fileObjectCollection;

        public ViewModelFunc1()
        {
            CmdFuncBtn01 = new Command(Execute_FuncBtn01, CanExecute_Btn01);
            CmdFuncBtn01_Add = new Command(Execute_FuncBtn01_Add, CanExecute_Btn01);
            CmdFuncBtn01_AddOK = new Command(Execute_FuncBtn01_AddOK, CanExecute_Btn01);
            CmdFuncBtn01_AddCancel = new Command(Execute_FuncBtn01_AddCancel, CanExecute_Btn01);
            rule = new RuleClass();
            RuleList = new List<RuleClass>();
            fileObjectCollection = new ObservableCollection<string>();
            spinner = false;
            count = 0;

            //ButtonCommand = new RelayCommand(new Action<object>(ChangeBgColor));
        }
        internal List<RuleClass> RuleList { get => ruleList; set => ruleList = value; }

        private bool CanExecute_Btn01(object obj) { return true; }

        private void Execute_FuncBtn01(object obj)
        {
            //do Something
            DoSpin();

            if(myThread == null || myThread.IsAlive == false)
            {
                myThread = new Thread(DoCounting);
                myThread.Start();
            }
            else
            {
                myThread.Abort();
            }
        }

        private void Execute_FuncBtn01_Add(object obj)
        {
            //do Something
            if (tempWindow == null)
            {
                tempWindow = FuncWindow1_Add.getInstance;
            }
            FuncWindow1.getInstance.Hide();
            MainWindow.getInstance.Hide();
            tempWindow.ShowDialog();
            FuncWindow1.getInstance.Show();
            MainWindow.getInstance.Show();
        }


        private void Execute_FuncBtn01_AddOK(object obj)
        {
            //FuncWindow1.getInstance.RuleList.Add(rule);
            this.RuleList.Add(rule);
            fileObjectCollection.Add("hi");
            OnPropertyChanged("FileObjectCollection");
            rule = null;
            FuncWindow1_Add.getInstance.Hide();
        }
        private void Execute_FuncBtn01_AddCancel(object obj)
        {
            rule = null;
            FuncWindow1_Add.getInstance.Hide();
        }


        public ObservableCollection<string> FileObjectCollection
        {
            get { return fileObjectCollection; }
            set
            {
                if (value != this.fileObjectCollection)
                    fileObjectCollection = value;
                OnPropertyChanged("FileObjectCollection");
            }
        }



        private void DoCounting()
        {
            while(count != 10000)
            {
                count++;
                OnPropertyChanged("Counter1");
                Thread.Sleep(10);
            }
        }
        private void DoSpin()
        {
            if(spinner == true)
            {
                spinner = false;
            }
            else
            {
                spinner = true;
            }
            OnPropertyChanged("CmdFuncSpin01");
        }

        public bool CmdFuncSpin01
        {
            get
            {
                return spinner;
            }
            set
            {
                spinner = value;
                OnPropertyChanged("CmdFuncSpin01");
            }
        }

        public int Counter1
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
                OnPropertyChanged("Counter1");
            }
        }


        public string fromStr
        {
            get
            {
                return rule.FromStr;
            }
            set
            {
                rule.FromStr = value;
                OnPropertyChanged("fromStr");
            }
        }
        public string toStr
        {
            get
            {
                return rule.ToStr;
            }
            set
            {
                rule.ToStr = value;
                OnPropertyChanged("toStr");
            }
        }
        public string prefixStr
        {
            get
            {
                return rule.PrefixStr;
            }
            set
            {
                rule.PrefixStr = value;
                OnPropertyChanged("prefixStr");
            }
        }
        public string postfixStr
        {
            get
            {
                return rule.PostfixStr;
            }
            set
            {
                rule.PostfixStr = value;
                OnPropertyChanged("postfixStr");
            }
        }
        public string nameStr
        {
            get
            {
                return rule.NameStr;
            }
            set
            {
                rule.NameStr = value;
                OnPropertyChanged("nameStr");
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
    }
}
