using Biden.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biden.Func
{
    class CorrectString
    {
        private List<RuleClass> ruleList;
        public CorrectString()
        {
            ruleList = Func1Class.getInstance.RuleList;
        }

        public String getModifiedText(String str)
        {
            String res = str;
            for (int i = 0; i < ruleList.Count; i++)
            {
                res = ruleList[i].PrefixStr + res + ruleList[i].PostfixStr;
                res = res.Replace(ruleList[i].FromStr, ruleList[i].ToStr);
            }
            return res;
        }
    }
}
