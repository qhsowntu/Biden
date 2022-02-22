using Biden.Func;
using Biden.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biden.ViewModel
{
    class ViewModelFunc2_Add : ViewModelFunc2
    {
        public Command CmdFuncBtn01_AddOK { get; set; }
        public Command CmdFuncBtn01_AddCancel { get; set; }

        public ViewModelFunc2_Add()
        {
            CmdFuncBtn01_AddOK = new Command(Execute_FuncBtn01_AddOK, CanExecute_Btn01);
            CmdFuncBtn01_AddCancel = new Command(Execute_FuncBtn01_AddCancel, CanExecute_Btn01);
        }

        private void Execute_FuncBtn01_AddOK(object obj)
        {
            //FuncWindow1.getInstance.RuleList.Add(rule);
            for (int i = 0; i < ruleList.Count; i++)
            {
                if(ruleList[i].NameStr == rule.NameStr) 
                {
                    MessageBox.Show("name already exists");
                    return;
                }
            }
            Func2RuleClass tempRules = new Func2RuleClass();
            tempRules.No = ruleList.Count+"";
            tempRules.NameStr = nameStr;
            tempRules.FromStr = fromStr;
            tempRules.ToStr = toStr;
            tempRules.PrefixStr = prefixStr;
            tempRules.PostfixStr = postfixStr;
            ruleList.Add(tempRules);
            removeStr();
            FuncWindow2_Add.getInstance.Hide();
        }
        private void Execute_FuncBtn01_AddCancel(object obj)
        {
            FuncWindow2_Add.getInstance.Hide();
        }




    }
}
