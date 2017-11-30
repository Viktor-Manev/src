using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using PTSPensjon.HTML;

namespace PTSPensjon
{
	public class RSSReader
	{
		public class Post
		{
			public string Title;
			public string Link;
			public string PublishedDate;
			public string Creator;
			public string Category;
			public string Description;
			public string ContentEncoded;
		}

		XNamespace content = "http://purl.org/rss/1.0/modules/content/";

		public IEnumerable<Post> ReadFeed(string url)
		{
			var rssFeed = XDocument.Load(url);
			//text.Replace("content:encoded", "contentt").Replace("xmlns:content=\"http://purl.org/rss/1.0/modules/content/\"","");

#if DEBUG
			foreach (var el in rssFeed.Elements())
			{
				//System.Diagnostics.Debug.WriteLine("rssFeed.Elements(). element: " + el.ToString());
			}
			foreach (var el in rssFeed.Descendants("item"))
			{
				//System.Diagnostics.Debug.WriteLine("rssFeed.Descendants(). element: " + el.ToString());
			}
#endif

			var posts = from item in rssFeed.Descendants("item")
				select new Post
				{
					Title = item.Element("title").Value,
					Link = item.Element("link").Value,
					PublishedDate = item.Element("pubDate").Value,
					//Creator = item.Element( XName.Get("encoded", item.GetDefaultNamespace()) ).Value,
					//Creator = item.Element( XName.Get("dc:creator") ).Value,
					//Creator = XElement.Parse("<creator>" + (string)item.DescendantsAndSelf + "</creator>"),
					Category = item.Element("category").Value,
					Description = item.Element("description").Value,
					ContentEncoded = item.Element( content + "encoded" ).Value,
					//ContentEncoded = item.Element( "description").Value,
				};

			posts = posts.Reverse();

			return posts;
		}
	}
}