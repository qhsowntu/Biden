using Biden.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biden.Model
{
    class Func1Class
    {
        private List<RuleClass> ruleList;

        private static Func1Class instance = null;

        private Func1Class()
        {
            RuleList = new List<RuleClass>();
        }

        public static Func1Class getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Func1Class();
                }
                return instance;
            }
        }


        internal List<RuleClass> RuleList { get => ruleList; set => ruleList = value; }
    }
}
