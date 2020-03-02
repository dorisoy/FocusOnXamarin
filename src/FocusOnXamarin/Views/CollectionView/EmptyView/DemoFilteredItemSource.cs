﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FocusOnXamarin.Views
{
	internal class DemoFilteredItemSource
	{
		readonly List<CollectionViewGalleryTestItem> _source;
		private readonly Func<string, CollectionViewGalleryTestItem, bool> _filter;

		public ObservableCollection<CollectionViewGalleryTestItem> Items { get; }

		public DemoFilteredItemSource(int count = 50, Func<string, CollectionViewGalleryTestItem, bool> filter = null)
		{
			_source = new List<CollectionViewGalleryTestItem>();
			
			string[] images = 
			{
				"fruit01.jpg",
				"fruit02.jpg",
				"fruit03.jpg",
				"fruit04.jpg",
				"fruit05.jpg",
				"fruit06.jpg"
			};

			for (int n = 0; n < count; n++)
			{
				_source.Add(new CollectionViewGalleryTestItem(DateTime.Now.AddDays(n),
					$"{images[n % images.Length]}, {n}", images[n % images.Length], n));
			}
			Items = new ObservableCollection<CollectionViewGalleryTestItem>(_source);

			_filter = filter ?? ItemMatches;
		}

		private bool ItemMatches(string filter, CollectionViewGalleryTestItem item)
		{
			filter = filter ?? "";
			return item.Caption.ToLower().Contains(filter?.ToLower());
		}

		public void FilterItems(string filter)
		{
			var filteredItems = _source.Where(item => _filter(filter, item)).ToList();

			foreach (CollectionViewGalleryTestItem collectionViewGalleryTestItem in _source)
			{
				if (!filteredItems.Contains(collectionViewGalleryTestItem))
				{
					Items.Remove(collectionViewGalleryTestItem);
				}
				else
				{
					if (!Items.Contains(collectionViewGalleryTestItem))
					{
						Items.Add(collectionViewGalleryTestItem);
					}
				}
			}
		}
	}
}