using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000160 RID: 352
	internal class buddyBlood : ParticleSystem
	{
		// Token: 0x06000CCA RID: 3274 RVA: 0x003767B7 File Offset: 0x003749B7
		public buddyBlood(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x003767C4 File Offset: 0x003749C4
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "blood2";
			settings.MaxParticles = 7000;
			settings.Duration = TimeSpan.FromSeconds(3.0);
			settings.DurationRandomness = 1.2f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 0f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 0f;
			settings.Gravity = new Vector3(0f, -150f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(100, 100, 100, 150);
			settings.MaxColor = new Color(225, 225, 225, 200);
			settings.MinRotateSpeed = 5f;
			settings.MaxRotateSpeed = 17f;
			settings.MinStartSize = 0.6f;
			settings.MaxStartSize = 1.5f;
			settings.MinEndSize = 0.2f;
			settings.MaxEndSize = 0.5f;
			settings.BlendState = BlendState.NonPremultiplied;
		}
	}
}
