﻿using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Security;
using System.Globalization;

using HtmlAgilityPack;

namespace PTSPensjon.HTML
{
	public static class HTMLUtils
	{
		static string DATE_FORMAT_CULTURE = "nb-NO";

		static string READ_FULL_NEWS = "<a href=\"{0}\">Link til Tabell</a>";

		public static string ConvertHTMLToPlainText(string input)
		{
			string tempString = input;

			tempString = tempString.Replace("<li>", "• ");
			tempString = tempString.Replace("&nbsp;", "");

			return Regex.Replace(tempString, "<.*?>", string.Empty);
		}

		public static string ConvertRSSToPlainText(string input, string link)
		{
			string tempString = input;

			tempString = tempString.Replace("<li>", "• ");
			tempString = tempString.Replace("&nbsp;", "");

			if (!string.IsNullOrEmpty(link) && tempString.Contains("<table>"))
			{
				string matchCodeTag = @"<table>([^]]+)</table>";
				string replaceWith = string.Format(READ_FULL_NEWS, link);

				tempString = Regex.Replace(tempString, matchCodeTag, "\n\n&#x0a;&#x0a;" + replaceWith);

				System.Diagnostics.Debug.WriteLine("TempString contains a table. Raw string remainder is: " + tempString);
			}

			return tempString;
		}

		public static string ToFormattedDate(string input)
		{
			//System.Diagnostics.Debug.WriteLine("Date to parse: " + input);

			string parseFormat = "ddd, dd MMM yyyy HH:mm:ss zzz";
			DateTime date = DateTime.ParseExact(input, parseFormat,
										  CultureInfo.InvariantCulture);

			//System.Diagnostics.Debug.WriteLine("Date to parse: " + date.ToString());
			var m_day = date.Day.ToString();
			return string.Format("{0}. {1} {2}", m_day.PadLeft(2, '0'), ToMonthName(date.Month), date.Year);
		}

		public static string ToMonthName(int month)
		{
			var ci = new CultureInfo(DATE_FORMAT_CULTURE);
			var monthName = ci.DateTimeFormat.GetMonthName(month);

			return monthName.Substring(0,3).ToUpperInvariant();  
		}

		public static string Convert(string path)
		{
			byte[] byteArray = Encoding.UTF8.GetBytes(path);
			MemoryStream stream = new MemoryStream(byteArray);

			HtmlDocument doc = new HtmlDocument();
			doc.Load(stream);

			StringWriter sw = new StringWriter();
			ConvertTo(doc.DocumentNode, sw);
			sw.Flush();
			return sw.ToString();
		}

		public static string ConvertHtml(string html)
		{
			HtmlDocument doc = new HtmlDocument();
			doc.LoadHtml(html);

			StringWriter sw = new StringWriter();
			ConvertTo(doc.DocumentNode, sw);
			sw.Flush();
			return sw.ToString();
		}

		private static void ConvertContentTo(HtmlNode node, TextWriter outText)
		{
			foreach (HtmlNode subnode in node.ChildNodes)
			{
				ConvertTo(subnode, outText);
			}
		}

		public static void ConvertTo(HtmlNode node, TextWriter outText)
		{
			string html;
			switch (node.NodeType)
			{
				case HtmlNodeType.Comment:
					//System.Diagnostics.Debug.WriteLine("Comment. node.name: " + node.ToString());
					// don't output comments
					break;

				case HtmlNodeType.Document:
					//System.Diagnostics.Debug.WriteLine("Document. node.name: " + node.ToString());
					ConvertContentTo(node, outText);
					break;

				case HtmlNodeType.Text:
					//System.Diagnostics.Debug.WriteLine("Text. node.name: " + node.ToString());
					// script and style must not be output
					string parentName = node.ParentNode.Name;
					if ((parentName == "script") || (parentName == "style"))
						break;

					// get text
					html = ((HtmlTextNode)node).Text;

					// is it in fact a special closing node output as text?
					if (HtmlNode.IsOverlappedClosingElement(html))
						break;

					// check the text is meaningful and not a bunch of whitespaces
					if (html.Trim().Length > 0)
					{
						outText.Write(HtmlEntity.DeEntitize(html));
					}
					break;

				case HtmlNodeType.Element:
					//System.Diagnostics.Debug.WriteLine("Element. node.name: " + node.ToString());
					switch (node.Name)
					{
						
						case "p":
							//System.Diagnostics.Debug.WriteLine("P. node.name: " + node.ToString());
							// treat paragraphs as crlf
							outText.Write("\r\n");
							break;
					}

					if (node.HasChildNodes)
					{
						ConvertContentTo(node, outText);
					}
					break;
				}
			}
	}
}