using System;
using System.Collections.Generic;
using System.Text;

namespace FixtureGenerator.SampleClasses
{
    public class SampleModelClass
    {
        public int IntValue { get; set; }

        public string StringValue { get; set; }

        public SampleEnum1 EnumValue { get; set; }

        public ChildClassLevel1 Child1 { get; set; }
    }
}
