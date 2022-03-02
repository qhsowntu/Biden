using Biden.Func;
using Biden.Model;
using Biden.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Biden.ViewModel
{
    class ViewModelFunc3 : ViewModelCommon
    {

        public Command CmdFuncBtn01 { get; set; }
        public Command CmdFuncBtn02 { get; set; }
        public Command CmdFuncBtn03 { get; set; }
        public Command CmdFuncBtn04 { get; set; }
        public Command CmdFuncBtn01_Add { get; set; }
        public Command CmdEditBtn { get; set; }
        public Command CmdDeleteBtn { get; set; }
        public Command CmdDeleteStrBtn { get; set; }
        public Command CmdOnOffBtn { get; set; }
        public Command TestBtn01 { get; set; }


        private bool spinner;
        private int count;
        private Thread myThread;

        protected static bool initRuleFlag = false;
        protected static List<Func3RuleClass> ruleList;
        protected static Func3RuleClass rule;
        protected static int ruleCounter;
        public static ObservableCollection<MacroInfo2> optionObjectCollection;
        protected static int editedIndex;
        public ModelFunc3 func3Class;
        private static Macro macro;

        public ViewModelFunc3()
        {
            CmdFuncBtn01 = new Command(Execute_FuncBtn01, CanExecute_Btn01);
            CmdFuncBtn01_Add = new Command(Execute_FuncBtn01_Add, CanExecute_Btn01);
            CmdEditBtn = new Command(Execute_CmdEditBtn01, CanExecute_Btn01);
            CmdDeleteBtn = new Command(Execute_CmdDeleteBtn01, CanExecute_Btn01);
            CmdDeleteStrBtn = new Command(Execute_CmdDeleteStrBtn01, CanExecute_Btn01);
            CmdOnOffBtn = new Command(Execute_CmdOnOffBtn, CanExecute_Btn01);
            TestBtn01 = new Command(Execute_TestBtn01, CanExecute_Btn01);
            spinner = false;
            count = 0;


            initRule();
        }


        private void initRule()
        {
            if (initRuleFlag == false)
            {
                initRuleFlag = true;
                ruleList = ModelFunc3.getInstance.RuleList;
                rule = new Func3RuleClass();
                ruleCounter = 1;
                optionObjectCollection = new ObservableCollection<MacroInfo2>();
                macro = Macro.getInstance;
                func3Class = ModelFunc3.getInstance;
                AddSampleRules();
                //ButtonCommand = new RelayCommand(new Action<object>(ChangeBgColor));
            }
        }

        private void AddSampleRules()
        {
            //nameStr = "SampleRules1";
            //fromStr = "1";
            //toStr = "2";
            //prefixStr = "Pre";
            //postfixStr = "Post";
            
            removeStr();
        }

        private void Execute_FuncBtn01(object obj)
        {
            //do Something
            DoSpin();
            DoCount();
            
        }


        private void Execute_FuncBtn01_Add(object obj)
        {


        }

        private void Execute_CmdEditBtn01(object obj)
        {
            
        }
        private void Execute_CmdDeleteBtn01(object obj)
        {
            //
            for (int i = 0; i < ModelFunc3.getInstance.StrList.Count; i++)
            {
                if (ModelFunc3.getInstance.StrList[i] == obj + "")
                {
                    ModelFunc3.getInstance.StrList.RemoveAt(i);
                }
            }
            StrObjectAndSync();
        }

        private void Execute_CmdDeleteStrBtn01(object obj)
        {
            
        }

        public void Execute_CmdOnOffBtn(object obj)
        {
            //MessageBox.Show(obj + "");

            if (obj + "" == "True")
            {
                if (Macro.getInstance.IsInit == false)
                {
                    Macro.getInstance.IsInit = true;
                    Macro.create();
                    macro.start();
                }
                Macro.getInstance.ModeOn = true;
                IsChecked03 = true;
            }
            else
            {
                IsChecked03 = false;
                Macro.getInstance.ModeOn = false;
            }
            MainWindow.getInstance.SetSyncCheckBox();
            DoSpin();
            DoCount();
        }

        public bool IsChecked03
        {
            get
            {
                return ModelFunc3.getInstance.IsChecked03;
            }
            set
            {
                ModelFunc3.getInstance.IsChecked03 = value;
                OnPropertyChanged("IsChecked03");
            }
        }

        public void StrObjectAndSync()
        {

            ObservableCollection<MacroInfo2> tempCollection = new ObservableCollection<MacroInfo2>();
            for (int i = 0; i < ModelFunc3.getInstance.StrList.Count; i++)
            {
                MacroInfo2 tempInfo = new MacroInfo2();
                {
                    tempInfo.No = "" + (i + 1);
                    tempInfo.Str = "" + ModelFunc3.getInstance.StrList[i];
                }
                tempCollection.Add(tempInfo);
            }
            OptionObjectCollection = tempCollection;
        }


        public ObservableCollection<MacroInfo2> OptionObjectCollection
        {
            get { return optionObjectCollection; }
            set
            {
                if (value != optionObjectCollection)
                    //StrObjectAndSync();
                    optionObjectCollection = value;
                OnPropertyChanged("OptionObjectCollection");
            }
        }
        public ObservableCollection<MacroInfo2> SelectedOptionObject
        {
            get { return optionObjectCollection; }
            set
            {
                if (value != optionObjectCollection)
                    optionObjectCollection = value;
                OnPropertyChanged("SelectedOptionObject");
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
        private void DoCount()
        {
            if (myThread == null || myThread.IsAlive == false)
            {
                myThread = new Thread(DoCounting);
                myThread.IsBackground = true;
                myThread.Start();
            }
            else
            {
                myThread.Abort();
            }

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


        //selectedItem
        public string SelectedString
        {
            get
            {
                return ModelFunc3.getInstance.SelectedString;
            }
            set
            {
                ModelFunc3.getInstance.SelectedString = value;
                WindowPos.SendWpfWindowBack(FuncWindow3_ClipList.getInstance);
                WindowPos.SendWpfWindowBack(FuncWindow3.getInstance);
                OnPropertyChanged("SelectedString");
            }
        }

        public string SelectedItem
        {
            get
            {
                return ModelFunc3.getInstance.TheSelectedItem;
            }
            set
            {
                ModelFunc3.getInstance.TheSelectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }
        public List<string> Source
        {
            get { return ModelFunc3.getInstance.Source; }
        }

        public class MacroInfo
        {
            public string No { get; set; }
            public string Name { get; set; }
            public override string ToString() => Name;
            //public string Edit;
            //public string Delete;
        }
        public class MacroInfo2
        {
            public string No { get; set; }
            public string Str { get; set; }
            public override string ToString() => Str;
            //public string Edit;
            //public string Delete;
        }
        

        public void removeStr()
        {
            
        }








    }
}
