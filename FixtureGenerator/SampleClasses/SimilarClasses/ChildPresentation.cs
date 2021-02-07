using System;
using System.Collections.Generic;
using System.Text;

namespace FixtureGenerator.SampleClasses.SimilarClasses
{
    public class ChildPresentation : ChildBase
    {
        public int Id { get; set; }

        public ChildPresentation Child { get; set; }
    }
}
