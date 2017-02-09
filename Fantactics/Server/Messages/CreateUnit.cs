using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantactics.Server.Messages
{
    class CreateUnit: Base
    {
        public string Code = "CreateUnit";
        public string Content = "{0};{1};{2}";

        public CreateUnit(string name, int column, int row)
        {
            Content = string.Format(Content, name, column, row);
        }
    }
}
