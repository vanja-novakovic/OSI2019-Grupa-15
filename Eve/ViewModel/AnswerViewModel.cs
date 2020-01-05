using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eve.ViewModel
{
    public class AnswerViewModel
    {
        public int IdAnswer { get; set; }

        public string Content { get; set; }

        public sbyte Correct { get; set; }

        public int IdQuestion { get; set; }
    }
}
