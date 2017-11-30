using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

using HtmlAgilityPack;

using PTSPensjon.HTML;

namespace PTSPensjon
{
	public class FAQReader
	{
		public static FAQReader Instance = null;
		public static IEnumerable<_FAQItem> faqItems;

		public HtmlDocument doc;

		public FAQReader()
		{
			doc = null;

			Instance = this;
			LoadWebsite();
		}

		public class _FAQItem
		{
			public string Question;
			public string Answer;
		}

		public void ReadFAQSite()
		{
			//Fallback solution if error or website not available
			//System.Diagnostics.Debug.WriteLine("doc == null: " + (doc == null));
			if (doc == null)
			{
				//System.Diagnostics.Debug.WriteLine("doc is null. use default faq document");

				var m_faqWebsite = Localization.Get("FAQ_link_local");

				var assembly = typeof(FAQReader).GetTypeInfo().Assembly;
				Stream stream = assembly.GetManifestResourceStream(m_faqWebsite);

				//System.Diagnostics.Debug.WriteLine("assembly: " + assembly + " | stream Length: " + stream.Length);

				doc = new HtmlDocument();
				doc.Load(stream);
			}

			//TODO Wrap this in try/catch. Request from json instead, if possible
			var m_faqItems = from node in doc.DocumentNode.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "faq-card")
				select new _FAQItem
				{
					Question = HTMLUtils.ConvertHTMLToPlainText(node.Descendants("div").FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "valign faq-title").InnerHtml),
					Answer = HTMLUtils.ConvertHTMLToPlainText(node.Descendants("div").FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "answer-answer faq-answer").InnerHtml),
				};

			faqItems = m_faqItems.Reverse();
		}

		public async void LoadWebsite()
		{
			HtmlWeb web = new HtmlWeb();

			var m_faqURL = Localization.Get("FAQ_link");
			try
			{
				var task = web.LoadFromWebAsync(m_faqURL);
				await task;

				doc = task.Result;
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine("Failed to read FAQ from webstream: " + e.ToString());
			}

			ReadFAQSite();
		}
	}
}