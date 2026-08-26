using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sandstone_Launcher
{
    public partial class SkinSelector : Form
    {
        private static class SkinUV
        {
            static public readonly Rectangle HeadRight = new Rectangle(0, 8, 8, 8);
            static public readonly Rectangle Head = new Rectangle(8, 8, 8, 8);
            static public readonly Rectangle HeadLeft = new Rectangle(16, 8, 8, 8);
            static public readonly Rectangle HeadBack = new Rectangle(24, 8, 8, 8);

            static public readonly Rectangle RightLegRight = new Rectangle(0, 20, 4, 12);
            static public readonly Rectangle RightLeg = new Rectangle(4, 20, 4, 12);
            static public readonly Rectangle RightLegLeft = new Rectangle(8, 20, 4, 12);
            static public readonly Rectangle RightLegBack = new Rectangle(12, 20, 4, 12);

            static public readonly Rectangle BodyRight = new Rectangle(16, 20, 4, 12);
            static public readonly Rectangle Body = new Rectangle(20, 20, 8, 12);
            static public readonly Rectangle BodyLeft = new Rectangle(28, 20, 4, 12);
            static public readonly Rectangle BodyBack = new Rectangle(32, 20, 8, 12);

            static public readonly Rectangle RightArmRight = new Rectangle(40, 20, 4, 12);
            static public readonly Rectangle RightArm = new Rectangle(44, 20, 4, 12);
            static public readonly Rectangle RightArmLeft = new Rectangle(48, 20, 4, 12);
            static public readonly Rectangle RightArmBack = new Rectangle(52, 20, 4, 12);

            static public readonly Rectangle LeftLegRight = new Rectangle(16, 52, 4, 12);
            static public readonly Rectangle LeftLeg = new Rectangle(20, 52, 4, 12);
            static public readonly Rectangle LeftLegLeft = new Rectangle(24, 52, 4, 12);
            static public readonly Rectangle LeftLegBack = new Rectangle(28, 52, 4, 12);

            static public readonly Rectangle LeftArmRight = new Rectangle(32, 52, 4, 12);
            static public readonly Rectangle LeftArm = new Rectangle(36, 52, 4, 12);
            static public readonly Rectangle LeftArmLeft = new Rectangle(40, 52, 4, 12);
            static public readonly Rectangle LeftArmBack = new Rectangle(44, 52, 4, 12);

            //Slim
            static public readonly Rectangle RightArmSlimRight = new Rectangle(40, 20, 4, 12);
            static public readonly Rectangle RightArmSlim = new Rectangle(44, 20, 3, 12);
            static public readonly Rectangle RightArmSlimLeft = new Rectangle(47, 20, 4, 12);
            static public readonly Rectangle RightArmSlimBack = new Rectangle(51, 20, 3, 12);

            static public readonly Rectangle LeftArmSlimRight = new Rectangle(32, 52, 4, 12);
            static public readonly Rectangle LeftArmSlim = new Rectangle(36, 52, 3, 12);
            static public readonly Rectangle LeftArmSlimLeft = new Rectangle(39, 52, 4, 12);
            static public readonly Rectangle LeftArmSlimBack = new Rectangle(43, 52, 3, 12);

            //Classic
            static public readonly Rectangle RightLegFlippedRight = new Rectangle(4, 20, -4, 12);
            static public readonly Rectangle RightLegFlipped = new Rectangle(8, 20, -4, 12);
            static public readonly Rectangle RightLegFlippedLeft = new Rectangle(12, 20, -4, 12);
            static public readonly Rectangle RightLegFlippedBack = new Rectangle(16, 20, -4, 12);

            static public readonly Rectangle RightArmFlippedRight = new Rectangle(44, 20, -4, 12);
            static public readonly Rectangle RightArmFlipped = new Rectangle(48, 20, -4, 12);
            static public readonly Rectangle RightArmFlippedLeft = new Rectangle(52, 20, -4, 12);
            static public readonly Rectangle RightArmFlippedBack = new Rectangle(56, 20, -4, 12);

            static public readonly Rectangle HeadLayerRight = new Rectangle(32, 8, 8, 8);
            static public readonly Rectangle HeadLayer = new Rectangle(40, 8, 8, 8);
            static public readonly Rectangle HeadLayerLeft = new Rectangle(48, 8, 8, 8);
            static public readonly Rectangle HeadLayerBack = new Rectangle(56, 8, 8, 8);

            static public readonly Rectangle RightLegLayerRight = new Rectangle(0, 36, 4, 12);
            static public readonly Rectangle RightLegLayer = new Rectangle(4, 36, 4, 12);
            static public readonly Rectangle RightLegLayerLeft = new Rectangle(8, 36, 4, 12);
            static public readonly Rectangle RightLegLayerBack = new Rectangle(12, 36, 4, 12);

            static public readonly Rectangle BodyLayerRight = new Rectangle(16, 36, 4, 12);
            static public readonly Rectangle BodyLayer = new Rectangle(20, 36, 8, 12);
            static public readonly Rectangle BodyLayerLeft = new Rectangle(28, 36, 4, 12);
            static public readonly Rectangle BodyLayerBack = new Rectangle(32, 36, 8, 12);

            static public readonly Rectangle RightArmLayerRight = new Rectangle(40, 36, 4, 12);
            static public readonly Rectangle RightArmLayer = new Rectangle(44, 36, 4, 12);
            static public readonly Rectangle RightArmLayerLeft = new Rectangle(48, 36, 4, 12);
            static public readonly Rectangle RightArmLayerBack = new Rectangle(52, 36, 4, 12);

            static public readonly Rectangle LeftLegLayerRight = new Rectangle(0, 52, 4, 12);
            static public readonly Rectangle LeftLegLayer = new Rectangle(4, 52, 4, 12);
            static public readonly Rectangle LeftLegLayerLeft = new Rectangle(8, 52, 4, 12);
            static public readonly Rectangle LeftLegLayerBack = new Rectangle(12, 52, 4, 12);

            static public readonly Rectangle LeftArmLayerRight = new Rectangle(48, 52, 4, 12);
            static public readonly Rectangle LeftArmLayer = new Rectangle(52, 52, 4, 12);
            static public readonly Rectangle LeftArmLayerLeft = new Rectangle(56, 52, 4, 12);
            static public readonly Rectangle LeftArmLayerBack = new Rectangle(60, 52, 4, 12);

            //Slim
            static public readonly Rectangle RightArmSlimLayerRight = new Rectangle(40, 36, 4, 12);
            static public readonly Rectangle RightArmSlimLayer = new Rectangle(44, 36, 3, 12);
            static public readonly Rectangle RightArmSlimLayerLeft = new Rectangle(47, 36, 4, 12);
            static public readonly Rectangle RightArmSlimLayerBack = new Rectangle(51, 36, 3, 12);

            static public readonly Rectangle LeftArmSlimLayerRight = new Rectangle(48, 52, 4, 12);
            static public readonly Rectangle LeftArmSlimLayer = new Rectangle(52, 52, 3, 12);
            static public readonly Rectangle LeftArmSlimLayerLeft = new Rectangle(55, 52, 4, 12);
            static public readonly Rectangle LeftArmSlimLayerBack = new Rectangle(59, 52, 3, 12);

            //Cape
            static public readonly Rectangle Cape = new Rectangle(1, 1, 10, 16);
            static public readonly Rectangle CapeBack = new Rectangle(12, 1, 10, 16);
            static public readonly Rectangle Elytra = new Rectangle(34, 2, 12, 20);
            static public readonly Rectangle ElytraFlipped = new Rectangle(46, 2, -12, 20);
        }
        private static class SkinDraw
        {
            static public readonly Rectangle Head = new Rectangle(30, 2, 56, 56);
            static public readonly Rectangle Body = new Rectangle(30, 58, 56, 84);
            static public readonly Rectangle RightArm = new Rectangle(2, 58, 28, 84);
            static public readonly Rectangle LeftArm = new Rectangle(86, 58, 28, 84);
            static public readonly Rectangle RightLeg = new Rectangle(30, 142, 28, 84);
            static public readonly Rectangle LeftLeg = new Rectangle(58, 142, 28, 84);

            static public readonly Rectangle RightArmSlim = new Rectangle(9, 58, 21, 84);
            static public readonly Rectangle LeftArmSlim = new Rectangle(86, 58, 21, 84);

            static public readonly Rectangle HeadLayer = new Rectangle(28, 0, 60, 60);
            static public readonly Rectangle BodyLayer = new Rectangle(28, 56, 60, 88);
            static public readonly Rectangle RightArmLayer = new Rectangle(0, 56, 32, 88);
            static public readonly Rectangle LeftArmLayer = new Rectangle(84, 56, 32, 88);
            static public readonly Rectangle RightLegLayer = new Rectangle(28, 140, 32, 88);
            static public readonly Rectangle LeftLegLayer = new Rectangle(56, 140, 32, 88);

            static public readonly Rectangle RightArmSlimLayer = new Rectangle(7, 56, 25, 88);
            static public readonly Rectangle LeftArmSlimLayer = new Rectangle(84, 56, 25, 88);

            static public readonly Rectangle HeadSide = new Rectangle(122, 2, 56, 56);
            static public readonly Rectangle ArmSide = new Rectangle(136, 58, 28, 84);
            static public readonly Rectangle LegSide = new Rectangle(136, 142, 28, 84);

            static public readonly Rectangle HeadLayerSide = new Rectangle(120, 0, 60, 60);
            static public readonly Rectangle ArmLayerSide = new Rectangle(134, 56, 32, 88);
            static public readonly Rectangle LegLayerSide = new Rectangle(134, 140, 32, 88);

            static public readonly Rectangle Cape = new Rectangle(5, 10, 50, 80);
            static public readonly Rectangle CapeBack = new Rectangle(60, 10, 50, 80);
            static public readonly Rectangle Elytra = new Rectangle(0, 0, 60, 100);
            static public readonly Rectangle ElytraBack = new Rectangle(55, 0, 60, 100);
        }

        Bitmap SkinPreviewFront = null;
        Bitmap SkinPreviewBack = null;
        Bitmap CapePreview = null;
        Bitmap ElytraPreview = null;
        public SkinSelector()
        {
            InitializeComponent();
        }

        void SetSkinPreview(Image Skin)
        {
            SkinPreviewFront?.Dispose();
            SkinPreviewBack?.Dispose();

            SkinPreviewFront = GetPreviewShot(Skin);
            SkinPreviewBack = GetPreviewShot(Skin, Flipped: true);
            SkinPreview.Image = SkinPreviewFront;
        }

        void SetCapePreview(Image Cape)
        {
            CapePreview?.Dispose();
            ElytraPreview?.Dispose();

            CapePreview = GetCapeShot(Cape);
            ElytraPreview = GetCapeShot(Cape, true);

            ElyCapePreview.Image = GetCapeShot(Cape);
        }

        //static Rectangle GetRectForOtherRes(Size ImageSize, Rectangle NormalRect)
        //{
        //    return new Rectangle(NormalRect.X / 64 * ImageSize.Width, NormalRect.Y / 64 * ImageSize.Height, NormalRect.Width / 64 * ImageSize.Width, NormalRect.Height / 64 * ImageSize.Height);
        //}

        //static Bitmap GetCapePreview(Image Cape, bool Elytra = false)
        //{
            
        //}

        static Bitmap GetPreviewShot(Image Skin, bool Slim = false, bool Classic = false, bool Flipped = false) //, bool Head = true, bool RightArm = true, bool Body = true, bool LeftArm = true, bool RightLeg = true, bool LeftLeg = true
        {
            Bitmap Image = new Bitmap(180, 228);
            using (Graphics g = Graphics.FromImage(Image))
            {
                bool IsClassic = Skin.Height == 32 || Classic;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                //Arms
                var ArmRight = Flipped ? SkinUV.LeftArmBack : SkinUV.RightArm;
                var ArmLeft = Flipped ? SkinUV.RightArmBack : SkinUV.LeftArm;
                var ArmRightLayer = Flipped ? SkinUV.LeftArmLayerBack : SkinUV.RightArmLayer;
                var ArmLeftLayer = Flipped ? SkinUV.RightArmLayerBack : SkinUV.LeftArmLayer;

                //Legs
                var LegRight = Flipped ? SkinUV.LeftLegBack : SkinUV.RightLeg;
                var LegLeft = Flipped ? SkinUV.RightLegBack : SkinUV.LeftLeg;

                //Rects
                var RightArmRect = SkinDraw.RightArm;
                var LeftArmRect = SkinDraw.LeftArm;
                var RightArmLayerRect = SkinDraw.RightArmLayer;
                var LeftArmLayerRect = SkinDraw.LeftArmLayer;

                //Side
                var SideArm = Flipped ? SkinUV.LeftArmLeft : SkinUV.RightArmRight;
                var SideArmLayer = Flipped ? SkinUV.LeftArmLayerLeft : SkinUV.RightArmLayerRight;

                if (!IsClassic && Slim)
                {
                    ArmRight = Flipped ? SkinUV.LeftArmSlimBack : SkinUV.RightArmSlim;
                    ArmLeft = Flipped ? SkinUV.RightArmSlimBack : SkinUV.LeftArmSlim;
                    ArmRightLayer = Flipped ? SkinUV.LeftArmSlimLayerBack : SkinUV.RightArmSlimLayer;
                    ArmLeftLayer = Flipped ? SkinUV.RightArmSlimLayerBack : SkinUV.LeftArmSlimLayer;
                    RightArmRect = SkinDraw.RightArmSlim;
                    LeftArmRect = SkinDraw.LeftArmSlim;
                    RightArmLayerRect = SkinDraw.RightArmSlimLayer;
                    LeftArmLayerRect = SkinDraw.LeftArmSlimLayer;

                    SideArm = Flipped ? SkinUV.LeftArmSlimLeft : SkinUV.RightArmSlimRight;
                    SideArmLayer = Flipped ? SkinUV.LeftArmSlimLayerLeft : SkinUV.RightArmSlimLayerRight;
                }
                else if (IsClassic)
                {
                    ArmRight = Flipped ? SkinUV.RightArmFlippedBack : SkinUV.RightArm;
                    ArmLeft = Flipped ? SkinUV.RightArmBack : SkinUV.RightArmFlipped;
                    LegRight = Flipped? SkinUV.RightLegFlippedBack: SkinUV.RightLeg;
                    LegLeft = Flipped ? SkinUV.RightLegBack : SkinUV.RightLegFlipped;
                }

                //Main
                g.DrawImage(Skin, SkinDraw.Body, Flipped ? SkinUV.BodyBack : SkinUV.Body, GraphicsUnit.Pixel);
                g.DrawImage(Skin, RightArmRect, ArmRight, GraphicsUnit.Pixel);
                g.DrawImage(Skin, LeftArmRect, ArmLeft, GraphicsUnit.Pixel);
                g.DrawImage(Skin, SkinDraw.RightLeg, LegRight, GraphicsUnit.Pixel);
                g.DrawImage(Skin, SkinDraw.LeftLeg, LegLeft, GraphicsUnit.Pixel);

                if (!IsClassic)
                {
                    g.DrawImage(Skin, SkinDraw.BodyLayer, Flipped ? SkinUV.BodyLayerBack : SkinUV.BodyLayer, GraphicsUnit.Pixel);
                    g.DrawImage(Skin, RightArmLayerRect, ArmRightLayer, GraphicsUnit.Pixel);
                    g.DrawImage(Skin, LeftArmLayerRect, ArmLeftLayer, GraphicsUnit.Pixel);
                    g.DrawImage(Skin, SkinDraw.RightLegLayer, Flipped ? SkinUV.LeftLegLayerBack : SkinUV.RightLegLayer, GraphicsUnit.Pixel);
                    g.DrawImage(Skin, SkinDraw.LeftLegLayer, Flipped ? SkinUV.RightLegLayerBack : SkinUV.LeftLegLayer, GraphicsUnit.Pixel);
                }

                g.DrawImage(Skin, SkinDraw.Head, Flipped ? SkinUV.HeadBack : SkinUV.Head, GraphicsUnit.Pixel);
                g.DrawImage(Skin, SkinDraw.HeadLayer, Flipped ? SkinUV.HeadLayerBack : SkinUV.HeadLayer, GraphicsUnit.Pixel);

                g.DrawImage(Skin, SkinDraw.ArmSide, SideArm, GraphicsUnit.Pixel);
                g.DrawImage(Skin, SkinDraw.LegSide, Flipped ? SkinUV.LeftLegLeft : SkinUV.RightLegRight, GraphicsUnit.Pixel);

                if (!IsClassic)
                {
                    g.DrawImage(Skin, SkinDraw.ArmLayerSide, SideArmLayer, GraphicsUnit.Pixel);
                    g.DrawImage(Skin, SkinDraw.LegLayerSide, Flipped ? SkinUV.LeftLegLayerLeft : SkinUV.RightLegLayerRight, GraphicsUnit.Pixel);
                }
                g.DrawImage(Skin, SkinDraw.HeadSide, Flipped ? SkinUV.HeadLeft : SkinUV.HeadRight, GraphicsUnit.Pixel);
                g.DrawImage(Skin, SkinDraw.HeadLayerSide, Flipped ? SkinUV.HeadLayerLeft : SkinUV.HeadLayerRight, GraphicsUnit.Pixel);
            }
            return Image;
        }

        static Bitmap GetCapeShot(Image Cape, bool Elytra = false)
        {
            Bitmap Image = new Bitmap(115, 100);
            using (Graphics g = Graphics.FromImage(Image))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                if (Elytra)
                {
                    g.DrawImage(Cape, SkinDraw.Elytra, SkinUV.ElytraFlipped, GraphicsUnit.Pixel);
                    g.DrawImage(Cape, SkinDraw.ElytraBack, SkinUV.Elytra, GraphicsUnit.Pixel);
                }
                else
                {
                    g.DrawImage(Cape, SkinDraw.Cape, SkinUV.Cape, GraphicsUnit.Pixel);
                    g.DrawImage(Cape, SkinDraw.CapeBack, SkinUV.CapeBack, GraphicsUnit.Pixel);
                }
            }
            return Image;
        }

        static Bitmap GetHeadShot(Image Skin)
        {
            Bitmap Image = new Bitmap(64, 64);
            using (Graphics g = Graphics.FromImage(Image))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                g.DrawImage(Skin, new Rectangle(2, 2, 60, 60), SkinUV.Head, GraphicsUnit.Pixel);
                g.DrawImage(Skin, new Rectangle(0, 0, 64, 64), SkinUV.HeadLayer, GraphicsUnit.Pixel);
            }
            return Image;
        }

        private void SkinPreview_MouseEnter(object sender, EventArgs e) => SkinPreview.Image = SkinPreviewBack;

        private void SkinPreview_MouseLeave(object sender, EventArgs e) => SkinPreview.Image = SkinPreviewFront;

        private void ElyCapePreview_MouseEnter(object sender, EventArgs e) => ElyCapePreview.Image = ElytraPreview;

        private void ElyCapePreview_MouseLeave(object sender, EventArgs e) => ElyCapePreview.Image = CapePreview;
    }
}
