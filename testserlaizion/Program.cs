using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using System.Xml.Serialization.Configuration;
namespace testserlaizion
{
    [Serializable]
    public class meperson
    {
        public string name { get; set; }
        public int age { get; set; }
        public meperson(string name, int age)
        {
            this.name = name;
            this.age = age;
        }
        public meperson() { }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //meperson p = new meperson {name= "alias",age= 25 };
            meperson p = new meperson("alias", 25);
            meperson p2 = new meperson("ali", 20);
            //string json = System.Text.Json.JsonSerializer.Serialize(p);
            //Console.WriteLine(json);
            //person p2 = System.Text.Json.JsonSerializer.Deserialize<person>(json);
            //Console.WriteLine(p2.name + " " + p2.age);
            //Console.ReadKey();
            XmlSerializer xmlSerializerWriter = new XmlSerializer(typeof(meperson));
            using (TextWriter t = new StreamWriter("person.xml"))
            {

                xmlSerializerWriter.Serialize(t, p);
            }
            XmlSerializer xmlSerializerReader = new XmlSerializer(typeof(meperson));
            using (TextReader r = new StreamReader("person.xml"))
            {
                while (r.ReadLine() != null)
                {
                    meperson p2 = (meperson)xmlSerializerReader.Deserialize(r);
                    Console.WriteLine(p2.name + " " + p2.age);
                }
            }


        }
    }
}
