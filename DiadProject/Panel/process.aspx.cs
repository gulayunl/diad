using Newtonsoft.Json.Linq;
using NReco.PdfGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DiadProject.panel
{
    public partial class process : System.Web.UI.Page
    {
        dbLibrary db = new dbLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["Diad"] != null)
            {
                if (Request["type"] == "hpslideimage")
                {
                    string imgUrl = Request["imgUrl"];
                    int imgInitW = Convert.ToInt32(Request["imgInitW"].ToString().Split('.')[0]);
                    int imgInitH = Convert.ToInt32(Request["imgInitH"].ToString().Split('.')[0]);

                    int imgW = Convert.ToInt32(Request["imgW"].ToString().Split('.')[0]);
                    int imgH = Convert.ToInt32(Request["imgH"].ToString().Split('.')[0]);

                    int imgy1 = Convert.ToInt32(Request["imgY1"].ToString().Split('.')[0]);
                    int imgx1 = Convert.ToInt32(Request["imgX1"].ToString().Split('.')[0]);

                    int cropw = Convert.ToInt32(Request["cropW"].ToString().Split('.')[0]);
                    int croph = Convert.ToInt32(Request["cropH"].ToString().Split('.')[0]);

                    float angle = float.Parse(Request["rotation"]);
                    string quality = "100";
                    string extension = "";
                    string filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

                    byte[] bytes = Convert.FromBase64String(imgUrl.Split(',')[1]);
                    System.Drawing.Image image;
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        image = System.Drawing.Image.FromStream(ms);
                        if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                        { extension = ".jpg"; }
                        else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                        { extension = ".png"; }
                        else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
                        { extension = ".gif"; }
                        else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                        { extension = ".bmp"; }

                        image.Save(Server.MapPath("/storage/files/homepageslider/") + "org-" + filename + extension);
                        System.Drawing.Image resizeimage = db.RezizeImage(image, imgW, imgH);
                        resizeimage.Save(Server.MapPath("/storage/files/homepageslider/") + "resize-" + filename + extension);
                    }

                    System.Drawing.Image orgImg = System.Drawing.Image.FromFile(Server.MapPath("/storage/files/homepageslider/") + "resize-" + filename + extension);
                    Rectangle CropArea = new Rectangle(
                        Convert.ToInt32(imgx1),
                        Convert.ToInt32(imgy1),
                        Convert.ToInt32(cropw),
                        Convert.ToInt32(croph));
                    try
                    {
                        Bitmap bitMap = new Bitmap(CropArea.Width, CropArea.Height);
                        using (Graphics g = Graphics.FromImage(bitMap))
                        {
                            g.DrawImage(orgImg, new Rectangle(0, 0, bitMap.Width, bitMap.Height), CropArea, GraphicsUnit.Pixel);
                        }
                        bitMap.Save(Server.MapPath("/storage/files/homepageslider/") + "crop-" + filename + extension);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                    FileInfo fi = new FileInfo(Server.MapPath("/storage/files/campaignimages/org-" + filename + extension));
                    fi.Delete();

                    var jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string json2 = jsonSerializer.Serialize(new successmsg
                    {
                        status = "success",
                        url = "/storage/files/homepageslider/crop-" + filename + extension
                    });
                    Response.Clear();
                    Response.ContentType = "application/json; charset=utf-8";
                    Response.Write(json2);
                    Response.End();
                }
                if (Request["type"] == "pageimage")
                {
                    string imgUrl = Request["imgUrl"];
                    int imgInitW = Convert.ToInt32(Request["imgInitW"].ToString().Split('.')[0]);
                    int imgInitH = Convert.ToInt32(Request["imgInitH"].ToString().Split('.')[0]);
                    //int imgW = 1000;
                    //int imgH = 370;

                    int imgW = Convert.ToInt32(Request["imgW"].ToString().Split('.')[0]);
                    int imgH = Convert.ToInt32(Request["imgH"].ToString().Split('.')[0]);

                    int imgy1 = Convert.ToInt32(Request["imgY1"].ToString().Split('.')[0]);
                    int imgx1 = Convert.ToInt32(Request["imgX1"].ToString().Split('.')[0]);

                    int cropw = Convert.ToInt32(Request["cropW"].ToString().Split('.')[0]);
                    int croph = Convert.ToInt32(Request["cropH"].ToString().Split('.')[0]);

                    float angle = float.Parse(Request["rotation"]);
                    string quality = "100";
                    string extension = "";
                    string filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

                    byte[] bytes = Convert.FromBase64String(imgUrl.Split(',')[1]);
                    System.Drawing.Image image;
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        image = System.Drawing.Image.FromStream(ms);
                        if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                        { extension = ".jpg"; }
                        else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                        { extension = ".png"; }
                        else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
                        { extension = ".gif"; }
                        else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                        { extension = ".bmp"; }

                        image.Save(Server.MapPath("/storage/files/pageimages/") + "org-" + filename + extension);
                        System.Drawing.Image resizeimage = db.RezizeImage(image, imgW, imgH);
                        resizeimage.Save(Server.MapPath("/storage/files/pageimages/") + "resize-" + filename + extension);
                    }

                    System.Drawing.Image orgImg = System.Drawing.Image.FromFile(Server.MapPath("/storage/files/pageimages/") + "resize-" + filename + extension);
                    Rectangle CropArea = new Rectangle(
                        Convert.ToInt32(imgx1),
                        Convert.ToInt32(imgy1),
                        Convert.ToInt32(cropw),
                        Convert.ToInt32(croph));
                    try
                    {
                        Bitmap bitMap = new Bitmap(CropArea.Width, CropArea.Height);
                        using (Graphics g = Graphics.FromImage(bitMap))
                        {
                            g.DrawImage(orgImg, new Rectangle(0, 0, bitMap.Width, bitMap.Height), CropArea, GraphicsUnit.Pixel);
                        }
                        bitMap.Save(Server.MapPath("/storage/files/pageimages/") + "crop-" + filename + extension);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                    FileInfo fi = new FileInfo(Server.MapPath("/storage/files/campaignimages/org-" + filename + extension));
                    fi.Delete();

                    var jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string json2 = jsonSerializer.Serialize(new successmsg
                    {
                        status = "success",
                        url = "/storage/files/pageimages/crop-" + filename + extension
                    });
                    Response.Clear();
                    Response.ContentType = "application/json; charset=utf-8";
                    Response.Write(json2);
                    Response.End();
                }
                if (Request["type"] == "mobileappimage")
                {
                    string imgUrl = Request["imgUrl"];
                    Response.Write("<script>console.log('" + Request["imgInitW"].ToString() + "');</script>");
                    Response.Write("<script>console.log('" + Request["imgInitH"].ToString() + "');</script>");
                    int imgInitW = Convert.ToInt32(Request["imgInitW"].ToString().Split('.')[0]);
                    int imgInitH = Convert.ToInt32(Request["imgInitH"].ToString().Split('.')[0]);

                    int imgW = Convert.ToInt32(Request["imgW"].ToString().Split('.')[0]);
                    int imgH = Convert.ToInt32(Request["imgH"].ToString().Split('.')[0]);

                    int imgy1 = Convert.ToInt32(Request["imgY1"].ToString().Split('.')[0]);
                    int imgx1 = Convert.ToInt32(Request["imgX1"].ToString().Split('.')[0]);

                    int cropw = Convert.ToInt32(Request["cropW"].ToString().Split('.')[0]);
                    int croph = Convert.ToInt32(Request["cropH"].ToString().Split('.')[0]);

                    float angle = float.Parse(Request["rotation"]);
                    string quality = "100";
                    string extension = "";
                    string filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

                    byte[] bytes = Convert.FromBase64String(imgUrl.Split(',')[1]);
                    System.Drawing.Image image;
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        image = System.Drawing.Image.FromStream(ms);
                        if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                        { extension = ".jpg"; }
                        else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                        { extension = ".png"; }
                        else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
                        { extension = ".gif"; }
                        else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                        { extension = ".bmp"; }

                        image.Save(Server.MapPath("/storage/files/mobile/") + "org-" + filename + extension);
                        System.Drawing.Image resizeimage = db.RezizeImage(image, imgW, imgH);
                        resizeimage.Save(Server.MapPath("/storage/files/mobile/") + "resize-" + filename + extension);
                    }

                    System.Drawing.Image orgImg = System.Drawing.Image.FromFile(Server.MapPath("/storage/files/mobile/") + "resize-" + filename + extension);
                    Rectangle CropArea = new Rectangle(
                        Convert.ToInt32(imgx1),
                        Convert.ToInt32(imgy1),
                        Convert.ToInt32(cropw),
                        Convert.ToInt32(croph));
                    try
                    {
                        Bitmap bitMap = new Bitmap(CropArea.Width, CropArea.Height);
                        using (Graphics g = Graphics.FromImage(bitMap))
                        {
                            g.DrawImage(orgImg, new Rectangle(0, 0, bitMap.Width, bitMap.Height), CropArea, GraphicsUnit.Pixel);
                        }
                        bitMap.Save(Server.MapPath("/storage/files/mobile/") + "crop-" + filename + extension);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                    FileInfo fi = new FileInfo(Server.MapPath("/storage/files/mobile/org-" + filename + extension));
                    fi.Delete();

                    var jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string json2 = jsonSerializer.Serialize(new successmsg
                    {
                        status = "success",
                        url = "/storage/files/mobile/crop-" + filename + extension
                    });
                    Response.Clear();
                    Response.ContentType = "application/json; charset=utf-8";
                    Response.Write(json2);
                    Response.End();
                }
                if (Request["type"] == "addhpslide")
                {
                    string anaslogan = "", altslogan = "", btnfaiz = "", btntext = "", slideresmi = "", yazirengi = "", link = "", yonlendirmeturu = "";
                    anaslogan = Request["anaslogan"].ToString();
                    altslogan = Request["altslogan"].ToString();
                    slideresmi = "storage/files/homepageslider/" + Regex.Split(Request["slideresmi"].ToString(), "storage/files/homepageslider/")[1];
                    btnfaiz = Request["butonfaiz"].ToString();
                    btntext = Request["butonslogan"].ToString();
                    yazirengi = Request["yazirengi"].ToString();
                    link = Request["link"].ToString();
                    yonlendirmeturu = Request["yonlendirmeturu"].ToString();

                    if (link.Length > 4)
                    {
                        if (link.StartsWith("http") == false)
                        {
                            link = "http://" + link;
                        }
                    }
                    else
                    {
                        link = "#";
                    }

                    string slideid = Guid.NewGuid().ToString();
                    string result = db.insertValue("tbl_homepageslider", "HS_Id,HS_ImageUrl,HS_Text1,HS_Text2,HS_BtnText1,HS_BtnText2,HS_AddedDateTime,HS_AddedAdmin,HS_Status,HS_TextColor,HS_Link,HS_RedirectType", slideid + "~" + db.clr(slideresmi) + "~" + db.clr(anaslogan) + "~" + altslogan + "~" + btnfaiz + "~" + btntext + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1" + "~" + yazirengi + "~" + link + "~" + yonlendirmeturu, "ConString");

                    string usaid = Guid.NewGuid().ToString();

                    db.insertValue("tbl_users_action_logs", "usa_log_id,usa_action_type,usa_page_type,usa_operation_date,usa_usr_user_id,usa_status", usaid + "~" + "Insert / Veri Ekleme" + "~" + "Yeni slide resmi eklendi." + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1", "ConString");

                    if (result.Contains("true") == true)
                    {
                        Response.Write("true");
                        return;
                    }
                    else
                    {
                        Response.Write("false");
                        return;
                    }
                }
                if (Request["type"] == "edithpslide")
                {
                    string anaslogan = "", altslogan = "", btnfaiz = "", btntext = "", slideresmi = "", slideid = "", yazirengi = "", link = "", yonlendirmeturu = "";
                    anaslogan = Request["anaslogan"].ToString();
                    altslogan = Request["altslogan"].ToString();
                    slideid = Request["slideid"].ToString();
                    btnfaiz = Request["butonfaiz"].ToString();
                    btntext = Request["butonslogan"].ToString();
                    yazirengi = Request["yazirengi"].ToString();
                    link = Request["link"].ToString();
                    yonlendirmeturu = Request["yonlendirmeturu"].ToString();

                    try
                    {
                        slideresmi = "storage/files/homepageslider/" + Regex.Split(Request["slideresmi"].ToString(), "storage/files/homepageslider/")[1];
                    }
                    catch (Exception)
                    {
                        slideresmi = "";
                    }

                    if (link.Length > 4)
                    {
                        if (link.StartsWith("http") == false)
                        {
                            link = "http://" + link;
                        }
                    }
                    else
                    {
                        link = "#";
                    }


                    string result = db.updateValueWhere1("tbl_homepageslider", "HS_Text1,HS_Text2,HS_BtnText1,HS_BtnText2,HS_AddedDateTime,HS_AddedAdmin,HS_Status,HS_TextColor,HS_Link,HS_RedirectType", db.clr(anaslogan) + "~" + altslogan + "~" + btnfaiz + "~" + btntext + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1" + "~" + yazirengi + "~" + link + "~" + yonlendirmeturu, "HS_Id", "=", slideid, "ConString");

                    if (slideresmi != "")
                    {
                        db.updateValueWhere1("tbl_homepageslider", "HS_ImageUrl", slideresmi, "HS_ID", "=", slideid, "ConString");
                    }

                    string usaid = Guid.NewGuid().ToString();
                    db.insertValue("tbl_users_action_logs", "usa_log_id,usa_action_type,usa_page_type,usa_operation_date,usa_usr_user_id,usa_status", usaid + "~" + "Edit / Veri Düzenleme" + "~" + "Anasayfa slide resmi düzenlendi." + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1", "ConString");

                    if (result.Contains("true") == true)
                    {
                        Response.Write("true");
                        return;
                    }
                    else
                    {
                        Response.Write("false");
                        return;
                    }
                }
                if (Request["type"] == "addpage")
                {
                    string sayfaadi = "", sayfabasligi = "", aciklama = "", detay = "", baglioldugusayfa = "", sayfaresmi = "", sayfatipi = "";
                    sayfaadi = Request["sayfaadi"].ToString();
                    sayfabasligi = Request["sayfabasligi"].ToString();
                    sayfaresmi = "storage/files/pageimages/" + Regex.Split(Request["sayfaresmi"].ToString(), "storage/files/pageimages/")[1];
                    aciklama = Request["aciklama"].ToString();
                    detay = Request["detay"].ToString();
                    baglioldugusayfa = Request["baglioldugusayfa"].ToString();
                    sayfatipi = Request["sayfatipi"].ToString();

                    string sayfaid = Guid.NewGuid().ToString();
                    string result = "";
                    if (baglioldugusayfa == "Bağımsız Sayfa")
                    {
                        result = db.insertValue("tbl_pages", "PG_Id,PG_PageName,PG_Title,PG_Description,PG_Content,PG_ImageUrl,PG_LastUpdateDate,PG_LastUpdateAdmin,PG_Status,PG_SeoUrl,PG_PageType", sayfaid + "~" + db.clr(sayfaadi) + "~" + db.clr(sayfabasligi) + "~" + aciklama + "~" + detay + "~" + sayfaresmi + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1" + "~" + db.SeoURL(sayfaadi) + "~" + sayfatipi, "ConString");
                    }
                    else
                    {
                        result = db.insertValue("tbl_pages", "PG_Id,PG_MainPageId,PG_PageName,PG_Title,PG_Description,PG_Content,PG_ImageUrl,PG_LastUpdateDate,PG_LastUpdateAdmin,PG_Status,PG_SeoUrl,PG_PageType", sayfaid + "~" + baglioldugusayfa + "~" + db.clr(sayfaadi) + "~" + db.clr(sayfabasligi) + "~" + aciklama + "~" + detay + "~" + sayfaresmi + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1" + "~" + db.SeoURL(sayfaadi) + "~" + sayfatipi, "ConString");
                    }

                    string usaid = Guid.NewGuid().ToString();

                    db.insertValue("tbl_users_action_logs", "usa_log_id,usa_action_type,usa_page_type,usa_operation_date,usa_usr_user_id,usa_status", usaid + "~" + "Insert / Veri Ekleme" + "~" + sayfaadi + " adında yeni bir sayfa eklendi." + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1", "ConString");

                    if (result.Contains("true") == true)
                    {
                        Response.Write("true");
                        return;
                    }
                    else
                    {
                        Response.Write("false");
                        return;
                    }
                }
                if (Request["type"] == "editpage")
                {
                    string sayfaadi = "", sayfabasligi = "", aciklama = "", detay = "", baglioldugusayfa = "", sayfaresmi = "", sayfaid = "", sayfatipi = "";
                    sayfaadi = Request["sayfaadi"].ToString();
                    sayfabasligi = Request["sayfabasligi"].ToString();
                    sayfaid = Request["sayfaid"].ToString();
                    sayfatipi = Request["sayfatipi"].ToString();
                    try
                    {
                        sayfaresmi = "storage/files/pageimages/" + Regex.Split(Request["sayfaresmi"].ToString(), "storage/files/pageimages/")[1];
                    }
                    catch (Exception)
                    {
                        sayfaresmi = "";
                    }

                    aciklama = Request["aciklama"].ToString();
                    detay = Request["detay"].ToString();
                    baglioldugusayfa = Request["baglioldugusayfa"].ToString();

                    string result = "";

                    if (baglioldugusayfa == "Bağımsız Sayfa")
                    {
                        if (sayfaresmi != "")
                        {
                            result = db.updateValueWhere1("tbl_pages", "PG_PageName,PG_Title,PG_Description,PG_Content,PG_ImageUrl,PG_LastUpdateDate,PG_LastUpdateAdmin,PG_Status,PG_SeoUrl,PG_PageType,PG_MainPageID", db.clr(sayfaadi) + "~" + db.clr(sayfabasligi) + "~" + aciklama + "~" + detay + "~" + sayfaresmi + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1" + "~" + db.SeoURL(sayfaadi) + "~" + sayfatipi + "~Né", "PG_Id", "=", sayfaid, "ConString");
                        }
                        else
                        {
                            result = db.updateValueWhere1("tbl_pages", "PG_PageName,PG_Title,PG_Description,PG_Content,PG_LastUpdateDate,PG_LastUpdateAdmin,PG_Status,PG_SeoUrl,PG_PageType,PG_MainPageID", db.clr(sayfaadi) + "~" + db.clr(sayfabasligi) + "~" + aciklama + "~" + detay + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1" + "~" + db.SeoURL(sayfaadi) + "~" + sayfatipi + "~Né", "PG_Id", "=", sayfaid, "ConString");
                        }
                    }
                    else
                    {
                        if (sayfaresmi != "")
                        {
                            result = db.updateValueWhere1("tbl_pages", "PG_PageName,PG_Title,PG_Description,PG_Content,PG_ImageUrl,PG_LastUpdateDate,PG_LastUpdateAdmin,PG_Status,PG_SeoUrl,PG_PageType,PG_MainPageID", db.clr(sayfaadi) + "~" + db.clr(sayfabasligi) + "~" + aciklama + "~" + detay + "~" + sayfaresmi + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1" + "~" + db.SeoURL(sayfaadi) + "~" + sayfatipi + "~" + baglioldugusayfa, "PG_Id", "=", sayfaid, "ConString");
                        }
                        else
                        {
                            result = db.updateValueWhere1("tbl_pages", "PG_PageName,PG_Title,PG_Description,PG_Content,PG_LastUpdateDate,PG_LastUpdateAdmin,PG_Status,PG_SeoUrl,PG_PageType,PG_MainPageID", db.clr(sayfaadi) + "~" + db.clr(sayfabasligi) + "~" + aciklama + "~" + detay + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1" + "~" + db.SeoURL(sayfaadi) + "~" + sayfatipi + "~" + baglioldugusayfa, "PG_Id", "=", sayfaid, "ConString");
                        }
                    }



                    //if (sayfaresmi != "")
                    //{
                    //    result = db.updateValueWhere1("tbl_pages", "PG_PageName,PG_Title,PG_Description,PG_Content,PG_ImageUrl,PG_LastUpdateDate,PG_LastUpdateAdmin,PG_Status,PG_SeoUrl,PG_PageType", db.clr(sayfaadi) + "~" + db.clr(sayfabasligi) + "~" + aciklama + "~" + detay + "~" + sayfaresmi + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1" + "~" + db.SeoURL(sayfaadi) + "~" + sayfatipi, "PG_Id", "=", sayfaid, "ConString");
                    //}
                    //else
                    //{
                    //    result = db.updateValueWhere1("tbl_pages", "PG_PageName,PG_Title,PG_Description,PG_Content,PG_LastUpdateDate,PG_LastUpdateAdmin,PG_Status,PG_SeoUrl,PG_PageType", db.clr(sayfaadi) + "~" + db.clr(sayfabasligi) + "~" + aciklama + "~" + detay + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1" + "~" + db.SeoURL(sayfaadi) + "~" + sayfatipi, "PG_Id", "=", sayfaid, "ConString");
                    //}

                    string usaid = Guid.NewGuid().ToString();

                    db.insertValue("tbl_users_action_logs", "usa_log_id,usa_action_type,usa_page_type,usa_operation_date,usa_usr_user_id,usa_status", usaid + "~" + "Update / Veri Güncelleme" + "~" + sayfaadi + " adındaki sayfa düzenlendi." + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1", "ConString");

                    if (result.Contains("true") == true)
                    {
                        Response.Write("true");
                        return;
                    }
                    else
                    {
                        Response.Write("false");
                        return;
                    }
                }
                if (Request["type"] == "addvsslide")
                {
                    string anaslogan = "", altslogan = "", btnfaiz = "", btntext = "", slideresmi = "", yazirengi = "", grupid = "", grupadi = "", link = "", yonlendirmeturu = "";
                    anaslogan = Request["anaslogan"].ToString();
                    altslogan = Request["altslogan"].ToString();
                    slideresmi = "storage/files/homepageslider/" + Regex.Split(Request["slideresmi"].ToString(), "storage/files/homepageslider/")[1];
                    btnfaiz = Request["butonfaiz"].ToString();
                    btntext = Request["butonslogan"].ToString();
                    yazirengi = Request["yazirengi"].ToString();
                    grupid = Request["urungrubu"].ToString();
                    link = Request["link"].ToString();
                    yonlendirmeturu = Request["yonlendirmeturu"].ToString();

                    if (link.Length > 4)
                    {
                        if (link.StartsWith("http") == false)
                        {
                            link = "http://" + link;
                        }
                    }
                    else
                    {
                        link = "#";
                    }

                    string slideid = Guid.NewGuid().ToString();
                    string result = db.insertValue("tbl_vehicleslider", "VGS_Id,VGS_VGId,VGS_ImageUrl,VGS_Text1,VGS_Text2,VGS_BtnText1,VGS_BtnText2,VGS_TextColor,VGS_AddedDateTime,VGS_AddedAdmin,VGS_Status,VGS_Link,VGS_RedirectType", slideid + "~" + grupid + "~" + db.clr(slideresmi) + "~" + db.clr(anaslogan) + "~" + altslogan + "~" + btnfaiz + "~" + btntext + "~" + yazirengi + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1" + "~" + link + "~" + yonlendirmeturu, "ConString");

                    if (grupid == "1")
                    { grupadi = "Otomobil"; }
                    else if (grupid == "2")
                    { grupadi = "Hafifi Ticari Araçlar"; }
                    else if (grupid == "3")
                    { grupadi = "Otobüs"; }
                    else if (grupid == "4")
                    { grupadi = "Kamyon"; }

                    string usaid = Guid.NewGuid().ToString();
                    db.insertValue("tbl_users_action_logs", "usa_log_id,usa_action_type,usa_page_type,usa_operation_date,usa_usr_user_id,usa_status", usaid + "~" + "Insert / Veri Ekleme" + "~" + grupadi + " ürün grubuna yeni slide resmi eklendi." + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1", "ConString");

                    if (result.Contains("true") == true)
                    {
                        Response.Write("true");
                        return;
                    }
                    else
                    {
                        Response.Write("false");
                        return;
                    }
                }
                if (Request["type"] == "editvsslide")
                {
                    string anaslogan = "", altslogan = "", btnfaiz = "", btntext = "", slideresmi = "", slideid = "", yazirengi = "", grupid = "", grupadi = "", link = "", yonlendirmeturu = "";
                    anaslogan = Request["anaslogan"].ToString();
                    altslogan = Request["altslogan"].ToString();
                    slideid = Request["slideid"].ToString();
                    btnfaiz = Request["butonfaiz"].ToString();
                    btntext = Request["butonslogan"].ToString();
                    yazirengi = Request["yazirengi"].ToString();
                    grupid = Request["urungrubu"].ToString();
                    link = Request["link"].ToString();
                    yonlendirmeturu = Request["yonlendirmeturu"].ToString();

                    if (link.Length > 4)
                    {
                        if (link.StartsWith("http") == false)
                        {
                            link = "http://" + link;
                        }
                    }
                    else
                    {
                        link = "#";
                    }

                    try
                    {
                        slideresmi = "storage/files/homepageslider/" + Regex.Split(Request["slideresmi"].ToString(), "storage/files/homepageslider/")[1];
                    }
                    catch (Exception)
                    {
                        slideresmi = "";
                    }

                    string result = db.updateValueWhere2("tbl_vehicleslider", "VGS_Text1,VGS_Text2,VGS_BtnText1,VGS_BtnText2,VGS_TextColor,VGS_AddedDateTime,VGS_AddedAdmin,VGS_Status,VGS_Link,VGS_RedirectType", db.clr(anaslogan) + "~" + altslogan + "~" + btnfaiz + "~" + btntext + "~" + yazirengi + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1" + "~" + link + "~" + yonlendirmeturu, "VGS_Id", "=", slideid, "and", "VGS_VGId", "=", grupid, "ConString");

                    if (slideresmi != "")
                    {
                        db.updateValueWhere2("tbl_vehicleslider", "VGS_ImageUrl", slideresmi, "VGS_ID", "=", slideid, "and", "VGS_VGId", "=", grupid, "ConString");
                    }

                    if (grupid == "1")
                    { grupadi = "Otomobil"; }
                    else if (grupid == "2")
                    { grupadi = "Hafifi Ticari Araçlar"; }
                    else if (grupid == "3")
                    { grupadi = "Otobüs"; }
                    else if (grupid == "4")
                    { grupadi = "Kamyon"; }

                    string usaid = Guid.NewGuid().ToString();
                    db.insertValue("tbl_users_action_logs", "usa_log_id,usa_action_type,usa_page_type,usa_operation_date,usa_usr_user_id,usa_status", usaid + "~" + "Edit / Veri Düzenleme" + "~" + grupadi + " ürün grubuna ait slide resmi düzenlendi." + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1", "ConString");

                    if (result.Contains("true") == true)
                    {
                        Response.Write("true");
                        return;
                    }
                    else
                    {
                        Response.Write("false");
                        return;
                    }
                }
                if (Request["type"] == "crmkampanyaresimleri")
                {
                    string imgUrl = Request["imgUrl"];
                    int imgInitW = Convert.ToInt32(Request["imgInitW"].ToString().Split('.')[0]);
                    int imgInitH = Convert.ToInt32(Request["imgInitH"].ToString().Split('.')[0]);

                    int imgW = Convert.ToInt32(Request["imgW"].ToString().Split('.')[0]);
                    int imgH = Convert.ToInt32(Request["imgH"].ToString().Split('.')[0]);

                    int imgy1 = Convert.ToInt32(Request["imgY1"].ToString().Split('.')[0]);
                    int imgx1 = Convert.ToInt32(Request["imgX1"].ToString().Split('.')[0]);

                    int cropw = Convert.ToInt32(Request["cropW"].ToString().Split('.')[0]);
                    int croph = Convert.ToInt32(Request["cropH"].ToString().Split('.')[0]);

                    float angle = float.Parse(Request["rotation"]);
                    string quality = "100";
                    string extension = "";
                    string filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

                    byte[] bytes = Convert.FromBase64String(imgUrl.Split(',')[1]);
                    System.Drawing.Image image;
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        image = System.Drawing.Image.FromStream(ms);
                        if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                        { extension = ".jpg"; }
                        else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                        { extension = ".png"; }
                        else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
                        { extension = ".gif"; }
                        else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                        { extension = ".bmp"; }

                        image.Save(Server.MapPath("/storage/files/campaignimages/") + "org-" + filename + extension);
                        System.Drawing.Image resizeimage = db.RezizeImage(image, imgW, imgH);
                        resizeimage.Save(Server.MapPath("/storage/files/campaignimages/") + "resize-" + filename + extension);
                    }

                    System.Drawing.Image orgImg = System.Drawing.Image.FromFile(Server.MapPath("/storage/files/campaignimages/") + "resize-" + filename + extension);
                    Rectangle CropArea = new Rectangle(
                        Convert.ToInt32(imgx1),
                        Convert.ToInt32(imgy1),
                        Convert.ToInt32(cropw),
                        Convert.ToInt32(croph));
                    try
                    {
                        Bitmap bitMap = new Bitmap(CropArea.Width, CropArea.Height);
                        using (Graphics g = Graphics.FromImage(bitMap))
                        {
                            g.DrawImage(orgImg, new Rectangle(0, 0, bitMap.Width, bitMap.Height), CropArea, GraphicsUnit.Pixel);
                        }
                        bitMap.Save(Server.MapPath("/storage/files/campaignimages/") + "crop-" + filename + extension);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                    FileInfo fi = new FileInfo(Server.MapPath("/storage/files/campaignimages/org-" + filename + extension));
                    fi.Delete();

                    var jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string json2 = jsonSerializer.Serialize(new successmsg
                    {
                        status = "success",
                        url = "/storage/files/campaignimages/crop-" + filename + extension
                    });
                    Response.Clear();
                    Response.ContentType = "application/json; charset=utf-8";
                    Response.Write(json2);
                    Response.End();
                }

                if (Request["type"] == "GetAtlasSeries")
                {
                    string response = serviceConJson("https://gstest.mbfh.com.tr/token", "{ \"AppKey\":\"CmsUser\", \"AppPassword\":\"TestPassword\"}");
                    dynamic stuff = JObject.Parse(response);
                    string token = stuff.AccessToken.Value;

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://gstest.mbfh.com.tr/serie?segmentId=" + Request["segmentid"].ToString());
                    request.PreAuthenticate = true;
                    request.Headers.Add("Authorization", "Bearer " + token);
                    var respons = (HttpWebResponse)request.GetResponse();

                    string responsetext = "";
                    using (var streamReader = new StreamReader(respons.GetResponseStream()))
                    {
                        responsetext = streamReader.ReadToEnd();
                    }
                    string list = "";
                    dynamic stuff2 = JArray.Parse(responsetext);
                    for (int i = 0; i < stuff2.Count; i++)
                    {
                        dynamic data = JObject.Parse(stuff2[i].ToString());
                        list += "<option value=\"" + data.Id.ToString() + "\">" + data.Name.ToString() + "</option>";
                    }
                    Response.Write(list);
                }
                if (Request["type"] == "addcampaign")
                {
                    string urungrubu = "", kampanyaadi = "", kampanyakodu = "", sayfabasligi = "", sayfaaciklamasi = "", fbbasligi = "", fbaciklamasi = "", kampanyaresmi = "", kapakresmi = "", pdfresmi = "", modeller = "", gecerliliktarihi = "", slogan = "", butonyazisi = "", iletisimformulinki = "", notlar = "", keywords = "", fiyatlistesi = "", mobilsegmentid = "", mobilserieid = "", mobilsloganrenk = "", mobilbrosururl = "", mobilkampanyaaciklama = "", mobilfaizorani = "", mobilfaizmetni = "", mobilfaizanasayfa = "", mobilfaizliste = "", mobilfaizdetay = "", mobilkapresmi = "", mobildetresmi = "", mobilkampanyaanasayfa = "", mobilsegmentname = "", mobilseriename = "", mobilanasayfaresmi = "";
                    urungrubu = Request["urungrubu"].ToString();
                    kampanyaadi = Request["kampanyaadi"].ToString();
                    kampanyakodu = Request["kampanyakodu"].ToString();
                    sayfabasligi = Request["sayfabasligi"].ToString();
                    sayfaaciklamasi = Request["sayfaaciklamasi"].ToString();
                    fbbasligi = Request["fbbasligi"].ToString();
                    fbaciklamasi = Request["fbaciklamasi"].ToString();
                    kampanyaresmi = "storage/files/campaignimages/" + Regex.Split(Request["kampanyaresmi"].ToString(), "storage/files/campaignimages/")[1];
                    kapakresmi = "storage/files/campaignimages/" + Regex.Split(Request["kapakresmi"].ToString(), "storage/files/campaignimages/")[1];
                    pdfresmi = "storage/files/campaignimages/" + Regex.Split(Request["pdfresmi"].ToString(), "storage/files/campaignimages/")[1];
                    modeller = Request["modeller"].ToString();
                    gecerliliktarihi = Request["gecerliliktarihi"].ToString();
                    slogan = Request["slogan"].ToString();
                    butonyazisi = Request["butonyazisi"].ToString();
                    iletisimformulinki = Request["iletisimformulinki"].ToString();
                    notlar = Request["notlar"].ToString();
                    keywords = Request["keywords"].ToString();
                    fiyatlistesi = Request["fiyatlistesi"].ToString();

                    mobilsegmentid = Request["mobilsegmentid"].ToString();
                    mobilserieid = Request["mobilserieid"].ToString();
                    mobilsegmentname = Request["mobilsegmentname"].ToString();
                    mobilseriename = Request["mobilseriename"].ToString();
                    mobilsloganrenk = Request["mobilsloganrenk"].ToString();
                    mobilbrosururl = Request["mobilbrosururl"].ToString();
                    mobilkampanyaaciklama = Request["mobilkampanyaaciklama"].ToString();
                    mobilfaizorani = Request["mobilfaizorani"].ToString();
                    mobilfaizmetni = Request["mobilfaizmetni"].ToString();
                    mobilfaizanasayfa = Request["mobilfaizanasayfa"].ToString();
                    mobilfaizliste = Request["mobilfaizliste"].ToString();
                    mobilfaizdetay = Request["mobilfaizdetay"].ToString();
                    mobilkampanyaanasayfa = Request["mobilkampanyaanasayfa"].ToString();

                    if (mobilfaizanasayfa.ToString() == "false") { mobilfaizanasayfa = "0"; } else { mobilfaizanasayfa = "1"; }
                    if (mobilfaizliste.ToString() == "false") { mobilfaizliste = "0"; } else { mobilfaizliste = "1"; }
                    if (mobilfaizdetay.ToString() == "false") { mobilfaizdetay = "0"; } else { mobilfaizdetay = "1"; }
                    if (mobilkampanyaanasayfa.ToString() == "false") { mobilkampanyaanasayfa = "0"; } else { mobilkampanyaanasayfa = "1"; }

                    if (Request.Files["mobilanasayfa"] != null)
                    {
                        string extension = "";
                        string filename = "";
                        var fileContent = Request.Files["mobilanasayfa"];
                        if (fileContent != null && fileContent.ContentLength > 0)
                        {
                            var stream = fileContent.InputStream;
                            var fileName = fileContent.FileName;
                            filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                            System.Drawing.Image image = System.Drawing.Image.FromStream(fileContent.InputStream);
                            if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                            { extension = ".jpg"; }
                            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                            { extension = ".png"; }
                            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
                            { extension = ".gif"; }
                            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                            { extension = ".bmp"; }
                            image.Save(Server.MapPath("/storage/files/campaignimages/") + "home-" + filename + extension);
                        }
                        mobilanasayfaresmi = "storage/files/campaignimages/home-" + filename + extension;
                    }
                    else
                    {
                        mobilanasayfaresmi = "";
                    }

                    if (Request.Files["mobilkapakresmi"] != null)
                    {
                        string extension = "";
                        string filename = "";
                        var fileContent = Request.Files["mobilkapakresmi"];
                        if (fileContent != null && fileContent.ContentLength > 0)
                        {
                            var stream = fileContent.InputStream;
                            var fileName = fileContent.FileName;
                            filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                            System.Drawing.Image image = System.Drawing.Image.FromStream(fileContent.InputStream);
                            if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                            { extension = ".jpg"; }
                            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                            { extension = ".png"; }
                            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
                            { extension = ".gif"; }
                            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                            { extension = ".bmp"; }
                            image.Save(Server.MapPath("/storage/files/campaignimages/") + "kapak-" + filename + extension);
                        }
                        mobilkapresmi = "storage/files/campaignimages/kapak-" + filename + extension;
                    }
                    else
                    {
                        mobilkapresmi = "";
                    }

                    if (Request.Files["mobildetayresmi"] != null)
                    {
                        string extension = "";
                        string filename = "";
                        var fileContent = Request.Files["mobildetayresmi"];
                        if (fileContent != null && fileContent.ContentLength > 0)
                        {
                            var stream = fileContent.InputStream;
                            var fileName = fileContent.FileName;
                            filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                            System.Drawing.Image image = System.Drawing.Image.FromStream(fileContent.InputStream);
                            if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                            { extension = ".jpg"; }
                            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                            { extension = ".png"; }
                            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
                            { extension = ".gif"; }
                            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                            { extension = ".bmp"; }
                            image.Save(Server.MapPath("/storage/files/campaignimages/") + "detay-" + filename + extension);
                        }
                        mobildetresmi = "storage/files/campaignimages/detay-" + filename + extension;
                    }
                    else
                    {
                        mobildetresmi = "";
                    }

                    if (mobilbrosururl.Length > 0)
                    {
                        if (!mobilbrosururl.StartsWith("http"))
                        {
                            mobilbrosururl = "http://" + mobilbrosururl;
                        }
                    }

                    if (fiyatlistesi.Length > 0)
                    {
                        if (!fiyatlistesi.StartsWith("http"))
                        {
                            fiyatlistesi = "http://" + fiyatlistesi;
                        }
                    }

                    if (iletisimformulinki.Length > 1)
                    {
                        if (!iletisimformulinki.StartsWith("http://"))
                        {
                            iletisimformulinki = "http://" + iletisimformulinki;
                        }
                    }


                    string result = "";
                    result = db.insertValue("tbl_campaigns", "CMP_Id,CMP_VGId,CM_VMId,CMP_CampaignName,CMP_CampaignCode,CMP_Slogan,CMP_ButtonText,CMP_CampaignImageUrl,CMP_CampaignCoverUrl,CMP_CampaignPDFUrl,CMP_ContactFormUrl,CMP_EffectiveDate,CMP_PageTitle,CMP_PageDescription,CMP_FBTitle,CMP_FBDescription,CMP_SeoUrl,CMP_LastUpdateDateTime,CMP_LastUpdateAdmin,CMP_Status,CMP_Notes,CMP_PageKeywords,CMP_PriceList,CMP_AtlasSegmentId,CMP_AtlasSegmentName,CMP_AtlasSerieId,CMP_AtlasSerieName,CMP_MobileTitleColor,CMP_MobileBrochureUrl,CMP_MobileCampaignDetail,CMP_MobileInterestRate,CMP_MobileInterestText,CMP_MobileInterestHome,CMP_MobileInterestList,CMP_MobileInterestDetail,CMP_MobileCoverImage,CMP_MobileDetailImage,CMP_MobileHomepageImage,CMP_MobileHomepage", Guid.NewGuid().ToString() + "~" + urungrubu + "~" + modeller + "~" + db.clr(kampanyaadi) + "~" + db.clr(kampanyakodu) + "~" + db.clr(slogan) + "~" + db.clr(butonyazisi) + "~" + kampanyaresmi + "~" + kapakresmi + "~" + pdfresmi + "~" + db.clr(iletisimformulinki) + "~é" + DateTime.ParseExact(gecerliliktarihi, "yyyy.MM.dd", null) + "~" + db.clr(sayfabasligi) + "~" + db.clr(sayfaaciklamasi) + "~" + db.clr(fbbasligi) + "~" + db.clr(fbaciklamasi) + "~" + db.SeoURL(kampanyaadi) + "-" + db.SeoURL(kampanyakodu) + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1" + "~" + notlar + "~" + keywords + "~" + fiyatlistesi + "~" + mobilsegmentid + "~" + mobilsegmentname + "~" + mobilserieid + "~" + mobilseriename + "~" + mobilsloganrenk + "~" + mobilbrosururl + "~" + mobilkampanyaaciklama + "~" + mobilfaizorani + "~" + mobilfaizmetni + "~" + mobilfaizanasayfa + "~" + mobilfaizliste + "~" + mobilfaizdetay + "~" + mobilkapresmi + "~" + mobildetresmi + "~" + mobilanasayfaresmi + "~" + mobilkampanyaanasayfa, "ConString");

                    string usaid = Guid.NewGuid().ToString();
                    db.insertValue("tbl_users_action_logs", "usa_log_id,usa_action_type,usa_page_type,usa_operation_date,usa_usr_user_id,usa_status", usaid + "~" + "Insert / Veri Ekleme" + "~" + kampanyaadi + " adında yeni bir kampanya eklendi." + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1", "ConString");
                    if (result.Contains("true") == true)
                    {
                        Response.Write("true");
                        return;
                    }
                    else
                    {
                        Response.Write("false");
                        return;
                    }
                }
                if (Request["type"] == "editcampaign")
                {
                    string urungrubu = "", kampanyaadi = "", kampanyakodu = "", sayfabasligi = "", sayfaaciklamasi = "", fbbasligi = "", fbaciklamasi = "", kampanyaresmi = "", kapakresmi = "", pdfresmi = "", modeller = "", kampanyaresmiyeniyol = "", kapakresmiyeniyol = "", pdfresmiyeniyol = "", gecerliliktarihi = "", slogan = "", butonyazisi = "", iletisimformulinki = "", kampanyaid = "", notlar = "", keywords = "", fiyatlistesi = "", mobilsegmentid = "", mobilserieid = "", mobilsloganrenk = "", mobilbrosururl = "", mobilkampanyaaciklama = "", mobilfaizorani = "", mobilfaizmetni = "", mobilfaizanasayfa = "", mobilfaizliste = "", mobilfaizdetay = "", mobilkapresmi = "", mobildetresmi = "", mobilkampanyaanasayfa = "", mobilsegmentname = "", mobilseriename = "", mobilanasayfaresmi = "";
                    kampanyaid = Request["kampanyaid"].ToString();
                    urungrubu = Request["urungrubu"].ToString();
                    kampanyaadi = Request["kampanyaadi"].ToString();
                    kampanyakodu = Request["kampanyakodu"].ToString();
                    sayfabasligi = Request["sayfabasligi"].ToString();
                    sayfaaciklamasi = Request["sayfaaciklamasi"].ToString();
                    fbbasligi = Request["fbbasligi"].ToString();
                    fbaciklamasi = Request["fbaciklamasi"].ToString();
                    modeller = Request["modeller"].ToString();
                    gecerliliktarihi = Request["gecerliliktarihi"].ToString();
                    slogan = Request["slogan"].ToString();
                    butonyazisi = Request["butonyazisi"].ToString();
                    iletisimformulinki = Request["iletisimformulinki"].ToString();
                    notlar = Request["notlar"].ToString();
                    keywords = Request["keywords"].ToString();
                    fiyatlistesi = Request["fiyatlistesi"].ToString();

                    mobilsegmentid = Request["mobilsegmentid"].ToString();
                    mobilsegmentname = Request["mobilsegmentname"].ToString();
                    mobilserieid = Request["mobilserieid"].ToString();
                    mobilseriename = Request["mobilseriename"].ToString();
                    mobilsloganrenk = Request["mobilsloganrenk"].ToString();
                    mobilbrosururl = Request["mobilbrosururl"].ToString();
                    mobilkampanyaaciklama = Request["mobilkampanyaaciklama"].ToString();
                    mobilfaizorani = Request["mobilfaizorani"].ToString();
                    mobilfaizmetni = Request["mobilfaizmetni"].ToString();
                    mobilfaizanasayfa = Request["mobilfaizanasayfa"].ToString();
                    mobilfaizliste = Request["mobilfaizliste"].ToString();
                    mobilfaizdetay = Request["mobilfaizdetay"].ToString();
                    mobilkampanyaanasayfa = Request["mobilkampanyaanasayfa"].ToString();

                    if (mobilfaizanasayfa.ToString() == "false") { mobilfaizanasayfa = "0"; } else { mobilfaizanasayfa = "1"; }
                    if (mobilfaizliste.ToString() == "false") { mobilfaizliste = "0"; } else { mobilfaizliste = "1"; }
                    if (mobilfaizdetay.ToString() == "false") { mobilfaizdetay = "0"; } else { mobilfaizdetay = "1"; }
                    if (mobilkampanyaanasayfa.ToString() == "false") { mobilkampanyaanasayfa = "0"; } else { mobilkampanyaanasayfa = "1"; }

                    if (Request.Files["mobilanasayfa"] != null)
                    {
                        string extension = "";
                        string filename = "";
                        var fileContent = Request.Files["mobilanasayfa"];
                        if (fileContent != null && fileContent.ContentLength > 0)
                        {
                            var stream = fileContent.InputStream;
                            var fileName = fileContent.FileName;
                            filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                            System.Drawing.Image image = System.Drawing.Image.FromStream(fileContent.InputStream);
                            if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                            { extension = ".jpg"; }
                            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                            { extension = ".png"; }
                            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
                            { extension = ".gif"; }
                            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                            { extension = ".bmp"; }
                            image.Save(Server.MapPath("/storage/files/campaignimages/") + "home-" + filename + extension);
                        }
                        mobilanasayfaresmi = "storage/files/campaignimages/home-" + filename + extension;
                        db.updateValueWhere1("tbl_campaigns", "CMP_MobileHomepageImage", mobilanasayfaresmi, "CMP_Id", "=", kampanyaid, "ConString");
                    }
                    else
                    {
                        mobilanasayfaresmi = "";
                    }

                    if (Request.Files["mobilkapakresmi"] != null)
                    {
                        string extension = "";
                        string filename = "";
                        var fileContent = Request.Files["mobilkapakresmi"];
                        if (fileContent != null && fileContent.ContentLength > 0)
                        {
                            var stream = fileContent.InputStream;
                            var fileName = fileContent.FileName;
                            filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                            System.Drawing.Image image = System.Drawing.Image.FromStream(fileContent.InputStream);
                            if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                            { extension = ".jpg"; }
                            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                            { extension = ".png"; }
                            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
                            { extension = ".gif"; }
                            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                            { extension = ".bmp"; }
                            image.Save(Server.MapPath("/storage/files/campaignimages/") + "kapak-" + filename + extension);
                        }
                        mobilkapresmi = "storage/files/campaignimages/kapak-" + filename + extension;

                        db.updateValueWhere1("tbl_campaigns", "CMP_MobileCoverImage", mobilkapresmi, "CMP_Id", "=", kampanyaid, "ConString");
                    }
                    else
                    {
                        mobilkapresmi = "";
                    }

                    if (Request.Files["mobildetayresmi"] != null)
                    {
                        string extension = "";
                        string filename = "";
                        var fileContent = Request.Files["mobildetayresmi"];
                        if (fileContent != null && fileContent.ContentLength > 0)
                        {
                            var stream = fileContent.InputStream;
                            var fileName = fileContent.FileName;
                            filename = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                            System.Drawing.Image image = System.Drawing.Image.FromStream(fileContent.InputStream);
                            if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                            { extension = ".jpg"; }
                            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                            { extension = ".png"; }
                            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
                            { extension = ".gif"; }
                            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                            { extension = ".bmp"; }
                            image.Save(Server.MapPath("/storage/files/campaignimages/") + "detay-" + filename + extension);
                        }
                        mobildetresmi = "storage/files/campaignimages/detay-" + filename + extension;
                        db.updateValueWhere1("tbl_campaigns", "CMP_MobileDetailImage", mobildetresmi, "CMP_Id", "=", kampanyaid, "ConString");
                    }
                    else
                    {
                        mobildetresmi = "";
                    }

                    if (mobilbrosururl.Length > 0)
                    {
                        if (!mobilbrosururl.StartsWith("http"))
                        {
                            mobilbrosururl = "http://" + mobilbrosururl;
                        }
                    }

                    if (fiyatlistesi.Length > 0)
                    {
                        if (!fiyatlistesi.StartsWith("http"))
                        {
                            fiyatlistesi = "http://" + fiyatlistesi;
                        }
                    }

                    try
                    {
                        kampanyaresmi = "storage/files/campaignimages/" + Regex.Split(Request["kampanyaresmi"].ToString(), "storage/files/campaignimages/")[1];
                    }
                    catch (Exception)
                    {
                        kampanyaresmi = "";
                    }
                    try
                    {
                        kapakresmi = "storage/files/campaignimages/" + Regex.Split(Request["kapakresmi"].ToString(), "storage/files/campaignimages/")[1];
                    }
                    catch (Exception)
                    {
                        kapakresmi = "";
                    }
                    try
                    {
                        pdfresmi = "storage/files/campaignimages/" + Regex.Split(Request["pdfresmi"].ToString(), "storage/files/campaignimages/")[1];
                    }
                    catch (Exception)
                    {
                        pdfresmi = "";
                    }

                    if (kampanyaresmi != "")
                    {
                        //kampanyaresmiyeniyol = "storage/files/campaignimages/" + db.SeoURL(kampanyaadi) + "-" + db.SeoURL(kampanyakodu) + Path.GetExtension(Regex.Split(kampanyaresmi, "storage/files/campaignimages/")[1].ToString());
                        //File.Copy(Server.MapPath("/") + kampanyaresmi, Server.MapPath("/") + kampanyaresmiyeniyol, true);
                        //try { File.Delete(Server.MapPath("/") + kampanyaresmi.Replace("crop-", "resize-")); }
                        //catch (Exception) { }
                        //File.Delete(Server.MapPath("/") + kampanyaresmi);
                        db.updateValueWhere1("tbl_campaigns", "CMP_CampaignImageUrl", kampanyaresmi, "CMP_Id", "=", kampanyaid, "ConString");

                    }
                    if (kapakresmi != "")
                    {
                        //kapakresmiyeniyol = "storage/files/campaignimages/" + db.SeoURL(kampanyaadi) + "-" + db.SeoURL(kampanyakodu) + "-cover" + Path.GetExtension(Regex.Split(kapakresmi, "storage/files/campaignimages/")[1].ToString());
                        //File.Copy(Server.MapPath("/") + kapakresmi, Server.MapPath("/") + kapakresmiyeniyol, true);
                        //try { File.Delete(Server.MapPath("/") + kapakresmi.Replace("crop-", "resize-")); }
                        //catch (Exception) { }
                        //File.Delete(Server.MapPath("/") + kapakresmi);
                        db.updateValueWhere1("tbl_campaigns", "CMP_CampaignCoverUrl", kapakresmi, "CMP_Id", "=", kampanyaid, "ConString");
                    }
                    if (pdfresmi != "")
                    {
                        //pdfresmiyeniyol = "storage/files/campaignimages/" + db.SeoURL(kampanyaadi) + "-" + db.SeoURL(kampanyakodu) + "-pdf" + Path.GetExtension(Regex.Split(pdfresmi, "storage/files/campaignimages/")[1].ToString());
                        //File.Copy(Server.MapPath("/") + pdfresmi, Server.MapPath("/") + pdfresmiyeniyol, true);
                        //try { File.Delete(Server.MapPath("/") + pdfresmi.Replace("crop-", "resize-")); }
                        //catch (Exception) { }
                        //File.Delete(Server.MapPath("/") + pdfresmi);
                        db.updateValueWhere1("tbl_campaigns", "CMP_CampaignPDFUrl", pdfresmi, "CMP_Id", "=", kampanyaid, "ConString");
                    }

                    if (iletisimformulinki.Length > 1)
                    {
                        if (!iletisimformulinki.StartsWith("http://"))
                        {
                            iletisimformulinki = "http://" + iletisimformulinki;
                        }
                    }

                    string result = "";
                    result = db.updateValueWhere1("tbl_campaigns", "CMP_VGId,CM_VMId,CMP_CampaignName,CMP_CampaignCode,CMP_Slogan,CMP_ButtonText,CMP_ContactFormUrl,CMP_EffectiveDate,CMP_PageTitle,CMP_PageDescription,CMP_FBTitle,CMP_FBDescription,CMP_SeoUrl,CMP_LastUpdateDateTime,CMP_LastUpdateAdmin,CMP_Status,CMP_Notes,CMP_PageKeywords,CMP_PriceList,CMP_AtlasSegmentId,CMP_AtlasSegmentName,CMP_AtlasSerieId,CMP_AtlasSerieName,CMP_MobileTitleColor,CMP_MobileBrochureUrl,CMP_MobileCampaignDetail,CMP_MobileInterestRate,CMP_MobileInterestText,CMP_MobileInterestHome,CMP_MobileInterestList,CMP_MobileInterestDetail,CMP_MobileHomepage", urungrubu + "~" + modeller + "~" + db.clr(kampanyaadi) + "~" + db.clr(kampanyakodu) + "~" + db.clr(slogan) + "~" + db.clr(butonyazisi) + "~" + db.clr(iletisimformulinki) + "~é" + DateTime.ParseExact(gecerliliktarihi, "yyyy.MM.dd", null) + "~" + db.clr(sayfabasligi) + "~" + db.clr(sayfaaciklamasi) + "~" + db.clr(fbbasligi) + "~" + db.clr(fbaciklamasi) + "~" + db.SeoURL(kampanyaadi) + "-" + db.SeoURL(kampanyakodu) + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1" + "~" + notlar + "~" + keywords + "~" + fiyatlistesi + "~" + mobilsegmentid + "~" + mobilsegmentname + "~" + mobilserieid + "~" + mobilseriename + "~" + mobilsloganrenk + "~" + mobilbrosururl + "~" + mobilkampanyaaciklama + "~" + mobilfaizorani + "~" + mobilfaizmetni + "~" + mobilfaizanasayfa + "~" + mobilfaizliste + "~" + mobilfaizdetay + "~" + mobilkampanyaanasayfa, "CMP_Id", "=", kampanyaid, "ConString");

                    string usaid = Guid.NewGuid().ToString();
                    db.insertValue("tbl_users_action_logs", "usa_log_id,usa_action_type,usa_page_type,usa_operation_date,usa_usr_user_id,usa_status", usaid + "~" + "Insert / Veri Ekleme" + "~" + kampanyaadi + " adında, " + kampanyakodu + " kodunda kampanya düzenlendi." + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1", "ConString");

                    if (result.Contains("true") == true)
                    {
                        var margins = new PageMargins();
                        margins.Top = 0;
                        margins.Bottom = 0;
                        margins.Left = 0;
                        margins.Right = 0;
                        HtmlToPdfConverter pdf = new HtmlToPdfConverter();
                        pdf.Size = PageSize.A4;
                        pdf.Orientation = PageOrientation.Landscape;
                        pdf.Margins = margins;
                        pdf.Zoom = 1F;
                        pdf.CustomWkHtmlArgs = "--encoding UTF-8";

                        string link = "";
                        DataRow drgs = db.getValueWhere1("select * from tbl_GeneralSettings", "GS_Id", "=", "1", "", "ConString");
                        if (drgs != null)
                        {
                            link = drgs["GS_GeneratorLink"].ToString();
                        }

                        pdf.GeneratePdfFromFile(link + "/panel/pdfgenerator.aspx?campaignid=" + kampanyaid, null, Server.MapPath("/storage/files/pdf/") + db.SeoURL(kampanyaadi + "-" + kampanyakodu) + ".pdf");

                        db.updateValueWhere1("tbl_campaigns", "CMP_PdfDownloadLink", "storage/files/pdf/" + db.SeoURL(kampanyaadi + "-" + kampanyakodu) + ".pdf", "CMP_Id", "=", kampanyaid, "ConString");

                        Response.Write("true");
                        return;
                    }
                    else
                    {
                        Response.Write("false");
                        return;
                    }
                }
                if (Request["type"] == "hpsort")
                {
                    try
                    {
                        string[] siraliidler = Request["siraliidler"].ToString().Substring(0, Request["siraliidler"].ToString().Length - 1).Split(',');
                        if (siraliidler.Count() > 1)
                        {
                            for (int i = 0; i < siraliidler.Count(); i++)
                            {
                                db.updateValueWhere1("tbl_homepageslider", "HS_Order", i.ToString(), "HS_Id", "=", siraliidler[i].ToString(), "ConString");
                            }
                        }
                        Response.Write("true"); return;
                    }
                    catch (Exception)
                    {
                        Response.Write("false"); return;
                    }
                }
                if (Request["type"] == "vssort")
                {
                    try
                    {
                        string[] siraliidler = Request["siraliidler"].ToString().Substring(0, Request["siraliidler"].ToString().Length - 1).Split(',');
                        if (siraliidler.Count() > 1)
                        {
                            for (int i = 0; i < siraliidler.Count(); i++)
                            {
                                db.updateValueWhere1("tbl_vehicleslider", "VGS_Order", i.ToString(), "VGS_Id", "=", siraliidler[i].ToString(), "ConString");
                            }
                        }
                        Response.Write("true"); return;
                    }
                    catch (Exception)
                    {
                        Response.Write("false"); return;
                    }
                }
                if (Request["type"] == "cmpsort")
                {
                    try
                    {
                        string[] siraliidler = Request["siraliidler"].ToString().Substring(0, Request["siraliidler"].ToString().Length - 1).Split(',');
                        if (siraliidler.Count() > 1)
                        {
                            for (int i = 0; i < siraliidler.Count(); i++)
                            {
                                db.updateValueWhere1("tbl_campaigns", "CMP_Order", i.ToString(), "CMP_Id", "=", siraliidler[i].ToString(), "ConString");
                            }
                        }
                        Response.Write("true"); return;
                    }
                    catch (Exception)
                    {
                        Response.Write("false"); return;
                    }
                }
                if (Request["type"] == "pgsort")
                {
                    try
                    {
                        string[] siraliidler = Request["siraliidler"].ToString().Substring(0, Request["siraliidler"].ToString().Length - 1).Split(',');
                        if (siraliidler.Count() > 1)
                        {
                            for (int i = 0; i < siraliidler.Count(); i++)
                            {
                                db.updateValueWhere1("tbl_pages", "PG_Order", i.ToString(), "PG_Id", "=", siraliidler[i].ToString(), "ConString");
                            }
                        }
                        Response.Write("true"); return;
                    }
                    catch (Exception)
                    {
                        Response.Write("false"); return;
                    }
                }
                if (Request["type"] == "hplinksort")
                {
                    try
                    {
                        string[] siraliidler = Request["siraliidler"].ToString().Substring(0, Request["siraliidler"].ToString().Length - 1).Split(',');
                        if (siraliidler.Count() > 1)
                        {
                            for (int i = 0; i < siraliidler.Count(); i++)
                            {
                                db.updateValueWhere1("tbl_homepagelinks", "HL_Order", i.ToString(), "HL_Id", "=", siraliidler[i].ToString(), "ConString");
                            }
                        }
                        Response.Write("true"); return;
                    }
                    catch (Exception)
                    {
                        Response.Write("false"); return;
                    }
                }
                if (Request["type"] == "bthsort")
                {
                    try
                    {
                        string[] siraliidler = Request["siraliidler"].ToString().Substring(0, Request["siraliidler"].ToString().Length - 1).Split(',');
                        if (siraliidler.Count() > 1)
                        {
                            for (int i = 0; i < siraliidler.Count(); i++)
                            {
                                db.updateValueWhere1("tbl_bthdocs", "BTHD_Order", i.ToString(), "BTHD_Id", "=", siraliidler[i].ToString(), "ConString");
                            }
                        }
                        Response.Write("true"); return;
                    }
                    catch (Exception)
                    {
                        Response.Write("false"); return;
                    }
                }
                if (Request["type"] == "addmobilesegmentimage")
                {
                    string segmentid = "", slideresmi = "";
                    segmentid = Request["segmentid"].ToString();
                    slideresmi = "storage/files/mobile/" + Regex.Split(Request["gorsel"].ToString(), "storage/files/mobile/")[1];


                    string slideid = Guid.NewGuid().ToString();
                    string result = db.insertValue("tbl_mobilimages", "MI_ImageTypeId,MI_SegmentId,MI_ImageUrl,MI_AddedDateTime", "1" + "~" + segmentid + "~" + slideresmi + "~é" + DateTime.Now, "ConString");

                    if (result.Contains("true") == true)
                    {
                        Response.Write("true");
                        return;
                    }
                    else
                    {
                        Response.Write("false");
                        return;
                    }
                }
                if (Request["type"] == "editmobilesegmentimage")
                {
                    string segmentid = "", mevcutid = "", slideresmi = "", result = "";
                    segmentid = Request["segmentid"].ToString();
                    mevcutid = Request["mevcutid"].ToString();

                    try
                    {
                        slideresmi = "storage/files/mobile/" + Regex.Split(Request["gorsel"].ToString(), "storage/files/mobile/")[1];
                    }
                    catch (Exception)
                    {
                        slideresmi = "";
                    }

                    result = db.updateValueWhere1("tbl_mobilimages", "MI_SegmentId", segmentid, "MI_Id", "=", mevcutid, "ConString");


                    if (slideresmi != "")
                    {
                        result = db.updateValueWhere1("tbl_mobilimages", "MI_ImageUrl", slideresmi, "MI_Id", "=", mevcutid, "ConString");
                    }

                    if (result.Contains("true") == true)
                    {
                        Response.Write("true");
                        return;
                    }
                    else
                    {
                        Response.Write("false");
                        return;
                    }
                }
                if (Request["type"] == "addmobilecontactimage")
                {
                    string slideresmi = "";
                    slideresmi = "storage/files/mobile/" + Regex.Split(Request["gorsel"].ToString(), "storage/files/mobile/")[1];


                    string slideid = Guid.NewGuid().ToString();
                    string result = db.insertValue("tbl_mobilimages", "MI_ImageTypeId,MI_ImageUrl,MI_AddedDateTime", "3" + "~" + slideresmi + "~é" + DateTime.Now, "ConString");

                    if (result.Contains("true") == true)
                    {
                        Response.Write("true");
                        return;
                    }
                    else
                    {
                        Response.Write("false");
                        return;
                    }
                }
                if (Request["type"] == "editmobilecontactimage")
                {
                    string segmentid = "", mevcutid = "", slideresmi = "", result = "";
                    mevcutid = Request["mevcutid"].ToString();

                    try
                    {
                        slideresmi = "storage/files/mobile/" + Regex.Split(Request["gorsel"].ToString(), "storage/files/mobile/")[1];
                    }
                    catch (Exception)
                    {
                        slideresmi = "";
                    }

                    if (slideresmi != "")
                    {
                        result = db.updateValueWhere1("tbl_mobilimages", "MI_ImageUrl", slideresmi, "MI_Id", "=", mevcutid, "ConString");
                    }

                    if (result.Contains("true") == true)
                    {
                        Response.Write("true");
                        return;
                    }
                    else
                    {
                        Response.Write("false");
                        return;
                    }
                }
                if (Request["type"] == "addmobileserieimage")
                {
                    string slideresmi = "", segmentid = "", serieid = "";
                    segmentid = Request["segmentid"].ToString();
                    serieid = Request["serieid"].ToString();
                    slideresmi = "storage/files/mobile/" + Regex.Split(Request["gorsel"].ToString(), "storage/files/mobile/")[1];


                    string slideid = Guid.NewGuid().ToString();
                    string result = db.insertValue("tbl_mobilimages", "MI_ImageTypeId,MI_SegmentId,MI_SerieId,MI_ImageUrl,MI_AddedDateTime", "2" + "~" + segmentid + "~" + serieid + "~" + slideresmi + "~é" + DateTime.Now, "ConString");

                    if (result.Contains("true") == true)
                    {
                        Response.Write("true");
                        return;
                    }
                    else
                    {
                        Response.Write("false");
                        return;
                    }
                }
                if (Request["type"] == "addmobiletypeimage")
                {
                    string slideresmi = "", segmentid = "", serieid = "", tipid = "";
                    segmentid = Request["segmentid"].ToString();
                    serieid = Request["serieid"].ToString();
                    tipid = Request["modeltip"].ToString();
                    slideresmi = "storage/files/mobile/" + Regex.Split(Request["gorsel"].ToString(), "storage/files/mobile/")[1];


                    string slideid = Guid.NewGuid().ToString();
                    string result = db.insertValue("tbl_mobilimages", "MI_ImageTypeId,MI_SegmentId,MI_SerieId,MI_ModelTypeId,MI_ImageUrl,MI_AddedDateTime", "6" + "~" + segmentid + "~" + serieid + "~" + tipid + "~" + slideresmi + "~é" + DateTime.Now, "ConString");

                    if (result.Contains("true") == true)
                    {
                        Response.Write("true");
                        return;
                    }
                    else
                    {
                        Response.Write("false");
                        return;
                    }
                }
                if (Request["type"] == "editmobileserieimage")
                {
                    string segmentid = "", mevcutid = "", slideresmi = "", result = "", serieid = "";
                    segmentid = Request["segmentid"].ToString();
                    serieid = Request["serieid"].ToString();
                    mevcutid = Request["mevcutid"].ToString();

                    try
                    {
                        slideresmi = "storage/files/mobile/" + Regex.Split(Request["gorsel"].ToString(), "storage/files/mobile/")[1];
                    }
                    catch (Exception)
                    {
                        slideresmi = "";
                    }

                    result = db.updateValueWhere1("tbl_mobilimages", "MI_SegmentId", segmentid, "MI_Id", "=", mevcutid, "ConString");

                    result = db.updateValueWhere1("tbl_mobilimages", "MI_SegmentId,MI_SerieId", segmentid + "~" + serieid, "MI_Id", "=", mevcutid, "ConString");

                    if (slideresmi != "")
                    {
                        result = db.updateValueWhere1("tbl_mobilimages", "MI_ImageUrl", slideresmi, "MI_Id", "=", mevcutid, "ConString");
                    }

                    if (result.Contains("true") == true)
                    {
                        Response.Write("true");
                        return;
                    }
                    else
                    {
                        Response.Write("false");
                        return;
                    }
                }
                if (Request["type"] == "editmobiletypeimage")
                {
                    string segmentid = "", mevcutid = "", slideresmi = "", result = "", serieid = "", tipid = "";
                    segmentid = Request["segmentid"].ToString();
                    serieid = Request["serieid"].ToString();
                    mevcutid = Request["mevcutid"].ToString();
                    tipid = Request["modeltip"].ToString();

                    try
                    {
                        slideresmi = "storage/files/mobile/" + Regex.Split(Request["gorsel"].ToString(), "storage/files/mobile/")[1];
                    }
                    catch (Exception)
                    {
                        slideresmi = "";
                    }

                    result = db.updateValueWhere1("tbl_mobilimages", "MI_SegmentId", segmentid, "MI_Id", "=", mevcutid, "ConString");

                    result = db.updateValueWhere1("tbl_mobilimages", "MI_SegmentId,MI_SerieId", segmentid + "~" + serieid, "MI_Id", "=", mevcutid, "ConString");

                    result = db.updateValueWhere1("tbl_mobilimages", "MI_SegmentId,MI_SerieId,MI_ModelTypeId", segmentid + "~" + serieid + "~" + tipid, "MI_Id", "=", mevcutid, "ConString");

                    if (slideresmi != "")
                    {
                        result = db.updateValueWhere1("tbl_mobilimages", "MI_ImageUrl", slideresmi, "MI_Id", "=", mevcutid, "ConString");
                    }

                    if (result.Contains("true") == true)
                    {
                        Response.Write("true");
                        return;
                    }
                    else
                    {
                        Response.Write("false");
                        return;
                    }
                }
            }
        }
        public string serviceConJson(string adr, string data)
        {
            string responsetext = "";
            try
            {
                string webAddr = adr;

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = data;

                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    responsetext = streamReader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                responsetext = ex.Message;
            }
            return responsetext;
        }
        public class successmsg
        {
            public string status;
            public string url;
            public int width;
            public int height;
        }
        public static Bitmap CropImage(System.Drawing.Image source, int x, int y, int width, int height)
        {
            Rectangle crop = new Rectangle(x, y, width, height);

            var bmp = new Bitmap(crop.Width, crop.Height);
            using (var gr = Graphics.FromImage(bmp))
            {
                gr.DrawImage(source, new Rectangle(0, 0, 435, 559), crop, GraphicsUnit.Pixel);
            }
            return bmp;
        }
        public static Bitmap CropRotatedRect(Bitmap source, Rectangle rect, float angle, bool HighQuality)
        {
            int[] offsets = { -1, 1, 0 }; //place 0 last!
            Bitmap result = new Bitmap(rect.Width, rect.Height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = HighQuality ? InterpolationMode.HighQualityBicubic : InterpolationMode.Default;
                foreach (int x in offsets)
                {
                    foreach (int y in offsets)
                    {
                        using (Matrix mat = new Matrix())
                        {
                            mat.Translate(-rect.Location.X - rect.Width * x, -rect.Location.Y - rect.Height * y);
                            mat.RotateAt(angle, rect.Location);
                            g.Transform = mat;
                            g.DrawImage(source, new Point(0, 0));
                        }
                    }
                }
            }
            return result;
        }
    }
}