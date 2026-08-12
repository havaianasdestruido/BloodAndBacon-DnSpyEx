using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Blood
{
	// Token: 0x0200006C RID: 108
	public class SoundEffects
	{
		// Token: 0x060003DB RID: 987 RVA: 0x000E3531 File Offset: 0x000E1731
		public SoundEffects(int size)
		{
			SoundEffects.total += size;
			this.sound = new SoundEffectInstance[size];
			this.canReplay = false;
			this.missed.Capacity = 0;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x000E356F File Offset: 0x000E176F
		public SoundEffects(int size, bool canReplay)
		{
			SoundEffects.total += size;
			this.sound = new SoundEffectInstance[size];
			this.canReplay = canReplay;
			this.missed.Capacity = 5;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x000E35B0 File Offset: 0x000E17B0
		public void Play(float vol, float pitch, float pan)
		{
			this.count++;
			if (this.count > this.sound.Length - 1)
			{
				this.count = 0;
			}
			if (this.sound[this.count].State == SoundState.Stopped)
			{
				this.sound[this.count].Volume = vol;
				this.sound[this.count].Pitch = pitch;
				this.sound[this.count].Pan = pan;
				this.sound[this.count].Play();
				return;
			}
			this.sound[this.count].Stop();
			if (this.sound[this.count].State == SoundState.Stopped)
			{
				this.sound[this.count].Play();
				return;
			}
			if (this.canReplay && this.missed.Count < 5)
			{
				this.missed.Add(this.count);
				SoundEffects.totalReplay++;
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x000E36B4 File Offset: 0x000E18B4
		public void rePlay()
		{
			if (this.missed.Count > 0)
			{
				if (this.sound[this.missed[0]].State == SoundState.Stopped)
				{
					this.sound[this.missed[0]].Play();
					this.missed.RemoveAt(0);
					SoundEffects.totalReplay--;
					return;
				}
				this.sound[this.missed[0]].Stop();
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000E3734 File Offset: 0x000E1934
		public void ramp(float vol, float rampy)
		{
			if (this.sound[0].State == SoundState.Stopped || this.sound[0].State == SoundState.Paused)
			{
				this.sound[0].Play();
			}
			if (this.sound[0].State == SoundState.Playing)
			{
				this.sound[0].Volume = vol * MathHelper.Lerp(0f, 1f, rampy);
				this.sound[0].Pitch = MathHelper.Lerp(-1f, 0f, rampy);
			}
		}

		// Token: 0x0400100F RID: 4111
		public static int totalReplay;

		// Token: 0x04001010 RID: 4112
		public int count;

		// Token: 0x04001011 RID: 4113
		public static int total;

		// Token: 0x04001012 RID: 4114
		public SoundEffectInstance[] sound;

		// Token: 0x04001013 RID: 4115
		public List<int> missed = new List<int>();

		// Token: 0x04001014 RID: 4116
		public bool canReplay;
	}
}
