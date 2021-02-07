using System;
using System.Collections.Generic;
using System.Text;

namespace FixtureGenerator.SampleClasses.SimilarClasses
{
    public class ChildBase
    {
        public string ChildString1 { get; set;}

        public SubChild SubChild { get; set; }

        public int ChildInt1 { get; set; }

        public List<SubChild> SubChildren { get; set; }

        public Dictionary<string, SubChild> SubchildrenDict { get; set; }

        public Dictionary<int, string> PlainDictionary { get; set; }
    }
}
