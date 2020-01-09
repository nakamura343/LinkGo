using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LinkGo.Common.Logger
{
    [XmlRoot]
    public class LogConfig
    {
        [XmlElement]
        public List<LoggerElement> loggers;
    }

    public class LoggerElement
    {
        [XmlAttribute]
        public string tag;

        [XmlAttribute]
        public int type;

        [XmlAttribute]
        public bool showTime;

        [XmlElement]
        public List<TargetElement> targets;
    }

    public class TargetElement
    {
        [XmlAttribute]
        public TargetType type;

        [XmlAttribute]
        public string path;

        [XmlAttribute]
        public string url;
    }
}
