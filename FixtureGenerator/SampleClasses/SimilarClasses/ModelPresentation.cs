using System;
using System.Collections.Generic;
using System.Text;

namespace FixtureGenerator.SampleClasses.SimilarClasses
{
    public class ModelPresentation : ModelBase
    {
        public int Id { get; set; }

        public ChildPresentation Child { get; set; }


        public List<ChildPresentation> ChildList { get; set; }

        public Dictionary<string, ChildPresentation> ChildDictionary { get; set; }
    }
}
