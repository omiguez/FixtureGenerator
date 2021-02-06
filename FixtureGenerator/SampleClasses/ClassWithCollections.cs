using System;
using System.Collections.Generic;
using System.Text;

namespace FixtureGenerator.SampleClasses
{
    public class ClassWithCollections
    {
        public List<string> SomeStringList { get; set; }

        public List<int> SomeIntList { get; set; }

        public Dictionary<string, int> Dictionary1 { get; set; }

        public Dictionary<int, SampleEnum1> Dictionary2 { get; set; }

        public Dictionary<string, Dictionary<string, List<float>>> NestedDictionary { get; set; }
    }
}
