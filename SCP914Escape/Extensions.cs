using System.Collections.Generic;

namespace SCP914Escape
{
	public static class Extensions
	{
		//These are two commonly used extensions that will make your life considerably easier
		//When sending RaReply's, you need to identify the 'source' of the message with a string followed by '#' at the start of the message, otherwise the message will not be sent
		public static void RAMessage(this CommandSender sender, string message, bool success = true) =>
			sender.RaReply("Sample Plugin#" + message, success, true, string.Empty);

		public static void Broadcast(this ReferenceHub rh, uint time, string message) => rh.GetComponent<Broadcast>().TargetAddElement(rh.scp079PlayerScript.connectionToClient, message, time, false);

		public static List<string> ToList(this string s)
		{
            if (s == null)

                return null;

            List<string> dict = new List<string>();

            if (!s.Contains(","))

            {

                dict.Add(s);

                return dict;

            }

            string[] tl = s.Split(',');

            foreach (string t in tl)

            {

                dict.Add(t);

            }

            return dict;
        }
	}
}