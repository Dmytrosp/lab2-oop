using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using XmlParser.Models;

namespace XmlParser
{
    class SaxXmlParser : IXmlParserStrategy
    {
        public Catalog Parse(string xmlFile, CD cd)
        {
            var xmlReader = new XmlTextReader(xmlFile);
            xmlReader.WhitespaceHandling = WhitespaceHandling.None;

            List<CD> cds = new List<CD>();
            int elementsCount = 6;
            string title = "";
            string artist = "";
            string country = "";
            string company = "";
            string price = "";
            string year = "";

            while (xmlReader.Read())
            {
                string name;
                string value;
                int i = 0;

                switch (xmlReader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (xmlReader.Name.ToLower() == "cd")
                        {
                            while (i < elementsCount)
                            {
                                xmlReader.Read();
                                name = xmlReader.Name.ToLower();

                                xmlReader.Read();
                                value = xmlReader.Value;
                                xmlReader.Read();

                                if (name == "title" && (cd.Title == "" || cd.Title == value))
                                    title = value;
                                else if (name == "artist" && (cd.Artist == "" || cd.Artist == value))
                                    artist = value;
                                else if (name == "country" && (cd.Country == "" || cd.Country == value))
                                    country = value;
                                else if (name == "company" && (cd.Company == "" || cd.Company == value))
                                    company = value;
                                else if (name == "price" && (cd.Price == "" || cd.Price == value))
                                    price = value;
                                else if (name == "year" && (cd.Year == "" || cd.Year == value))
                                    year = value;

                                i++;
                            }
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if(xmlReader.Name.ToLower() == "cd")
                        {
                            if (title != "" && artist != "" && country != ""
                                && company != "" && price != "" && year != "")
                            {
                                CD cd1 = new CD(title, artist, country, company, price, year);
                                cds.Add(cd1);
                            }

                            title = "";
                            artist = "";
                            country = "";
                            company = "";
                            price = "";
                            year = "";
                        }
                        break;
                }
            }
            xmlReader.Close();

            Catalog catalog = new Catalog() { CDs = cds };
            return catalog;
        }
    }
}












