using System;
using System.Collections.Generic;
using System.Text;

namespace FixtureGenerator.SampleClasses.SimilarClasses
{
    public class ModelNuevo : ModelBase
    {
        public ChildNuevo Child { get; set; }

        public List<ChildNuevo> ChildList { get; set; }

        public Dictionary<string, ChildNuevo> ChildDictionary { get; set; }
    }
}
