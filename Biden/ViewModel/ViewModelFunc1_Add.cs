using Biden.Func;
using Biden.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biden.ViewModel
{
    class ViewModelFunc1_Add : ViewModelFunc1
    {
        private RuleClass rule;
        public string gggg;
        public Command CmdFuncBtn01_AddOK { get; set; }
        public Command CmdFuncBtn01_AddCancel { get; set; }

        public ViewModelFunc1_Add()
        {
            CmdFuncBtn01_AddOK = new Command(Execute_FuncBtn01_AddOK, CanExecute_Btn01);
            CmdFuncBtn01_AddCancel = new Command(Execute_FuncBtn01_AddCancel, CanExecute_Btn01);
            rule = new RuleClass();
        }

        private void Execute_FuncBtn01_AddOK(object obj)
        {
            //FuncWindow1.getInstance.RuleList.Add(rule);
            ruleList.Add(rule);
            FuncWindow1_Add.getInstance.Hide();
        }
        private void Execute_FuncBtn01_AddCancel(object obj)
        {
            FuncWindow1_Add.getInstance.Hide();
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


        public void removeStr()
        {
            nameStr = "";
            fromStr = "";
            toStr = "";
            prefixStr = "";
            postfixStr = "";
        }


    }
}
