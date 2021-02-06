using FixtureGenerator.SampleClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FixtureGenerator
{
    public class FixtureGenerator
    {
        private Random rand = new Random(32); // seed
        public void DoStuff()
        {
            ////GenerateFixture(typeof(ClassWithCollections));
            GenerateFixture(typeof(SampleModelClass));
        }

        public object GenerateFixture(Type objectType)
        {
            var constructor = objectType.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, CallingConventions.HasThis, new Type[0], null);
            object newFixture = constructor.Invoke(new Type[0]);
            var fixtureType = objectType;

            Console.WriteLine(GenerateRandomString(30));
            foreach (PropertyInfo member in fixtureType.GetProperties())
            {
                Type memberType = member.PropertyType;
                object generatedValue = GenerateValue(memberType);                            
                member.SetValue(newFixture, generatedValue);
            }

            return newFixture;
        }

        private object GenerateValue(Type memberType)
        {
            Console.WriteLine(memberType);

            if (memberType.IsPrimitive)
            {
                if (memberType == typeof(int))
                {
                    return GenerateRandomInt();
                }
                else if (memberType == typeof(bool))
                {
                    return GenerateRandomBool();
                }
                else if (memberType == typeof(double))
                {
                    return GenerateRandomDouble(2);
                }
                else if (memberType == typeof(float))
                {
                    return (float)GenerateRandomDouble(2);
                }
            }
            else if (memberType == typeof(string))
            {
                return GenerateRandomString(10);
            }
            else if (memberType.IsEnum)
            {
                Array enumValues = memberType.GetEnumValues();
                return enumValues.GetValue(rand.Next(enumValues.Length));
            }
            else if (memberType.IsClass)
            {
                if (DoesItImplementInterface(memberType, typeof(IList)))
                {
                    Console.WriteLine($"Collection");
                    return SetListValues(memberType);
                }
                else if (DoesItImplementInterface(memberType, typeof(IDictionary)))
                {
                    return SetDictionaryValues(memberType);
                }
                else
                {
                    return GenerateFixture(memberType);
                    Console.WriteLine($"Class: {memberType.Name}");
                    return null;
                }
            }
            else if (memberType.IsValueType)
            {
                if (memberType == typeof(decimal))
                {
                    return (decimal)GenerateRandomDouble(2);
                }
                return null;
            }
            else
            {
                Console.WriteLine("Something else");
            }

            return null;
        }

        private object SetListValues(Type memberType)
        {            
            Type[] typeArguments = memberType.GetGenericArguments();
            if (typeArguments.Length == 1)
            {
                IList newCollection = (IList)Activator.CreateInstance(memberType);
                for (int i = 0; i < 5; i++)
                {
                    newCollection.Add(GenerateValue(typeArguments[0]));
                }
                return newCollection;
            }
            else
            {
                throw new Exception($"Member of type {memberType} is a list but more or less than 1 type argument was found");
            }            
        }

        private object SetDictionaryValues(Type memberType)
        {
            Type[] typeArguments = memberType.GetGenericArguments();
            if (typeArguments.Length == 2)
            {
                IDictionary newCollection = (IDictionary)Activator.CreateInstance(memberType);
                for (int i = 0; i < 5; i++)
                {
                    var key = GenerateValue(typeArguments[0]);
                    var value = GenerateValue(typeArguments[1]);
                    newCollection.Add(key, value);
                }
                return newCollection;
            }
            else
            {
                throw new Exception($"Member of type {memberType} is a dictionary but more or less than 2 type argument was found");
            }
        }

        private bool DoesItImplementInterface(Type @type, Type @interface)
        {
            return @type.GetInterfaces().Contains(@interface);
        }

        private string GenerateRandomString(int length)
        {
            char[] generated = new char[length];
            string possibleChars = "abcdefghijklmnñopqrstuvwxyz";
            for (int i = 0; i < length; i++)
            {
                generated[i] = possibleChars[rand.Next(possibleChars.Length)];
            }

            return new string(generated);
        }

        public int GenerateRandomInt()
        {
            return rand.Next(int.MaxValue);
        }

        public double GenerateRandomDouble(int numDecimalDigits)
        {
            return rand.Next(int.MaxValue) / Math.Pow(10, numDecimalDigits);
        }

        public bool GenerateRandomBool()
        {
            return rand.Next(10) > 5;
        }
    }
}
