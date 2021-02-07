using System;
using System.Collections.Generic;
using System.Text;

namespace FixtureGenerator.CommonTreeGeneration
{
    public class MemberTypeData
    {
        public Type MemberType { get; set; }

        public string MemberName { get; set; }

        public object Value { get; set; }

        public List<MemberTypeData> Children { get; set; }

        public MemberTypeData(Type memberType, string memberName)
        {
            MemberType = memberType;
            MemberName = memberName;
            Children = new List<MemberTypeData>();
        }
    }
}
