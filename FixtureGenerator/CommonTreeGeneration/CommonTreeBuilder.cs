using FixtureGenerator.SampleClasses.SimilarClasses;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FixtureGenerator.CommonTreeGeneration
{
    public class CommonTreeBuilder
    {
        public void TryBuildTree()
        {
            var tree = BuildTree(typeof(ModelActualizado));
            return;
        }

        public List<MemberTypeData> BuildTree(Type objectType)
        {
            List<MemberTypeData> tree = new List<MemberTypeData>();
            foreach (PropertyInfo member in objectType.GetProperties())
            {
                Type memberType = member.PropertyType;
                string memberName = member.Name;
                MemberTypeData memberInfo = new MemberTypeData(memberType, memberName);
                // todo: dictionaries, lists
                if (memberType.IsClass)
                {
                    if (memberType != typeof(string))
                    {
                        memberInfo.Children = BuildTree(memberType);
                    }                    
                }
                tree.Add(memberInfo);
            }

            return tree;
        }
    }
}
