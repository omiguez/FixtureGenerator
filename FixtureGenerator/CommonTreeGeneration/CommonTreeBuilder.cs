using FixtureGenerator.SampleClasses.SimilarClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FixtureGenerator.CommonTreeGeneration
{
    public class CommonTreeBuilder
    {
        public void TryBuildTree()
        {
            var tree1 = BuildTree(typeof(ModelActualizado));
            var tree2 = BuildTree(typeof(ModelPresentation));
            var common = GetCommonTree(tree1, tree2);
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

        public List<MemberTypeData> GetCommonTree(List<MemberTypeData> tree1, List<MemberTypeData> tree2)
        {            
            List<MemberTypeData> commonTree = new List<MemberTypeData>();
            Dictionary<string, MemberTypeData> tree2Map = tree2.ToDictionary(element => element.MemberName);
            foreach (MemberTypeData member in tree1)
            {
                if (tree2Map.TryGetValue(member.MemberName, out MemberTypeData correspondingMember))
                {
                    var commonMemberData = GetCommonMemberData(member, correspondingMember);
                    if (commonMemberData != null)
                    {
                        commonTree.Add(commonMemberData);
                    }
                }
            }

            return commonTree;
        }

        private MemberTypeData GetCommonMemberData(MemberTypeData member1, MemberTypeData member2)
        {
            MemberTypeData common = member1.Clone();
            if (member1.MemberType == member2.MemberType)
            {
                return common;
            }
            else
            {
                if (member1.MemberType.IsClass && member2.MemberType.IsClass)
                {
                    if (member1.MemberType != typeof(string) && member2.MemberType != typeof(string))
                    {
                        if (FixtureGenerator.DoesItImplementInterface(member1.MemberType, typeof(IList))
                            && FixtureGenerator.DoesItImplementInterface(member2.MemberType, typeof(IList)))
                        {
                            common.ItemType = GetCommonMemberData(member1.ItemType, member2.ItemType);
                            if (common.ItemType == null)
                            {
                                // list value types do not have common elements
                                return null;
                            }

                        }
                        else if (FixtureGenerator.DoesItImplementInterface(member1.MemberType, typeof(IDictionary))
                            && FixtureGenerator.DoesItImplementInterface(member2.MemberType, typeof(IDictionary)))
                        {
                            if (member1.KeyType.MemberType != member1.KeyType.MemberType)
                            {
                                return null; // keys MUST be of the same type, commmon members are not enough
                            }
                            common.ItemType = GetCommonMemberData(member1.ItemType, member2.ItemType);
                            if (common.ItemType == null)
                            {
                                // dictionary value types do not have common elements
                                return null;
                            }

                        }
                        else
                        {
                            common.Children = GetCommonTree(member1.Children, member2.Children);
                        }

                        return common;                        
                    }

                    return null;
                }
            }

            return null;
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
