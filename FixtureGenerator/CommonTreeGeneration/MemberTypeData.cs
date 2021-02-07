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

        public MemberTypeData KeyType { get; set; } // dictionaries

        public MemberTypeData ItemType { get; set; } // collections (including dictionaries)

        public MemberTypeData(Type memberType, string memberName)
        {
            MemberType = memberType;
            MemberName = memberName;
            Children = new List<MemberTypeData>();
        }

        public MemberTypeData Clone()
        {
            return new MemberTypeData(this.MemberType, this.MemberName)
            {
                Value = this.Value,
                KeyType = this.KeyType,
                ItemType = this.ItemType
            };
        }
    }
}
