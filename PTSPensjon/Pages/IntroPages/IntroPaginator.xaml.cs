using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PTSPensjon
{
    public partial class IntroPaginator : ContentView
    {
        const int TOTAL_PAGES = 5;

        public int Index
        {
            get { return (int)Resources["_index"]; }
            set
            {
                Resources["_index"] = value;
                SetChildrenTextures();
            }
        }

        public IntroPaginator()
        {
            InitializeComponent();
        }

        public void SetChildrenTextures()
        {
            for (int i = 0; i < TOTAL_PAGES; i++)
            {
                var m_image = new Image
                {
                    Scale = 1.0f,
                    HeightRequest = 12f,
                    Source = Index == i ? "PageIndicator_highlight" : "PageIndicator_idle",
                };

                stackLayout.Children.Add(m_image);
            }
        }
    }
}
