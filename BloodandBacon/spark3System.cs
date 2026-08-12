using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x020000F2 RID: 242
	internal class spark3System : ParticleSystem
	{
		// Token: 0x06000827 RID: 2087 RVA: 0x001DD45C File Offset: 0x001DB65C
		public spark3System(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x001DD468 File Offset: 0x001DB668
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "skully";
			settings.MaxParticles = 4000;
			settings.Duration = TimeSpan.FromSeconds(3.0);
			settings.DurationRandomness = 0.2f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 2f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 2f;
			settings.Gravity = new Vector3(0f, -256f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(108, 108, 220, 200);
			settings.MaxColor = new Color(255, 255, 255, 255);
			settings.MinRotateSpeed = 5f;
			settings.MaxRotateSpeed = 15f;
			settings.MinStartSize = 2.5f;
			settings.MaxStartSize = 6.2f;
			settings.MinEndSize = 2f;
			settings.MaxEndSize = 3f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
