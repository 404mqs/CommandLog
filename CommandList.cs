using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CommandLog
{
    public class CommandLog
    {
        [XmlAttribute("name")]
        public string name;

        public CommandLog() { }

    }

}