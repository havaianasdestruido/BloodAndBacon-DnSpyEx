using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000076 RID: 118
	internal class farmerSpit : ParticleSystem
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x000E6771 File Offset: 0x000E4971
		public farmerSpit(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000E677C File Offset: 0x000E497C
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "spit";
			settings.MaxParticles = 1000;
			settings.Duration = TimeSpan.FromSeconds(2.0);
			settings.DurationRandomness = 1.2f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 0f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 0f;
			settings.Gravity = new Vector3(0f, -40f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(120, 100, 100, 200);
			settings.MaxColor = new Color(245, 245, 225, 200);
			settings.MinRotateSpeed = 5f;
			settings.MaxRotateSpeed = 17f;
			settings.MinStartSize = 0.1f;
			settings.MaxStartSize = 0.7f;
			settings.MinEndSize = 0.1f;
			settings.MaxEndSize = 0.1f;
			settings.BlendState = BlendState.NonPremultiplied;
		}
	}
}
