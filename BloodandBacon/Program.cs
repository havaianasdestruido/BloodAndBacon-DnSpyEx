using System;
using System.Diagnostics;
using System.IO;
using Steamworks;

namespace Blood
{
	// Token: 0x02000026 RID: 38
	internal static class Program
	{
		// Token: 0x06000177 RID: 375 RVA: 0x00031834 File Offset: 0x0002FA34
		private static void Main()
		{
			bool flag = false;
			SteamAPI.Init();
			if (flag)
			{
				using (myGame myGame = new myGame())
				{
					myGame.Run();
					goto IL_0117;
				}
			}
			try
			{
				using (myGame myGame2 = new myGame())
				{
					myGame2.Run();
				}
			}
			catch (Exception ex)
			{
				SteamAPI.Shutdown();
				string text = "unfriendly.txt";
				using (StreamWriter streamWriter = new StreamWriter(text))
				{
					streamWriter.WriteLine("**********************  SORRY THE GAME CRASHED **************************");
					streamWriter.WriteLine("");
					streamWriter.WriteLine("Sexy Date :" + DateTime.Now.ToString());
					streamWriter.WriteLine("");
					streamWriter.WriteLine("ERROR MESSAGE :  " + ex.Message);
					streamWriter.WriteLine("");
					streamWriter.WriteLine("StackTrace : " + ex.StackTrace);
					streamWriter.WriteLine("");
					streamWriter.WriteLine("**********************  SORRY THE GAME CRASHED **************************");
					streamWriter.WriteLine("");
					streamWriter.Close();
				}
			}
			IL_0117:
			SteamAPI.Shutdown();
			Process[] processesByName = Process.GetProcessesByName("BloodandBacon");
			processesByName[0].Kill();
		}
	}
}
