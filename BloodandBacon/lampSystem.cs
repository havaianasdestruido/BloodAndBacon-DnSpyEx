using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x020000A4 RID: 164
	internal class lampSystem : ParticleSystem
	{
		// Token: 0x060005ED RID: 1517 RVA: 0x0014221F File Offset: 0x0014041F
		public lampSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0014222C File Offset: 0x0014042C
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "lamp";
			settings.MaxParticles = 30;
			settings.Duration = TimeSpan.FromSeconds(0.017000000923871994);
			settings.DurationRandomness = 0f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 2f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 2f;
			settings.Gravity = new Vector3(0f, 0f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(190, 190, 190, 255);
			settings.MaxColor = new Color(220, 220, 220, 255);
			settings.MinRotateSpeed = 0.4f;
			settings.MaxRotateSpeed = 0.5f;
			settings.MinStartSize = 100f;
			settings.MaxStartSize = 100f;
			settings.MinEndSize = 100f;
			settings.MaxEndSize = 100f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
