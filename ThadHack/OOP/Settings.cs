using System.IO;
using System.Xml;

namespace ZzukBot.OOP
{
    internal static class Settings
    {
        internal static void Recreate(string parPathToWoW)
        {
            var doc = new XmlDocument();
            XmlNode settingsNode = doc.CreateElement("Settings");
            doc.AppendChild(settingsNode);
            XmlNode pathNode = doc.CreateElement("Path");
            pathNode.InnerText = parPathToWoW;
            settingsNode.AppendChild(pathNode);
            if (!Directory.Exists("..\\Settings"))
                Directory.CreateDirectory("..\\Settings");
            doc.Save("..\\Settings\\Settings.xml");
        }
    }
}