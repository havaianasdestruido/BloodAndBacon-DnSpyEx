using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x020000A6 RID: 166
	internal class bitSystem : ParticleSystem
	{
		// Token: 0x060005F7 RID: 1527 RVA: 0x00142CC9 File Offset: 0x00140EC9
		public bitSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00142CD4 File Offset: 0x00140ED4
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "bit1";
			settings.MaxParticles = 5000;
			settings.Duration = TimeSpan.FromSeconds(1.2);
			settings.DurationRandomness = 0.23f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 0f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 0f;
			settings.Gravity = new Vector3(0f, -110f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(255, 255, 200, 215);
			settings.MaxColor = new Color(255, 255, 255, 255);
			settings.MinRotateSpeed = -8f;
			settings.MaxRotateSpeed = 10f;
			settings.MinStartSize = 1.2f;
			settings.MaxStartSize = 5.2f;
			settings.MinEndSize = 0.1f;
			settings.MaxEndSize = 0.3f;
			settings.BlendState = BlendState.NonPremultiplied;
		}
	}
}
