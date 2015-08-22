namespace GroundJobs.ServiceBus.Services
{
    public static class StringExtensions
    {
        public static string SubstringBetweenStrings(this string value, string start, string end, int startPos)
        {
            var returnStartPos = value.IndexOf(start, startPos) + start.Length;
            var returnEndPos = value.IndexOf(end, returnStartPos);
            var retval = value.Substring(returnStartPos, returnEndPos - returnStartPos);

            return retval;
        }
    }
}