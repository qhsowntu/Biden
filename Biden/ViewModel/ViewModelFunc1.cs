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
using System.Windows.Forms;
using System.Windows.Input;

namespace Biden.ViewModel
{
    class ViewModelFunc1 : ViewModelCommon
    {

        public Command CmdFuncBtn01 { get; set; }
        public Command CmdFuncBtn02 { get; set; }
        public Command CmdFuncBtn03 { get; set; }
        public Command CmdFuncBtn04 { get; set; }
        public Command CmdFuncBtn01_Add { get; set; }
        public Command CmdEditBtn { get; set; }
        public Command CmdDeleteBtn { get; set; }
        public Command TestBtn01 { get; set; }
        private bool spinner;
        private int count;
        private Thread myThread;
        private FuncWindow1_Add tempWindow;
        protected static List<RuleClass> ruleList;
        private ObservableCollection<MacroInfo> fileObjectCollection;
        private int ruleCounter;

        public ViewModelFunc1()
        {
            CmdFuncBtn01 = new Command(Execute_FuncBtn01, CanExecute_Btn01);
            CmdFuncBtn01_Add = new Command(Execute_FuncBtn01_Add, CanExecute_Btn01);
            CmdEditBtn = new Command(Execute_CmdEditBtn01, CanExecute_Btn01);
            CmdDeleteBtn = new Command(Execute_CmdDeleteBtn01, CanExecute_Btn01);
            TestBtn01 = new Command(Execute_TestBtn01, CanExecute_Btn01);
            ruleList = new List<RuleClass>();
            fileObjectCollection = new ObservableCollection<MacroInfo>();
            spinner = false;
            count = 0;
            ruleCounter = 1;
            //ButtonCommand = new RelayCommand(new Action<object>(ChangeBgColor));
        }

        

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
            
            if (FileObjectCollection.Count < ruleList.Count)
            {
                MacroInfo tempInfo = new MacroInfo();
                {
                    tempInfo.No = "" + ruleList.Count;
                    tempInfo.Name = "" + ruleList[ruleList.Count - 1].NameStr;
                }
                FileObjectCollection.Add(new MacroInfo() { No = "" + ruleCounter++, Name = "" + ruleList[ruleList.Count - 1].NameStr });
            }
        }

        private void Execute_CmdEditBtn01(object obj)
        {
            if (obj != null)
            {
                MessageBox.Show(obj.ToString());
            }
        }
        private void Execute_CmdDeleteBtn01(object obj)
        {
            for (int i = 0; i < ruleList.Count; i++)
            {
                if (ruleList[i].NameStr == obj + "")
                {
                    ruleList.RemoveAt(i);
                    FileObjectCollection.RemoveAt(i);
                }
            }
            MessageBox.Show(obj.ToString());
        }

        











        public ObservableCollection<MacroInfo> FileObjectCollection
        {
            get { return fileObjectCollection; }
            set
            {
                if (value != this.fileObjectCollection)
                    fileObjectCollection = value;
                OnPropertyChanged("FileObjectCollection");
            }
        }
        public ObservableCollection<MacroInfo> SelectedFileObject
        {
            get { return fileObjectCollection; }
            set
            {
                if (value != this.fileObjectCollection)
                    fileObjectCollection = value;
                OnPropertyChanged("SelectedFileObject");
            }
        }

        private void Execute_TestBtn01(object obj)
        {
            //FileObjectCollection.Add("hi2");
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



        internal List<RuleClass> RuleList { get => ruleList; set => ruleList = value; }


        public class MacroInfo
        {
            public string No { get; set; }
            public string Name { get; set; }
            public override string ToString() => Name;
            //public string Edit;
            //public string Delete;

        }

    }
}
