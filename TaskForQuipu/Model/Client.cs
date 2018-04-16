using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TaskForQuipu.Model
{
   public class Client
    {
        [XmlAttribute("ID")]
        public int Id { get; set; }

        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public string Address { get; set; }

    }

    [XmlRoot("Clients")]
    public class ClientXML
    {
        [XmlElement("Client")]
        public List<Client> Clients { get; set; }


        public static ClientXML Deserialize(string xmlString)
        {
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xmlString));
            StreamReader reader = new StreamReader(ms, Encoding.UTF8);
            XmlSerializer serializer = new XmlSerializer(typeof(ClientXML));
            return (ClientXML)serializer.Deserialize(reader);
        }
    }
}

