using System;
using System.Collections.Generic;
using System.Text;

namespace FixtureGenerator.SampleClasses.SimilarClasses
{
    public class ModelActualizado : ModelBase
    {
        public int? Id { get; set; }

        public ChildActualizado Child { get; set; }
    }
}
