using System;
using System.IO;
using System.IO.IsolatedStorage;

namespace Blood
{
	// Token: 0x020000E9 RID: 233
	public class TrialStorage
	{
		// Token: 0x06000800 RID: 2048 RVA: 0x001D8248 File Offset: 0x001D6448
		public TrialData LoadTrial()
		{
			TrialData trialData2;
			using (IsolatedStorageFile store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null))
			{
				this.status = "";
				TrialData trialData = default(TrialData);
				trialData.fileExists = false;
				string text = "savetrial";
				if (!store.FileExists(text))
				{
					this.status = "";
					trialData2 = trialData;
				}
				else
				{
					try
					{
						using (BinaryReader binaryReader = new BinaryReader(store.OpenFile(text, FileMode.Open)))
						{
							trialData.fileExists = binaryReader.ReadBoolean();
							trialData.attempt = binaryReader.ReadInt32();
							binaryReader.Close();
						}
					}
					catch
					{
						this.status = "Error Loading Trial\n";
					}
					trialData2 = trialData;
				}
			}
			return trialData2;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x001D831C File Offset: 0x001D651C
		public void SaveTrial(TrialData sg)
		{
			this.status = "";
			using (IsolatedStorageFile store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null))
			{
				string text = "savetrial";
				try
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(store.OpenFile(text, FileMode.Create)))
					{
						binaryWriter.Write(true);
						binaryWriter.Write(sg.attempt);
						binaryWriter.Close();
					}
				}
				catch
				{
					this.status = "Error Saving Trial\n";
				}
			}
		}

		// Token: 0x040020FC RID: 8444
		public string status = "";
	}
}
