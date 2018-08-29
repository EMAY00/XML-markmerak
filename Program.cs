using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace XML_markmerak
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckAndLoad();
            List<Note> Notes = new List<Note>();

            Notes = new List<Note>();
            var Serializer = new XmlSerializer(typeof(List<Note>));
            using (var Reader = XmlReader.Create("../../Notes.xml"))
            {
                Notes = (List<Note>)Serializer.Deserialize(Reader);
            }

            while (true)
            {
                Console.WriteLine("Kirjuta faili lisamiseks(\"lisa\"), vaatamiseks(\"vaata\") või kustutamiseks(\"kustuta\").Lahkumise soovil kirjuta \"exit\".");
                string teha1 = Console.ReadLine();

                if (teha1 == "lisa")
                {
                    while (true)
                    {
                        Console.WriteLine("Sisestage märkme nimetus");
                        string Name = Console.ReadLine();
                        Console.WriteLine("Sisestage märkme sisu");
                        string Content = Console.ReadLine();

                        if (Name != "" && Content != "")
                        {
                            Notes.Add(new Note() { Nimi = Name, Sisu = Content });
                            using (var Writer = XmlWriter.Create("../../Notes.xml"))
                            {
                                Serializer.Serialize(Writer, Notes);
                                Console.Clear();
                                Console.WriteLine("Märge on lisatud!");
                                break;
                            }
                        }
                    }
                }

                else if (teha1 == "vaata")
                {
                    if (Notes.Count == 0)
                    {
                        while (true)
                        {
                            Console.WriteLine("Teil pole märkmeid");
                            Console.WriteLine("Soovite jätkata Notepad9009 kasutusega? (\"jah\"/\"ei\")");
                            string teha3 = Console.ReadLine();

                            if (teha3 == "jah")
                            {
                                Console.Clear();
                                break;
                            }
                            else if (teha3 == "ei")
                            {
                                Environment.Exit(1);
                            }
                        }
                    }

                    else
                    {
                        while (true)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Märkmed: ");
                            int i = 0;
                            foreach (var item in Notes)
                            {
                                i++;
                                Console.WriteLine(i + ") " + item.Nimi);
                            }
                            Console.WriteLine("Sisestage number avamiseks:");
                            int vaata = int.Parse(Console.ReadLine());
                            if (vaata < 1 | vaata > Notes.Count)
                            {
                                Console.Clear();
                                Console.WriteLine("Midagi läks valesti. Valige uuesti!");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Märkme: " + "\"" + Notes[vaata - 1].Nimi + "\"" + " sisu:\n" + Notes[vaata - 1].Sisu);
                                break;
                            }
                        }
                    }
                }

                else if (teha1 == "kustuta")
                {
                    if (Notes.Count == 0)
                    {
                        while (true)
                        {
                            Console.WriteLine("Teil pole märkmeid mida kustutada.");
                            Console.WriteLine("Soovite jätkata kasutusega? (\"jah\"/\"ei\")");
                            string teha3 = Console.ReadLine();

                            if (teha3 == "jah")
                            {
                                Console.Clear();
                                break;
                            }
                            else if (teha3 == "ei")
                            {
                                Environment.Exit(1);
                            }
                        }
                    }

                    else
                    {
                        while (true)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Märkmed: ");
                            int i = 0;
                            foreach (var item in Notes)
                            {
                                i++;
                                Console.WriteLine(i + ") " + item.Nimi);
                            }
                            Console.WriteLine("Sisestage number kustutamiseks:");
                            int kustuta = int.Parse(Console.ReadLine());
                            if (kustuta < 1 | kustuta > Notes.Count)
                            {
                                Console.Clear();
                                Console.WriteLine("Midagi läks valesti.Valige uuesti!");
                            }
                            else
                            {
                                Notes.RemoveAt(kustuta - 1);
                                using (var Writer = XmlWriter.Create("../../Notes.xml"))
                                {
                                    Serializer.Serialize(Writer, Notes);
                                    Console.Clear();
                                    Console.WriteLine("Märge kustutatud!");
                                    break;
                                }
                            }
                        }
                    }
                }

                else if (teha1 == "exit")
                {
                    Environment.Exit(1);
                }

                else continue;
            }
        }

        static void CheckAndLoad()
        {
            List<Note> Notes = new List<Note>();
            XmlSerializer Serializer = new XmlSerializer(typeof(List<Note>));

            if (!(File.Exists("../../Notes.xml")))
            {
                using (var Writer = XmlWriter.Create("../../Notes.xml"))
                {
                    Serializer.Serialize(Writer, Notes);
                }
            }
        }
    }

    public class Note
    {
        public string Nimi { get; set; }
        public string Sisu { get; set; }
    }
}
