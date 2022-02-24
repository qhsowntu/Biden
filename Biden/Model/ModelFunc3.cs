using Biden.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biden.Model
{
    class ModelFunc3
    {
        private List<Func3RuleClass> ruleList;
        private List<string> strList;

        private static ModelFunc3 instance = null;
        private bool _isChecked03 = false;

        private ModelFunc3()
        {
            RuleList = new List<Func3RuleClass>();
        }

        public static ModelFunc3 getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ModelFunc3();
                }
                return instance;
            }
        }


        internal List<Func3RuleClass> RuleList { get => ruleList; set => ruleList = value; }
        public bool IsChecked03 { get => _isChecked03; set => _isChecked03 = value; }
        public List<string> StrList { get => strList; set => strList = value; }
    }
}
