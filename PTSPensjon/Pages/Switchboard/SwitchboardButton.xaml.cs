 using System; using System.Collections.Generic; using System.Linq; using System.Text; using System.Threading.Tasks;  using Xamarin.Forms;  namespace PTSPensjon {
	public partial class SwitchboardButton : ContentView
	{
		public SwitchboardButton()
		{
			InitializeComponent();  			//PushIconText = "1";  			//Icon = "Knapp.png"; 			//OverlayIcon = "seatime.png"; 			//Label = "Pensjonskalkulator. Sjekk din pensjon her";  			PushIcon.IsVisible = false; 			//PushIconLabel.IsVisible = false; 		}

		public ImageSource Icon
		{
			get { return SwitchboardIcon.Source; }
			set { SwitchboardIcon.Source = value; }
		}  		public ImageSource OverlayIcon 		{ 			get { return SwitchboardOverlayicon.Source; } 			set {  				SwitchboardOverlayicon.Source = value;
			} 		}

		public string Label
		{
			get { return SwitchboardLabel.Text; }
			set { SwitchboardLabel.Text = value; }
		}  		public ImageSource Push
		{
			get { return PushIcon.Source; } 		} 		public string PushIconText
		{
			get { return PushIconLabel.Text; }
			set {  				//PushIconLabel.Text = value;  				System.Diagnostics.Debug.WriteLine("Updating PushIconText. value: " + value);  				var m_pushIconValue = 0;  				Int32.TryParse(value, out m_pushIconValue); 				PushIcon.IsVisible = (m_pushIconValue > 0 ? true : false);  				System.Diagnostics.Debug.WriteLine("PushIcon.IsVisible: " + PushIcon.IsVisible);
			} 		}
	} } 