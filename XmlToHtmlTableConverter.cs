using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XMLToHTML
{
    public class XmlToHtmlTableConverter
    {
        public string ConvertXmlToHtmlTable(string xml)
        {     
            StringBuilder html = new StringBuilder("<table align='center' " +
               "border='1' class='xmlTable'>\r\n");
            try
            {
                if (xml != null && xml != String.Empty)
                {
                    XDocument xDocument = XDocument.Parse(xml);
                    List<XElement> contentElements = xDocument.Root.Elements("Items").Elements("Item").ToList();
                    List<XElement> ItemData = new List<XElement>();
                    List<List<XElement>> ItemDataList = new List<List<XElement>>();
                    int RowNumber = contentElements.Count();
                    int ColumnNumber = contentElements.ElementAt(0).Elements().Count();
                    for (int i = 0; i < RowNumber; i++)
                    {
                        if (contentElements.ElementAt(i).HasElements)
                        {
                            ItemData = new List<XElement>();
                            foreach (var item in contentElements.ElementAt(i).Elements())
                            {
                                ItemData.Add(item);
                            }
                            ItemDataList.Add(ItemData);
                        }
                    }
                    StringBuilder rowHeder = new StringBuilder("");
                    rowHeder.Append("<tr>");
                    for (int i = 0; i < ColumnNumber; i++)
                    {
                        rowHeder.Append("<th>" + System.Xml.XmlConvert.DecodeName(ItemData.ElementAt(i).Name.ToString())  + "</th>");
                    }
                    rowHeder.Append("</tr>");
                    StringBuilder rowdata = new StringBuilder("");
                    foreach (var rowDataList in ItemDataList)
                    {
                        rowdata.Append("<tr>");
                        foreach (var item in rowDataList)
                        {
                            rowdata.Append("<td>" + System.Xml.XmlConvert.DecodeName(item.Value.ToString()) + "</td>");
                        }
                        rowdata.Append("</tr>");
                    }
                    html.Append(rowHeder.ToString());
                    html.Append(rowdata.ToString());
                    html.Append("</table>");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return html.ToString();
        }

    }
}
