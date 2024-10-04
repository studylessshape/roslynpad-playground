string timeStamp = "1716360582";

DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
TimeSpan span = TimeSpan.FromMilliseconds(long.Parse(timeStamp + "000"));
dtStart.Add(span).Dump();

(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds.Dump();