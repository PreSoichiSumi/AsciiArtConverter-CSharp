using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsciiArtConverter08.Manager
{
    public class Command
    {
        private string command = "";
        private object value = null;

        public Command(string command, object value)
        {
            this.command = command;
            this.value = value;
        }

        public Command(string command)
        {
            this.command = command;
        }

        public string CommandString
        {
            get
            {
                return command;
            }
        }

        public object Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }
    }
}
