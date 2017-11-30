using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Rox;

namespace PTSPensjon
{
	public partial class IntroPage5 : ContentPage
	{
		bool _isPlayingVideo = false;

		readonly Easing ease = Easing.SinInOut;

		public IntroPage5()
		{
			InitializeComponent();

			if (Device.RuntimePlatform == Device.Android)
				header.IsVisible = false;

			this.BindingContext = new IntroPage5ViewModel(GetVideoView());

			NextButton.GestureRecognizers.Add( new TapGestureRecognizer
			{
				Command = new Command( () =>
				{
					VideoView.Stop();
					IntroCarousel.NavigateToMainMenu();
				}),
			});
			SkipIntroText.GestureRecognizers.Add(new TapGestureRecognizer
			{
				Command = new Command( () =>
			   {
				   VideoView.Stop();
				   IntroCarousel.NavigateToMainMenu();
			   }),
			});

			//System.Diagnostics.Debug.WriteLine("Source: " + "android.resource://" + _Source);
		}

		public VideoView GetVideoView()
		{
			return VideoView;   
		}

		public async void PlayIntroVideo(object sender, System.EventArgs e)
		{
			//System.Diagnostics.Debug.WriteLine("Play into video test");
			VideoView.IsVisible = true;
			VideoView.FullScreen = true;
			VideoView.ShowController = true;

			videoButton.IsVisible = false;

#if __ANDROID__
			VideoView.ShowController = true;
			AnimateImage();
#endif
			await VideoView.Start();
		}

		public void AnimateImage()
		{
			_isPlayingVideo = !_isPlayingVideo;
			if (_isPlayingVideo)
			{
				//bg_overlay
				var animate = new Animation(d => bg_overlay.Opacity = d, 0f, 0.7f, ease);
				animate.Commit(bg_overlay, "anim1", 16, 80);

				Grid.SetColumn(videoViewGrid, 0);
				Grid.SetColumnSpan(videoViewGrid, 4);
			}
			else
			{
				var animate = new Animation(d => bg_overlay.Opacity = d, 0f, 0.0f, ease);
				animate.Commit(bg_overlay, "anim1", 16, 80);

				Grid.SetColumn(videoViewGrid, 1);
				Grid.SetColumnSpan(videoViewGrid, 2);
			}

			VideoView.ShowController = !_isPlayingVideo;
		}
	}
}
