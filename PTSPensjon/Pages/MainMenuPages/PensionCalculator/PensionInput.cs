namespace PTSPensjon
{
	public class PensionInput
	{
		public int BirthYear { get; set; }

		public int SuperiorPeriod { get; set; }

		public int PreSubordinatePeriod { get; set; }

		public int PostSubordinatePeriod { get; set; }

		public int ChildSupport { get; set; }

		public PensionInput(string Birthyear, string SuperiorPeriod, string PreSubordinatePeriod, 
		                    string PostSubordinatePeriod, string ChildSupport)
		{
            this.BirthYear = TryParseValue(Birthyear);
            this.SuperiorPeriod = TryParseValue(SuperiorPeriod);
            this.PreSubordinatePeriod = TryParseValue(PreSubordinatePeriod);
            this.PostSubordinatePeriod = TryParseValue(PostSubordinatePeriod);
            this.ChildSupport = TryParseValue(ChildSupport);
		}

		private int TryParseValue(string local)
		{
			int val = 0;
			int.TryParse(local, out val);

			return val;
		}

		public override string ToString()
		{
			return string.Format("[PensionInput: BirthYear={0}, SuperiorPeriod={1}, PreSubordinatePeriod={2}, PostSubordinatePeriod={3}, ChildSupport={4}]", BirthYear, SuperiorPeriod, PreSubordinatePeriod, PostSubordinatePeriod, ChildSupport);
		}
	}
}