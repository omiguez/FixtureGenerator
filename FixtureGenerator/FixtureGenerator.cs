using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FixtureGenerator
{
    public class FixtureGenerator
    {
        private Random rand = new Random(32); // seed
        public void DoStuff()
        {
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
                SetValue(member, newFixture);
            }

            return newFixture;
        }

        private void SetValue(PropertyInfo member, object newFixture)
        {
            Type memberType = member.PropertyType;
            Console.WriteLine(memberType);

            if (memberType.IsPrimitive)
            {
                if (memberType == typeof(int))
                {
                    member.SetValue(newFixture, GenerateRandomInt());
                }
                else if (memberType == typeof(bool))
                {
                    member.SetValue(newFixture, GenerateRandomBool());
                }
                else if (memberType == typeof(double))
                {
                    member.SetValue(newFixture, GenerateRandomDouble(2));
                }
                else if (memberType == typeof(float))
                {
                    member.SetValue(newFixture, (float)GenerateRandomDouble(2));
                }
            }
            else if (memberType == typeof(string))
            {
                member.SetValue(newFixture, GenerateRandomString(10));
            }
            else if (memberType.IsEnum)
            {
                Array enumValues = memberType.GetEnumValues();
                member.SetValue(newFixture, enumValues.GetValue(rand.Next(enumValues.Length)));
            }
            else if (memberType.IsClass)
            {
                member.SetValue(newFixture, GenerateFixture(memberType)); // TODO: 
                Console.WriteLine($"Class: {memberType.Name}");
            }
            else if (memberType.IsValueType)
            {
                if (memberType == typeof(decimal))
                {
                    member.SetValue(newFixture, (decimal)GenerateRandomDouble(2));
                }
            }
            else
            {
                Console.WriteLine("Something else");
            }
        }

        public string GenerateRandomString(int length)
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
