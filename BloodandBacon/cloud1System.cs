using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x020000B6 RID: 182
	internal class cloud1System : ParticleSystem
	{
		// Token: 0x06000632 RID: 1586 RVA: 0x00148E74 File Offset: 0x00147074
		public cloud1System(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00148E80 File Offset: 0x00147080
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "tracer";
			settings.MaxParticles = 39000;
			settings.Duration = TimeSpan.FromSeconds(25.0);
			settings.DurationRandomness = 0f;
			settings.EmitterVelocitySensitivity = 3f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 0f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 0f;
			settings.Gravity = new Vector3(0f, 0f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(119, 119, 119, 119);
			settings.MaxColor = new Color(119, 119, 119, 119);
			settings.MinRotateSpeed = 0f;
			settings.MaxRotateSpeed = 0f;
			settings.MinStartSize = 15f;
			settings.MaxStartSize = 15f;
			settings.MinEndSize = 30f;
			settings.MaxEndSize = 30f;
			settings.BlendState = BlendState.NonPremultiplied;
		}
	}
}
