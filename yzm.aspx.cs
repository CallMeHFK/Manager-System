﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
public partial class yzm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string yzm = CreateRandomCode();
        Session["yzmid"] = yzm;
        CreateCheckCodeImage(yzm);
    }
    private string CreateRandomCode()
    {
        int number;
        char code;
        string checkCode = string.Empty;
        Random radom = new Random();
        for (int i = 0; i < 6; i++)//产生一组随机数
        {
            number = radom.Next();
            if (number % 2 == 0)
            {
                code = (char)('0' + (char)(number % 10));
            }
            else
            {
                code = (char)('A' + (char)(number % 26));
            }
            checkCode += code.ToString();
        }
        //Response.Cookies.Add(new HttpCookie("CheckCode", checkCode));
        return checkCode;
    }
   
     private void CreateCheckCodeImage(string checkCode)//根据随机数产生随机图片
    {
        if (checkCode == null || checkCode.Trim() == String.Empty)
        {
            return;
        }
        System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling(checkCode.Length * 12.5), 22);
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
        try
        {
            //生成随机生成器
            Random random = new Random();
            //清空图片背景色
            g.Clear(Color.White);
            //画图片的背景噪音线
            for (int i = 0; i < 40; i++)
            {
                int x1 = random.Next(image.Width);
                int x2 = random.Next(image.Width);
                int y1 = random.Next(image.Height);
                int y2 = random.Next(image.Height);
                g.DrawLine(new Pen(Color.Red), x1, y1, x2, y2);
            }
            Font font = new System.Drawing.Font("Verdana", 12, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
            System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
            g.DrawString(checkCode, font, brush, 2, 2);
            //画图片的前景噪音点
            for (int i = 0; i < 40; i++)
            {
                int x = random.Next(image.Width);
                int y = random.Next(image.Height);
                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }
            //画图片的边框线
            g.DrawRectangle(new Pen(Color.Red), 0, 0, image.Width - 1, image.Height - 1);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            Response.ClearContent();
            Response.ContentType = "image/Gif";
            Response.BinaryWrite(ms.ToArray());
        }
        finally
        {
            g.Dispose();
            image.Dispose();
        }
    }

    }
    
