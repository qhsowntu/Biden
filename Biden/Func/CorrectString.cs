using Biden.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biden.Func
{
    class CorrectString
    {
        private List<Func1RuleClass> ruleList;
        public CorrectString()
        {
            ruleList = ModelFunc1.getInstance.RuleList;
        }

        public string getModifiedText(String str)
        {
            String res = str;
            for (int i = 0; i < ruleList.Count; i++)
            {
                res = ruleList[i].PrefixStr + res + ruleList[i].PostfixStr;
                if (ruleList[i].FromStr != null)
                {
                    res = res.Replace(ruleList[i].FromStr + "", ruleList[i].ToStr + "");
                }
            }
            return res;
        }
    }
}
