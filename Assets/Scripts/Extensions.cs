public static class Extensions
{
	public static bool IsDeepBot(this TwitchAlertsType type)
	{
		switch (type)
		{
			case TwitchAlertsType.YoutubeCurrentSong:
			case TwitchAlertsType.YoutubeCurrentSongRequestedBy:
				return true;
			default:
				return false;
		}
	}
}