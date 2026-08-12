using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Steamworks;

namespace Blood
{
	// Token: 0x02000098 RID: 152
	public class Workshop
	{
		// Token: 0x060005A5 RID: 1445 RVA: 0x00136F7C File Offset: 0x0013517C
		public Workshop(ScreenManager screenmanager)
		{
			this.sc = screenmanager;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x001370A8 File Offset: 0x001352A8
		public void forceDownloadFileId()
		{
			this.weSubscribed = false;
			string text = "\\diary";
			if (this.testmode)
			{
				text = "\\diarytest";
			}
			this.entry = new List<Workshop.diaryEntry>();
			this.entry.Clear();
			this.entryBox.Clear();
			this.publishedfiledid = default(PublishedFileId_t);
			if (SteamAPI.IsSteamRunning() && this.published != null)
			{
				for (int i = 0; i < this.published.Length; i++)
				{
					try
					{
						ulong num;
						string text2;
						uint num2;
						if (SteamUGC.GetItemInstallInfo(this.published[i], out num, out text2, 512U, out num2))
						{
							this.publishedfileFolder = "";
							if (File.Exists(text2 + text))
							{
								FileInfo fileInfo = new FileInfo(text2 + text);
								this.fileSize = (int)fileInfo.Length;
								this.weSubscribed = true;
								this.publishedfileFolder = text2;
								this.publishedfiledid = this.published[i];
								SteamUGC.SubscribeItem(this.publishedfiledid);
								SteamUGC.DownloadItem(this.publishedfiledid, true);
								this.downloadComplete = 4;
								this.myTimer = 240;
							}
						}
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x001371F4 File Offset: 0x001353F4
		public bool populateDiary()
		{
			if (this.downloadComplete <= 0 || !(this.publishedfileFolder != ""))
			{
				return false;
			}
			ulong num = 0UL;
			ulong num2 = 5UL;
			SteamUGC.GetItemDownloadInfo(this.publishedfiledid, out num, out num2);
			if (num2 == num && num2 == 0UL)
			{
				this.downloadComplete = 0;
				this.entry.Clear();
				this.entryBox.Clear();
				int num3 = 0;
				int num4 = 0;
				string[] directories = Directory.GetDirectories(this.publishedfileFolder);
				for (int i = directories.Length - 1; i >= 0; i--)
				{
					this.sc.tick.Play(0.5f, -0.2f, 1f);
					if (File.Exists(directories[i].ToString() + "\\report.wav") && File.Exists(directories[i].ToString() + "\\report.txt"))
					{
						try
						{
							bool flag = true;
							Workshop.diaryEntry diaryEntry = new Workshop.diaryEntry();
							diaryEntry.path = directories[i];
							diaryEntry.dates = new DirectoryInfo(directories[i]).Name;
							diaryEntry.motion = new List<float>();
							diaryEntry.media = new List<Texture2D>();
							diaryEntry.soundhit = new List<SoundEffect>();
							diaryEntry.voice = this.sc.tick;
							if (flag)
							{
								this.entry.Add(diaryEntry);
								Rectangle rectangle = new Rectangle(630, 230, 120, 30);
								if (diaryEntry.dates.Split(new char[] { '.' })[2] == "21")
								{
									if (num3 == 0)
									{
										rectangle = new Rectangle(635, 232, 120, 30);
									}
									else
									{
										rectangle = new Rectangle(660, 290 + (num3 - 1) % 20 * 30, 100, 15);
									}
									num3++;
								}
								else
								{
									num4++;
									rectangle = new Rectangle(790, 290 + (num4 - 1) % 20 * 30, 100, 15);
								}
								this.entryBox.Add(rectangle);
							}
						}
						catch
						{
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00137438 File Offset: 0x00135638
		public bool populateDiaryContents(int myIndex)
		{
			bool flag = false;
			if (this.publishedfileFolder != "")
			{
				string[] directories = Directory.GetDirectories(this.publishedfileFolder);
				for (int i = 0; i < directories.Length; i++)
				{
					if (new DirectoryInfo(directories[i]).Name == this.entry[myIndex].dates)
					{
						try
						{
							using (FileStream fileStream = new FileStream(directories[i] + "\\report.wav", FileMode.Open))
							{
								this.entry[myIndex].voice = SoundEffect.FromStream(fileStream);
							}
							CultureInfo invariantCulture = CultureInfo.InvariantCulture;
							StreamReader streamReader = new StreamReader(directories[i] + "\\report.txt");
							this.entry[myIndex].motion.Clear();
							while (!streamReader.EndOfStream)
							{
								float num = (float)Convert.ToDecimal(streamReader.ReadLine(), invariantCulture);
								this.entry[myIndex].motion.Add(num);
							}
							streamReader.Close();
							streamReader.Dispose();
							this.entry[myIndex].soundhit.Clear();
							this.entry[myIndex].media.Clear();
							for (int j = 0; j < 10; j++)
							{
								if (File.Exists(string.Concat(new object[]
								{
									directories[i],
									"\\",
									j,
									"ss.wav"
								})))
								{
									using (FileStream fileStream2 = new FileStream(string.Concat(new object[]
									{
										directories[i],
										"\\",
										j,
										"ss.wav"
									}), FileMode.Open))
									{
										this.entry[myIndex].soundhit.Add(SoundEffect.FromStream(fileStream2));
									}
								}
								if (File.Exists(string.Concat(new object[]
								{
									directories[i],
									"\\",
									j,
									"mm.png"
								})))
								{
									using (FileStream fileStream3 = new FileStream(string.Concat(new object[]
									{
										directories[i],
										"\\",
										j,
										"mm.png"
									}), FileMode.Open))
									{
										this.entry[myIndex].media.Add(Texture2D.FromStream(this.sc.GraphicsDevice, fileStream3));
									}
								}
								if (File.Exists(string.Concat(new object[]
								{
									directories[i],
									"\\",
									j,
									"mm.jpg"
								})))
								{
									using (FileStream fileStream4 = new FileStream(string.Concat(new object[]
									{
										directories[i],
										"\\",
										j,
										"mm.jpg"
									}), FileMode.Open))
									{
										this.entry[myIndex].media.Add(Texture2D.FromStream(this.sc.GraphicsDevice, fileStream4));
									}
								}
							}
							flag = true;
							break;
						}
						catch
						{
							flag = false;
							break;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00137814 File Offset: 0x00135A14
		public void createUpdateItemX()
		{
			string text = "fileid";
			if (this.testmode)
			{
				text = "fileTestid";
			}
			if (SteamAPI.IsSteamRunning())
			{
				if (File.Exists(text))
				{
					using (BinaryReader binaryReader = new BinaryReader(File.Open(text, FileMode.Open)))
					{
						ulong num = binaryReader.ReadUInt64();
						this.publishedfiledid = new PublishedFileId_t(num);
						binaryReader.Close();
					}
					this.startUpdatingNewItemX();
					return;
				}
				this.sc.abort.Play(this.sc.ev, 1f, 0f);
			}
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x001378B4 File Offset: 0x00135AB4
		public void startUpdatingNewItemX()
		{
			if (SteamAPI.IsSteamRunning())
			{
				if (!this.testmode)
				{
					this.updateHandle = SteamUGC.StartItemUpdate(this.myappID, this.publishedfiledid);
					SteamUGC.SetItemTitle(this.updateHandle, "DearDiary");
					SteamUGC.SetItemDescription(this.updateHandle, "Weekly Readings From The Farmer");
					SteamUGC.SetItemUpdateLanguage(this.updateHandle, "en");
					SteamUGC.SetItemMetadata(this.updateHandle, "New Update Panel Notes");
					string[] array = new string[] { "Farmer Diary" };
					SteamUGC.SetItemTags(this.updateHandle, array);
					SteamUGC.SetItemVisibility(this.updateHandle, this.formPublic);
					string text = "UploadData2";
					if (Directory.Exists(text))
					{
						text = Path.GetFullPath(text);
					}
					SteamUGC.SetItemContent(this.updateHandle, text);
					string text2 = "image3.jpg";
					if (File.Exists(text2))
					{
						text2 = Path.GetFullPath(text2);
					}
					SteamUGC.SetItemPreview(this.updateHandle, text2);
					this.submitCreatedItemX();
					return;
				}
				this.updateHandle = SteamUGC.StartItemUpdate(this.myappID, this.publishedfiledid);
				SteamUGC.SetItemTitle(this.updateHandle, "DearDiaryTest");
				SteamUGC.SetItemDescription(this.updateHandle, "TESTING TESTING");
				SteamUGC.SetItemUpdateLanguage(this.updateHandle, "en");
				SteamUGC.SetItemMetadata(this.updateHandle, "New Test");
				string[] array2 = new string[] { "Farmer Test Diary" };
				SteamUGC.SetItemTags(this.updateHandle, array2);
				SteamUGC.SetItemVisibility(this.updateHandle, this.formVisibilityPrivate);
				string text3 = "UploadDataTest";
				if (Directory.Exists(text3))
				{
					text3 = Path.GetFullPath(text3);
				}
				SteamUGC.SetItemContent(this.updateHandle, text3);
				string text4 = "imageTest.jpg";
				if (File.Exists(text4))
				{
					text4 = Path.GetFullPath(text4);
				}
				SteamUGC.SetItemPreview(this.updateHandle, text4);
				this.submitCreatedItemX();
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00137A94 File Offset: 0x00135C94
		public void submitCreatedItemX()
		{
			if (SteamAPI.IsSteamRunning())
			{
				string text = this.changenotes;
				this.submitItem = CallResult<SubmitItemUpdateResult_t>.Create(new CallResult<SubmitItemUpdateResult_t>.APIDispatchDelegate(this.onSubmitResultX));
				this.Handle = SteamUGC.SubmitItemUpdate(this.updateHandle, text);
				this.submitItem.Set(this.Handle, null);
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00137AEC File Offset: 0x00135CEC
		private void onSubmitResultX(SubmitItemUpdateResult_t pCallback, bool fails)
		{
			if (SteamAPI.IsSteamRunning())
			{
				if (pCallback.m_eResult == EResult.k_EResultOK)
				{
					this.sc.achievepop.Play(this.sc.ev, -1f, 0f);
					return;
				}
				this.sc.abort.Play(this.sc.ev, 0f, 0f);
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00137B58 File Offset: 0x00135D58
		public void createNewItem()
		{
			if (SteamAPI.IsSteamRunning())
			{
				this.createItem = CallResult<CreateItemResult_t>.Create(new CallResult<CreateItemResult_t>.APIDispatchDelegate(this.onCreatedResult));
				SteamAPICall_t steamAPICall_t = SteamUGC.CreateItem(this.myappID, EWorkshopFileType.k_EWorkshopFileTypeFirst);
				this.createItem.Set(steamAPICall_t, null);
				this.status = "WORK";
				this.workstatus = "UPLOAD";
			}
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00137BB4 File Offset: 0x00135DB4
		private void onCreatedResult(CreateItemResult_t pCallback, bool fails)
		{
			if (SteamAPI.IsSteamRunning())
			{
				if (pCallback.m_eResult == EResult.k_EResultOK)
				{
					this.publishedfiledid = pCallback.m_nPublishedFileId;
					this.startUpdatingNewItem();
					return;
				}
				MessageBoxScreen2 messageBoxScreen = new MessageBoxScreen2("On Create Attempt \n" + pCallback.m_eResult.ToString() + "\n", 16);
				this.sc.AddScreen(messageBoxScreen, null);
				this.status = "FAIL";
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00137C30 File Offset: 0x00135E30
		public void startUpdatingNewItem()
		{
			if (SteamAPI.IsSteamRunning())
			{
				this.updateHandle = SteamUGC.StartItemUpdate(this.myappID, this.publishedfiledid);
				this.excellent = false;
				if (this.formTitle != "my title" && this.formDescr != "my crosshair")
				{
					this.excellent = true;
					SteamUGC.SetItemTitle(this.updateHandle, this.formTitle);
					SteamUGC.SetItemDescription(this.updateHandle, this.formDescr);
					SteamUGC.SetItemUpdateLanguage(this.updateHandle, "en");
					SteamUGC.SetItemMetadata(this.updateHandle, "No New Notes");
					string[] array = new string[] { this.formTAG };
					SteamUGC.SetItemTags(this.updateHandle, array);
					SteamUGC.SetItemVisibility(this.updateHandle, this.formVisibility);
				}
				else
				{
					SteamUGC.SetItemTitle(this.updateHandle, "incompatible");
					SteamUGC.SetItemDescription(this.updateHandle, "item marked as null");
					SteamUGC.SetItemUpdateLanguage(this.updateHandle, "en");
					SteamUGC.SetItemMetadata(this.updateHandle, "warning");
					string[] array2 = new string[] { "Broken" };
					SteamUGC.SetItemTags(this.updateHandle, array2);
					SteamUGC.SetItemVisibility(this.updateHandle, this.formVisibilityPrivate);
				}
				string text = "UploadData";
				if (Directory.Exists(text))
				{
					this.sc.harp2.Play(this.sc.ev, 1f, 0f);
					text = Path.GetFullPath(text);
				}
				SteamUGC.SetItemContent(this.updateHandle, text);
				string text2 = "image.jpg";
				if (File.Exists(text2))
				{
					this.sc.harp2.Play(0.5f, -0.5f, 0f);
					text2 = Path.GetFullPath(text2);
				}
				if (this.excellent)
				{
					SteamUGC.SetItemPreview(this.updateHandle, text2);
				}
				this.submitCreatedItem();
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00137E24 File Offset: 0x00136024
		public void submitCreatedItem()
		{
			if (SteamAPI.IsSteamRunning())
			{
				string text = "No Change notes today.";
				if (!this.excellent)
				{
					text = "warning: item incompatible";
				}
				this.submitItem = CallResult<SubmitItemUpdateResult_t>.Create(new CallResult<SubmitItemUpdateResult_t>.APIDispatchDelegate(this.onSubmitResult));
				this.Handle = SteamUGC.SubmitItemUpdate(this.updateHandle, text);
				this.submitItem.Set(this.Handle, null);
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00137E88 File Offset: 0x00136088
		private void onSubmitResult(SubmitItemUpdateResult_t pCallback, bool fails)
		{
			if (SteamAPI.IsSteamRunning())
			{
				if (pCallback.m_eResult == EResult.k_EResultOK)
				{
					this.status = "DONE";
					this.workstatus = "";
					this.sc.achievepop.Play(this.sc.ev, 0f, 0f);
					return;
				}
				this.status = "FAIL";
				this.workstatus = "";
				MessageBoxScreen2 messageBoxScreen = new MessageBoxScreen2("Submission Failure \n" + pCallback.m_eResult.ToString() + "\n", 16);
				this.sc.AddScreen(messageBoxScreen, null);
			}
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00137F3C File Offset: 0x0013613C
		public void getSubscribedItems()
		{
			this.textureBusy = true;
			this.totalSubscriptions = 0U;
			if (SteamAPI.IsSteamRunning())
			{
				try
				{
					this.totalSubscriptions = SteamUGC.GetNumSubscribedItems();
					if (this.totalSubscriptions > 0U)
					{
						this.published = new PublishedFileId_t[this.totalSubscriptions];
						SteamUGC.GetSubscribedItems(this.published, this.totalSubscriptions);
					}
				}
				catch
				{
				}
			}
			this.textureBusy = false;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00137FB4 File Offset: 0x001361B4
		public void getSubbedTextures()
		{
			this.textureBusy = true;
			if (SteamAPI.IsSteamRunning())
			{
				this.crosshairS.Clear();
				this.subIndex = 0;
				if (this.published != null)
				{
					for (int i = 0; i < this.published.Length; i++)
					{
						try
						{
							ulong num;
							string text;
							uint num2;
							if (SteamUGC.GetItemInstallInfo(this.published[i], out num, out text, 512U, out num2))
							{
								string text2 = "dummyB.png";
								if (File.Exists(text + "\\" + text2))
								{
									try
									{
										using (FileStream fileStream = new FileStream(text + "\\" + text2, FileMode.Open))
										{
											this.crosshairS.Add(Texture2D.FromStream(this.sc.GraphicsDevice, fileStream));
										}
									}
									catch
									{
									}
								}
							}
						}
						catch
						{
						}
					}
				}
			}
			this.textureBusy = false;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x001380C0 File Offset: 0x001362C0
		public void openWEB(string mypage)
		{
			if (SteamAPI.IsSteamRunning())
			{
				try
				{
					SteamFriends.ActivateGameOverlayToWebPage(mypage);
				}
				catch
				{
				}
			}
		}

		// Token: 0x04001602 RID: 5634
		public bool testmode;

		// Token: 0x04001603 RID: 5635
		private bool excellent;

		// Token: 0x04001604 RID: 5636
		public List<Rectangle> entryBox = new List<Rectangle>();

		// Token: 0x04001605 RID: 5637
		public List<Workshop.diaryEntry> entry = new List<Workshop.diaryEntry>();

		// Token: 0x04001606 RID: 5638
		public int entryIndex;

		// Token: 0x04001607 RID: 5639
		public bool showPublishbox;

		// Token: 0x04001608 RID: 5640
		public bool populateNow;

		// Token: 0x04001609 RID: 5641
		public ERemoteStoragePublishedFileVisibility seeFriends = ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityFriendsOnly;

		// Token: 0x0400160A RID: 5642
		public ERemoteStoragePublishedFileVisibility seePublic;

		// Token: 0x0400160B RID: 5643
		public ERemoteStoragePublishedFileVisibility seePrivate = ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPrivate;

		// Token: 0x0400160C RID: 5644
		public ERemoteStoragePublishedFileVisibility formVisibility;

		// Token: 0x0400160D RID: 5645
		public ERemoteStoragePublishedFileVisibility formVisibilityPrivate = ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPrivate;

		// Token: 0x0400160E RID: 5646
		public ERemoteStoragePublishedFileVisibility formPublic;

		// Token: 0x0400160F RID: 5647
		public string changenotes = "The Farmer Says Hello";

		// Token: 0x04001610 RID: 5648
		public string formTitle = "my title";

		// Token: 0x04001611 RID: 5649
		public string formDescr = "my crosshair";

		// Token: 0x04001612 RID: 5650
		public string formMark = "MOD";

		// Token: 0x04001613 RID: 5651
		public string formTAG = "Crosshair";

		// Token: 0x04001614 RID: 5652
		public string[] availableTAGS = new string[] { "Crosshair", "Testing", "Funny", "Fullscreen", "Night", "Daytime", "Boss", "Small", "Random" };

		// Token: 0x04001615 RID: 5653
		public AppId_t myappID = new AppId_t(434570U);

		// Token: 0x04001616 RID: 5654
		private PublishedFileId_t[] published;

		// Token: 0x04001617 RID: 5655
		public uint totalSubscriptions;

		// Token: 0x04001618 RID: 5656
		public string status = "";

		// Token: 0x04001619 RID: 5657
		public string workstatus = "";

		// Token: 0x0400161A RID: 5658
		public SteamAPICall_t Handle;

		// Token: 0x0400161B RID: 5659
		public UGCUpdateHandle_t updateHandle;

		// Token: 0x0400161C RID: 5660
		public PublishedFileId_t publishedfiledid;

		// Token: 0x0400161D RID: 5661
		public string publishedfileFolder = "";

		// Token: 0x0400161E RID: 5662
		public int downloadComplete = 4;

		// Token: 0x0400161F RID: 5663
		public int myTimer = 240;

		// Token: 0x04001620 RID: 5664
		public SteamAPICall_t smallhandle;

		// Token: 0x04001621 RID: 5665
		public bool weSubscribed;

		// Token: 0x04001622 RID: 5666
		public int fileSize = 250;

		// Token: 0x04001623 RID: 5667
		private ScreenManager sc;

		// Token: 0x04001624 RID: 5668
		public bool textureBusy;

		// Token: 0x04001625 RID: 5669
		public int subIndex;

		// Token: 0x04001626 RID: 5670
		public List<Texture2D> crosshairS = new List<Texture2D>();

		// Token: 0x04001627 RID: 5671
		public CallResult<CreateItemResult_t> createItem;

		// Token: 0x04001628 RID: 5672
		public CallResult<UGCUpdateHandle_t> updateItem;

		// Token: 0x04001629 RID: 5673
		public CallResult<SubmitItemUpdateResult_t> submitItem;

		// Token: 0x0400162A RID: 5674
		public CallResult<UGCQueryHandle_t> getItem;

		// Token: 0x0400162B RID: 5675
		private GraphicsDeviceManager gr;

		// Token: 0x02000099 RID: 153
		public class diaryEntry
		{
			// Token: 0x0400162C RID: 5676
			public string path;

			// Token: 0x0400162D RID: 5677
			public string dates;

			// Token: 0x0400162E RID: 5678
			public SoundEffect voice;

			// Token: 0x0400162F RID: 5679
			public List<float> motion;

			// Token: 0x04001630 RID: 5680
			public List<Texture2D> media;

			// Token: 0x04001631 RID: 5681
			public List<SoundEffect> soundhit;
		}
	}
}
