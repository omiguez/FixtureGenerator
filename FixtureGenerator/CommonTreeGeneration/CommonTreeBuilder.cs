using FixtureGenerator.SampleClasses.SimilarClasses;
using System;
using System.Collections;
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
                MemberTypeData memberInfo = GenerateInfo(memberType, memberName);
                tree.Add(memberInfo);
            }

            return tree;
        }

        private MemberTypeData GenerateInfo(Type memberType, string memberName)
        {
            MemberTypeData memberInfo = new MemberTypeData(memberType, memberName);
            if (memberType.IsClass)
            {
                if (memberType != typeof(string))
                {
                    if (FixtureGenerator.DoesItImplementInterface(memberType, typeof(IList)))
                    {
                        var itemType = memberType.GetGenericArguments()[0];
                        memberInfo.ItemType = GenerateInfo(itemType, "Item");
                    }
                    else if (FixtureGenerator.DoesItImplementInterface(memberType, typeof(IDictionary)))
                    {
                        var genericTypes = memberType.GetGenericArguments();
                        memberInfo.KeyType = GenerateInfo(genericTypes[0], "Key");
                        memberInfo.ItemType = GenerateInfo(genericTypes[1], "Item");
                    }
                    else
                    {
                        memberInfo.Children = BuildTree(memberType);
                    }
                }
            }

            return memberInfo;
        }
    }
}
