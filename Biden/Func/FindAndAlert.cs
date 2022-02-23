using Biden.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biden.Func
{
    class FindAndAlert
    {
        private List<Func2RuleClass> ruleList;
        public FindAndAlert()
        {
            ruleList = ModelFunc2.getInstance.RuleList;
        }

        public string FindStringAndAlert(String str)
        {
            String msg = "";
            for (int i = 0; i < ruleList.Count; i++)
            {
                bool isFlag = true;
                for(int j = 0; i<ruleList[i].StrList.Count; j++)
                {
                    if (str.Contains(ruleList[i].StrList[j]))
                    {
                    }
                    else
                    {
                        isFlag = false;
                        break;
                    }
                }
                if (isFlag)
                {
                    msg = msg + (i + 1) + ". " + ruleList[i].AlertMsg + "\n";
                }
            }
            if(msg != "")
            {
                MessageBox.Show(msg + "");
            }
            return "";
        }
    }
}
