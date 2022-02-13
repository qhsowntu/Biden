using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biden.Func
{
    class RuleClass
    {
        private string nameStr;
        private string fromStr;
        private string toStr;
        private string prefixStr;
        private string postfixStr;

        public RuleClass()
        {

        }

        public string NameStr { get => nameStr; set => nameStr = value; }
        public string FromStr { get => fromStr; set => fromStr = value; }
        public string ToStr { get => toStr; set => toStr = value; }
        public string PrefixStr { get => prefixStr; set => prefixStr = value; }
        public string PostfixStr { get => postfixStr; set => postfixStr = value; }
    }
}
