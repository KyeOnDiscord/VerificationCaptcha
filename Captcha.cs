using System;
using System.Drawing;
using System.Linq;
namespace VerificationCaptcha
{
    public class Captcha
    {
        public static string CaptchaAnswer { get; set; }
        public static Image GenerateImage()
        {
            return DrawText(Color.Black, Color.White);
        }
        private static Random random = new Random();
        private static string RandomChar
        {
            get
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                return new string(Enumerable.Repeat(chars, 1).Select(s => s[random.Next(s.Length)]).ToArray());
            }
        }
        private static Image DrawText(Color textColor, Color backColor)
        {
            int charlength = 5;
            //create a new image of the right size
            Image img = new Bitmap(300, 300);
            Graphics drawing = Graphics.FromImage(img);
            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            CaptchaAnswer = string.Empty;
            for (int i = 1; i < charlength + 1; i++)
            {
                string[] fontlist = FontList();
                int index = random.Next(fontlist.Length);
                string randomchar = RandomChar;
                Font font = new Font(fontlist[index], 15F);
                float offset = (i * 15);
                drawing.DrawString(randomchar, font, textBrush, offset, i /2);
                CaptchaAnswer += randomchar;
            }

            drawing.Save();
            textBrush.Dispose();
            drawing.Dispose();
            return img;
        }
        private static string[] FontList()
        {
            string[] fonts = { "Microsoft YaHei UI", "Montserrat Subrayada", "Noto Sans", "Showcard Gothic", "STHupo" };
            return fonts;
        }
    }
}